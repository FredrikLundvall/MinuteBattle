using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinuteBattle.Logic
{
    public class Team
    {
        public Dictionary<int, Card> _cardDeckList = new Dictionary<int, Card>();
        public Dictionary<int, CardPlayed> _cardPlayedList = new Dictionary<int, CardPlayed>();
        public int _id;
        public string _name;
        public Resources _resourcesMax;
        public Resources _resourcesLevel;
        public float _gold;
        public float _xp;
        public Team(int id, string name, Resources resourcesMax, Resources resourcesLevel, float gold, float xp)
        {
            _id = id; 
            _name = name;
            _resourcesMax = resourcesMax;
            _resourcesLevel = resourcesLevel;
            _gold = gold;
            _xp = xp;
        }
        public void AddCardToDeck(int id, Card card)
        {
            _cardDeckList.Add(id, card);
        }
        public Card GetCardFromDeck(int id)
        {
            return _cardDeckList[id];
        }
    }
}
