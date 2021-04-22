using Microsoft.Xna.Framework.Input;

namespace Core.Input
{
    public static class KeyboardController
    {
        private static KeyboardState _previousState;
        private static KeyboardState _currentState;

        static KeyboardController()
        {
            _previousState = Keyboard.GetState();
            _currentState = _previousState;
        }

        public static void Update()
        {
            _previousState = _currentState;
            _currentState = Keyboard.GetState();
        }

        public static bool IsKeyClicked(Keys key)
        {
            return _currentState.IsKeyDown(key) && !_previousState.IsKeyDown(key);
        }

        public static bool IsKeyDown(Keys key)
        {
            return _currentState.IsKeyDown(key);
        }
    }
}
