using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinuteBattle.Logic
{
    public class Team
    {
        public TeamSetup _teamSetup;
        public Dictionary<int, DeployedArmyUnit> _deployedArmyUnitList = new Dictionary<int, DeployedArmyUnit>();
        public List<ArmyUnit> _armyUnitDeckList = new List<ArmyUnit>();
    }
}
