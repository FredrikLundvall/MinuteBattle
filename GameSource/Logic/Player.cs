using System;
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
        private int _reinforcementRp;
        private int _baseRp = 0;
        public int _rpSpeed;
        public int _xp;
        public Player(string name, int gold, int rp, int xp, int rpSpeed)
        {
            _name = name;
            _gold = gold;
            _reinforcementRp = rp;
            _xp = xp;
            _rpSpeed = rpSpeed;
        }
        public static Player CreateHero()
        {
            return new("Swedish army", 0, 100, 0, 10);
        }
        public static Player CreateEnemy()
        {
            return new("Imperial-Catholic League army", 0, 100, 0, 10);
        }
        public Card GetCard(String name)
        {
            return _cardDeck.Find(it => it._name == name);
        }
        public int GetTotalRp()
        {
            return _reinforcementRp + _baseRp;
        }
        public int GetReinforcementRp()
        {
            return _reinforcementRp;
        }
        public int AddReinforcementRp(int rp)
        {
            return _reinforcementRp += rp;
        }
        public int GetBaseRp()
        {
            return _baseRp;
        }
        public int AddBaseRp(int rp)
        {
            return _baseRp += rp;
        }
        public void MoveRpFromReinforcementToBase()
        {
            int movedRp = Math.Min(_rpSpeed, _reinforcementRp);
            AddReinforcementRp(-movedRp);
            AddBaseRp(movedRp);
        }
    }
}
