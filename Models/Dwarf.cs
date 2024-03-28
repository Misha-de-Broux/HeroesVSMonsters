using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models {
    public class Dwarf : PlayableCharacter{
        public override int Constitution => base.Constitution + 2;
        public Dwarf(string name) : base(name) { }
    }
}
