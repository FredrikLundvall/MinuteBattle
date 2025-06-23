using MinuteBattle.Logic;

namespace MinuteBattleTests;

public class ShopTests
{
    [SetUp]
    public void Setup()
    {
        TestUtils.InitShop();
    }
    [Test]
    public void CardShop_AddsCardToStock()
    {
        //There are cards in the CardShop
        Shop.Init();
        Assert.That(Shop._inStock.Count, Is.AtLeast(1));
    }
    [Test]
    public void PlayerBuysCard_GetsCopyAndPaysGold()
    {
        //A player buys a card and a copy is added to his deck and the price is subtracted from his gold
        Player player = TestUtils.CreatePlayer();
        Shop.BuyCard(player, TestUtils.CARD_NAME);
        Assert.That(player._cardDeck.Count, Is.EqualTo(1));
        Assert.That(player._cardDeck.First()._name, Is.EqualTo(TestUtils.CARD_NAME));
        Assert.That(player._gold, Is.EqualTo(TestUtils.PLAYER_GOLD - TestUtils.CARD_PRICE));
        //Make sure that players copy is not the same instance as the shops instance
        //By setting the price to zero and expecting the shop price to still be as before
        player._cardDeck.First()._price = 0;
        Shop.BuyCard(player, TestUtils.CARD_NAME);
        Assert.That(player._gold, Is.EqualTo(TestUtils.PLAYER_GOLD - (TestUtils.CARD_PRICE * 2)));
    }
    [Test]
    public void PlayerBuysCard_WhenTooLittleGold_DontGetCopy()
    {
        //A player buys a card when he can't afford it, no card is received
        Player player = TestUtils.CreatePlayer();
        player._gold = TestUtils.CARD_PRICE - 1;
        Assert.That(Shop.BuyCard(player, TestUtils.CARD_NAME), Is.False);
        Assert.That(player._cardDeck.Count, Is.EqualTo(0));
        Assert.That(player._gold, Is.EqualTo(TestUtils.CARD_PRICE - 1));
    }
    [Test]
    public void PlayerBuysResource_PaysGoldAndReceivesResource()
    {
        //A player buys resource points and it is added to his resources and the price is subtracted from his gold
        Player player = TestUtils.CreatePlayer();
        Shop.BuyResourcePoints(player, 7);
        Assert.That(player.GetReinforcementRp(), Is.EqualTo(TestUtils.PLAYER_RESOURCE_POINT + 7));
        Assert.That(player._gold, Is.EqualTo(TestUtils.PLAYER_GOLD - (TestUtils.SHOP_RESOURCE_POINT_PRICE * 7)));
    }
    [Test]
    public void PlayerBuysResource_WhenToLittleGold_DontGetResource()
    {
        //A player buys resource points when he can't afford it, no resource is received
        Player player = TestUtils.CreatePlayer();
        player._gold = TestUtils.SHOP_RESOURCE_POINT_PRICE - 1;
        Assert.That(Shop.BuyResourcePoints(player, 1), Is.False);
        Assert.That(player.GetReinforcementRp(), Is.EqualTo(TestUtils.PLAYER_RESOURCE_POINT));
        Assert.That(player._gold, Is.EqualTo(TestUtils.SHOP_RESOURCE_POINT_PRICE - 1));
    }
    [Test]
    public void PlayerUpgradesCard_PaysXpAndReceivesCardLevel()
    {
        //A player buys upgrade and the upgrade is made to the card and the xp is subtracted from his xp
        Player player = TestUtils.CreatePlayer();
        Shop.BuyCard(player, TestUtils.CARD_NAME);
        Card card = player.GetCard(TestUtils.CARD_NAME);
        Shop.UpgradeCard(player, card);
        Assert.That(player._cardDeck.First()._level, Is.EqualTo(TestUtils.CARD_LEVEL + 1));
        Assert.That(player._xp, Is.EqualTo(TestUtils.PLAYER_XP - card.XpToLevelUp()));
    }
    [Test]
    public void PlayerUpgradesCard_WhenTooLittleXp_DontGetLevel()
    {
        //A player buys upgrade when he don't have enough xp and don't get the level
        Player player = TestUtils.CreatePlayer();
        Shop.BuyCard(player, TestUtils.CARD_NAME);
        Card card = player.GetCard(TestUtils.CARD_NAME);
        player._xp = card.XpToLevelUp() - 1;
        Assert.That(Shop.UpgradeCard(player, card), Is.False);
        Assert.That(player._cardDeck.First()._level, Is.EqualTo(TestUtils.CARD_LEVEL));
        Assert.That(player._xp, Is.EqualTo(card.XpToLevelUp() - 1));
    }
}
