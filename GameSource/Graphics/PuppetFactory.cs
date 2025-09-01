using Microsoft.Xna.Framework;
using System;

namespace MinuteBattle.Graphics
{
    public static class PuppetFactory
    {
        public static Puppet CreatePuppet(PuppetEnum puppetType, Vector2 position, float rotation, Action<Puppet> clickAction, Rectangle clickRectangle)
        {
            var puppet = new Puppet(position, rotation, clickAction, clickRectangle);
            var darkBrown = new Color(70, 15, 8);
            switch (puppetType) {
                case PuppetEnum.HeroMelee:
                {
                    puppet.AddClip(new TextureAnimation(ClipCategoryEnum.BaseTexture, TextureEnum.HeroMelee, new Vector2(16, 16), Vector2.Zero, 0, 1.0f, Color.White));
                    puppet.AddClip(new FontAnimation(ClipCategoryEnum.NameTag, FontEnum.JSL_Ancient_Small, new Vector2(16, -16), Vector2.Zero, 0, false, "", darkBrown));
                    break;
                }
                case PuppetEnum.HeroProjectile:
                {
                    puppet.AddClip(new TextureAnimation(ClipCategoryEnum.BaseTexture, TextureEnum.HeroProjectile, new Vector2(16, 16), Vector2.Zero, 0, 1.0f, Color.White));
                    puppet.AddClip(new FontAnimation(ClipCategoryEnum.NameTag, FontEnum.JSL_Ancient_Small, new Vector2(16, -16), Vector2.Zero, 0, false, "", darkBrown));
                    break;
                }
                case PuppetEnum.HeroArtillery:
                {
                    puppet.AddClip(new TextureAnimation(ClipCategoryEnum.BaseTexture, TextureEnum.HeroArtillery, new Vector2(16, 16), Vector2.Zero, 0, 1.0f, Color.White));
                    puppet.AddClip(new FontAnimation(ClipCategoryEnum.NameTag, FontEnum.JSL_Ancient_Small, new Vector2(16, -16), Vector2.Zero, 0, false, "", darkBrown));
                    break;
                }
                case PuppetEnum.HeroMeleeCard:
                {
                    puppet.AddClip(new TextureAnimation(ClipCategoryEnum.BaseTexture, TextureEnum.BlankCard, new Vector2(32, 32), Vector2.Zero, 0, 1.0f, Color.White));
                    puppet.AddClip(new TextureAnimation(ClipCategoryEnum.BaseTexture, TextureEnum.HeroMelee, new Vector2(16, 16), Vector2.Zero, 0, 1.0f, Color.White));
                    puppet.AddClip(new FontAnimation(ClipCategoryEnum.NameTag, FontEnum.JSL_Ancient_Small, new Vector2(16, -16), Vector2.Zero, 0, false, "", darkBrown));
                    break;
                }
                case PuppetEnum.EnemyMelee:
                    {
                        puppet.AddClip(new TextureAnimation(ClipCategoryEnum.BaseTexture, TextureEnum.EnemyMelee, new Vector2(16, 16), Vector2.Zero, 0, 1.0f, Color.White));
                        puppet.AddClip(new FontAnimation(ClipCategoryEnum.NameTag, FontEnum.JSL_Ancient_Small, new Vector2(16, -16), Vector2.Zero, 0, false, "", darkBrown));
                        break;
                    }
                case PuppetEnum.EnemyProjectile:
                    {
                        puppet.AddClip(new TextureAnimation(ClipCategoryEnum.BaseTexture, TextureEnum.EnemyProjectile, new Vector2(16, 16), Vector2.Zero, 0, 1.0f, Color.White));
                        puppet.AddClip(new FontAnimation(ClipCategoryEnum.NameTag, FontEnum.JSL_Ancient_Small, new Vector2(16, -16), Vector2.Zero, 0, false, "", darkBrown));
                        break;
                    }
                case PuppetEnum.EnemyArtillery:
                    {
                        puppet.AddClip(new TextureAnimation(ClipCategoryEnum.BaseTexture, TextureEnum.EnemyArtillery, new Vector2(16, 16), Vector2.Zero, 0, 1.0f, Color.White));
                        puppet.AddClip(new FontAnimation(ClipCategoryEnum.NameTag, FontEnum.JSL_Ancient_Small, new Vector2(16, -16), Vector2.Zero, 0, false, "", darkBrown));
                        break;
                    }
                case PuppetEnum.Button:
                {
                    puppet.AddClip(new TextureAnimation(ClipCategoryEnum.BaseTexture, TextureEnum.Button, new Vector2(80, 26), Vector2.Zero, 0, 1.0f, Color.White));
                    puppet.AddClip(new FontAnimation(ClipCategoryEnum.NameTag, FontEnum.JSL_Ancient_Medium, new Vector2(20, 10), Vector2.Zero, 0, false, "", darkBrown));
                    break;
                }
                case PuppetEnum.PaperSheet:
                {
                    puppet.AddClip(new TextureAnimation(ClipCategoryEnum.BaseTexture, TextureEnum.PaperSheet, Vector2.Zero, Vector2.Zero, 0, 1.0f, Color.White));
                    puppet.AddClip(new TextureAnimation(ClipCategoryEnum.BaseTexture, TextureEnum.Stripe, Vector2.Zero, new Vector2(230, 130), 0, 1.0f, Color.White));
                    puppet.AddClip(new TextureAnimation(ClipCategoryEnum.BaseTexture, TextureEnum.Stripe, Vector2.Zero, new Vector2(1680, 130), 0, 1.0f, Color.White));
                    break;
                }
                case PuppetEnum.TerrainDitch:
                {
                    puppet.AddClip(new TextureAnimation(ClipCategoryEnum.BaseTexture, TextureEnum.TerrainDitch, Vector2.Zero, Vector2.Zero, 0, 1.0f, Color.White));
                    break;
                }
                case PuppetEnum.TerrainBush:
                {
                    puppet.AddClip(new TextureAnimation(ClipCategoryEnum.BaseTexture, TextureEnum.TerrainBush, Vector2.Zero, Vector2.Zero, 0, 1.0f, Color.White));
                    break;
                }
                case PuppetEnum.TerrainHill:
                {
                    puppet.AddClip(new TextureAnimation(ClipCategoryEnum.BaseTexture, TextureEnum.TerrainHill, Vector2.Zero, Vector2.Zero, 0, 1.0f, Color.White));
                    break;
                }
                default:
                    return null;
            }
            return puppet;
        }
    }
}
