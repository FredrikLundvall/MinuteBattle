using MinuteBattle.Logic;

namespace MinuteBattleTests
{
    public class PlayerTests
    {
        [SetUp]
        public void Setup()
        {
        }
        [Test]
        public void PlayerBuysCardFromShop_GetsCopy()
        {
            //A player buys a card and a copy is added to his deck and the price is subtracted from his gold
            Player player = new("Brittish", 120, 0, 100);
            Shop.BuyCard(player, "Private");
            Assert.That(player._cardDeck.Count, Is.EqualTo(1));
            Assert.That(player._cardDeck.First()._name, Is.EqualTo("Private"));
            Assert.That(player._gold, Is.EqualTo(100));
            //Make sure that players copy is not the same instance as the shops instance
            //By setting the price to zero and expecting the shop price to still be 20
            player._cardDeck.First()._price = 0;
            Shop.BuyCard(player, "Private");
            Assert.That(player._gold, Is.EqualTo(80));
        }
        [Test]
        public void PlayerBuysResource_PaysGoldAndReceivesResource()
        {
            //A player buys resource points and it is added to his resources and the price is subtracted from his gold
            Player player = new("Brittish", 120, 0, 100);
            Shop._resourcePointPrice = 10;
            Shop.BuyResourcePoints(player, 10);
            Assert.That(player._resourcePoints, Is.EqualTo(10));
            Assert.That(player._gold, Is.EqualTo(20));
        }
        [Test]
        public void PlayerUpgradesCard_PaysXpAndReceivesCardLevel()
        {
            //A player buys upgrade and the upgrade is made to the card and the xp is subtracted from his xp
            Player player = new("Brittish", 120, 0, 100);
            Shop.BuyCard(player, "Private");
            Shop.UpgradeCard(player, player.GetCard("Private"));
            Assert.That(player._cardDeck.First()._level, Is.EqualTo(2));
            Assert.That(player._xp, Is.EqualTo(75));
        }
    }
}