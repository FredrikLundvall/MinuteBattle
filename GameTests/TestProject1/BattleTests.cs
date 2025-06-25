using MinuteBattle.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinuteBattleTests
{
    public class BattleTests
    {
        [SetUp]
        public void Setup()
        {
        }
        [Test]
        public void CreatingBattleWithWinConditionEliminateAll()
        {
            //Creating a battle with win condition to eliminate all enemies
            Battle battle = new(new(), WinConditionEnum.EliminateAllEnemies);
            Assert.That(battle._winCondition, Is.EqualTo(WinConditionEnum.EliminateAllEnemies));
        }
        [Test]
        public void CreatingBattleWithWinConditionSurviveRounds()
        {
            //Creating a battle with win condition to survive for fifteen rounds
            Battle battle = new(new(), WinConditionEnum.SurviveForFifteenRounds);
            Assert.That(battle._winCondition, Is.EqualTo(WinConditionEnum.SurviveForFifteenRounds));
        }
        [Test]
        public void CreatingBattle_AddsMap()
        {
            //Creating a battle also creates a map
            Battle battle = new(new(), WinConditionEnum.SurviveForFifteenRounds);
            Assert.That(battle._map, Is.Not.Null);
        }
        [Test]
        public void WhenStarting_BattleIsInReinforcementStage()
        {
            Battle battle = new(new(), WinConditionEnum.SurviveForFifteenRounds);
            Assert.That(battle._state, Is.EqualTo(BattleStateEnum.NotStarted));
            battle.NextStage();
            Assert.That(battle._state, Is.EqualTo(BattleStateEnum.Reinforcement));
        }
        [Test]
        public void WhenBattleLeavesReinforcementStage_BattleIsInCardPlayingStage()
        {
            Battle battle = new(new(), WinConditionEnum.SurviveForFifteenRounds);
            battle.NextStage();
            battle.NextStage();
            Assert.That(battle._state, Is.EqualTo(BattleStateEnum.CardPlay));
        }
        [Test]
        public void WhenBattleLeavesCardPlayingStage_BattleIsInFightingStage()
        {
            Battle battle = new(new(), WinConditionEnum.SurviveForFifteenRounds);
            battle.NextStage();
            battle.NextStage();
            battle.NextStage();
            Assert.That(battle._state, Is.EqualTo(BattleStateEnum.Fighting));
        }
        [Test]
        public void WhenBattleLeavesInFightingStage_AndBattleIsNotOver_BattleIsInReinforcementStage()
        {
            Battle battle = new(new(), WinConditionEnum.SurviveForFifteenRounds);
            battle.NextStage();
            battle.NextStage();
            battle.NextStage();
            battle.NextStage();
            Assert.That(battle._state, Is.EqualTo(BattleStateEnum.Reinforcement));
        }
        [Test]
        public void WhenBattleLeavesInFightingStage_AndWinConditionIsMet_BattleIsWon()
        {
            Battle battle = new(new(), WinConditionEnum.SurviveForFifteenRounds);
            foreach (var _ in Enumerable.Range(0, 3 * 15))
            {
                battle.NextStage();
            }
            battle.NextStage();
            Assert.That(battle._state, Is.EqualTo(BattleStateEnum.Won));
        }
        [Test]
        public void WhenBattleLeavesInFightingStage_AndLooseConditionIsMet_BattleIsLost()
        {
            Game game = new();
            Battle battle = new(game, WinConditionEnum.SurviveForFifteenRounds);
            battle.NextStage();
            battle.NextStage();
            battle.NextStage();
            game._hero.AddReinforcementRp(-game._hero.GetReinforcementRp());
            game._hero.AddBaseRp(-game._hero.GetBaseRp());
            battle.NextStage();
            Assert.That(battle._state, Is.EqualTo(BattleStateEnum.Lost));
        }
        [Test]
        public void WhenBattleLeavesInFightingStage_AndBothWinAndLooseConditionIsMet_BattleIsLost()
        {
            Game game = new();
            Battle battle = new(game, WinConditionEnum.EliminateAllEnemies);
            battle.NextStage();
            battle.NextStage();
            battle.NextStage();
            game._hero.AddReinforcementRp(-game._hero.GetReinforcementRp());
            game._hero.AddBaseRp(-game._hero.GetBaseRp());
            battle.NextStage();
            Assert.That(battle._state, Is.EqualTo(BattleStateEnum.Lost));
        }
        [Test]
        public void WhenInReinforcementStage_ResourcesAreMovedToBase()
        {
            Game game = new();
            int rpAfterOneMove = game._hero.GetReinforcementRp() - game._hero._rpSpeed;
            Battle battle = new(game, WinConditionEnum.SurviveForFifteenRounds);
            battle.NextStage();
            Assert.That(battle._state, Is.EqualTo(BattleStateEnum.Reinforcement));
            Assert.That(battle._game._hero.GetReinforcementRp(), Is.EqualTo(rpAfterOneMove));
        }
        [Test]
        public void WhenInCardPlayingStageStage_PlayerCanPlayCard()
        {
            Game game = new();
            Battle battle = new(game, WinConditionEnum.SurviveForFifteenRounds);
            game._hero._gold = TestUtils.CARD_PRICE;
            Shop.BuyCard(game._hero, TestUtils.CARD_NAME);
            game._hero.AddBaseRp(TestUtils.CARD_RP_TO_PLAY);
            battle.NextStage();
            battle.NextStage();
            Assert.That(battle.HeroPlayCard(TestUtils.CARD_NAME), Is.True);
            Assert.That(game._hero._cardInBattle.First()._name, Is.EqualTo(TestUtils.CARD_NAME));
        }
        //[Test]
        //public void WhenInFightingStage_CardFightsOneRound()
        //{
        //    Game game = new();
        //    Battle battle = new(game, WinConditionEnum.SurviveForFifteenRounds);
        //    game._hero._gold = TestUtils.CARD_PRICE;
        //    Shop.BuyCard(game._hero, TestUtils.CARD_NAME);
        //    game._hero.AddBaseRp(TestUtils.CARD_RP_TO_PLAY);
        //    battle.NextStage();
        //    battle.NextStage();
        //    Assert.That(battle.HeroPlayCard(TestUtils.CARD_NAME), Is.True);
        //    Assert.That(game._hero._cardInBattle.First()._name, Is.EqualTo(TestUtils.CARD_NAME));
        //}
    }
}
