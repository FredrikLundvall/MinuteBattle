using MinuteBattle.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinuteBattleTests
{
    public class TestUtils
    {
        public const int SHOP_RESOURCE_POINT_PRICE = 10;
        public const string CARD_NAME = "Private";
        public const string CARD_DESCRIPTION = "A single shot rifleman";
        public const int CARD_PRICE = 20;
        public const int CARD_LEVEL = 1;
        public const int CARD_XP_PER_LEVEL = 25;
        public const int CARD_RP_TO_PLAY = 10;
        public const string PLAYER_NAME = "Brittish";
        public const int PLAYER_GOLD = 120;
        public const int PLAYER_RESOURCE_POINT = 0;
        public const int PLAYER_XP = 100;
        public const int PLAYER_RESORCE_POINT_SPEED = 10;
        public static Player CreatePlayer()
        {
            return new(PLAYER_NAME, PLAYER_GOLD, PLAYER_RESOURCE_POINT, PLAYER_XP, PLAYER_RESORCE_POINT_SPEED);
        }
        public static void InitShop()
        {
            Shop._inStock.Clear();
            Shop._inStock.Add(new(CARD_NAME, CARD_DESCRIPTION, CARD_PRICE, CARD_LEVEL, CARD_XP_PER_LEVEL, CARD_RP_TO_PLAY));
            Shop._resourcePointPrice = SHOP_RESOURCE_POINT_PRICE;
        }
    }
}
