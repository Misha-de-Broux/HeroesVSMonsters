using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models {
    public class Weapon {
        private Die _die;
        private int _modifier;
        public int DamageDie {  get; init; }
        public string Name { get; init; }
        public bool IsSecondary { get; init; }
        public bool IsTwoHanded { get; init; }
        public bool AllowsSecondary { get; init; }
        public int Value { get; private set; }

        private Weapon(int damageDie, string name, bool isSecondary, bool isTwoHanded, bool allowsSecondary) {
            _die = new Die(damageDie);
            DamageDie = damageDie;
            int modifierRoll = _die.Roll();
            _modifier = modifierRoll == 1 ? -2 :
                modifierRoll == damageDie ? 2 :
                modifierRoll > 2 * damageDie / 3 ? 1 :
                modifierRoll < damageDie / 3 ? -1 : 0;
            switch(_modifier) {
                case 2:
                    Name = "exceptional " + name;
                    break;
                case 1:
                    Name = "good " + name;
                    break;
                case -1:
                    Name = "poor " + name;
                    break;
                case -2:
                    Name = "abysmal " + name;
                    break;
                default:
                    Name = name;
                    break;
            }
            IsSecondary = isSecondary;
            IsTwoHanded = isTwoHanded;
            AllowsSecondary = allowsSecondary;
            Value = damageDie/2 + _modifier*2 > 0 ? damageDie / 2 + _modifier * 2 : 1;
        }

        private Weapon(int damageDie, string name, bool isTwoHanded) {
            _modifier = 0;
            _die = new Die(damageDie);
            DamageDie = damageDie;
            Name = name;
            IsSecondary = true;
            IsTwoHanded = isTwoHanded;
            AllowsSecondary = true;
            Value = -1;
        }

        internal static Weapon NaturalWeapon(int damageDie, string name) {
            return new Weapon(damageDie, name, false);
        }
        internal static Weapon HeavyNaturalWeapon(int damageDie, string name) {
            return new Weapon(damageDie, name, true);
        }
        internal static Weapon LightWeapon(int damageDie, string name) {
            return new Weapon(damageDie, name, true, false, true);
        }
        internal static Weapon MainWeapon(int damageDie, string name) {
            return new Weapon(damageDie, name, false, false, true);
        }
        internal static Weapon HeavyWeapon(int damageDie, string name) {
            return new Weapon(damageDie, name, false, true, false);
        }
        internal static Weapon? GenerateWeapon(int missOdds) {
            Weapon? weapon = null;
            switch (Random.Shared.Next(6 + missOdds)) {
                case 0:
                    weapon = Weapon.LightWeapon(4, "dagger");
                    break;
                case 1:
                    weapon = Weapon.LightWeapon(6, "shortsword");
                    break;
                case 2:
                    weapon = Weapon.MainWeapon(6, "club");
                    break;
                case 3:
                    weapon = Weapon.MainWeapon(8, "longsword");
                    break;
                case 4:
                    weapon = Weapon.HeavyWeapon(8, "thicc club");
                    break;
                case 5:
                    weapon = Weapon.HeavyWeapon(10, "battleaxe");
                    break;
            }
            return weapon;
        }

        public override string ToString() {
            return $"{Name} : 1D{DamageDie}. {(IsSecondary ? " It can be used as a secondary weapon." : "")}{(IsTwoHanded ? " It required both your hands." : "")} Its value is arround {Value} golds";
        }
        public int DamageRoll() {
            return _die.Roll() + _modifier;
        }

    }
}
