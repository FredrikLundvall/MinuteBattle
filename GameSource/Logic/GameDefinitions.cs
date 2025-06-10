using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MinuteBattle.Logic
{
    public static class GameDefinitions
    {
        public static Dictionary<int, TeamSide> _teamSideList = new Dictionary<int, TeamSide>();
        public static Dictionary<int, TeamSetup> _teamSetupList = new Dictionary<int, TeamSetup>();
        static public void AddTeamSide(int id, TeamSide teamSide)
        {
            _teamSideList.Add(id, teamSide);
        }
        static public TeamSide GetTeamSide(int id)
        {
            return _teamSideList[id];
        }
        static public void AddTeamSetup(int id, TeamSetup teamDefinition)
        {
            _teamSetupList.Add(id, teamDefinition);
        }
        static public TeamSetup GetTeamSetup(int id)
        {
            return _teamSetupList[id];
        }
    }
}
