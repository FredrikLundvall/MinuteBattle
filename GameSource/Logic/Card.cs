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
        public string _name;
        public string _description;
        public int _price;
        public int _level;
        public int _xpPerLevel;
        public int _rpToPlay;
        public Card(string name, string description, int price, int level, int xpPerLevel, int rpToPlay)
        {
            _name = name;
            _description = description;
            _price = price;
            _level = level;
            _xpPerLevel = xpPerLevel;
            _rpToPlay = rpToPlay;
        }
        public Card Copy()
        {
            return new(_name, _description, _price, _level, _xpPerLevel, _rpToPlay);
        }
        public int XpToLevelUp()
        { 
            return _xpPerLevel; 
        }
        public void LevelUp()
        {
            ++_level;
        }
    }
}
