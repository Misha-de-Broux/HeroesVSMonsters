using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models {
    public class Position {
        public int X { get; private set; }
        public int Y { get; private set; }
        public Position(int x, int y) {
            X = x;
            Y = y;
        }
        internal void Move(Directions direction) {
            switch (direction) {
                case Directions.North:
                    Y -= 1;
                    break;
                case Directions.South:
                    Y += 1;
                    break;
                case Directions.East:
                    X += 1;
                    break;
                case Directions.West:
                    X -= 1;
                    break;
            }
        }

        public static int operator -(Position p1, Position p2) {
            return Math.Abs(p1.X - p2.X) + Math.Abs(p1.Y - p2.Y);
        }
    }
}
