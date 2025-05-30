using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinuteBattle.Graphics
{
    public static class PuppetFactory
    {
        public static Puppet CreatePuppet(PuppetEnum puppetType)
        {
            switch (puppetType) {
                case PuppetEnum.BrittishPrivate:
                    return new Puppet(new TextureAnimation(new Vector2(40, 77), TextureEnum.BrittishSoldier));
                case PuppetEnum.GermanPrivate:
                    return new Puppet(new TextureAnimation(new Vector2(40, 77), TextureEnum.GermanSoldier));
                case PuppetEnum.GermanMachineGun:
                    return new Puppet(new TextureAnimation(new Vector2(40, 77), TextureEnum.GermanMachineGun));
                default:
                    return null;
            }
        }
    }
}
