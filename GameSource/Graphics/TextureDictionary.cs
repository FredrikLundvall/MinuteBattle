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
            Texture2D spriteTexture = content.Load<Texture2D>("Brittish/brittish_soldier_128");
            TextureDictionary.Add(TextureEnum.BrittishSoldier, spriteTexture);

            spriteTexture = content.Load<Texture2D>("German/german_soldier_128");
            TextureDictionary.Add(TextureEnum.GermanSoldier, spriteTexture);

            spriteTexture = content.Load<Texture2D>("German/german_machine_gun_128");
            TextureDictionary.Add(TextureEnum.GermanMachineGun, spriteTexture);

            spriteTexture = content.Load<Texture2D>("GUI/button_320_104");
            TextureDictionary.Add(TextureEnum.Button, spriteTexture);

            SpriteFont font = content.Load<SpriteFont>("GUI/fonts/BebasNeue-Regular_18");
            FontDictionary.Add(FontEnum.BebasNeue_Regular_18, font);

            spriteTexture = content.Load<Texture2D>("Maps/great_plains_1600_900");
            TextureDictionary.Add(TextureEnum.MapGreatPlain, spriteTexture);

            spriteTexture = content.Load<Texture2D>("Maps/Terrain/hill_64");
            TextureDictionary.Add(TextureEnum.TerrainHill, spriteTexture);

            spriteTexture = content.Load<Texture2D>("Maps/Terrain/ditch_64");
            TextureDictionary.Add(TextureEnum.TerrainDitch, spriteTexture);

            spriteTexture = content.Load<Texture2D>("Maps/Terrain/bush_64");
            TextureDictionary.Add(TextureEnum.TerrainBush, spriteTexture);
        }

    }
}
