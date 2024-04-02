using Models;
using static System.Formats.Asn1.AsnWriter;

namespace Controllers {
    internal class Program {
        public static void Main(string[] args) {
            const string HUMAN = "Human";
            const string DWARF = "Dwarf";

            string[] raceNames = [HUMAN, DWARF];

            Console.WriteLine("Hello, new adventurer!\nWhat is your name ?");
            string name = Console.ReadLine();
            Console.WriteLine($"{name} ? What a great name ! What race are you ? {ListRaces(raceNames)} ?");
            string race = Console.ReadLine();
            while (!raceNames.Contains(race)) {
                Console.WriteLine("Sorry, the only available races are : " + ListRaces(raceNames));
                race = Console.ReadLine();
            }
            PlayableCharacter you;
            switch (race) {
                case HUMAN:
                    you = new Human(name);
                    break;
                case DWARF:
                    you = new Dwarf(name);
                    break;
                default:
                    Console.WriteLine("An error occured during the race choice.");
                    return;
            }
            Console.WriteLine($"Welcome to Shorewoods, here is your character sheet :\n{you}\nDo you wanna play on a board ? (Y)es or (N)o");
            int mode = OnBoard(Console.ReadLine());
            while (mode < 0) {
                Console.WriteLine("Please enter a valid answer.\nDo you wanna play on a board ? (Y)es or (N)o");
                mode = OnBoard(Console.ReadLine());
            }
            switch (mode) {
                case 1:
                    PlayOnBoard(you);
                    break;
                default:
                    PlayOffBoard(you);
                    break;
            }



        }

        private static string ListRaces(string[] raceNames) {
            string result = raceNames[0];
            for (int i = 1; i < raceNames.Length; i++) {
                if (i == raceNames.Length - 1) {
                    result += " or " + raceNames[i];
                } else {
                    result += ", " + raceNames[i];
                }
            }
            return result;
        }
        private static int OnBoard(String answer) {
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
        private static void PlayOffBoard(PlayableCharacter you) {
            bool isAlive = true;
            Monster monster;
            while (isAlive) {
                Console.WriteLine();
                switch (Random.Shared.Next(3)) {
                    case 0:
                        monster = new Wolf();
                        Console.WriteLine("A wolf jumps on you !");
                        break;
                    case 1:
                        Console.WriteLine("An orc charges at you !");
                        monster = new Orc();
                        break;
                    default:
                        Console.WriteLine("A drake comes down from the sky !");
                        monster = new Drake();
                        break;
                }
                isAlive = you.WinFight(monster);
                if (isAlive) {
                    Inventory loot = monster.GetLoot();
                    you.TakeLoot(loot);
                    Console.WriteLine($"You beat the monster ! You loot the following :\n{loot}\nIt is now time to rest.");
                    you.Rest();
                } else {
                    Console.WriteLine("You died ...\n\nLets remember your character.");
                    Console.WriteLine(you);
                    Console.WriteLine($"Final score : {you.Inventory.Value}.");
                }
            }
        }
        private static void PlayOnBoard(PlayableCharacter you) {
            Board board = new Board(you);
            Console.WriteLine(board);
            bool isAlive = true;
            int score = 0;
            while (isAlive) {
                Console.WriteLine("What direction do you wanna head North, East, West or South ?");
                string input = Console.ReadLine();
                Directions direcion;
                Command command;
                bool isDirection = TryParseDirection(input, out direcion);
                bool isCommand = TryParseDirection(input, out command);
                while (!isDirection && !isCommand) {
                    Console.WriteLine("Please enter a correct input.\nWhat direction do you wanna head North, East, West or South ?");
                    input = Console.ReadLine();
                    isDirection = TryParseDirection(input, out direcion);
                    isCommand = TryParseDirection(input, out command);
                }
                if (isDirection) {
                    Monster? monster = board.MoveHero(direcion);
                    if (monster is not null) {
                        Console.WriteLine($"\nA {monster.GetType().Name} attacks you !");
                        isAlive = you.WinFight(monster);
                        if (isAlive) {
                            Inventory loot = monster.GetLoot();
                            you.TakeLoot(loot);
                            Console.WriteLine($"You beat the monster ! You loot the following :\n{loot}");
                            if (board.isWon()) {
                                isAlive = false;
                                score += 500;
                                Console.WriteLine($"Congratulation, you've cleared Shorewoods out of its monsters. Legends will sing your name for decades to come !\n{you}");
                            }
                        } else {
                            Console.WriteLine("You died ...\n\nLets remember your character.");
                            Console.WriteLine(you);
                        }
                    }
                    Console.WriteLine(board);
                } if (isCommand) {
                    switch (command) {
                        case Command.Inventory:
                            Console.WriteLine($"Your inventory is :\n{you.Inventory}");
                            break;
                        case Command.Equip:
                            
                            break;
                    }
                }
            }
            Console.WriteLine($"Final score : {score + you.Inventory.Value}.");
        }
        private static bool TryParseDirection(string input, out Directions direction) {
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
        private static bool TryParseDirection(string input, out Command command) {
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
                default:
                    command = Command.Inventory;
                    break;
            }
            return parsed;
        }
    }
}
