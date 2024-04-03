using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models {
    public class PlayableCharacter : Character {

        public Inventory Inventory { get; private set; }
        public Weapon? SecondaryWeapon { get { 
                if (SecondaryWeapons.Length == 0) {
                    return null;
                } else {
                    return SecondaryWeapons[0];
                }
            } 
        }
        public string Name { get; init; }
        public override string Denomination { get { return Name + " the " + this.GetType().Name; } }

        public PlayableCharacter(string name) : base() {
            Inventory = new Inventory();
            Name = name;
            PrimaryWeapon = Weapon.GenerateWeapon(0);
        }
        public override string ToString() {
            return $"{base.ToString()}\nInventory :\n{Inventory}{(PrimaryWeapon is not null ? $"\nPrimary Weapon : {PrimaryWeapon}":"")}{(SecondaryWeapon is null ? "" : $"\nSecondary Weapon : {SecondaryWeapon}")}";
        }

        public bool WinFight(Monster opponent) {
            bool isAlive = true, oppoIsAlive = true;
            for (int i = 0; isAlive && oppoIsAlive; i++) {
                if (i % 2 == 0) {
                    oppoIsAlive = opponent.DieFrom(this);
                } else {
                    isAlive = DieFrom(opponent);
                }
            }
            return isAlive;
        }
        public void TakeLoot(Inventory loot) {
            Inventory += loot;
        }

        public void EquipPrimary(Weapon? weapon) {
            Inventory.Add(PrimaryWeapon);
            PrimaryWeapon = weapon;
            if (weapon is not null && weapon.IsTwoHanded) {
                EquipSecondary(null);
            }
        }

        public void EquipSecondary(Weapon? weapon) {
            Weapon? previous = SecondaryWeapon;

            if (weapon is null) {
                _secondaryWeapons = new List<Weapon>();
            } 
            else if(weapon.IsSecondary) {
                _secondaryWeapons = new List<Weapon>() { weapon };
            }
            if(previous is not null) {
                Inventory.Add(previous);
            }
        }

    }
}
