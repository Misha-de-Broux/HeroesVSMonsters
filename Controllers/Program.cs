using Models;
using static System.Formats.Asn1.AsnWriter;

namespace Controllers {
    internal class Program {
        public static void Main(string[] args) {
            Console.WriteLine("Hello, new adventurer!\nWhat is your name ?");
            string name = Console.ReadLine();
            Console.WriteLine($"{name} ? What a great name ! What race are you ? {Utils.ListRaces()} ?");
            Race race;
            bool parsed = Enum.TryParse(Console.ReadLine(), out race);
            while (!parsed) {
                Console.WriteLine("Sorry, the only available races are : " + Utils.ListRaces());
                parsed = Enum.TryParse(Console.ReadLine(), out race);
            }
            PlayableCharacter you = PlayableCharacter.CreateHero(race, name);
            Console.WriteLine($"Welcome to Shorewoods, here is your character sheet :\n{you}\n");
            if (CheckYesOrNo("Do you wanna play on a board ?")) {
                PlayOnBoard(you);
            } else {
                PlayOffBoard(you);
            }
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
        private static void PlayOnBoard(PlayableCharacter hero) {
            Board board = new Board(hero);
            Console.WriteLine(board);
            bool isAlive = true;
            int score = 0;
            while (isAlive) {
                Console.WriteLine("What direction do you wanna head North, East, West or South ?");
                string input = Console.ReadLine();
                Directions direcion;
                Command command;
                bool isDirection = Utils.TryParseDirection(input, out direcion);
                bool isCommand = Utils.TryParseCommand(input, out command);
                while (!isDirection && !isCommand) {
                    Console.WriteLine("Please enter a correct input.\nWhat direction do you wanna head North, East, West or South ?");
                    input = Console.ReadLine();
                    isDirection = Utils.TryParseDirection(input, out direcion);
                    isCommand = Utils.TryParseCommand(input, out command);
                }
                if (isDirection) {
                    Monster? monster = board.MoveHero(direcion);
                    if (monster is not null) {
                        Console.WriteLine($"\nA {monster.GetType().Name} attacks you !");
                        isAlive = hero.WinFight(monster);
                        if (isAlive) {
                            Inventory loot = monster.GetLoot();
                            hero.TakeLoot(loot);
                            Console.WriteLine($"You beat the monster ! You loot the following :\n{loot}");
                            if (board.isWon()) {
                                isAlive = false;
                                score += 500;
                                Console.WriteLine($"Congratulation, you've cleared Shorewoods out of its monsters. Legends will sing your name for decades to come !\n{hero}");
                            }
                        } else {
                            Console.WriteLine("You died ...\n\nLets remember your character.");
                            Console.WriteLine(hero);
                        }
                    }
                    Console.WriteLine(board);
                }
                if (isCommand) {
                    switch (command) {
                        case Command.Inventory:
                            Console.WriteLine($"Your inventory is :\n{hero.Inventory}");
                            break;
                        case Command.Character:
                            Console.WriteLine(hero);
                            break;
                        case Command.Map:
                            Console.WriteLine(board);
                            break;
                        case Command.Equip:
                            Console.WriteLine($"Your primary weapon is : {hero.PrimaryWeapon}");
                            if (hero.SecondaryWeapons.Length > 0)
                                Console.WriteLine($"Your secondary weapon is = {hero.SecondaryWeapons[0]}");
                            SwitchPrimaryWeapon(hero);
                            if (hero.PrimaryWeapon is null || hero.PrimaryWeapon.AllowsSecondary) {
                                SwitchSecondaryWeapon(hero);
                            }
                            break;
                    }
                }
            }
            Console.WriteLine($"Final score : {score + (hero.PrimaryWeapon is null ? 0 : hero.PrimaryWeapon.Value) + (hero.SecondaryWeapon is null ? 0 : hero.SecondaryWeapon.Value) + hero.Inventory.Value}.");
        }

        private static void SwitchPrimaryWeapon(PlayableCharacter hero) {
            Console.WriteLine("Available weapons : \n\t0 : none");
            for (int i = 0; i < hero.Inventory.WeaponsCount; i++) {
                Console.WriteLine($"\t{i + 1} : {hero.Inventory[i]}");
            }
            if (CheckYesOrNo("Do you want to equip an new weapon ?")) {
                Console.WriteLine("Which one do you want to equip ?");
                int x = 0;
                bool parsed = int.TryParse(Console.ReadLine(), out x);
                while (!parsed || x < 0 || x > hero.Inventory.WeaponsCount) {
                    Console.WriteLine("Unvalid number, which one do you want to equip ?");
                    parsed = int.TryParse(Console.ReadLine(), out x);
                }
                Weapon newWeapon = null;
                if (x == 0) {
                    hero.EquipPrimary(null);
                } else {
                    newWeapon = hero.Inventory[x - 1];
                    hero.EquipPrimary(newWeapon);
                    hero.Inventory.Remove(newWeapon);
                }
            }
        }

        private static void SwitchSecondaryWeapon(PlayableCharacter hero) {
            Console.WriteLine("Available seconcary weapons :\n\t0 : none");
            List<Weapon> availableSecondaries = new List<Weapon>();
            for (int i = 0; i < hero.Inventory.WeaponsCount; i++) {
                if (hero.Inventory[i].IsSecondary) {
                    availableSecondaries.Add(hero.Inventory[i]);
                    Console.WriteLine($"\t{availableSecondaries.Count} : {hero.Inventory[i]}");
                }
            }
            if (CheckYesOrNo("Do you want to equip an new weapon ?")) {
                int x;
                bool parsed = int.TryParse(Console.ReadLine(), out x);
                while (!parsed || x < 0 || x > availableSecondaries.Count) {
                    Console.WriteLine("Unvalid number, which one do you want to equip ?");
                    parsed = int.TryParse(Console.ReadLine(), out x);
                }
                if (x == 0) {
                    hero.EquipSecondary(null);
                } else {
                    Weapon newWeapon = availableSecondaries[x - 1];
                    hero.EquipSecondary(newWeapon);
                    hero.Inventory.Remove(newWeapon);
                }
            }

        }

        private static bool CheckYesOrNo(string question) {
            Console.WriteLine(question);
            int yesOrNo = Utils.TryParseYesOrNo(Console.ReadLine());
            while (yesOrNo < 0) {
                Console.WriteLine($"Please enter a valid answer.\n{question} (Y)es or (N)o");
                yesOrNo = Utils.TryParseYesOrNo(Console.ReadLine());
            }
            return yesOrNo > 0;
        }
    }
}
