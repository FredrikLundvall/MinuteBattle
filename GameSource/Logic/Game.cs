using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinuteBattle.Logic
{
    public class Game
    {
        public GameStateEnum _state = GameStateEnum.InProgress;
        public Player _hero;
        public Player _enemy;
        public Campaign _campaign;
        public Random _rnd;

        public Game() {
            _rnd = new Random(); //seed can be used to get same random for repeating the game
            _hero = Player.CreateHero();
            _enemy = Player.CreateEnemy();
            _campaign = Campaign.CreateCampaign(this);
        }
    }
}
