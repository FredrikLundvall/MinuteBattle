using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using MinuteBattle.Graphics;
using MinuteBattle.Logic;

namespace MinuteBattle
{
    public class GameLoop : Game
    {
        CardGame _game = new();
        Scene _currentScene = Scene.EmptyScene;
        GraphicsDeviceManager _graphicsDeviceManager;

        public GameLoop()
        {
            _graphicsDeviceManager = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }
        protected override void Initialize()
        {
            if (GraphicsDevice == null)
            {
                Globals.GraphicsDeviceMan.ApplyChanges();
            }
            Globals.Initialize(_graphicsDeviceManager);
            Globals.SetResolution();
            MouseChecker.Initialize();
            base.Initialize();
        }
        protected override void LoadContent()
        {
            LoadGameAssets();
            Stage.AddStartScene(_game);
            Globals.TryForcingGarbageCollect();
        }
        private void LoadGameAssets()
        {
            TextureDictionary.LoadTextures(Content);
            Globals.StaticSpriteBatch = new SpriteBatch(GraphicsDevice);
            Globals._testClick = Content.Load<SoundEffect>("sound/click");
            Globals._testTransition = Content.Load<SoundEffect>("sound/transition");
            Globals._testSong = Content.Load<Song>("sound/suite");
        }
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            Globals.GarbageCollect(gameTime);
            _currentScene = Stage.GetCurrentScene(_game);
            _currentScene.Update(gameTime);
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            _currentScene.Draw(gameTime);
            base.Draw(gameTime);
        }
    }
}
