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
  is_enabled: true
  debug: false
  
  # Tesla Settings
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
  
  # Camera Scanner (SCP Leaving Facility)
  is_camera_scanner_look_for_scp_leaving_feature_enabled: true
  should_camera_scanner_announce_scp_leaving_only_one_time: true
  scp_leaving_cassie:
    content: 'the camera system has detected {ScpRole} outside the facility at {Gate}'
    subtitles: 'The Camera System has detected {ScpRole} outside the Facility at {Gate}.'
    show_subtitles: true
    is_noisy: true
    delay: 10

  # Warhead Settings
  is_warhead_feature_enabled: true
  is_warhead_announcement_turning_on_enabled: true
  is_warhead_announcement_turning_off_enabled: true
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
  
  # SCP Escape
  is_scp_escape_enabled: true
  command_name: 'escape'
  hint_when_can_escape: 'You can escape by typing .{CommandName} in the console by pressing [`] or [~]!'
  scp_escaping_cassie:
    content: 'warning . the camera system has lost information about the location of {ScpRole} . it is possible that there has been an escape'
    subtitles: 'Warning. The camera system has lost information about the location of {ScpRole}. It is possible that there has been an escape.'
    show_subtitles: true
    is_noisy: true
    delay: 15
  
  # Door Locking
  is_locking_doors_enabled: true
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
  
  # Timed CASSIE Announcements
  cassie_announcements:
    - content: 'attention all personnel . cassie has lost control of the door controlling system'
      subtitles: 'Attention all personnel. C.A.S.S.I.E. has lost control of the Door Controlling System'
      show_subtitles: true
      is_noisy: true
      delay: 10
```

---


Enjoy using **CassieFeatures** and feel free to contribute or report issues on GitHub!

