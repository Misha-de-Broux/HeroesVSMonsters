using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models {
    public class Board {
        private List<Monster> _hidden;
        private List<Monster> _visible;
        private PlayableCharacter _hero;
        private int _height, _width;
        private const string _ERROR_OOB_MSG = "You're trying to go out of bonds, please don't.";

        public Board(PlayableCharacter hero) {
            _hero = hero;
            _height = 15;
            _width = 15;
            _visible = new List<Monster>();
            GenerateMonsters(Random.Shared.Next(8, 13));
        }

        private void GenerateMonsters(int amount) {
            _hidden = new List<Monster>();
            while (_hidden.Count < amount) {
                Position p = new Position(Random.Shared.Next(_height), Random.Shared.Next(_width));
                bool farEnough = _hero.Position - p > 2;
                for (int i = 0; i < _hidden.Count && farEnough; i++) {
                    farEnough = _hidden[i].Position - p > 2;
                }
                if (farEnough) {
                    switch (Random.Shared.Next(3)) {
                        case 0:
                            _hidden.Add(new Wolf(p));
                            break;
                        case 1:
                            _hidden.Add(new Orc(p));
                            break;
                        case 2:
                            _hidden.Add(new Drake(p));
                            break;
                    }
                }
            }
        }

        public Monster? MoveHero(Directions direction) {
            bool oob = false;
            switch (direction) {
                case Directions.North:
                    if (_hero.Position.Y == 0) {
                        OOB();
                        oob = true;
                    }
                    break;
                case Directions.South:
                    if (_hero.Position.Y == _height - 1) {
                        OOB();
                        oob = true;
                    }
                    break;
                case Directions.East:
                    if (_hero.Position.X == _height - 1) {
                        OOB();
                        oob = true;
                    }
                    break;
                case Directions.West:
                    if (_hero.Position.X == 0) {
                        OOB();
                        oob = true;
                    }
                    break;
            }
            if (!oob) {
                _hero.Position.Move(direction);
                for (int i = 0; i < _hidden.Count; i++)
                {
                    if (_hidden[i].Sees(_hero)) {
                        Monster monster = _hidden[i];
                        _visible.Add(monster);
                        _hidden.Remove(monster);
                        return monster;
                    }
                }
                _hero.Rest();
            }
            return null;
        }

        public override string ToString() {
            string board = "";
            for(int x = 0; x < _width; x++) {
                board += " _";
            }
            board += "\n";
            for (int y = 0; y < _height; y++) {
                board += "|";
                for (int x = 0; x < _width; x++) {
                    if (y == _hero.Position.Y && x == _hero.Position.X) {
                        board += "@";
                    } else {
                        bool seen = false;
                        for (int i = 0; i < _visible.Count; i++) {
                            if (y == _visible[i].Position.Y && x == _visible[i].Position.X) {
                                seen = true;
                                switch (_visible[i].GetType().Name) {
                                    case "Wolf":
                                        board += "W";
                                        break;
                                    case "Orc":
                                        board += "O";
                                        break;
                                    case "Drake":
                                        board += "D";
                                        break;
                                    default:
                                        board += "?";
                                        break;
                                }
                            }
                        }
                        if (!seen) {
                            board += "_";
                        }
                    }
                    board += "|";
                }
                board += "\n";
            }
            return board;
        }

        private void OOB() {
            Console.WriteLine(_ERROR_OOB_MSG);
        }
    }
}
