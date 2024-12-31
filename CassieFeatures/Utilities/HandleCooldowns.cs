using Exiled.API.Features;
using MEC;

namespace CassieFeatures.Utilities
{
    public class HandleCooldowns
    {
        // This is for warhead cooldown
        public static bool IsWarheadOnCooldown = false;
        public static void WarheadCooldown()
        {
            IsWarheadOnCooldown = true;
            Log.Debug("Warhead cooldown started");
            Timing.CallDelayed(Plugin.Instance.Config.WarheadAnnouncementCooldown, () =>
            {
                IsWarheadOnCooldown = false;
                Log.Debug("Warhead cooldown stopped");
            }, Server.Host.GameObject);
        }
    }
}