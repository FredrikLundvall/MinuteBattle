using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace MinuteBattle.Graphics
{
    public class InputChecker
    {
        private Point _currentMousePosition;
        private Point _oldMousePosition;
        private int _currentScrollWheelValue;
        private int _oldScrollWheelValue;
        private InputConnector _inputConnector;
        private Dictionary<InputFunctionEnum, InputButtonStatus> _inputFunctionStatusList;
        private Dictionary<MouseButtonEnum, InputButtonStatus> _mouseButtonStatusList;

        public virtual void Initialize()
        {
            if (_inputConnector == null)
                _inputConnector = new InputConnector();
            if (_inputFunctionStatusList == null)
                _inputFunctionStatusList = new Dictionary<InputFunctionEnum, InputButtonStatus>();
            if (_mouseButtonStatusList == null)
                _mouseButtonStatusList = new Dictionary<MouseButtonEnum, InputButtonStatus>();
            foreach (int i in Enum.GetValues(typeof(InputFunctionEnum)))
            {
                //Adding all InputFunctionEnums here
                _inputFunctionStatusList.Add((InputFunctionEnum)i, new InputButtonStatus(false, new TimeSpan(0)));
            }
            foreach (int i in Enum.GetValues(typeof(MouseButtonEnum)))
            {
                //Adding all MouseButtonEnums here
                _mouseButtonStatusList.Add((MouseButtonEnum)i, new InputButtonStatus(false, new TimeSpan(0)));
            }
            _oldScrollWheelValue = _inputConnector.GetMouseState().ScrollWheelValue;
            _oldMousePosition = _inputConnector.GetMouseState().Position;
        }
        public void SetInputConnector(InputConnector inputConnector)
        {
            if (_inputConnector == null)
                _inputConnector = inputConnector;
        }
        public void SetInputFunctionStatusList(Dictionary<InputFunctionEnum, InputButtonStatus> inputFunctionStatusList)
        {
            if (_inputFunctionStatusList == null)
                _inputFunctionStatusList = inputFunctionStatusList;
        }
        public void SetMouseButtonStatusList(Dictionary<MouseButtonEnum, InputButtonStatus> mouseButtonStatusList)
        {
            if (_mouseButtonStatusList == null)
                _mouseButtonStatusList = mouseButtonStatusList;
        }
        public virtual void BeforeUpdate(GameTime gameTime, GameSettings gameSettings)
        {
            _currentScrollWheelValue = _inputConnector.GetMouseState().ScrollWheelValue;
            _currentMousePosition = _inputConnector.GetMouseState().Position;
        }
        public virtual void AfterUpdate(GameTime gameTime, GameSettings gameSettings)
        {
            _oldScrollWheelValue = _currentScrollWheelValue;
            _oldMousePosition = _currentMousePosition;
        }
        public virtual bool InputFunctionWasTriggered(InputFunctionEnum inputFunction, GameTime gameTime, GameSettings gameSettings, double triggerTimeoutSeconds)
        {
            //Check for buttons pressed while avoiding cascading event in next screen
            var isPressed = InputFunctionIsCurrentlyPressed(inputFunction, gameSettings);
            if (isPressed)
            {             
                if (_inputFunctionStatusList[inputFunction].ButtonIsHeldDown && (_inputFunctionStatusList[inputFunction].ButtonIsHeldDownAtTotalTime != gameTime.TotalGameTime) && (triggerTimeoutSeconds == 0 || (gameTime.TotalGameTime - _inputFunctionStatusList[inputFunction].ButtonIsHeldDownAtTotalTime).TotalSeconds < triggerTimeoutSeconds))
                {
                    isPressed = false;
                }
                else
                {
                    _inputFunctionStatusList[inputFunction] = new InputButtonStatus(true, gameTime.TotalGameTime);
                }
            }
            else
            {
                //The elapsed time isn't used, but preserving it anyway if some future use of last time pressed is needed
                _inputFunctionStatusList[inputFunction] = new InputButtonStatus(false, _inputFunctionStatusList[inputFunction].ButtonIsHeldDownAtTotalTime);
            }
            return isPressed;
        }
        public virtual bool InputFunctionIsCurrentlyPressed(InputFunctionEnum inputFunction, GameSettings gameSettings)
        {
            //Check for buttons being pressed down right now
            //Broken down for debugging
            var isPressed = IsAnyOfTheseKeboardKeysPressed(gameSettings.GetInputButtonsForFunction(inputFunction).KeyboardKeys);
            return isPressed;
        }
        public virtual bool MouseButtonWasTriggered(MouseButtonEnum mouseButton, GameTime gameTime, GameSettings gameSettings, double triggerTimeoutSeconds)
        {
            //Check for buttons pressed while avoiding cascading event in next screen
            var isPressed = MoueseButtonIsCurrentlyPressed(mouseButton, gameSettings);
            if (isPressed)
            {
                if (_mouseButtonStatusList[mouseButton].ButtonIsHeldDown && _mouseButtonStatusList[mouseButton].ButtonIsHeldDownAtTotalTime != gameTime.TotalGameTime && (triggerTimeoutSeconds == 0 || (gameTime.TotalGameTime - _mouseButtonStatusList[mouseButton].ButtonIsHeldDownAtTotalTime).TotalSeconds < triggerTimeoutSeconds))
                {
                    isPressed = false;
                }
                else
                {
                    _mouseButtonStatusList[mouseButton] = new InputButtonStatus(true, gameTime.TotalGameTime);
                }
            }
            else
            {
                //The elapsed time isn't used, but preserving it anyway if some future use of last time pressed is needed
                _mouseButtonStatusList[mouseButton] = new InputButtonStatus(false, _mouseButtonStatusList[mouseButton].ButtonIsHeldDownAtTotalTime);
            }
            return isPressed;
        }
        public virtual bool MoueseButtonIsCurrentlyPressed(MouseButtonEnum mouseButton, GameSettings gameSettings)
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
        public virtual bool AnyButtonIsCurrentlyPressed(GameSettings gameSettings)
        {
            return _inputConnector.GetKeyboardState().GetPressedKeys().GetLength(0) > 0 ||
                MoueseButtonIsCurrentlyPressed(MouseButtonEnum.LeftButton , gameSettings) ||
                MoueseButtonIsCurrentlyPressed(MouseButtonEnum.RightButton, gameSettings);
        }
        public virtual bool HasMouseMoved(GameTime gameTime, GameSettings gameSettings)
        {
            return _oldMousePosition != _currentMousePosition;
        }
        public virtual bool MouseIsCurrentlyOverArea(Rectangle buttonArea, IResolution resolution)
        {
            Point mouseScreenPosition = _currentMousePosition;
            Vector2 mousePosition = resolution.ScreenToGameCoord(new Vector2(mouseScreenPosition.X, mouseScreenPosition.Y));
            return buttonArea.Contains(mousePosition);
        }
        protected bool IsAnyOfTheseKeboardKeysPressed(Keys[] keyboardKeys)
        {
            foreach (Keys key in keyboardKeys)
            {
                if (_inputConnector.GetKeyboardState().IsKeyDown(key))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
