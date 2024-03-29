using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models {
    public abstract class Character {

        protected List<Weapon> _secondaryWeapons;
        public Weapon? PrimaryWeapon {  get; protected set; }
        public Weapon[] SecondaryWeapon { get { return _secondaryWeapons.ToArray(); }  }
        public virtual int Strength { get; private set; }
        public virtual int Constitution { get; private set; }
        public Position Position { get; protected set; }
        private int HP { get; set; }

        public abstract String Denomination { get; }

        public Character() : this(new Position(0, 0)) {
        }

        public Character(Position position) {
            Strength = RollStat();
            Constitution = RollStat();
            HP = Constitution + GetModif(Constitution);
            Position = position;
            _secondaryWeapons = new List<Weapon>();
        }

        public override string ToString() {
            return $"{Denomination}\nStrength : {Strength}\nConstitution : {Constitution}\nHP : {HP}/{Constitution + GetModif(Constitution)}";
        }

        public void Rest() {
            Console.WriteLine($"{Denomination} is resting...");
            HP = Constitution + GetModif(Constitution);
        }

        internal bool DieFrom(Character opponent) {
            Weapon primary = opponent.PrimaryWeapon is null ? DEFAULT_WEAPON : opponent.PrimaryWeapon;
            int damage = primary.DamageRoll() + (primary.IsTwoHanded ? 2 : 1) * GetModif(opponent.Strength) ;
            if (damage < 0) { damage = 0; }
            HP -= damage;
            Console.WriteLine($"{Denomination} takes {damage} damages from {opponent.Denomination}'s {primary.Name}");
            foreach (Weapon seconday in opponent.SecondaryWeapon) {
                damage = seconday.DamageRoll() + GetModif(opponent.Strength) < 0 ? GetModif(opponent.Strength) : 0;
                if (damage < 0) { damage = 0; }
                HP -= damage;
                Console.WriteLine($"{Denomination} takes {damage} damages from {opponent.Denomination}'s {seconday.Name}");
            }
            if(HP < 0) {
                Console.WriteLine($"{Denomination} dies.");
            }
            return HP > 0;
        }

        private int GetModif(int stat) {
            return stat < 5 ? -1 : stat < 10 ? 0 : stat < 15 ? 1 : 2;
        }
        private int RollStat() {
            Die d6 = new Die(6);
            int stat = 0, minRoll = d6.Roll();
            for (int i = 0; i < 3; i++) {
                int roll = d6.Roll();
                if (roll < minRoll) {
                    stat += minRoll;
                    minRoll = roll;
                } else {
                    stat += roll;
                }
            }
            return stat;
        }

        private static Weapon DEFAULT_WEAPON = Weapon.NaturalWeapon(4, "natural weapons");

    }
}
