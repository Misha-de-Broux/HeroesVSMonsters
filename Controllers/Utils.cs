using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controllers {
    internal static class Utils {
        public static int TryParseYesOrNo(string answer) {
            int result;
            switch (answer) {
                case "y":
                    result = 1;
                    break;
                case "Y":
                    result = 1;
                    break;
                case "yes":
                    result = 1;
                    break;
                case "Yes":
                    result = 1;
                    break;
                case "n":
                    result = 0;
                    break;
                case "N":
                    result = 0;
                    break;
                case "no":
                    result = 0;
                    break;
                case "No":
                    result = 0;
                    break;
                default:
                    result = -1;
                    break;
            }
            return result;
        }
        public static bool TryParseDirection(string input, out Directions direction) {
            bool parsed = false;
            switch (input) {
                case "North":
                    direction = Directions.North;
                    parsed = true;
                    break;
                case "north":
                    direction = Directions.North;
                    parsed = true;
                    break;
                case "N":
                    direction = Directions.North;
                    parsed = true;
                    break;
                case "n":
                    direction = Directions.North;
                    parsed = true;
                    break;
                case "z":
                    direction = Directions.North;
                    parsed = true;
                    break;
                case "South":
                    direction = Directions.South;
                    parsed = true;
                    break; ;
                case "south":
                    direction = Directions.South;
                    parsed = true;
                    break; ;
                case "S":
                    direction = Directions.South;
                    parsed = true;
                    break; ;
                case "s":
                    direction = Directions.South;
                    parsed = true;
                    break;
                case "East":
                    direction = Directions.East;
                    parsed = true;
                    break;
                case "e":
                    direction = Directions.East;
                    parsed = true;
                    break;
                case "east":
                    direction = Directions.East;
                    parsed = true;
                    break;
                case "E":
                    direction = Directions.East;
                    parsed = true;
                    break;
                case "d":
                    direction = Directions.East;
                    parsed = true;
                    break;
                case "West":
                    direction = Directions.West;
                    parsed = true;
                    break;
                case "west":
                    direction = Directions.West;
                    parsed = true;
                    break;
                case "W":
                    direction = Directions.West;
                    parsed = true;
                    break;
                case "w":
                    direction = Directions.West;
                    parsed = true;
                    break;
                case "q":
                    direction = Directions.West;
                    parsed = true;
                    break;
                default:
                    direction = Directions.East;
                    break;
            }
            return parsed;
        }
        public static bool TryParseCommand(string input, out Command command) {
            bool parsed = false;
            switch (input) {
                case "equip":
                    parsed = true;
                    command = Command.Equip;
                    break;
                case "Equip":
                    parsed = true;
                    command = Command.Equip;
                    break;
                case "p":
                    parsed = true;
                    command = Command.Equip;
                    break;
                case "P":
                    parsed = true;
                    command = Command.Equip;
                    break;
                case "inventory":
                    parsed = true;
                    command = Command.Inventory;
                    break;
                case "Inventory":
                    parsed = true;
                    command = Command.Inventory;
                    break;
                case "i":
                    parsed = true;
                    command = Command.Inventory;
                    break;
                case "I":
                    parsed = true;
                    command = Command.Inventory;
                    break;
                case "character":
                    parsed = true;
                    command = Command.Character;
                    break;
                case "Character":
                    parsed = true;
                    command = Command.Character;
                    break;
                case "c":
                    parsed = true;
                    command = Command.Character;
                    break;
                case "C":
                    parsed = true;
                    command = Command.Character;
                    break;
                case "map":
                    parsed = true;
                    command = Command.Map;
                    break;
                case "Map":
                    parsed = true;
                    command = Command.Map;
                    break;
                case "m":
                    parsed = true;
                    command = Command.Map;
                    break;
                case "M":
                    parsed = true;
                    command = Command.Map;
                    break;
                default:
                    command = Command.Inventory;
                    break;
            }
            return parsed;
        }
        public static string ListRaces() {
            string result = "";
            foreach (Race race in Enum.GetValues(typeof(Race))) {
                if (result == "") {
                    result += race.ToString();
                } else {
                    result += ", " + race.ToString();
                }
            }
            return result;
        }
    }
}

