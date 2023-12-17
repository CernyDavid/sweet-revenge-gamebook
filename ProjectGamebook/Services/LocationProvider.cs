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
        private readonly List<Location> _locations = new()
        { 
            new Location { Texts = new List<string> {"a", "b", "c", "d" }, ImageUrl="~/imgs/bg1.jpg"},
            new Location { Texts = new List<string> {"a" }, ImageUrl="~/imgs/bg2.jpg"},
            new Location { Texts = new List<string> {"a", "b" }, ImageUrl="~/imgs/bg3.jpg"}

        };

        private readonly List<Connection> _map = new()
        {
            new Connection { From = 0, Target = 1, Top="80px", Left="0", Width="120px", Height="220px"},
            new Connection { From = 0, Target = 2, Top="80px", Left="240px", Width="120px", Height="220px"}
        };
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
