﻿using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace ProjectGamebook.Models
{
    public class Shield : Item
    {
        public Shield(string name, string imageUrl, int blockChance, string equippedImageUrl, int id) : base(name, imageUrl, id)
        {
            BlockChance = blockChance;
            EquippedImageUrl = equippedImageUrl;
        }

        public int BlockChance { get; set; }
        public string EquippedImageUrl { get; set; }

        public override string ReturnItem()
        {
            return ("<div class=\"item\" id=\"item\">" +
                "<div class=\"item__tag\">" +
                "<p>" + Name + "</p>" +
                "<p>" + "Block Chance: " + BlockChance + "</p>" +
                "</div>" +
                "<img class=\"item__img\" src=\"" + ImageUrl + "\">" +
                "</div>");
        }
    }
}
