using ProjectGamebook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectGamebook.Services
{
    public interface ILocationProvider
    {
        bool ExistLocation(LocationId id);
        Location GetLocation(LocationId id);
        bool IsNavigationLegitimate(LocationId from, LocationId to, GameState state);
        List<Connection> GetConnectionsFrom(LocationId id);
        List<Connection> GetConnectionsTo(LocationId id);
    }
}
