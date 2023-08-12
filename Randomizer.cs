using Rocket.Unturned.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace RandomizerPlugin
{
    public abstract class Randomizer
    {
        public string Name { get; protected set; } = "sample";

        public int Cooldown = 100;

        public abstract void Execute(UnturnedPlayer player);

        protected Random GetRand()
        {
            return RandomizerPlugin.Rand;
        }

        protected int GetWeightedChoice(List<double> weights)
        {
            double totalWeight = weights.Sum();
            double randFloat = GetRand().NextDouble() * totalWeight;
            double current = 0;

            for(int i = 0; i < weights.Count; i++)
            {
                current += weights[i];
                if (randFloat < current)
                    return i;
            }

            return 0;
        }
    }
}
