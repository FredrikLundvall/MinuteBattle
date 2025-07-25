using Microsoft.Xna.Framework;
using System;

namespace MinuteBattle.Graphics
{
    public static class PuppetFactory
    {
        public static Puppet CreatePuppet(PuppetEnum puppetType, Vector2 position, float rotation, Action clickAction, Rectangle clickRectangle)
        {
            var puppet = new Puppet(position, rotation, clickAction, clickRectangle);
            switch (puppetType) {
                case PuppetEnum.HeroMelee:
                {
                    puppet.AddClip(new TextureAnimation(ClipCategoryEnum.BaseTexture, TextureEnum.HeroMelee, new Vector2(16, 16), Vector2.Zero, 0, 1.0f));
                    puppet.AddClip(new FontAnimation(ClipCategoryEnum.NameTag, FontEnum.BebasNeue_Regular_14, new Vector2(16, -16), Vector2.Zero, 0, false, "", Color.DarkOliveGreen));
                    break;
                }
                case PuppetEnum.EnemyMelee:
                {
                    puppet.AddClip(new TextureAnimation(ClipCategoryEnum.BaseTexture, TextureEnum.EnemyMelee, new Vector2(16, 16), Vector2.Zero, 0, 1.0f));
                    puppet.AddClip(new FontAnimation(ClipCategoryEnum.NameTag, FontEnum.BebasNeue_Regular_14, new Vector2(16, -16), Vector2.Zero, 0, false, "", Color.DarkOliveGreen));
                    break;
                }
                case PuppetEnum.EnemyProjectile:
                {
                    puppet.AddClip(new TextureAnimation(ClipCategoryEnum.BaseTexture, TextureEnum.EnemyProjectile, new Vector2(16, 16), Vector2.Zero, 0, 1.0f));
                    puppet.AddClip(new FontAnimation(ClipCategoryEnum.NameTag, FontEnum.BebasNeue_Regular_14, new Vector2(16, -16), Vector2.Zero, 0, false, "", Color.DarkOliveGreen));
                    break;
                }
                case PuppetEnum.StartButton:
                {
                    puppet.AddClip(new TextureAnimation(ClipCategoryEnum.BaseTexture, TextureEnum.Button, new Vector2(160, 52), Vector2.Zero, 0, 0.5f));
                    puppet.AddClip(new FontAnimation(ClipCategoryEnum.NameTag, FontEnum.BebasNeue_Regular_20, new Vector2(20, 12), Vector2.Zero, 0, false, "", Color.Black));
                    break;
                }
                case PuppetEnum.MapGreatPlain:
                {
                    puppet.AddClip(new TextureAnimation(ClipCategoryEnum.BaseTexture, TextureEnum.MapGreatPlain, Vector2.Zero, Vector2.Zero, 0, 1.0f));
                    break;
                }
                case PuppetEnum.TerrainDitch:
                {
                    puppet.AddClip(new TextureAnimation(ClipCategoryEnum.BaseTexture, TextureEnum.TerrainDitch, Vector2.Zero, Vector2.Zero, 0, 1.0f));
                    break;
                }
                case PuppetEnum.TerrainBush:
                {
                    puppet.AddClip(new TextureAnimation(ClipCategoryEnum.BaseTexture, TextureEnum.TerrainBush, Vector2.Zero, Vector2.Zero, 0, 1.0f));
                    break;
                }
                case PuppetEnum.TerrainHill:
                {
                    puppet.AddClip(new TextureAnimation(ClipCategoryEnum.BaseTexture, TextureEnum.TerrainHill, Vector2.Zero, Vector2.Zero, 0, 1.0f));
                    break;
                }
                default:
                    return null;
            }
            return puppet;
        }
    }
}
