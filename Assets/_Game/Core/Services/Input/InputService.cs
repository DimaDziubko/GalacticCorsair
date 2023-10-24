using UnityEngine;

namespace _Game.Core.Services.Input
{
    public abstract class InputService :  IInputService
    {
        protected const string HORIZONTAL = "Horizontal";
        protected const string VERTICAL = "Vertical";
        private string FIRE_BUTTON = "Jump";

        public abstract Vector2 Axis { get; }

        public bool IsFireButtonUp() => 
            SimpleInput.GetButtonUp(FIRE_BUTTON);

        protected Vector2 SimpleInputAxis() => 
            new Vector2(SimpleInput.GetAxis(HORIZONTAL), SimpleInput.GetAxis(VERTICAL));
    }
}
