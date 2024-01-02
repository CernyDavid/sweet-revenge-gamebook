﻿using Newtonsoft.Json.Linq;
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
            new Location( texts: new List<string> { "This is a text", "Here's another text", "Another text, can you believe it?", "Finally the final text" }, isFight: false, imageUrl: "~/imgs/bg1.jpg"),
            new Location( new List<string> {"Text", "New text" }, "~/imgs/bg2.jpg"),
            new Location( new List<string> {"First", "Second" }, "~/imgs/bg3.jpg"),
            new Location( new List<string> { "This is a text", "Here's another text", "Another text, can you believe it?", "Finally the final text" }, "~/imgs/bg2.jpg"),
            new Location( new List<string> { "Text" }, "~/imgs/bg3.jpg"),
            new Location( new List<string> { "Text" }, "~/imgs/bg3.jpg"),
            new Location( new List<string> { "Text" }, "~/imgs/bg3.jpg"),
            new Location( new List<string> { "Text" }, "~/imgs/bg3.jpg")

        };

        private readonly List<Connection> _map = new()
        {
            new Connection(0,1,"80px","0","120px","220px", IsFightFinished, null),
            new Connection(0,2,"80px","240px","120px","220px", IsFightFinished, null),
            new Connection(1,3,"80px","140px","80px","150px", null, null),
            new Connection(2,0,"120px","155px","55px","80px", null, "GameEnd"),
            new Connection(3,4,"80px","140px","80px","150px", null, null),
            new Connection(4,5,"80px","140px","80px","150px", null, null),
            new Connection(5,6,"80px","140px","80px","150px", null, null),
            new Connection(6,7,"80px","140px","80px","150px", null, null)
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
