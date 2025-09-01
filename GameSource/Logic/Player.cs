using System;
using System.Collections.Generic;

namespace MinuteBattle.Logic
{
    public class Player
    {
        public List<Card> _cardDeck = [];
        public List<Card> _cardInBattle = [];
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
            Player hero = new("Swedish army", 0, 100, 0, 10);
            //hero._cardDeck.Add(new Card(CardTypeEnum.HeroMeleeCard, "Pikeman", "", 100, 3, 10, 10));
            hero._cardDeck.Add(new Card(CardTypeEnum.HeroMelee, "Pikeman LVL 1", "", 100, 3, 10, 10));
            hero._cardDeck.Add(new Card(CardTypeEnum.HeroProjectile, "Musketeer LVL 1", "", 100, 1, 10, 10));
            hero._cardDeck.Add(new Card(CardTypeEnum.HeroArtillery, "Artillery LVL 2", "", 100, 1, 10, 10));
            return hero;
        }
        public static Player CreateEnemy()
        {
            Player enemy = new("Imperial-Catholic League army", 0, 100, 0, 10);
            enemy._cardDeck.Add(new Card(CardTypeEnum.EnemyMelee, "Pikeman LVL 2", "", 100, 3, 10, 10));
            enemy._cardDeck.Add(new Card(CardTypeEnum.EnemyProjectile, "Musketeer LVL 2", "", 100, 1, 10, 10));
            enemy._cardDeck.Add(new Card(CardTypeEnum.EnemyArtillery, "Artillery LVL 3", "", 100, 1, 10, 10));
            return enemy;
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
        public void AddReinforcementRp(int rp)
        {
            _reinforcementRp += rp;
        }
        public int GetBaseRp()
        {
            return _baseRp;
        }
        public void AddBaseRp(int rp)
        {
            _baseRp += rp;
        }
        public void MoveRpFromReinforcementToBase()
        {
            int movedRp = Math.Min(_rpSpeed, _reinforcementRp);
            AddReinforcementRp(-movedRp);
            AddBaseRp(movedRp);
        }
        public bool PlayCard(string name)
        {
            Card card = GetCard(name);
            if (card == null)
                return false;
            if (card._rpToPlay > _baseRp)
                return false;
            AddBaseRp(-card._rpToPlay);
            _cardInBattle.Add(card.Copy());
            return true;
        }
    }
}
