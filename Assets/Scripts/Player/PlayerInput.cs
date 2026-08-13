using UnityEngine;

namespace PulseJump.Player
{
    public class PlayerInput : MonoBehaviour
    {
        [SerializeField]
        private PulseController pulseController;


        private void Update()
        {
#if UNITY_EDITOR || UNITY_STANDALONE
            if (Input.GetMouseButtonDown(0))
            {
                Pulse();
            }
#endif


            if (Input.touchCount > 0)
            {
                Touch touch =
                    Input.GetTouch(0);


                if (touch.phase ==
                    TouchPhase.Began)
                {
                    Pulse();
                }
            }
        }


        private void Pulse()
        {
            if (pulseController == null)
                return;


            pulseController.TryPulse();
        }
    }
}