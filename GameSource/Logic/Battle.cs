using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinuteBattle.Logic
{
    public class Battle
    {
        public WinConditionEnum _winCondition = WinConditionEnum.EliminateAllEnemies;
        public Battle(WinConditionEnum winCondition)
        {
            _winCondition = winCondition;
        }
    }
}
