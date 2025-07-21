using Microsoft.VisualBasic;

namespace MinuteBattle.Logic
{
    public class Card
    {
        public CardTypeEnum _cardType;
        public string _name;
        public string _description;
        public int _price;
        public int _level;
        public int _xpPerLevel;
        public int _rpToPlay;
        public Card(CardTypeEnum cardType, string name, string description, int price, int level, int xpPerLevel, int rpToPlay)
        {
            _cardType = cardType;
            _name = name;
            _description = description;
            _price = price;
            _level = level;
            _xpPerLevel = xpPerLevel;
            _rpToPlay = rpToPlay;
        }
        public Card Copy()
        {
            return new(_cardType, _name, _description, _price, _level, _xpPerLevel, _rpToPlay);
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
