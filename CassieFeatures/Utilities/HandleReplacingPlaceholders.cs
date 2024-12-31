using System;
using System.Collections.Generic;
using System.Linq;
using Exiled.API.Features;
using Exiled.API.Features.Roles;
using PlayerRoles;

namespace CassieFeatures.Utilities
{
    public static class HandleReplacingPlaceholders
    {
        // This is for CASSIEs
        public static string ReplacePlaceholdersTeam(string input, Team team)
        {
            var teamProperties = new Dictionary<Team, (Func<int> Count, string Name)>
            {
                { Team.ChaosInsurgency, (() => Player.Get(Team.ChaosInsurgency).Count(), "Chaos Insurgency Agent") },
                { Team.ClassD, (() => Player.Get(RoleTypeId.ClassD).Count(), "Class D Personnel") },
                { Team.FoundationForces, (() => Player.Get(Team.FoundationForces).Count(), "Foundation Forces Personnel") },
                { Team.OtherAlive, (() => Player.Get(RoleTypeId.Tutorial).Count(), "Unspecified Agent") },
                { Team.SCPs, (() => Player.Get(Team.SCPs).Count(), "SCP") },
                { Team.Scientists, (() => Player.Get(RoleTypeId.Scientist).Count(), "Scientist") }
            };
            
            if (teamProperties.TryGetValue(team, out var properties))
            {
                string teamMembersAlive = properties.Count().ToString();
                string teamName = properties.Name;
                return input.Replace("{TeamMembersAlive}", teamMembersAlive)
                    .Replace("{PlayersTeam}", teamName);
            }
            
            return input.Replace("{TeamMembersAlive}", "Unknown number")
                .Replace("{PlayersTeam}", "Unknown team");
        }

        public static string ReplacePlaceholdersScpRole(string input, Role scpRole)
        {
            var scpRoleTexts = new Dictionary<Type, string>
            {
                { typeof(Scp049Role), "SCP 0 4 9" },
                { typeof(Scp0492Role), "SCP 0 4 9 2" },
                { typeof(Scp096Role), "SCP 0 9 6" },
                { typeof(Scp106Role), "SCP 1 0 6" },
                { typeof(Scp173Role), "SCP 1 7 3" },
                { typeof(Scp3114Role), "SCP 3 1 1 4" },
                { typeof(Scp939Role), "SCP 9 3 9" },
                { typeof(Scp1507Role), "SCP 1 5 0 7" }
            };
            
            if (scpRoleTexts.TryGetValue(scpRole.GetType(), out string scpText))
            {
                return input.Replace("{ScpRole}", scpText);
            }
            
            Log.Error("[CassieFeatures] Unspecified SCP role was used! Report this to the plugin manager (dc: iksemdem_)");
            return input.Replace("{ScpRole}", "unspecified SCP");
        }
    }
}