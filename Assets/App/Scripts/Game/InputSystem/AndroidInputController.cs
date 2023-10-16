using UnityEngine;

namespace App.Scripts.Game.InputSystem
{
    public class AndroidInputController : IInput
    {
        public bool GetPress()
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began)
                {
                    return true;
                }
            }
            return false;
        }

        public bool GetUp()
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Ended)
                {
                    return true;
                }
            }
            return false;
        }
    }
}