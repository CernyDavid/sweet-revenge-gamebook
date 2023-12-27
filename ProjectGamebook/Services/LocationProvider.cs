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

        private static readonly List<Location> _locations = new()
        {
            new Location( new List<string> { "This is a text", "Here's another text", "Another text, can you believe it?", "Finally the final text" }, true, new Monster(0, "Bachi", 50, 25, 50, "/imgs/bachi.jpg"), "~/imgs/bg1.jpg"),
            new Location( new List<string> {"Single text" }, false, null, "~/imgs/bg2.jpg"),
            new Location( new List<string> {"First", "Second" }, false, null, "~/imgs/bg3.jpg"),
            new Location( new List<string> { "This is a text", "Here's another text", "Another text, can you believe it?", "Finally the final text" }, true, new Monster(0, "Bachi", 50, 25, 50, "/imgs/bachi.jpg"), "~/imgs/bg1.jpg")

        };

        private readonly List<Connection> _map = new()
        {
            new Connection(0,1,"80px","0","120px","220px", IsFightFinished, null),
            new Connection(0,2,"80px","240px","120px","220px", IsFightFinished, null),
            new Connection(1,3,"80px","140px","80px","150px", null, null),
            new Connection(2,0,"120px","155px","55px","80px", null, "GameEnd")
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
            return (_locations.Count > id);
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
