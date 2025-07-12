using System;

namespace MinuteBattle.Logic
{
    public class CardGame
    {
        public GameStateEnum _state = GameStateEnum.InProgress;
        public Player _hero;
        public Player _enemy;
        public Campaign _campaign;
        public Random _rnd;

        public CardGame()
        {
            _rnd = new Random(); //seed can be used to get same random for repeating the game
            _hero = Player.CreateHero();
            _enemy = Player.CreateEnemy();
            _campaign = Campaign.CreateCampaign(this);
        }
        public void Start()
        {
            if (_campaign._state == CampaignStateEnum.NotStarted)
            {
                _campaign.NextStage();
            }
        }
    }
}
