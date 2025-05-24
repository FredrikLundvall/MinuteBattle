using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MinuteBattle.Graphics;
using static System.Net.Mime.MediaTypeNames;

namespace MinuteBattle
{
    public class Game1 : Game
    {
        Texture2D spriteTexture;
        private GraphicsDeviceManager _graphics;
        protected Rectangle drawingRectangle;
        private float rotationAngle;
        private Vector2 spritePosition;
        private Vector2 spriteOrigin;
        SpriteFont font;
        Clip _clip;

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
            // TODO: use this.Content to load your game content here
            spriteTexture = Content.Load<Texture2D>("Brittish/brittish_soldier_128");
            TextureList.Add(TextureList.BRIT_SOLDIER, spriteTexture);
            Globals.StaticSpriteBatch = new SpriteBatch(GraphicsDevice);

            font = Content.Load<SpriteFont>("GUI/fonts/BebasNeue-Regular");
            // Get the viewport (window) dimensions
            var viewport = _graphics.GraphicsDevice.Viewport;

            // Set the sprite drawing area from the Viewport origin (0,0) to 80% the sprite scale width and 100% of the sprite scale height.
            drawingRectangle.X = viewport.X;
            drawingRectangle.Y = viewport.Y;
            drawingRectangle.Width = 32;
            drawingRectangle.Height = 32;

            // Set the Texture origin to be the center of the texture.
            spriteOrigin.X = 40; // spriteTexture.Width / 2;
            spriteOrigin.Y = 77; // spriteTexture.Height / 2;

            // Set the position of the texture to be the center of the screen.
            spritePosition.X = viewport.Width / 2;
            spritePosition.Y = viewport.Height / 2;

            _clip = new Clip(spritePosition, 0, new Animation(spriteOrigin, TextureList.BRIT_SOLDIER));
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            // The time since Update was called last.
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

            // TODO: Add your game logic here.
            // Simple roation logic to rotate the sprite in a clockwise direction over time
            rotationAngle += elapsed;
            float circle = MathHelper.Pi * 2;
            rotationAngle %= circle;

            _clip._rotation = rotationAngle;
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            Globals.StaticSpriteBatch.Begin();
            //_spriteBatch.Draw(soldierSprite, spritePosition, Color.White);
            //_spriteBatch.Draw(soldierSprite, drawingRectangle, Color.White);
            //_spriteBatch.Draw(spriteTexture, spritePosition, null, Color.White, rotationAngle, spriteOrigin, 0.5f, SpriteEffects.None, 0f);
            _clip.Draw(gameTime);

            // Finds the center of the string in coordinates inside the text rectangle
            var text = "Minute Battle";
            Vector2 textMiddlePoint = font.MeasureString(text) / 2;
            // Places text in center of the screen
            Vector2 position = new Vector2(Window.ClientBounds.Width / 2, Window.ClientBounds.Height / 4);
            Globals.StaticSpriteBatch.DrawString(font, text, position, Color.DarkOliveGreen, 0, textMiddlePoint, 1.0f, SpriteEffects.None, 0.5f);

            Globals.StaticSpriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
