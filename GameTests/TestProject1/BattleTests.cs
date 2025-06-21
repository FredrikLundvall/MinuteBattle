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
            Battle battle = new(WinConditionEnum.EliminateAllEnemies);
            Assert.That(battle._winCondition, Is.EqualTo(WinConditionEnum.EliminateAllEnemies));
        }
        [Test]
        public void CreatingBattleWithWinConditionSurviveRounds()
        {
            //Creating a battle with win condition to survive for fifteen rounds
            Battle battle = new(WinConditionEnum.SurviveForFifteenRounds);
            Assert.That(battle._winCondition, Is.EqualTo(WinConditionEnum.SurviveForFifteenRounds));
        }
        [Test]
        public void CreatingBattle_AddsMap()
        {
            //Creating a battle also creates a map
            Battle battle = new(WinConditionEnum.EliminateAllEnemies);
            Assert.That(battle._map, Is.Not.Null);
        }
        [Test]
        public void WhenStarting_BattleIsInResourceStage()
        {
            Battle battle = new(WinConditionEnum.EliminateAllEnemies);
            Assert.That(battle._state, Is.EqualTo(BattleStateEnum.NotStarted));
            battle.NextStage();
            Assert.That(battle._state, Is.EqualTo(BattleStateEnum.Reinforcement));
        }
        [Test]
        public void WhenBattleLeavesResourceStage_BattleIsInCardPlayingStage()
        {
            Battle battle = new(WinConditionEnum.EliminateAllEnemies);
            battle.NextStage();
            battle.NextStage();
            Assert.That(battle._state, Is.EqualTo(BattleStateEnum.CardPlay));
        }
    }
}
