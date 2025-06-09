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
        public static Puppet CreatePuppet(int puppetType, Vector2 position, float rotation)
        {
            var puppet = new Puppet(position, rotation);
            switch (puppetType) {
                case 0: //PuppetEnum.BrittishPrivate:
                {
                    puppet.AddClip(new TextureAnimation(ClipCategoryEnum.BaseTexture, TextureEnum.BrittishSoldier, new Vector2(40, 77), Vector2.Zero, 0));
                    puppet.AddClip(new FontAnimation(ClipCategoryEnum.NameTag, FontEnum.BebasNeue_Regular_18, new Vector2(40, -60), Vector2.Zero, 0, false, "", Color.DarkOliveGreen));
                    break;
                }
                case 1: //PuppetEnum.GermanPrivate:
                {
                    puppet.AddClip(new TextureAnimation(ClipCategoryEnum.BaseTexture, TextureEnum.GermanSoldier, new Vector2(40, 77), Vector2.Zero, 0));
                    puppet.AddClip(new FontAnimation(ClipCategoryEnum.NameTag, FontEnum.BebasNeue_Regular_18, new Vector2(40, -60), Vector2.Zero, 0, false, "", Color.DarkOliveGreen));
                    break;
                }
                case 2: // PuppetEnum.GermanMachineGun:
                {
                    puppet.AddClip(new TextureAnimation(ClipCategoryEnum.BaseTexture, TextureEnum.GermanMachineGun, new Vector2(40, 77), Vector2.Zero, 0));
                    puppet.AddClip(new FontAnimation(ClipCategoryEnum.NameTag, FontEnum.BebasNeue_Regular_18, new Vector2(40, -60), Vector2.Zero, 0, false, "", Color.DarkOliveGreen));
                    break;
                }
                default:
                    return null;
            }
            return puppet;
        }
    }
}
