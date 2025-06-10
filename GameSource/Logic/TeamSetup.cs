using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinuteBattle.Logic
{
    public class TeamSetup
    {
        public TeamSide _teamSide;
        public int _id;
        public string _name;
        public Resources _resourcesMax;
        public Resources _resourcesLevel;
        public float _gold;
        public float _xp;
        public TeamSetup(TeamSide teamSide, int id, string name, Resources resourcesMax, Resources resourcesLevel, float gold, float xp)
        {
            _teamSide = teamSide;
            _id = id; 
            _name = name;
            _resourcesMax = resourcesMax;
            _resourcesLevel = resourcesLevel;
            _gold = gold;
            _xp = xp;
        }
    }
}
