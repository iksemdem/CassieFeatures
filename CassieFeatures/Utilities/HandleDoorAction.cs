using System.Linq;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Doors;
using MEC;

namespace CassieFeatures.Utilities
{
    public static class HandleDoorAction
    {
        public static void LockDoors()
        {
            foreach (DoorType door in Plugin.Instance.Config.LockedDoors)
            {
                var doors = Door.List.Where(d => MatchesDoorType(d, door)).ToList();
                
                if (doors.Count == 0)
                {
                    Log.Debug($"No doors matching type {door} found!");
                    continue;
                }

                foreach (var d in doors)
                {
                    if (!d.IsLocked)
                    {
                        Log.Debug($"Locking door: {door}. Lock type: {DoorLockType.SpecialDoorFeature}");
                        d.ChangeLock(DoorLockType.SpecialDoorFeature);
                    }
                }
            }
        }

        public static void ActionOnDoor()
        {
            foreach (LockedDoor door in Plugin.Instance.Config.DoorsAction)
            {
                var doors = Door.List.Where(d => MatchesDoorType(d, door.DoorType)).ToList();

                if (doors.Count == 0)
                {
                    Log.Debug($"No doors matching type {door.DoorType} found!");
                    continue;
                }

                foreach (var d in doors)
                {
                    Log.Debug($"Starting actions for a {door.DoorType} with delay {door.Delay}");
                    Timing.CallDelayed(door.Delay, () =>
                    {
                        Log.Debug($"Executing actions on a {door.DoorType}:");

                        if (door.Open)
                        {
                            if (!d.IsOpen)
                            {
                                Log.Debug("Opening...");
                                d.IsOpen = true;
                            }
                        }

                        if (door.Unlock)
                        {
                            if (d.IsLocked)
                            {
                                Log.Debug("Unlocking...");
                                d.Unlock();
                            }
                        }

                        if (door.Lock && !door.Unlock)
                        {
                            if (!d.IsLocked)
                            {
                                Log.Debug("Locking...");
                                d.ChangeLock(DoorLockType.SpecialDoorFeature);
                            }
                        }

                        if (door.Destroy)
                        {
                            if (d is BreakableDoor breakable)
                            {
                                Log.Debug("Destroying...");
                                breakable.Break();
                            }
                        }
                    }, Server.Host.GameObject);
                }
            }
        }
        private static bool MatchesDoorType(Door door, DoorType targetType)
        {
            return door.Type == targetType;
        }
    }
}