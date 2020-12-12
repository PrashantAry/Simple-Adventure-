using System;
using System.Collections.Generic;
using static System.Console;

namespace SimpleAdventure
{
    class GameAction
    {
        public static void DrinkWater(Player player)
        {
            player.Heal(4);
            WriteLine("You drink some water from the river. You feels refreshed");

        }

        public static void TalkTOGuard(Player player)
        {
            if (player.Has(GameItems.SilverRing))
            {
                ShowsMessage($"Guard: I see you have defeated the goblin that has plagued us for years. You may enter our town, {player.Name} the Disconcerted!");
                player.MoveTo(Locale.Town);
            }
            else
            {
                ShowsMessage($"Guard: Hello there stranger. Sorry, but I cannot let a stranger enter the town without proof that you are trustworthy.");
            }
        }

        public static void FindForestItem(Player player)
        {
            player.Equip(GameItems.ChainMailArmor);
            player.Equip(GameItems.LongSword);
            ShowsMessage("You found some chain mail armor and long sword behind the tree!");
            Program.theWorld[player.Locale].RemoveMenuItems("Search the wood nearby");

        }

        public static void ShowsMessage(string message)
        {
            WriteLine();
            WriteLine(message);
            WriteLine();
            WriteLine("Please enter to continue...");
            ReadLine();
        }

        public static void PlayerAttacks(Player player)
        {
            GameAction.Fight(player, false);
        }
        public static void MonsterAttacks(Player player)
        {
            GameAction.Fight(player, true);
        }

        private static void Fight(Player player, bool monsterStarts = false)
        {
            Actor opponent = Program.theWorld[player.Locale].Resident;
            bool stillFighting = true;
            if (monsterStarts)
            {
                opponent.Attack(player);
                WriteLine($"The {opponent.Name} attacks you with their {opponent.Weapon}! You now have {player.Health} health points.");
                if (player.Health <= 0)
                {
                    stillFighting = false;
                    WriteLine("\nOh no! You are mortally wounded!  You are dead...");
                }
            }
            while (stillFighting)
            {
                player.Attack(opponent);
                Console.WriteLine($"You attack the {opponent.Name} with your {player.Weapon}! They now have {opponent.Health} health points.");
                if (opponent.Health == 0)
                {
                    Console.WriteLine("YOU ARE VICTORIOUS!!!!\n");
                    if (opponent is Monster)
                    {
                        Monster monster = (Monster)opponent;
                        List<Item> myLoot = monster.TakeLoot();
                        foreach (Item item in myLoot)
                        {
                            player.AddItemToBag(item);
                            Console.WriteLine($"You got a {item}!");
                        }
                    }
                    stillFighting = false;
                }
                if (stillFighting)
                {
                    opponent.Attack(player);
                    Console.WriteLine($"The {opponent.Name} attacks you with their {opponent.Weapon}!You now have {player.Health} health points.");
                    if (player.Health == 0)
                    {
                        Console.WriteLine("\nOh no! You are mortally wounded!  You are dead...");
                        stillFighting = false;
                    }
                }

                if (stillFighting)
                {
                    string name;
                    Console.WriteLine("Do you wish to continue fighting (y/n)?");
                    name = Console.ReadLine();
                    if(name == "n")
                    {
                        Console.WriteLine("You beat a hasty retreat and live to fight another day.");
                        stillFighting = false;
                    }
                    else if(name == "y")
                    {
                        stillFighting = true;
                    }
                    else
                    {
                         Console.WriteLine("Invalid entry.  Please enter a y and n.");
                          stillFighting = false;
                    }
                }
            }
        }
    }
}