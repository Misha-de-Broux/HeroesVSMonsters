using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models {
    public class Drake : Monster {
        public override int Constitution => base.Constitution + 1;
        public Drake() : this(new Position(0, 0)) {
        }
        public Drake(Position position) : base(position) {
            Loot.Add(LootType.Leather, new Die(4).Roll());
            Loot.Add(LootType.Gold, new Die(6).Roll());
        }
    }
}
