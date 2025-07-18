﻿using MinuteBattle.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinuteBattleTests
{
    public class GameTests
    {
        [SetUp]
        public void Setup()
        {
        }
        [Test]
        public void WhenGameIsCreated_StatusIsStarted()
        {
            //User creates the game and the status is in progress
            CardGame game = new();
            Assert.That(game._state, Is.EqualTo(GameStateEnum.InProgress));
        }
        [Test]
        public void WhenGameIsCreated_PlayerEnemyAndCampaignIsCreated()
        {
            //User creates the game and a player, an enemy player and a campaign is created
            CardGame game = new();
            Assert.That(game._hero, Is.Not.Null);
            Assert.That(game._enemy, Is.Not.Null);
            Assert.That(game._campaign, Is.Not.Null);
        }
    }
}
