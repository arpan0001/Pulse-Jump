using UnityEngine;

namespace PulseJump.Player
{
    public class SphereRollVisual : MonoBehaviour
    {
        [SerializeField]
        private float rollSpeed = 360f;


        private void Update()
        {
            // Track moves toward the player along Z,
            // so the ball rolls around its local X axis.
            transform.Rotate( Vector3.right, rollSpeed * Time.deltaTime,  Space.Self);
        }
    }
}