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
            //User creates the game and the status is in progress
            Battle battle = new(WinConditionEnum.EliminateAllEnemies);
            Assert.That(battle._winCondition, Is.EqualTo(WinConditionEnum.EliminateAllEnemies));
        }
        [Test]
        public void CreatingBattleWithWinConditionSurviveRounds()
        {
            //User creates the game and the status is in progress
            Battle battle = new(WinConditionEnum.SurviveForFifteenRounds);
            Assert.That(battle._winCondition, Is.EqualTo(WinConditionEnum.SurviveForFifteenRounds));
        }
    }
}
