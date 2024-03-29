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
        public bool AllowsSeconday { get; init; }
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
            IsSecondary = isTwoHanded;
            AllowsSeconday = allowsSecondary;
            Value = damageDie/2 + _modifier*2 > 0 ? damageDie / 2 + _modifier * 2 : 1;
        }

        private Weapon(int damageDie, string name, bool isTwoHanded) {
            _modifier = 0;
            _die = new Die(damageDie);
            DamageDie = damageDie;
            Name = name;
            IsSecondary = true;
            IsTwoHanded = isTwoHanded;
            AllowsSeconday = true;
            Value = -1;
        }

        public static Weapon NaturalWeapon(int damageDie, string name) {
            return new Weapon(damageDie, name, false);
        }
        public static Weapon HeavyNaturalWeapon(int damageDie, string name) {
            return new Weapon(damageDie, name, true);
        }
        public static Weapon LightWeapon(int damageDie, string name) {
            return new Weapon(damageDie, name, true, false, true);
        }
        public static Weapon MainWeapon(int damageDie, string name) {
            return new Weapon(damageDie, name, false, false, true);
        }
        public static Weapon HeavyWeapon(int damageDie, string name) {
            return new Weapon(damageDie, name, false, true, false);
        }

        public override string ToString() {
            return $"{Name} : 1D{DamageDie}. {(IsSecondary ? " It can be used as a secondary weapon." : "")}{(IsTwoHanded ? " It required both your hands." : "")} Its value is arround {Value} golds";
        }
        public int DamageRoll() {
            return _die.Roll() + _modifier;
        }

    }
}
