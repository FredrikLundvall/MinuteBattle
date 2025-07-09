using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinuteBattle.Graphics
{
    public struct InputButtonStatus
    {
        public bool ButtonIsHeldDown;
        public TimeSpan ButtonIsHeldDownAtTotalTime;

        public InputButtonStatus(bool buttonIsHeldDown, TimeSpan buttonIsHeldDownAtElapsedTime)
        {
            ButtonIsHeldDown = buttonIsHeldDown;
            ButtonIsHeldDownAtTotalTime = buttonIsHeldDownAtElapsedTime;
        }
    }
}
