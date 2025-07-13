using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MinuteBattle.Graphics;
using MinuteBattle.Logic;
using System;

namespace MinuteBattle
{
    public class GameLoop : Game
    {
        private GraphicsDeviceManager _graphics;
        TimeSpan _lastGarbageCollection;
        CardGame _game = new();
        Scene _currentScene = Scene.EmptyScene;

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
            Globals.Initialize(_graphics.GraphicsDevice);
            MouseChecker.Initialize();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            LoadGameAssets();
            Stage.AddStartScene(_game, _graphics.GraphicsDevice.Viewport);

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
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            GarbageCollect(gameTime);
            _currentScene = Stage.GetCurrentScene(_game);
            _currentScene.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            _currentScene.Draw(gameTime);

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
    }
}
