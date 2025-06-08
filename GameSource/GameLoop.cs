using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MinuteBattle.Graphics;
using System;

namespace MinuteBattle
{
    public class GameLoop : Game
    {
        private GraphicsDeviceManager _graphics;
        private float _rotationAngle;
        private Vector2 _spritePosition;
        int _brittishPrivate1 = 1;
        int _germanPrivate1 = 2;
        int _germanMachineGun1 = 3;
        TimeSpan _lastGarbageCollection;

        public GameLoop()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            if (GraphicsDevice == null)
            {
                _graphics.ApplyChanges();
            }

            // Change the resolution to match your current desktop
            _graphics.IsFullScreen = true;
            _graphics.PreferredBackBufferWidth = GraphicsDevice.Adapter.CurrentDisplayMode.Width;
            _graphics.PreferredBackBufferHeight = GraphicsDevice.Adapter.CurrentDisplayMode.Height;
            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            Texture2D spriteTexture;
            spriteTexture = Content.Load<Texture2D>("Brittish/brittish_soldier_128");
            TextureDictionary.Add(TextureEnum.BrittishSoldier, spriteTexture);

            spriteTexture = Content.Load<Texture2D>("German/german_soldier_128");
            TextureDictionary.Add(TextureEnum.GermanSoldier, spriteTexture);

            spriteTexture = Content.Load<Texture2D>("German/german_machine_gun_128");
            TextureDictionary.Add(TextureEnum.GermanMachineGun, spriteTexture);

            SpriteFont font = Content.Load<SpriteFont>("GUI/fonts/BebasNeue-Regular_18");
            FontDictionary.Add(FontEnum.BebasNeue_Regular_18, font);

            Globals.StaticSpriteBatch = new SpriteBatch(GraphicsDevice);


            // Get the viewport (window) dimensions
            var viewport = _graphics.GraphicsDevice.Viewport;
            // Set the position of the texture to be the center of the screen.
            _spritePosition.X = viewport.Width / 2;
            _spritePosition.Y = viewport.Height / 2;

            Scene.AddPuppet(_brittishPrivate1, PuppetEnum.BrittishPrivate, new Vector2(viewport.Width / 2, viewport.Height / 2), 0);
            Scene.AddPuppet(_germanPrivate1, PuppetEnum.GermanPrivate, new Vector2(viewport.Width / 3, viewport.Height / 3), MathHelper.Pi / 2);
            Scene.AddPuppet(_germanMachineGun1, PuppetEnum.GermanMachineGun, new Vector2(viewport.Width / 4, viewport.Height / 2 + viewport.Height / 4), MathHelper.Pi / 10);

            Scene.getPuppet(_brittishPrivate1).getFirstClip(ClipCategoryEnum.NameTag).SetText("Garreth");
            Scene.getPuppet(_germanPrivate1).getFirstClip(ClipCategoryEnum.NameTag).SetText("Heisenberg");
            Scene.getPuppet(_germanMachineGun1).getFirstClip(ClipCategoryEnum.NameTag).SetText("Eichmann");

            //Try to force a garbage collection 
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            GC.WaitForPendingFinalizers();
            _lastGarbageCollection = new TimeSpan();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if ((gameTime.TotalGameTime - _lastGarbageCollection).TotalSeconds > 11.0)
            {
                //Suggest a garbage collection 
                GC.Collect();
                GC.WaitForPendingFinalizers();
                _lastGarbageCollection = gameTime.TotalGameTime;
            }

            // The time since Update was called last.
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Simple roation logic to rotate the sprite in a clockwise direction over time
            _rotationAngle += elapsed;
            float circle = MathHelper.Pi * 2;
            _rotationAngle %= circle;

            Scene.getPuppet(_brittishPrivate1).getFirstClip(ClipCategoryEnum.BaseTexture).SetRotation(_rotationAngle);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            Scene.Draw(gameTime);
            base.Draw(gameTime);
        }
    }
}
