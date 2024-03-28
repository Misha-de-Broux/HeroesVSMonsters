using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models {
    internal class Die {
        private int min, max;
        public Die(int max) {
            this.max = max;
            this.min = 1;
        }
        public Die(int min, int max) {
            this.min = min;
            this.max = max;
        }

        public int Roll() { return Random.Shared.Next(min, max + 1); }
    }
}
