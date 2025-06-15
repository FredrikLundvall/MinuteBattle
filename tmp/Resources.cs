using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinuteBattle.Logic
{
    public class Resources
    {
        public float _reinforcements;
        public float _camp;
        public float _frontline;
        public float _movementSpeed;
        public Resources(float reinforcements, float camp, float frontline, float movementSpeed)
        {
            _reinforcements = reinforcements;
            _camp = camp;
            _frontline = frontline;
            _movementSpeed = movementSpeed;
        }
    }
}
