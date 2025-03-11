# CassieFeatures - An Exiled (SCP:SL) Plugin

A plugin for Exiled that enhances the CASSIE system with additional features.

<div align="center">
    <img src="https://img.shields.io/github/downloads/iksemdem/CassieFeatures/total?style=for-the-badge&logo=github" alt="Downloads">
</div>

## Overview
This plugin extends the functionality of CASSIE beyond the basic announcement system, providing various additional features for Tesla gates, warheads, SCPs, doors, and more.

---

## Installation
1. Download the latest release and place `CassieFeatures.dll` into `.config/EXILED/Plugins`.
2. Download the latest release of **HintServiceMeow** ([GitHub](https://github.com/MeowServer/HintServiceMeow/releases/latest)) and place it into `.config/EXILED/Plugins/Dependencies`.
3. Restart the server and configure the plugin in `.config/EXILED/Configs/YourPort-config.yml`.
4. You're ready to go!

---

## Features

### Tesla Enhancements
- Disables Tesla gates for selected teams.
- Announces when selected teams die due to Tesla gates.
- Displays how many team members are left.

### Camera Scanner Enhancements
- Announces when an SCP leaves the facility after a set time.
- CASSIE specifies which SCP left and via which gate (Gate A/B).
- Announces when Chaos Insurgency (CI) enters the facility (disabled by default, as SCP:SL now has this feature natively in v14.0).
- CI announcements can be configured to be played only once per CI spawn.

### Warhead Announcements
- Announces when someone changes the warhead status.
- Can be set with a cooldown or as a one-time event.

### SCP Escape System
- Allows SCPs to escape using a configurable command.
- Displays a hint when an SCP can escape.
- Sends a CASSIE announcement when an SCP successfully escapes.

### Door Management
- Lock specific doors at the start of the round.
- Configure doors to open, lock, unlock, or be destroyed after a set time.

### Custom CASSIE Announcements
- Schedule delayed announcements from the start of the round.

---

## Performance Considerations
This plugin does not rely on coroutines for the Camera Scanner system. Instead, it uses Unity colliders to detect movement, ensuring minimal performance impact.

---

## Customization & Translations
The plugin is **highly customizable** and **mostly translatable**, except for console logs.

---

## CASSIE Placeholders
These placeholders can be used in CASSIE announcements:

| Event | Placeholder | Output | Example |
|--------|------------|--------|---------|
| Tesla Death | `{PlayersTeam}` | Player's team | Class D Personnel |
| Tesla Death | `{TeamMembersAlive}` | Number of teammates left | 7 |
| SCP Escapes | `{ScpRole}` | SCP role number | SCP 173 |
| SCP Leaves Facility | `{ScpRole}` | SCP role number | SCP 173 |
| SCP Leaves Facility | `{Gate}` | Exit gate | Gate B |
| CI Enters Facility | `{Gate}` | Entry gate | Gate A |
| Warhead Change | `{PlayersTeam}` | Player's team | Class D Personnel |
| Warhead Change | `{TeamMembersAlive}` | Number of teammates left | 7 |

## Hint Placeholders
These placeholders can be used in hints:

| Event | Placeholder | Output | Example |
|--------|------------|--------|---------|
| SCP Escape | `{CommandName}` | Escape command name | escape |

---

## Dependency
This plugin requires **HintServiceMeow** to function.

---

## Default Configuration
```yaml
cassie_features:
# General:
  is_enabled: true
  debug: false
  # Settings for Tesla:
  is_tesla_feature_enabled: true
  tesla_does_not_activate_on_teams:
  - Scientists
  - FoundationForces
  cassie_announces_death_on_tesla_on_teams:
  - ClassD
  - ChaosInsurgency
  - OtherAlive
  tesla_cassie:
    content: '{PlayersTeam} died on tesla . {TeamMembersAlive} {PlayersTeam}s left'
    subtitles: '{PlayersTeam} died on tesla. {TeamMembersAlive} {PlayersTeam}s left.'
    show_subtitles: true
    is_noisy: true
    delay: 3
  # Settings for Camera Scanner (When SCP enters Surface):
  is_camera_scanner_look_for_scp_leaving_feature_enabled: true
  should_camera_scanner_announce_scp_leaving_only_one_time: true
  should_cassie_check_if_scp_is_still_on_surface_after_the_delay: true
  scp_leaving_cassie:
    content: 'the camera system has detected {ScpRole} outside the facility at {Gate}'
    subtitles: 'The Camera System has detected {ScpRole} outside the Facility at {Gate}.'
    show_subtitles: true
    is_noisy: true
    delay: 10
  # Settings for Camera Scanner (When CI enters the Facility) (Warning! This feature is in the base game now! At the time of making this plugin, there is no way to turn it off. If you want to use both features, from the plugin and the base game, set this to true. Its False by default.):
  is_camera_scanner_looking_for_ci_entering_feature_enabled: false
  # If set to false, cassie wont announce CI untill next CI spawn (option below)
  should_camera_scanner_announce_ci_entering_only_one_time: false
  ci_entering_cassie:
    content: 'the camera system has detected chaos insurgency agents inside the facility at {Gate}'
    subtitles: 'The Camera System has detected Chaos Insurgency Agents inside the Facility at {Gate}.'
    show_subtitles: true
    is_noisy: true
    delay: 10
  # Settings for Warhead (When someone turns it on):
  is_warhead_feature_enabled: true
  is_warhead_announcement_turning_on_enabled: true
  is_warhead_announcement_turning_off_enabled: true
  # If set to false, the announcements will be on cooldown specified below. If set to true, cassie will announce it only one time
  should_warhead_announce_only_one_time: false
  warhead_announcement_cooldown: 30
  warhead_turning_on_cassie:
    content: 'Warhead has been turned on by {PlayersTeam}'
    subtitles: 'Warhead has been turned on by {PlayersTeam}.'
    show_subtitles: true
    is_noisy: true
    delay: 3
  warhead_turning_off_cassie:
    content: 'Warhead has been turned off by {PlayersTeam}'
    subtitles: 'Warhead has been turned off by {PlayersTeam}.'
    show_subtitles: true
    is_noisy: true
    delay: 3
  # Settings for SCP escape:
  is_scp_escape_enabled: true
  command_name: 'escape'
  command_description: 'Lets you escape the facility as a SCP, when you''re at the escape room'
  hint_when_can_escape: 'You can escape by typing .{CommandName} in the console by pressing [`] or [~]!'
  escape_failed_due_to_feature_turned_off: 'This feature is turned off!'
  escape_failed_due_to_player_not_being_scp: 'Only SCPs can escape!'
  escape_failed_due_to_player_not_being_at_escape: 'You are not in the escape area!'
  escape_success: 'You escaped!'
  role_to_change: NtfCaptain
  spawn_reason: Escaped
  role_spawn_flags: All
  should_sent_cassie_after_escape: true
  scp_escaping_cassie:
    content: 'warning . the camera system has lost information about the location of {ScpRole} . it is possible that there has been an escape'
    subtitles: 'Warning. The camera system has lost information about the location of {ScpRole}. It is possible that there has been an escape.'
    show_subtitles: true
    is_noisy: true
    delay: 15
  # Settings for Door Locker:
  is_locking_doors_enabled: true
  # LockedDoors are doors that are locked at the start of the round, use DoorsAction to open/unlock/destroy doors
  locked_doors:
  - PrisonDoor
  - CheckpointLczA
  - CheckpointLczB
  doors_action:
  - door_type: PrisonDoor
    delay: 20
    open: true
    unlock: true
    lock: false
    destroy: false
  - door_type: CheckpointLczA
    delay: 60
    open: false
    unlock: true
    lock: false
    destroy: false
  - door_type: CheckpointLczB
    delay: 60
    open: false
    unlock: true
    lock: false
    destroy: false
  # Here you can put your timed CASSIEs, delay starts at the start of the round.
  cassie_announcements:
  - content: 'attention all personnel . cassie has lost control of the door controlling system'
    subtitles: 'Attention all personnel. C.A.S.S.I.E. has lost control of the Door Controlling System'
    show_subtitles: true
    is_noisy: true
    delay: 10
```

---


Enjoy using **CassieFeatures** and feel free to contribute or report issues on GitHub/Discord!

