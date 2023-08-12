using Rocket.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace RandomizerPlugin
{
    public class RandomizerPluginConfiguration : IRocketPluginConfiguration
    {
        [XmlArrayItem(ElementName = "Weapon")]
        public List<Weapon> Weapons;

        public int GlobalCooldown;
        public int MaxAmmo;
        public int MinAmmo;

        public void LoadDefaults()
        {
            Weapons = new List<Weapon>();
            GlobalCooldown = 10;
            MaxAmmo = 5;
            MinAmmo = 2;
        }
    }
}
