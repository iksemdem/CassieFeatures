# An Exiled (SCP:SL) plugin that adds additional features, focusing on CASSIE System.

<div align="center">
    
<img src="https://img.shields.io/github/downloads/iksemdem/CassieFeatures/total?style=for-the-badge&logo=github" alt="Downloads">

</div>

This plugin adds few features to CASSIE. Not the CASSIE as the announcement system, but as the CASSIE functions

Features for tesla:
 - Disables teslas for selected teams.
 - Announces when selected teams die due to tesla gate.
 - Tells how many people from that team are left.

Features for Camera Scanner:
- Sends an announcement when SCP leaves the facility, and is there for setted time.
- The CASSIE knows what SCP it is, and where it is (Gate A/B).
- Sends an announcement when CI enters the facility.
- The CASSIE knows where CI is (Gate A/B).
- CASSIE announcement about CI can be played only one time per CI spawn (Configurable).

Features for warhead:
- Sends an announecement when someone changes the warhead status (Lever)
- Can be on cooldown or just one time event

There is no coroutine running, plugin checks if someone actually entered or exited the facility, by using colliders from unity.

The plugin is almost fully translatable (without console logs), and almost fully customizable.

Here are the placeholders that you can use in CASSIE announcements

| What Announcement | Input | Output | Output Example |
| ------------- | ------------- | ------------- | ------------- |
| `Death On Tesla`  | {PlayersTeam}  | Players team | Class D Personnel |
| `Death On Tesla`  | {TeamMembersAlive}  | Number of teammates left | 7 |
| `Scp Leaves Facility`  | {ScpRole}  | SCPs role number | SCP 1 7 3 |
| `Scp Leaves Facility`  | {Gate}  | Gate that SCP left | Gate B |
| `Ci Enters Facility`  | {Gate}  | Gate that CI entered | Gate A |
| `Warhead Status Change`  | {PlayersTeam}  | Players team | Class D Personnel |
| `Warhead Status Change`  | {TeamMembersAlive}  | Number of teammates left | 7 |

Default Config:
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
  death_on_tesla_cassie_announcement: '{PlayersTeam} died on tesla . {TeamMembersAlive} {PlayersTeam}s left'
  death_on_tesla_cassie_announcement_subtitles: '{PlayersTeam} died on tesla. {TeamMembersAlive} {PlayersTeam}s left.'
  should_tesla_cassie_announcements_be_noisy: true
  should_tesla_cassie_announcements_have_subtitles: true
  # Settings for Camera Scanner (When SCP enters Surface):
  is_camera_scanner_look_for_scp_leaving_feature_enabled: true
  should_camera_scanner_announce_scp_leaving_only_one_time: true
  cassie_delay_since_scp_leaving: 10
  should_cassie_check_if_scp_is_still_on_surface_after_the_delay: true
  scp_leaving_facility_cassie_announcement: 'the camera system has detected {ScpRole} outside the facility at {Gate}'
  scp_leaving_facility_cassie_announcement_subtitles: 'The Camera System has detected {ScpRole} outside the Facility at {Gate}.'
  should_scp_leaving_cassie_announcements_be_noisy: true
  should_scp_leaving_cassie_announcements_have_subtitles: true
  # Settings for Camera Scanner (When CI enters the Facility):
  is_camera_scanner_looking_for_ci_entering_feature_enabled: true
  # If set to false, cassie wont announce CI untill next CI spawn (option below)
  should_camera_scanner_announce_ci_entering_only_one_time: false
  cassie_delay_since_ci_entering: 10
  ci_entering_facility_cassie_announcement: 'the camera system has detected chaos insurgency agents inside the facility at {Gate}'
  ci_entering_facility_cassie_announcement_subtitles: 'The Camera System has detected Chaos Insurgency Agents inside the Facility at {Gate}.'
  should_ci_entering_facility_cassie_announcements_be_noisy: true
  should_ci_entering_facility_cassie_announcements_have_subtitles: true
  # Settings for Warhead (When someone turns it on):
  is_warhead_feature_enabled: true
  # If set to false, the announcements will be on cooldown specified below. If set to true, cassie will announce it only one time
  should_warhead_announce_only_one_time: false
  warhead_announcement_cooldown: 30
  warhead_announcement_turning_on_cassie_announcement: 'Warhead has been turned on by {PlayersTeam}'
  warhead_announcement_turning_on_cassie_announcement_subtitles: 'Warhead has been turned on by {PlayersTeam}.'
  should_warhead_announcement_turning_on_be_noisy: true
  should_warhead_announcement_turning_on_have_subtitles: true
  warhead_announcement_turning_off_cassie_announcement: 'Warhead has been turned off by {PlayersTeam}'
  warhead_announcement_turning_off_cassie_announcement_subtitles: 'Warhead has been turned off by {PlayersTeam}.'
  should_warhead_announcement_turning_off_be_noisy: true
  should_warhead_announcement_turning_off_have_subtitles: true
```
