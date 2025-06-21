using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinuteBattle.Logic
{
    public class Campaign
    {
        public Battle _battle = null;
        public CampaignStateEnum _state = CampaignStateEnum.NotStarted;
        public Campaign() { }
        public static Campaign CreateCampaign()
        {
            Campaign campaign = new();
            campaign._battle = new(WinConditionEnum.EliminateAllEnemies);
            return campaign;
        }
        public void NextStage()
        {
            if (_state == CampaignStateEnum.NotStarted)
            {
                _state = CampaignStateEnum.InProgress;
            }
            if (_battle == null)
                return;
            _battle.NextStage();
        }
    }
}
