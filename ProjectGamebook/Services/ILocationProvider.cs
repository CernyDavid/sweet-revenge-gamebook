using ProjectGamebook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectGamebook.Services
{
    public interface ILocationProvider
    {
        bool ExistLocation(int id);
        Location GetLocation(int id);
        bool IsNavigationLegitimate(int? from, int to, GameState state);
        List<Connection> GetConnectionsFrom(int id);
        List<Connection> GetConnectionsTo(int id);
    }
}
