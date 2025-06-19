using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinuteBattle.Logic
{
    public class Campaign
    {
        public List<Battle> _battleList = [];
        public Campaign() { }
        public static Campaign CreateCampaign()
        {
            Campaign campaign = new();
            campaign._battleList.Add(new(WinConditionEnum.EliminateAllEnemies));
            campaign._battleList.Add(new(WinConditionEnum.SurviveForFifteenRounds));
            return campaign;
        }
    }
}
