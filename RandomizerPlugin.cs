using Rocket.Core.Logging;
using Rocket.Core.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace RandomizerPlugin
{
    public class RandomizerPlugin : RocketPlugin<RandomizerPluginConfiguration>
    {
        public static Random Rand { get; } = new Random();
        public static RandomizerPlugin Instance { get; private set; }
        public CooldownManager Cooldowns { get; } = new CooldownManager();
        private List<Randomizer> Randomizers = new List<Randomizer>();

        protected override void Load()
        {
            Instance = this;
            Logger.Log($"{Name} is loading...");

            RandomizerPluginConfiguration config = Configuration.Instance;

            Logger.Log($"Loaded {config.Weapons.Count} weapons!");

            Randomizers.Add(new WeaponRandomizer(
               config.GlobalCooldown,
               config.Weapons,
               config.MinAmmo,
               config.MaxAmmo
            ));
        }

        public Randomizer GetRandomizer(string rand)
        {
            return Randomizers.Where(i => i.Name == rand).FirstOrDefault();
        }

        protected override void Unload()
        {
            Logger.Log($"{Name} is unloading...");
        }
    }
}
