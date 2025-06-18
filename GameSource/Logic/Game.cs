using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinuteBattle.Logic
{
    public class Game
    {
        public GameStatusEnum _status = GameStatusEnum.InProgress;
        public Player _hero;
        public Player _enemy;
        public Campaign _campaign;
        public Game() {
            _hero = Player.CreateHero();
            _enemy = Player.CreateEnemy();
            _campaign = Campaign.CreateCampaign();
        }
    }
}
