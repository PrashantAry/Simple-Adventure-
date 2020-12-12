using System;
using System.Collections.Generic;
using LWTech.CSD228.TextMenus;

namespace SimpleAdventure
{
    enum Direction { North, South, East, West }

    enum Locale
    {
        Nowhere,
        Town,
        TownGates,
        Crossroads,
        Woods,
        River,
        Bridge
    }

    class Location
    {
        public Locale Id { get; private set; }
        public string Description { get; private set; }
        public Dictionary<Direction, Locale> Pathways { get; private set; }
        public Actor Resident {get; private set;}
        public List<TextMenuItem<Player>> MenuItems {get; private set;}

        public Action<Player> preaction  {get; private set;}
        public Location(Locale id, string description)
        {
            if (description == null)
                throw new ArgumentNullException("description cannot be null");
            if (description.Length == 0)
                throw new ArgumentException("description cannot be empty");

            this.Id = id;
            this.Description = description;
            this.Pathways = new Dictionary<Direction, Locale>();
            this.Resident = null;
            this.MenuItems = new List<TextMenuItem<Player>>();
            this.preaction = null;

        }

        public void AddPathway(Direction direction, Locale locale)
        {
            Pathways.Add(direction, locale);
        }

        public void AddResident(Actor resident)
        {
          Resident = resident;
        }

        public void AddMenuItems(TextMenuItem<Player> menuitem)
        {
            MenuItems.Add(menuitem);
        }

        public void RemoveMenuItems(string description)
        {
            foreach(TextMenuItem<Player> menuItem in MenuItems)
            {
                if(menuItem.Description.Equals(description))
                {
                    MenuItems.Remove(menuItem);
                    return;
                }
            }

        }

        public TextMenu<Player> GetMenu()
        {
            var menu = new TextMenu<Player>();
            foreach (Direction direction in Pathways.Keys)
            {
                menu.AddItem(new TextMenuItem<Player>($"Go {direction}",
                                    (p)=>{ p.MoveTo(Pathways[direction]); }));
            }
             if(Resident != null)
            {
                menu.AddItem(new TextMenuItem<Player>($"Fight {Resident.Name}",GameAction.PlayerAttacks));
            }

            foreach(TextMenuItem<Player> menuItem in MenuItems)
            {
                menu.AddItem(menuItem);
            }

            return menu;
        }

        public void  AddPreAction(Action<Player> preaction)
        {
             this.preaction = preaction;
        }

        public void RunPreAction(Player player)
        {

            if(preaction != null)
            {
                 preaction(player);
            }
        }

        public override string ToString()
        {
            string s = Description;
            if( Resident != null)
                s += $"\nA {Resident.Name} is standing nearby ";
            return s;
        }
    }
}