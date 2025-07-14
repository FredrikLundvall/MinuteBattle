using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MinuteBattle.Graphics
{
    public static class Globals
    {
        public static GraphicsDeviceManager _graphics;
        public static SpriteBatch StaticSpriteBatch;
        public static IResolution StaticResolution = new Resolution();
        private static TimeSpan _lastGarbageCollection = new();
        // create 1x1 texture for line drawing
        private static Texture2D _line = null;

        public static void DrawRectangle(Rectangle rect, Color lineColor)
        {
            if(rect == Rectangle.Empty)
            {
                return; // No rectangle to draw
            }
            DrawLine(new Vector2(rect.X, rect.Y), new Vector2(rect.X + rect.Width, rect.Y), lineColor);
            DrawLine(new Vector2(rect.X + rect.Width, rect.Y), new Vector2(rect.X + rect.Width, rect.Y + rect.Height), lineColor);
            DrawLine(new Vector2(rect.X + rect.Width, rect.Y + rect.Height), new Vector2(rect.X, rect.Y + rect.Height), lineColor);
            DrawLine(new Vector2(rect.X, rect.Y + rect.Height), new Vector2(rect.X, rect.Y), lineColor);
        }

        public static void DrawLine(Vector2 start, Vector2 end, Color lineColor)
        {
            Vector2 edge = end - start;
            // calculate angle to rotate line
            float angle = (float)Math.Atan2(edge.Y, edge.X);


            StaticSpriteBatch.Draw(_line,
                new Rectangle(// rectangle defines shape of line and position of start of line
                    (int)start.X,
                    (int)start.Y,
                    (int)edge.Length(), //sb will strech the texture to fill this rectangle
                    1), //width of line, change this to make thicker line
                null,
                lineColor, //colour of line
                angle,     //angle of line (calulated above)
                new Vector2(0, 0), // point in line about which to rotate
                SpriteEffects.None,
                0);

        }
        public static void Initialize(GraphicsDeviceManager graphicsDeviceManager)
        {
            _graphics = graphicsDeviceManager;
            _line = new Texture2D(_graphics.GraphicsDevice, 1, 1);
            _line.SetData(new[] { Color.White }); // set the texture to be a 1x1 white pixel
        }
        public static void SetResolution()
        {
            // Change the resolution to match your current desktop
            _graphics.IsFullScreen = true;
            _graphics.PreferredBackBufferWidth = _graphics.GraphicsDevice.Adapter.CurrentDisplayMode.Width;
            _graphics.PreferredBackBufferHeight = _graphics.GraphicsDevice.Adapter.CurrentDisplayMode.Height;
            _graphics.ApplyChanges();
        }
        public static void TryForcingGarbageCollect()
        {
            //Suggest a garbage collection 
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
        public static void GarbageCollect(GameTime gameTime)
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
