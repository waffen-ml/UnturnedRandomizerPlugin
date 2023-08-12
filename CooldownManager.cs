using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RandomizerPlugin
{
    public class CooldownManager
    {
        public static CooldownManager Instance { get; private set; }
        private Dictionary<string, PlayerCooldown> players = new Dictionary<string, PlayerCooldown>();

        public CooldownManager()
        {
            Instance = this;
        }
        public int GetRandomizerCooldown(string rand) {
            return RandomizerPlugin.Instance.GetRandomizer(rand).Cooldown;
        }

        private PlayerCooldown GetPlayerData(string player)
        {
            if (!players.ContainsKey(player))
                players[player] = new PlayerCooldown();
            return players[player];
        }

        public void ResetCooldown(string player, string rand)
        {
            GetPlayerData(player).ResetTime(rand);
        }

        public int GetRemain(string player, string rand)
        {
            return GetPlayerData(player).GetRemain(rand);
        }

    }

    class PlayerCooldown
    {
        private Dictionary<string, DateTime> Cooldowns = new Dictionary<string, DateTime>();

        public void ResetTime(string rand)
        {
            Cooldowns[rand] = DateTime.Now;
        }

        public int GetRemain(string rand)
        {
            if (!Cooldowns.ContainsKey(rand)) return 0;
            DateTime last = Cooldowns[rand];
            TimeSpan ts = (DateTime.Now - last);
            int targetTime = CooldownManager.Instance.GetRandomizerCooldown(rand);
            return Math.Max(0, targetTime - (int)ts.TotalSeconds);
        }

    }

}
