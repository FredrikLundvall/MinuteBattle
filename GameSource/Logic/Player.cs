﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinuteBattle.Logic
{
    public class Player
    {
        public List<Card> _cardDeck = [];
        public string _name;
        public int _gold;
        public int _resourcePoints;
        public int _xp;
        public Player(string name, int gold, int resourcePoints, int xp)
        {
            _name = name;
            _gold = gold;
            _resourcePoints = resourcePoints;
            _xp = xp;
        }
        public static Player CreateHero()
        {
            return new("Swedish army", 0, 100, 0);
        }
        public static Player CreateEnemy()
        {
            return new("Imperial-Catholic League army", 0, 100, 0);
        }
        public Card GetCard(String name)
        {
            return _cardDeck.Find(it => it._name == name);
        }
    }
}
