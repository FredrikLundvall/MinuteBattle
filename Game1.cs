using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MinuteBattle.Graphics;

namespace MinuteBattle
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private float _rotationAngle;
        private Vector2 _spritePosition;
        SpriteFont _font;
        Puppet _brittishPrivate;

        public Game1()
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
            Globals.StaticSpriteBatch = new SpriteBatch(GraphicsDevice);

            _font = Content.Load<SpriteFont>("GUI/fonts/BebasNeue-Regular");

            // Get the viewport (window) dimensions
            var viewport = _graphics.GraphicsDevice.Viewport;
            // Set the position of the texture to be the center of the screen.
            _spritePosition.X = viewport.Width / 2;
            _spritePosition.Y = viewport.Height / 2;

            _brittishPrivate = PuppetFactory.CreatePuppet(PuppetEnum.BrittishPrivate);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // The time since Update was called last.
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Simple roation logic to rotate the sprite in a clockwise direction over time
            _rotationAngle += elapsed;
            float circle = MathHelper.Pi * 2;
            _rotationAngle %= circle;

            _brittishPrivate._rotation = _rotationAngle;
            _brittishPrivate._position = _spritePosition;
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            Globals.StaticSpriteBatch.Begin();
            _brittishPrivate.Draw(gameTime);

            // Finds the center of the string in coordinates inside the text rectangle
            var text = "Minute Battle";
            Vector2 textMiddlePoint = _font.MeasureString(text) / 2;
            // Places text in center of the screen
            Vector2 position = new Vector2(Window.ClientBounds.Width / 2, Window.ClientBounds.Height / 4);
            Globals.StaticSpriteBatch.DrawString(_font, text, position, Color.DarkOliveGreen, 0, textMiddlePoint, 1.0f, SpriteEffects.None, 0.5f);

            Globals.StaticSpriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
