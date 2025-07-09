using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace MinuteBattle.Graphics
{
    public class GameSettings
    {
        protected bool _graphicsChanged = false;
        protected bool _fullscreen = true;
        protected Dictionary<InputFunctionEnum, InputButtonSetting> _inputButtonsForFunction = new Dictionary<InputFunctionEnum, InputButtonSetting>()
        {
            {InputFunctionEnum.PrimarySelect, new InputButtonSetting { KeyboardKeys = new Keys[] { Keys.Enter, Keys.E } } },
            {InputFunctionEnum.AlternateSelect, new InputButtonSetting { KeyboardKeys = new Keys[] { Keys.Space, Keys.F } } },
            {InputFunctionEnum.GoBack, new InputButtonSetting { KeyboardKeys = new Keys[] { Keys.Escape } } },
            {InputFunctionEnum.Pause, new InputButtonSetting { KeyboardKeys = new Keys[] { Keys.Escape } } }
        };
        public void SetFullscreen(bool fullscreen)
        {
            _graphicsChanged = _graphicsChanged || fullscreen != _fullscreen;
            _fullscreen = fullscreen;
        }
        public bool GetFullscreen()
        {
            return _fullscreen;
        }
        public bool IsGraphicsChanged()
        {
            return _graphicsChanged;
        }
        public void GraphicsChangeApplied()
        {
            _graphicsChanged = false;
        }
        public void SetInputButtonsForFunction(InputFunctionEnum inputFunction, InputButtonSetting inputButtonsForSelect)
        {
            _inputButtonsForFunction[inputFunction] = inputButtonsForSelect;
        }
        public InputButtonSetting GetInputButtonsForFunction(InputFunctionEnum inputFunction)
        {
            return _inputButtonsForFunction[inputFunction];
        }
    }
}
