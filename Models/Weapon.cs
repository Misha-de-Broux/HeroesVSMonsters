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
        
    }
}
