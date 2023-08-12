using Rocket.API;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RandomizerPlugin
{
    public class RandomizerCommand : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Player;

        public string Name => "rand";

        public string Help => "";

        public string Syntax => "<name>";

        public List<string> Aliases => new List<string>();

        public List<string> Permissions => new List<string>();

        public void Execute(IRocketPlayer caller, string[] command)
        {
            UnturnedPlayer player = (UnturnedPlayer)caller;
            
            if (command.Length == 0)
            {
                UnturnedChat.Say(caller, "You must specify randomizer name!");
                return;
            }

            Randomizer randomizer = RandomizerPlugin.Instance.GetRandomizer(command[0]);

            if (randomizer == null) {
                UnturnedChat.Say(caller, "Unknown randomizer.");
                return;
            }

            int remain = RandomizerPlugin.Instance.Cooldowns.GetRemain(caller.ToString(), command[0]);

            if (remain > 0)
            {
                UnturnedChat.Say(caller, $"Wait for {remain} seconds to receive next weapon.");
                return;
            }

            randomizer.Execute(player);

            UnturnedChat.Say(caller, "Here you are!");

            RandomizerPlugin.Instance.Cooldowns.ResetCooldown(caller.ToString(), command[0]);
        }
    }
}

