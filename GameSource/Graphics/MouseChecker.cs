using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MinuteBattle.Graphics
{
    public static class MouseChecker
    {
        internal static InputConnector _inputConnector;
        internal static IResolution _resolution;

        public static void Initialize()
        {
            if (_inputConnector == null)
                _inputConnector = new InputConnector();
            if (_resolution == null)
                _resolution = Globals.StaticResolution;
        }
        public static bool ButtonIsCurrentlyPressed(MouseButtonEnum mouseButton)
        {
            //Check for buttons being pressed down right now
            switch (mouseButton)
            {
                case MouseButtonEnum.LeftButton: return _inputConnector.GetMouseState().LeftButton == ButtonState.Pressed;
                case MouseButtonEnum.RightButton: return _inputConnector.GetMouseState().RightButton == ButtonState.Pressed;
                case MouseButtonEnum.MiddleButton: return _inputConnector.GetMouseState().MiddleButton == ButtonState.Pressed;
                case MouseButtonEnum.XButton1: return _inputConnector.GetMouseState().XButton1 == ButtonState.Pressed;
                case MouseButtonEnum.XButton2: return _inputConnector.GetMouseState().XButton2 == ButtonState.Pressed;
            }
            return false;
        }
        public static bool IsCurrentlyOverArea(Rectangle buttonArea)
        {
            Point mouseScreenPosition = _inputConnector.GetMouseState().Position;
            Vector2 mousePosition = _resolution.ScreenToGameCoord(new Vector2(mouseScreenPosition.X, mouseScreenPosition.Y));
            return buttonArea.Contains(mousePosition);
        }
    }
}
