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
            new Location { Texts = new List<string> {"a", "b" } }
        };

        private readonly List<Connection> _map = new()
        {
            new Connection { From = LocationId.First, Target = LocationId.Second}
        };
        public bool ExistLocation(LocationId id)
        {
            return (_locations.Count > (int)id);
        }

        public List<Connection> GetConnectionsFrom(LocationId id)
        {
            if (ExistLocation(id))
            {
                return _map.Where(c => c.From == id).ToList();
            }
            throw new Exception();
        }

        public List<Connection> GetConnectionsTo(LocationId id)
        {
            if (ExistLocation(id))
            {
                return _map.Where(c => c.Target == id).ToList();
            }
            throw new Exception();
        }

        public Location GetLocation(LocationId id)
        {
            if (ExistLocation(id))
            {
                return _locations[(int)id];
            }
            throw new Exception();
        }

        public bool IsNavigationLegitimate(LocationId from, LocationId to, GameState state)
        {
            throw new NotImplementedException();
        }
    }
}
