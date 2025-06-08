using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinuteBattle.Logic
{
    public class Team
    {
        public TeamSideEnum _teamSide;
        public Resources _resources;
        public float _gold;
        public float _xp;
        public Dictionary<int, DeployedArmyUnit> _deployedArmyUnitList = new Dictionary<int, DeployedArmyUnit>();
        public List<ArmyUnit> _armyUnitDeckList = new List<ArmyUnit>();
    }
}
