using Exiled.API.Enums;

namespace CassieFeatures.Utilities
{
    public class LockedDoor
    {
        public LockedDoor()
        {
        }

        public LockedDoor(DoorType doorType, int delay, bool open, bool unlock, bool @lock, bool destroy)
        {
            DoorType = doorType;
            Delay = delay;
            Open = open;
            Unlock = unlock;
            Lock = @lock;
            Destroy = destroy;
        }
        
        public DoorType DoorType { get; init; }
        public int Delay { get; init; }
        public bool Open { get; init; }
        public bool Unlock { get; init; }
        public bool Lock { get; init; }
        public bool Destroy { get; init; }
    }
}