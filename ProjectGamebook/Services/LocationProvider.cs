using Newtonsoft.Json.Linq;
using ProjectGamebook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;

namespace ProjectGamebook.Services
{
    public class LocationProvider : ILocationProvider
    {

        private static readonly Dictionary<int, Location> _locations = LoadLocations();

        /*            {0, new Location(new List<string> { "This is it. The Labyrinth Castle.", "Looks like some monster came to welcome you. Get rid of it before it's too late.", "(You have two ways of attacking. Regular attacks are sure to hit and deal regular amount of damage. You can also attempt a critical hit. Critical hit won't always succeed but deals great damage if it lands.)" }, "~/imgs/bgs/one_door_bg2.jpeg" ) },
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
            {30, new Location(new List<string> { "Well, you sure ain’t a pushover. But neither are they, so don’t let it get to your head, ‘kay?", "Also, you got a new weapon. Isn’t that great?" }, "~/imgs/bg1.jpg") },*/

        private readonly List<Connection> _map = LoadConnections();

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

        private static Dictionary<int, Location> LoadLocations()
        {
            string json = "locations.json";

            try
            {
                string jsonContent = File.ReadAllText(json);
                List<LocationData> locationDataList = JsonSerializer.Deserialize<List<LocationData>>(jsonContent);

                Dictionary<int, Location> locations = new Dictionary<int, Location>();

                foreach (LocationData locationData in locationDataList)
                {
                    Location location = new Location(locationData.Texts, locationData.ImageUrl);
                    locations.Add(locationData.Id, location);
                }

                return locations;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading locations: " + ex.Message);
                return new Dictionary<int, Location>();
            }
        }
        public class LocationData
        {
            public int Id { get; set; }
            public List<string> Texts { get; set; }
            public string ImageUrl { get; set; }
        }
        private static List<Connection> LoadConnections()
        {
            string json = "connections.json";

            try
            {
                string jsonContent = File.ReadAllText(json);
                List<ConnectionData> connectionDataList = JsonSerializer.Deserialize<List<ConnectionData>>(jsonContent);

                List<Connection> connections = new List<Connection>();

                foreach (ConnectionData connectionData in connectionDataList)
                {
                    Connection connection = new Connection(connectionData.From, connectionData.To, connectionData.Top, connectionData.Left, connectionData.Width, connectionData.Height, null, connectionData.SpecialPage);
                    connections.Add(connection);
                }

                return connections;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading connections: " + ex.Message);
                return new List<Connection>();
            }
        }
        public class ConnectionData
        {
            public int From { get; set; }
            public int To { get; set; }
            public string Top { get; set; }
            public string Left { get; set; }
            public string Width { get; set; }
            public string Height { get; set; }
            public bool? Condition { get; set; }
            public string SpecialPage { get; set; }

        }
    }
}
