using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models {
    public class Orc : Monster {
        public override int Strength => base.Strength + 1;

        public Orc() : this(new Position(0, 0)) {
        }
        public Orc(Position pos) : base(pos) {
            Loot.Add(LootType.Gold, new Die(6).Roll());
            PrimaryWeapon = Weapon.GenerateWeapon(4);
            Loot.Add(PrimaryWeapon);
        }

        

    }
}
