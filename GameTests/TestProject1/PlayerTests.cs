using MinuteBattle.Logic;

namespace MinuteBattleTests
{
    public class PlayerTests
    {
        [SetUp]
        public void Setup()
        {
            TestUtils.InitShop();
        }
        [Test]
        public void PlayerBuysCard_CardIsInDeck()
        {
            //A player buys a card and a copy is added to his deck
            Player player = TestUtils.CreatePlayer();
            Shop.BuyCard(player, TestUtils.CARD_NAME);
            Assert.That(player.GetCard(TestUtils.CARD_NAME), Is.Not.Null);
            Assert.That(player._cardDeck.First()._name, Is.EqualTo(TestUtils.CARD_NAME));
        }
        [Test]
        public void PlayerPlaysCard_HavingBaseRpEnough_CardIsInBattle()
        {
            //A player plays a card and a copy is added to in-battle
            Player player = TestUtils.CreatePlayer();
            player.AddBaseRp(TestUtils.CARD_RP_TO_PLAY);
            Shop.BuyCard(player, TestUtils.CARD_NAME);
            Assert.That(player.GetCard(TestUtils.CARD_NAME), Is.Not.Null);
            Assert.That(player.PlayCard(TestUtils.CARD_NAME), Is.True);
            Assert.That(player._cardInBattle.First()._name, Is.EqualTo(TestUtils.CARD_NAME));
        }
    }
}