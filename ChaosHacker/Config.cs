using Exiled.API.Interfaces;
using System.Collections.Generic;
using System.ComponentModel;

namespace ChaosHacker
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        [Description("A chance to spawn a CI Hacker in the next CI spawn wave:")]
        public int CiHackerSpawnChance { get; set; } = 10;
        [Description("Max CI Hackers on the map at the same time. Set to 0 for no limit.")]
        public uint MaxCiHackers { get; set; } = 1;
        [Description("The Chaos Hacker's role name that is shown to other players when they look at him (leave empty to disable custom role name):")]
        public string ChaosHackerRoleName { get; set; } = "<color=cyan>Chaos Hacker</color>";
        [Description("The broadcast shown to Chaos Hacker:")]
        public string CiHackerBroadcastMessage { get; set; } = "You are a <color=cyan>Chaos Hacker</color>!\nYou have ability to remote access the doors listed in [~]";
        [Description("The broadcast duration (in seconds):")]
        public ushort CiHackerBroadcastDuration { get; set; } = 10;
        [Description("The duration of lock door ability:")]
        public float CiHackerLockAbilityDuration { get; set; } = 10;
        [Description("The Chaos Hacker ability cooldown (in seconds):")]
        public int CiHackerAbilityCooldown { get; set; } = 100;



        public List<string> DoorNames = new List<string>
        {
            "012",
            "012_BOTTOM",
            "012_LOCKER",
            "049_ARMORY",
            "079_FIRST",
            "079_SECOND",
            "096",
            "106_BOTTOM",
            "106_PRIMARY",
            "106_SECONDARY",
            "173_ARMORY",
            "173_BOTTOM",
            "173_CONNECTOR",
            "173_GATE",
            "914",
            "CHECKPOINT_EZ_HCZ",
            "CHECKPOINT_LCZ_A",
            "CHECKPOINT_LCZ_B",
            "ESCAPE_PRIMARY",
            "ESCAPE_SECONDARY",
            "GATE_A",
            "GATE_B",
            "GR18",
            "HCZ_ARMORY",
            "HID",
            "HID_LEFT",
            "HID_RIGHT",
            "INTERCOM",
            "LCZ_ARMORY",
            "LCZ_CAFE",
            "LCZ_WC",
            "NUKE_ARMORY",
            "SERVERS_BOTTOM",
            "SURFACE_GATE",
            "SURFACE_NUKE",
        };

    }
}
