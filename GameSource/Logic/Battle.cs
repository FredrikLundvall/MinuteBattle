using MinuteBattle.Graphics;

namespace MinuteBattle.Logic
{
    public class Battle
    {
        public CardGame _game = null;
        public WinConditionEnum _winCondition = WinConditionEnum.EliminateAllEnemies;
        public Map _map = null;
        public BattleStateEnum _state = BattleStateEnum.NotStarted;
        public int _numberOfRounds = 0;
        public int _numberOfEnemies = 0;
        public Battle(CardGame game, WinConditionEnum winCondition)
        {
            _game = game;
            _winCondition = winCondition;
            // Rectangle(260, 30, 1400, 1020);
            //1920 1080
            _map = Map.CreateMap(1400, 1020, _game._rnd);
        }
        public void NextStage()
        {
            if (_state == BattleStateEnum.NotStarted)
            {
                _state = BattleStateEnum.Reinforcement;
                _game._hero.MoveRpFromReinforcementToBase();
                _game._enemy.MoveRpFromReinforcementToBase();
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
                _numberOfRounds += 1;
                if(isBattleLost())
                    _state = BattleStateEnum.Lost;
                else if (isBattleWon())
                    _state = BattleStateEnum.Won;
                else
                    _state = BattleStateEnum.Reinforcement;
            }
        }
        private bool isBattleLost()
        {
            return _game._hero.GetTotalRp() <= 0;
        }
        private bool isBattleWon()
        {
            if(_game._enemy.GetTotalRp() <= 0)
            {
                return true;
            }
            else if (_winCondition == WinConditionEnum.EliminateAllEnemies && _numberOfEnemies <= 0)
            {
                return true;
            }
            else if (_winCondition == WinConditionEnum.SurviveForFifteenRounds && _numberOfRounds >= 15)
            {
                return true;
            }
            return false;
        }
        public bool HeroPlayCard(string name)
        {
            if (_state != BattleStateEnum.CardPlay)
                return false;
            return _game._hero.PlayCard(name);
        }
        public bool EnemyPlayCard(string name)
        {
            if (_state != BattleStateEnum.CardPlay)
                return false;
            return _game._enemy.PlayCard(name);
        }
    }
}
