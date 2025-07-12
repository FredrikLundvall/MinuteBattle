using System.Collections.Generic;

namespace MinuteBattle.Logic
{
    public static class Shop
    {
        public static List<Card> _inStock = [];
        public static int _resourcePointPrice = 10;
        static Shop()
        {
            Init();
        }
        public static void Init()
        {
            _inStock.Clear();
            _inStock.Add(new("Private", "A single shot rifleman", 20, 1, 25, 10));
        }
        public static bool BuyCard(Player player, string name)
        {
            Card card = _inStock.Find(it => it._name == name);
            if (card == null)
                return false;
            int sum = card._price;
            if (sum > player._gold)
                return false;
            player._gold -= sum;
            player._cardDeck.Add(card.Copy());
            return true;
        }
        public static bool BuyResourcePoints(Player player, int resourcePoints)
        {
            int sum = _resourcePointPrice * resourcePoints;
            if (sum > player._gold)
                return false;
            player._gold -= sum;
            player.AddReinforcementRp(resourcePoints);
            return true;
        }
        public static bool UpgradeCard(Player player, Card card)
        {
            int sum = card.XpToLevelUp();
            if (sum > player._xp)
                return false;
            player._xp -= sum;
            card.LevelUp();
            return true;
        }
    }
}
