using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinuteBattle.Logic
{
    public class TeamSide
    {
        public Dictionary<int, Team> _teamList = new Dictionary<int, Team>();
        public int _id;
        public string _name;
        public TeamSide(int id, string name)
        {
            _id = id;
            _name = name;
        }
        public void AddTeam(int id, Team team)
        {
            _teamList.Add(id, team);
        }
        public Team GetTeam(int id)
        {
            return _teamList[id];
        }
    }
}
