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
            new Location { Texts = new List<string> {"a", "b" }, ImageUrl="~/imgs/bg2.jpg"},
            new Location { Texts = new List<string> {"a", "b" }, ImageUrl="~/imgs/bg3.jpg"}

        };

        private readonly List<Connection> _map = new()
        {
            new Connection { From = LocationId.First, Target = LocationId.Second, Top="80px", Left="0", Width="120px", Height="220px"},
            new Connection { From = LocationId.First, Target = LocationId.Third, Top="80px", Left="240px", Width="120px", Height="220px"}
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
