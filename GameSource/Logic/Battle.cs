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
        public Map _map = null;
        public BattleStateEnum _state = BattleStateEnum.NotStarted;
        public Battle(WinConditionEnum winCondition)
        {
            _winCondition = winCondition;
            _map = new();
        }
        public void NextStage()
        {
            if (_state == BattleStateEnum.NotStarted)
            {
                _state = BattleStateEnum.Reinforcement;
            }
            else if (_state == BattleStateEnum.Reinforcement)
            {
                _state = BattleStateEnum.CardPlay;
            }
        }
    }
}
