using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinuteBattle.Logic
{
    public class TeamDefinition
    {
        public TeamSide _teamSide;
        public int _id;
        public string _name;
        public TeamDefinition(TeamSide teamSide, int id, string name)
        {
            _teamSide = teamSide;
            _id = id; 
            _name = name;
        }
    }
}
