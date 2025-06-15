using MinuteBattle.Logic;

namespace MinuteBattleTests;

public class ShopTests
{
    [SetUp]
    public void Setup()
    {
    }
    [Test]
    public void CardShop_SetupsCards()
    {
        //There are cards in the CardShop
        Assert.That(Shop._inStock.Count, Is.AtLeast(1));
    }
}
