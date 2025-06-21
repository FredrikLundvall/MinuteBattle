using MinuteBattle.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinuteBattleTests
{
    public class CampaignTests
    {
        [SetUp]
        public void Setup()
        {
        }
        [Test]
        public void WhenCampignIsCreated_BattleIsCreatded()
        {
            Campaign campaign = Campaign.CreateCampaign();
            Assert.That(campaign._battle, Is.Not.Null);
        }
        [Test]
        public void WhenCampignIsStarted_BattleIsStarted()
        {
            Campaign campaign = Campaign.CreateCampaign();
            Assert.That(campaign._state, Is.EqualTo(CampaignStateEnum.NotStarted));
            Assert.That(campaign._battle._state, Is.EqualTo(BattleStateEnum.NotStarted));
            campaign.NextStage();
            Assert.That(campaign._state, Is.EqualTo(CampaignStateEnum.InProgress));
            Assert.That(campaign._battle._state, Is.EqualTo(BattleStateEnum.Reinforcement));
        }
    }
}
