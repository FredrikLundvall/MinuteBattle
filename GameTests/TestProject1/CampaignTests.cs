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
        public void WhenCampignIsCreated_MoreThanOneBattleIsAdded()
        {
            Campaign campaign = Campaign.CreateCampaign();
            Assert.That(campaign._battleList, Is.Not.Null);
            Assert.That(campaign._battleList.Count, Is.AtLeast(2));
        }
    }
}
