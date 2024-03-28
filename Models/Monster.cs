using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models {
    public abstract class Monster : Character {
        private bool _looted;
        protected Inventory Loot {  get; set; }
        public override string Denomination { get { return this.GetType().Name; } }
        public Monster() : base() {
            _looted = false;
            Loot = new Inventory();
        }

        public Monster(Position position) : base(position){
            _looted = false;
            Loot = new Inventory();
        }
        public Inventory GetLoot() {
            if (_looted) {
                return new Inventory();
            } else {
                _looted = true;
                return Loot;
            }
        }
        public bool Sees(PlayableCharacter pc) { 
            return this.Position - pc.Position <= 1;
        }

        public override string ToString() {
            return $"{base.ToString()}{(_looted ? $"\nLoot found :\n{Loot}" : "")}";
        }

    }
}
