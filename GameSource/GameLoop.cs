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

            SetResolution();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            LoadGameAssets();

            //Try to force a garbage collection 
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            GC.WaitForPendingFinalizers();
            _lastGarbageCollection = new TimeSpan();
        }

        private void LoadGameAssets()
        {
            TextureDictionary.LoadTextures(Content);

            Globals.StaticSpriteBatch = new SpriteBatch(GraphicsDevice);

            // Get the viewport (window) dimensions
            Scene.InitScene(_graphics.GraphicsDevice.Viewport, _brittishPrivate1, _germanPrivate1, _germanMachineGun1);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            GarbageCollect(gameTime);

            //RotateSprite(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            Scene.Draw(gameTime);

            base.Draw(gameTime);
        }

        private void SetResolution()
        {
            // Change the resolution to match your current desktop
            _graphics.IsFullScreen = true;
            _graphics.PreferredBackBufferWidth = GraphicsDevice.Adapter.CurrentDisplayMode.Width;
            _graphics.PreferredBackBufferHeight = GraphicsDevice.Adapter.CurrentDisplayMode.Height;
            _graphics.ApplyChanges();
        }

        private void GarbageCollect(GameTime gameTime)
        {
            if ((gameTime.TotalGameTime - _lastGarbageCollection).TotalSeconds > 11.0)
            {
                //Suggest a garbage collection 
                GC.Collect();
                GC.WaitForPendingFinalizers();
                _lastGarbageCollection = gameTime.TotalGameTime;
            }
        }

        private void RotateSprite(GameTime gameTime)
        {
            // The time since Update was called last.
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Simple roation logic to rotate the sprite in a clockwise direction over time
            _rotationAngle += elapsed;
            float circle = MathHelper.Pi * 2;
            _rotationAngle %= circle;

            Scene.getPuppet(_brittishPrivate1).getFirstClip(ClipCategoryEnum.BaseTexture).SetRotation(_rotationAngle);
        }
    }
}
