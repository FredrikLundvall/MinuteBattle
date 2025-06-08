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
        public static Dictionary<int, TeamDefinition> _teamDefinitionList = new Dictionary<int, TeamDefinition>();
        static public void AddTeamSide(int id, TeamSide teamSide)
        {
            _teamSideList.Add(id, teamSide);
        }
        static public TeamSide GetTeamSide(int id)
        {
            return _teamSideList[id];
        }
        static public void AddTeamDefinition(int id, TeamDefinition teamDefinition)
        {
            _teamDefinitionList.Add(id, teamDefinition);
        }
        static public TeamDefinition GetTeamDefinition(int id)
        {
            return _teamDefinitionList[id];
        }
    }
}
