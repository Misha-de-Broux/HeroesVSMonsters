using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models {
    public class Wolf : Monster {
        public Wolf() : this(new Position(0, 0)) {
        }

        public Wolf(Position position) : base(position) {
            Loot.Add(LootType.Leather, new Die(4).Roll());
        }


    }
}
