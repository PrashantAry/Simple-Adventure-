using System;
using static System.Console;
using System.Collections.Generic;
using LWTech.CSD228.TextMenus;

namespace SimpleAdventure
{
    class Program
    {

        public static Dictionary<Locale, Location> theWorld = new Dictionary<Locale, Location>();
        static void Main(string[] args)
        {
            string name = "";
             Console.WriteLine("Please enter the name?");
             name = Console.ReadLine();
              name += " the Malodorous"; 
             Console.WriteLine($"Welcome to Simple AdventureLand, {name}!");
            Location location = new Location(Locale.TownGates, "You are at the gates of a town. A guard is standing in front of you.");
            location.AddPathway(Direction.North, Locale.Crossroads);
            Actor townGuard = new Actor("TownGuard", Locale.TownGates, 1000);
             location.AddMenuItems(new TextMenuItem<Player>("Talk to the guard", GameAction.TalkTOGuard));
            townGuard.Equip(GameItems.GuardsArmor);
            townGuard.Equip(GameItems.GuardsPike);
            location.AddResident(townGuard);
            theWorld.Add(Locale.TownGates, location);

            location = new Location(Locale.Crossroads, "You are at a lonely 4-way crossroads. You cannot see what lies in each direction.");
            location.AddPathway(Direction.North, Locale.River);
            location.AddPathway(Direction.West, Locale.Woods);
            location.AddPathway(Direction.South, Locale.TownGates);
            location.AddPathway(Direction.East, Locale.Bridge);
            theWorld.Add(Locale.Crossroads, location);

            location = new Location(Locale.Woods, "You are in a dark forboding forest. Fallen trees block your way.");
            location.AddPathway(Direction.East, Locale.Crossroads);
            location.AddMenuItems(new TextMenuItem<Player>("Search the wood nearby", GameAction.FindForestItem));
            theWorld.Add(Locale.Woods, location);

            location = new Location(Locale.River, "You are at a swift-flowing, broad river that cannot be crossed.");
            location.AddPathway(Direction.South, Locale.Crossroads);
            location.AddMenuItems(new TextMenuItem<Player>("Drink some water", GameAction.DrinkWater));
            theWorld.Add(Locale.River, location);

            location = new Location(Locale.Bridge, "You come up to a bridge that appears to have been barricaded.");
            location.AddPathway(Direction.West, Locale.Crossroads);
            Monster goblin = new Monster("Gobline", Locale.Bridge, health: 20);
            location.AddPreAction(GameAction.MonsterAttacks);
            goblin.Equip(GameItems.LeatherArmor);
            goblin.Equip(GameItems.RustyAxe);
            goblin.AddLoot(GameItems.SilverRing);
            location.AddResident(goblin);
            theWorld.Add(Locale.Bridge, location);

            Player ourHero = new Player(name, health: 20);
            ourHero.Equip(GameItems.LeatherArmor);
            ourHero.Equip(GameItems.Dagger);
            ourHero.AddItemToBag(new Item("Rabbit's Foot"));
            ourHero.MoveTo(Locale.TownGates);

            while (ourHero.Health > 0 && ourHero.Locale != Locale.Town)
            {
                location = theWorld[ourHero.Locale];
                location.RunPreAction(ourHero);
                if (ourHero.Health <= 0)
                {
                    break;
                }
                TextMenu<Player> menu = location.GetMenu();

                // Display the Player's current stats and location
                WriteLine("\n-----------------------------------------------------");
                WriteLine(ourHero);
                WriteLine("-----------------------------------------------------");
                WriteLine(location);

                int i = menu.GetMenuChoiceFromUser() - 1;
                menu.Run(i, ourHero);
            }
            string name1 = ourHero.Health > 0  ? "amaging" : "sad";
            Console.WriteLine($"\n Thus ends the {name1} tale of {ourHero.Name}.");
            Console.WriteLine("\nTHE END...?\n");

        }
    }
}
