using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models {
    public abstract class Character {

        private Die d4;
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
            d4 = new Die(4);
            this.Position = position;
        }

        public override string ToString() {
            return $"{Denomination}\nStrength : {Strength}\nConstitution : {Constitution}\nHP : {HP}/{Constitution + GetModif(Constitution)}";
        }

        public void Rest() {
            Console.WriteLine($"{Denomination} is resting...");
            HP = Constitution + GetModif(Constitution);
        }

        internal bool DieFrom(int damage) {
            if (damage < 0) { damage = 0; }
            HP -= damage;
            Console.WriteLine($"{Denomination} takes {damage} damages{(HP <= 0 ? " and dies." : "")}");
            return HP > 0;
        }

        internal int RollDamage() {
            return d4.Roll() + GetModif(Strength);
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

    }
}
