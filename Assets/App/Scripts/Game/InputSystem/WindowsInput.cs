using UnityEngine;

namespace App.Scripts.Game.InputSystem
{
    public class WindowsInput : IInput
    {
        public bool GetPress()
        {
            if (Input.GetMouseButtonDown(0))
            {
                return true;
            }

            return false;
        }

        public bool GetUp()
        {
            if (Input.GetMouseButtonUp(0))
            {
                return true;
            }

            return false;
        }
    }
}