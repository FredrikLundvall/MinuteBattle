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
        public int _numberOfRounds = 0;
        public int _numberOfEnemies = 0;
        public int _resourcePoints = 1;
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
            else if (_state == BattleStateEnum.CardPlay)
            {
                _state = BattleStateEnum.Fighting;
            }
            else if (_state == BattleStateEnum.Fighting)
            {
                if(isBattleLost())
                    _state = BattleStateEnum.Lost;
                else if (isBattleWon())
                    _state = BattleStateEnum.Won;
                else
                    _state = BattleStateEnum.Reinforcement;
                _numberOfRounds += 1;
            }
        }
        private bool isBattleLost()
        {
            return _resourcePoints <= 0;
        }
        private bool isBattleWon()
        {
            if (_winCondition == WinConditionEnum.EliminateAllEnemies && _numberOfEnemies <= 0)
            {
                return true;
            }
            else if (_winCondition == WinConditionEnum.SurviveForFifteenRounds && _numberOfRounds >= 15)
            {
                return true;
            }
            return false;
        }
    }
}
