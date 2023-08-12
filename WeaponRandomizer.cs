using Rocket.Unturned.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace RandomizerPlugin
{
    public class WeaponRandomizer : Randomizer
    {
        private List<Weapon> Weapons;
        private List<double> Weights;
        private int MaxAmmo;
        private int MinAmmo;

        public WeaponRandomizer(int cooldown, List<Weapon> weapons, int minAmmo, int maxAmmo)
        {
            Name = "weapon";
            Cooldown = cooldown;
            Weapons = weapons;
            MaxAmmo = maxAmmo;
            MinAmmo = minAmmo;

            Weights = new List<double>();

            foreach (Weapon w in weapons)
                Weights.Add(w.Weight);
            
        } 

        public override void Execute(UnturnedPlayer player)
        {
            int id = GetWeightedChoice(Weights);
            Weapon weapon = Weapons[id];
            int ammo = GetRand().Next(MinAmmo, MaxAmmo + 1);

            player.GiveItem(weapon.GunId, 1);
            player.GiveItem(weapon.AmmoId, (byte)ammo);

            foreach (ushort a in weapon.Attachments)
                player.GiveItem(a, 1);
        }

    }

    public class Weapon
    {
        [XmlElement("GunId")]
        public ushort GunId = 0;

        [XmlElement("AmmoId")]
        public ushort AmmoId = 0;

        [XmlElement("Weight")]
        public double Weight = 1.0;

        [XmlArrayItem(ElementName = "Attachment")]
        public List<ushort> Attachments = new List<ushort>();

        public Weapon() { }

        public Weapon(ushort gunId, ushort ammoId)
        {
            GunId = gunId;
            AmmoId = ammoId;
        }

        public Weapon(ushort gunId, ushort ammoId, double weight, List<ushort> attachments)
        {
            GunId = gunId;
            AmmoId = ammoId;
            Weight = weight;
            Attachments = attachments;
        }
    }
}
