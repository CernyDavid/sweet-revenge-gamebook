using Newtonsoft.Json.Linq;
using ProjectGamebook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectGamebook.Services
{
    public class LocationProvider : ILocationProvider
    {

        private static readonly Dictionary<int, Location> _locations = new()
        {
            {0, new Location(new List<string> { "This is it. The Labyrinth Castle.", "Looks like some monster came to welcome you. Get rid of it before it's too late.", "(You have two ways of attacking. Regular attacks are sure to hit and deal regular amount of damage. You can also attempt a critical hit. Critical hit won't always succeed but deals great damage if it lands.)" }, "~/imgs/bgs/one_door_bg2.jpeg" ) },
            {1, new Location(new List<string> { "You may be wondering, why is it called the Labyrinth Castle? Well, you see, this place is like a labyrinth.", "You have two paths ahead of you. There is no map available, so just trust your gut and choose. Which way will you go?" }, "~/imgs/bgs/two_door_bg1.jpeg") },
            {2, new Location(new List<string> { "What is the best way to kill monsters made of sweets? Using something salty, of course.", "Luckily for you, you found a very salty and very deadly weapon. Pick it up and continue." }, "~/imgs/bgs/one_door_bg1.jpeg") },
            {3, new Location(new List<string> { "Oh, a monster. You know what to do." }, "~/imgs/bgs/one_door_bg2.jpeg" ) },
            {4, new Location(new List<string> { "Oh, a monster. You know what to do." }, "~/imgs/bgs/one_door_bg2.jpeg") },
            {5, new Location(new List<string> { "What is the best way to kill monsters made of sweets? Using something salty, of course.", "Luckily for you, you found a very salty and very deadly weapon. Pick it up and continue." }, "~/imgs/bgs/one_door_bg1.jpeg") },
            {6, new Location(new List<string> { "You found a shield.", "You know what shields are for, right? Surely you don’t expect me to explain basic stuff like that to you, right?" }, "~/imgs/bgs/one_door_bg1.jpeg") },
            {7, new Location(new List<string> { "Getting hit by the sweet monsters increases your diabetes level. Make sure it never fills up, because you sure don't want to die.", "Now, which door will you choose?" }, "~/imgs/bgs/two_door_bg1.jpeg") },
            {8, new Location(new List<string> { "Would you look at that! Food!", "Eating something salty will lower your diabetes level a bit. Try it out." }, "~/imgs/bgs/one_door_bg2.jpeg") },
            {9, new Location(new List<string> { "This fucker really thinks he stands a chance, huh?", "Show him who’s the boss here." }, "~/imgs/bgs/one_door_bg1.jpeg") },
            {10, new Location(new List<string> { "This fucker really thinks he stands a chance, huh?", "Show him who’s the boss here." }, "~/imgs/bgs/one_door_bg1.jpeg") },
            {11, new Location(new List<string> { "Would you look at that! Food!", "Eating something salty will lower your diabetes level a bit. Try it out." }, "~/imgs/bgs/one_door_bg2.jpeg") },
            {30, new Location(new List<string> { "Well, you sure ain’t a pushover. But neither are they, so don’t let it get to your head, ‘kay?", "Also, you got a new weapon. Isn’t that great?" }, "~/imgs/bg1.jpg") },
        };

        private readonly List<Connection> _map = new()
        {
            new Connection(0,1,"160px","140px","80px","150px", IsFightFinished, null),
            new Connection(1,2,"100px","0","120px","220px", null, null),
            new Connection(1,3,"100px","240px","120px","220px", null, null),
            new Connection(2,4,"160px","130px","100px","150px", null, null),
            new Connection(3,5,"160px","140px","80px","150px", IsFightFinished, null),
            new Connection(5,6,"160px","130px","100px","150px", null, null),
            new Connection(4,6,"160px","140px","80px","150px", null, null),
            new Connection(6,7,"160px","130px","100px","150px", null, null),
            new Connection(7,8,"100px","0","120px","220px", null, null),
            new Connection(7,9,"100px","240px","120px","220px", null, null),
            new Connection(9,11,"160px","130px","100px","150px", IsFightFinished, null),
            new Connection(8,10,"160px","140px","80px","150px", null, null),
            new Connection(10,30,"160px","130px","100px","150px", null, "ChocoBossFight"),
            new Connection(11,30,"160px","140px","80px","150px", null, "ChocoBossFight")

        };

        public static bool IsFightFinished(int id)
        {
            if (_locations[id].IsFight)
            {
                return false;
            }
            return true;
        }

        public bool ExistLocation(int id)
        {
            return (_locations.ContainsKey(id));
        }

        public List<Connection> GetConnectionsFrom(int id)
        {
            if (ExistLocation(id))
            {
                return _map.Where(c => c.From == id).ToList();
            }
            throw new LocationNotFoundException("Location not found.");
        }

        public List<Connection> GetConnectionsTo(int id)
        {
            if (ExistLocation(id))
            {
                return _map.Where(c => c.Target == id).ToList();
            }
            throw new LocationNotFoundException("Location not found.");
        }

        public Location GetLocation(int id)
        {
            if (ExistLocation(id))
            {
                return _locations[id];
            }
            throw new LocationNotFoundException("Location not found.");
        }

        public bool IsNavigationLegitimate(int? from, int to, GameState state)
        {
            if (from != null)
            {
                if (from == 666666)
                {
                    return true;
                }
                List<Connection> validConnections = GetConnectionsTo(to);
                foreach (var c in validConnections)
                {
                    if (c.From == from) return true;
                }
                throw new InvalidNavigationException("Invalid move detected.");
            }
            throw new InvalidNavigationException("Invalid move detected.");
        }
    }
}
