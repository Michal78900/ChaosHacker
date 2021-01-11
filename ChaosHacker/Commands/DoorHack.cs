using System;
using Exiled.API.Features;
using CommandSystem;
using MEC;

namespace ChaosHacker.Commands
{
    [CommandHandler(typeof(ClientCommandHandler))]
    public class DoorHack : ICommand
    {
        public string Command { get; } = "door";

        public string[] Aliases { get; } = new string[] { "d" };

        public string Description { get; } = "Command for Chaos Hacker to hack doors";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            string action;

            Player ply = Player.Get((sender as CommandSender)?.SenderId);

            if (!Handler.ChaosHackers.Contains(ply))
            {
                response = "You are not a Chaos Hacker! You can not use this command!";
                return false;

            }
            else if (ChaosHacker.Instance.Config.CiHackerAbilityCooldown - ((int)Round.ElapsedTime.TotalSeconds - Handler.ChaosHackersCooldown[ply]) > 0)
            {
                response = $"You need to wait {ChaosHacker.Instance.Config.CiHackerAbilityCooldown - ((int)Round.ElapsedTime.TotalSeconds - Handler.ChaosHackersCooldown[ply])} seconds to use this ability!";
                return false;
            }

            else if (arguments.Count != 2)
            {
                response = "Correct command format: .door <door_name> <open/close/lock>";
                return false;
            }

            else if (!ChaosHacker.Instance.Config.DoorNames.Contains(arguments.At(0).ToUpper()))
            {
                Log.Info(arguments.At(0));
                response = "Door name is incorect!";
                return false;
            }

            else
            {
                switch (action = arguments.At(1).ToLower())
                {
                    case "open":
                        {
                            var door = Map.GetDoorByName(arguments.At(0).ToUpper());

                            if(door.NetworkTargetState == true)
                            {
                                response = "The door is already opened!";
                                return false;
                            }

                            door.NetworkTargetState = true;

                            response = "Command executed successfully! The door has been opened!";

                            Handler.ChaosHackersCooldown[ply] = (int)Round.ElapsedTime.TotalSeconds;

                            return true;
                        }
                    case "close":
                        {
                            var door = Map.GetDoorByName(arguments.At(0).ToUpper());

                            if (door.NetworkTargetState == false)
                            {
                                response = "The door is already closed!";
                                return false;
                            }

                            door.NetworkTargetState = false;

                            response = "Command executed successfully! The door has been closed!";

                            Handler.ChaosHackersCooldown[ply] = (int)Round.ElapsedTime.TotalSeconds;

                            return true;
                        }
                    case "lock":
                        {
                            var door = Map.GetDoorByName(arguments.At(0).ToUpper());

                            if(door.NetworkActiveLocks == 1)
                            {
                                response = "The door is already locked!";
                                return false;
                            }

                            door.NetworkActiveLocks = 1;

                            Timing.CallDelayed(ChaosHacker.Instance.Config.CiHackerLockAbilityDuration, () => door.NetworkActiveLocks = 0);

                            response = $"Command executed successfully! The door has been locked for {ChaosHacker.Instance.Config.CiHackerLockAbilityDuration} seconds!";

                            Handler.ChaosHackersCooldown[ply] = (int)Round.ElapsedTime.TotalSeconds;

                            return true;
                        }

                    default:
                        {
                            response = "Incorrect second argument!";
                            return false;
                        }
                }
            }
        }
    }
}
