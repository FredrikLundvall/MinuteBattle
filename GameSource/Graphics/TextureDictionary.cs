using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

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

            SpriteFont font = content.Load<SpriteFont>("GUI/fonts/BebasNeue-Regular_18");
            FontDictionary.Add(FontEnum.BebasNeue_Regular_18, font);
        }

    }
}
