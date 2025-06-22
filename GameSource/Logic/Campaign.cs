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
                _state = CampaignStateEnum.Battle;
                if (_battle != null)
                    _battle.NextStage();
            }
            else if (_state == CampaignStateEnum.Battle)
            {
                if (_battle != null)
                {
                    _battle.NextStage();
                    if (_battle._state == BattleStateEnum.Lost || _battle._state == BattleStateEnum.Won)
                    {
                        _state = CampaignStateEnum.Achievement;
                    }
                }
            }
        }
    }
}
