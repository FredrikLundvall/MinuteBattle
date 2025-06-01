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
                    return new Puppet(
                        new TextureAnimation(new Vector2(40, 77), TextureEnum.BrittishSoldier, Vector2.Zero, 0), 
                        new FontAnimation(Vector2.Zero, FontEnum.BebasNeue_Regular_18, Vector2.Zero, "", Color.Transparent)
                        );
                case PuppetEnum.GermanPrivate:
                    return new Puppet(
                        new TextureAnimation(new Vector2(40, 77), TextureEnum.GermanSoldier, Vector2.Zero, 0), 
                        new FontAnimation(Vector2.Zero, FontEnum.BebasNeue_Regular_18, Vector2.Zero, "", Color.Transparent)
                        );
                case PuppetEnum.GermanMachineGun:
                    return new Puppet(
                        new TextureAnimation(new Vector2(40, 77), TextureEnum.GermanMachineGun, Vector2.Zero, 0), 
                        new FontAnimation(Vector2.Zero, FontEnum.BebasNeue_Regular_18, Vector2.Zero, "", Color.Transparent)
                        );
                default:
                    return null;
            }
        }
    }
}
