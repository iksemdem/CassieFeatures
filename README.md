# An Exiled (SCP:SL) plugin that adds additional features, focusing on CASSIE System.

<div align="center">
    
<img src="https://img.shields.io/github/downloads/iksemdem/CassieFeatures/total?style=for-the-badge&logo=github" alt="Downloads">

</div>

This plugin adds few features to CASSIE. Not the CASSIE as the announcement system, but as the CASSIE functions

Install:
- 1 Download latest release, and put CassieFeatures.dll into `.config/EXILED/Plugins`
- 2 Download HintServiceMeow latest release (https://github.com/MeowServer/HintServiceMeow/releases/latest), and put it into `.config/EXILED/Plugins/Dependencies`
- 3 Restart the server, and configure the plugin in `.config/EXILED/Configs/YourPort-config.yml`
- 4 You're ready to go!

Features for tesla:
 - Disables teslas for selected teams.
 - Announces when selected teams die due to tesla gate.
 - Tells how many people from that team are left.

Features for Camera Scanner:
- Sends an announcement when SCP leaves the facility, and is there for setted time.
- The CASSIE knows what SCP it is, and where it is (Gate A/B).
- Sends an announcement when CI enters the facility. (Disabled by default, as the base game covers this feature as of 14.0)
- The CASSIE knows where CI is (Gate A/B).
- CASSIE announcement about CI can be played only one time per CI spawn (Configurable).

Features for warhead:
- Sends an announecement when someone changes the warhead status (Lever).
- Can be on cooldown or just one time event.

Features for SCPs:
- Allows SCPs to escape with a configurable command.
- Displays a hint when player can escape.
- Sends CASSIE after SCP escapes.

Features for doors:
- You can lock almost every door at the start of the round.
- You can open/lock/unlock/destroy almost every door after setted time.

Features for CASSIE announcements:
- You can set delayed announcements (since start of the round).

There is no coroutine running for Camera Scanner, plugin checks if someone actually entered, exited or escpaed the facility, by using colliders from unity.

The plugin is almost fully translatable (without console logs), and almost fully customizable.

Here are the placeholders that you can use in CASSIE announcements

| What Announcement | Input | Output | Output Example |
| ------------- | ------------- | ------------- | ------------- |
| `Death On Tesla`  | {PlayersTeam}  | Players team | Class D Personnel |
| `Death On Tesla`  | {TeamMembersAlive}  | Number of teammates left | 7 |
| `Scp Escapes Facility`  | {ScpRole}  | SCPs role number | SCP 1 7 3 |
| `Scp Leaves Facility`  | {ScpRole}  | SCPs role number | SCP 1 7 3 |
| `Scp Leaves Facility`  | {Gate}  | Gate that SCP left | Gate B |
| `Ci Enters Facility`  | {Gate}  | Gate that CI entered | Gate A |
| `Warhead Status Change`  | {PlayersTeam}  | Players team | Class D Personnel |
| `Warhead Status Change`  | {TeamMembersAlive}  | Number of teammates left | 7 |

Here are the placeholders that you can use in hints

| What hint | Input | Output | Output Example |
| ------------- | ------------- | ------------- | ------------- |
| `While player can escape`  | {CommandName}  | Name of the escape command | escape |

**This plugin is using HintServiceMeow**

Default Config:
```yaml
cassie_features:
# General:
  is_enabled: true
  debug: true
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
