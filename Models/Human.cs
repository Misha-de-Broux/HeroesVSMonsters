using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models {
    public class Human : PlayableCharacter {
        public override int Constitution => base.Constitution + 1;
        public override int Strength => base.Strength + 1;

        public Human(string name) : base(name) { }
    }
}
