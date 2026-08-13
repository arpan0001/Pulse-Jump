using UnityEngine;

namespace PulseJump.Player
{
    public class PlayerInput : MonoBehaviour
    {
        [SerializeField]
        private PulseController pulseController;


        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                pulseController.TryPulse();
            }


            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began)
                {
                    pulseController.TryPulse();
                }
            }
        }
    }
}