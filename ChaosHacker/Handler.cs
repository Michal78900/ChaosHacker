using System;
using System.Linq;
using System.Text;
using Exiled.API.Features;
using Exiled.Events.EventArgs;
using System.Collections.Generic;
using MEC;
using UnityEngine;


namespace ChaosHacker
{
    class Handler
    {
        System.Random rng = new System.Random();

        public static List<Player> ChaosHackers = new List<Player>();

        public static Dictionary<Player, int> ChaosHackersCooldown = new Dictionary<Player, int>();

        uint NumberOfChaosHackers = 0;

        public void OnTeamRespawn(RespawningTeamEventArgs ev)
        {
            if(ev.NextKnownTeam == Respawning.SpawnableTeamType.ChaosInsurgency)
            {
                if (ChaosHacker.Instance.Config.MaxCiHackers != 0 && ChaosHacker.Instance.Config.MaxCiHackers == NumberOfChaosHackers) return;

                if (rng.Next(0, 101) > ChaosHacker.Instance.Config.CiHackerSpawnChance) return;

                ChaosHackers.Add(ev.Players[rng.Next(ev.Players.Count)]);

                HackerChaos(ChaosHackers.Last());
            }
        }

        public void HackerChaos(Player ply)
        {
            if (!string.IsNullOrEmpty(ChaosHacker.Instance.Config.ChaosHackerRoleName))
            {
                ply.ReferenceHub.nicknameSync.ShownPlayerInfo &= ~PlayerInfoArea.Role;
                ply.CustomInfo = ChaosHacker.Instance.Config.ChaosHackerRoleName;
            }

            ply.Broadcast(ChaosHacker.Instance.Config.CiHackerBroadcastDuration, ChaosHacker.Instance.Config.CiHackerBroadcastMessage);

            ChaosHackersCooldown.Add(ply, (int)Round.ElapsedTime.TotalSeconds);

            StringBuilder doorNames = new StringBuilder();

            for (int i = 0; i < ChaosHacker.Instance.Config.DoorNames.Count; i++)
            {
                doorNames.Append($"{ ChaosHacker.Instance.Config.DoorNames[i]}\n");
            }

            ply.SendConsoleMessage($"\nList of all doors that you can interact with:\n{doorNames}", "yellow");

            ply.SendConsoleMessage("\nCommand: .door <door_name> <open/close/lock>\n" +
                "Example: .door GATE_B lock\n\n" +
                "You can also use .d for short and you can write door names in lowercase.", "green");

            NumberOfChaosHackers++;
        }





        public void OnPlayerLeft(LeftEventArgs ev)
        {
            if(ChaosHackers.Contains(ev.Player))
            {
                KillHackerChaos(ev.Player);
            }
        }

        public void OnPlayerDied(DiedEventArgs ev)
        {
            if(ChaosHackers.Contains(ev.Target))
            {
                KillHackerChaos(ev.Target);
            }
        }

        public void OnPlayerRoleChange(ChangingRoleEventArgs ev)
        {
            if (ChaosHackers.Contains(ev.Player) && ev.Player.Role != RoleType.Spectator)
            {
                KillHackerChaos(ev.Player);
            }
        }

        public void KillHackerChaos(Player ply)
        {
            if (!string.IsNullOrEmpty(ChaosHacker.Instance.Config.ChaosHackerRoleName))
            {
                ply.CustomInfo = string.Empty;
                ply.ReferenceHub.nicknameSync.ShownPlayerInfo |= PlayerInfoArea.Role;
            }

            ChaosHackers.Remove(ply);
            ChaosHackersCooldown.Remove(ply);

            NumberOfChaosHackers--;
        }
    }
}
