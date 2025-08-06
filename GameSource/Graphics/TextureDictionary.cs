using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace MinuteBattle.Graphics
{
    public static class TextureDictionary
    {
        internal static Dictionary<TextureEnum, Texture2D> _textureList = new Dictionary<TextureEnum, Texture2D>();
        static TextureDictionary()
        {
            Add(TextureEnum.Empty, null);
        }
        public static void Add(TextureEnum index, Texture2D texture)
        {
            _textureList[index] = texture;
        }
        public static Texture2D Get(TextureEnum index)
        {
            return _textureList[index];
        }
        public static void LoadTextures(ContentManager content)
        {
            Texture2D spriteTexture = content.Load<Texture2D>("hero/melee");
            TextureDictionary.Add(TextureEnum.HeroMelee, spriteTexture);

            spriteTexture = content.Load<Texture2D>("hero/projectile");
            TextureDictionary.Add(TextureEnum.HeroProjectile, spriteTexture);

            spriteTexture = content.Load<Texture2D>("hero/artillery");
            TextureDictionary.Add(TextureEnum.HeroArtillery, spriteTexture);

            spriteTexture = content.Load<Texture2D>("enemy/melee");
            TextureDictionary.Add(TextureEnum.EnemyMelee, spriteTexture);

            spriteTexture = content.Load<Texture2D>("enemy/projectile");
            TextureDictionary.Add(TextureEnum.EnemyProjectile, spriteTexture);

            spriteTexture = content.Load<Texture2D>("enemy/artillery");
            TextureDictionary.Add(TextureEnum.EnemyArtillery, spriteTexture);

            spriteTexture = content.Load<Texture2D>("gui/button_160_52");
            TextureDictionary.Add(TextureEnum.Button, spriteTexture);

            SpriteFont font = content.Load<SpriteFont>("gui/fonts/BebasNeue-Regular_14");
            FontDictionary.Add(FontEnum.BebasNeue_Regular_14, font);

            font = content.Load<SpriteFont>("gui/fonts/BebasNeue-Regular_20");
            FontDictionary.Add(FontEnum.BebasNeue_Regular_20, font);

            font = content.Load<SpriteFont>("gui/fonts/JSL_Ancient_Small");
            FontDictionary.Add(FontEnum.JSL_Ancient_Small, font);

            font = content.Load<SpriteFont>("gui/fonts/JSL_Ancient_Medium");
            FontDictionary.Add(FontEnum.JSL_Ancient_Medium, font);

            spriteTexture = content.Load<Texture2D>("maps/great_plains_1600_900");
            TextureDictionary.Add(TextureEnum.MapGreatPlain, spriteTexture);

            spriteTexture = content.Load<Texture2D>("maps/Terrain/hill");
            TextureDictionary.Add(TextureEnum.TerrainHill, spriteTexture);

            spriteTexture = content.Load<Texture2D>("maps/Terrain/ditch");
            TextureDictionary.Add(TextureEnum.TerrainDitch, spriteTexture);

            spriteTexture = content.Load<Texture2D>("maps/Terrain/bush");
            TextureDictionary.Add(TextureEnum.TerrainBush, spriteTexture);
        }

    }
}
