using MinuteBattle.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinuteBattle.Logic
{
    public static class PuppetEnumExtension
    {
        public static TeamSideEnum GetTeamSide(this PuppetEnum puppet)
        {
            switch (puppet)
            {
                case PuppetEnum.BrittishPrivate:
                    return TeamSideEnum.SideOne;
                default:
                    return TeamSideEnum.SideTwo;
            }
        }
    }
}
