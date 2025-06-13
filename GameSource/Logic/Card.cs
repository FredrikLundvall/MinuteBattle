using Microsoft.Xna.Framework;
using MinuteBattle.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinuteBattle.Logic
{
    public class Card
    {
        public int _id;
        public string _name;
        public string _description;
        public Attributes _attributes;
        public Card(int id, string name, string description, Attributes attributes)
        {
            _id = id;
            _name = name;
            _description = description;
            _attributes = attributes;
        }
    }
}
