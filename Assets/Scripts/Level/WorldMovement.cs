using UnityEngine;

namespace PulseJump.Level
{
    public class WorldMovement : MonoBehaviour
    {
        [Header("Movement")]
        [SerializeField]
        private float moveSpeed = 5f;

        private bool _movementAuthorized = false;

        public float MoveSpeed => moveSpeed;

        [SerializeField]
        private PulseJump.VFX.SpeedLineVFX speedLineVFX;
        private void Awake()
        {
            _movementAuthorized = false;
            enabled = false;
        }

        // If something else tries to enable
        // this component before the game starts,
        // immediately disable it again.

        private void OnEnable()
        {
        
            if (!_movementAuthorized)
            {
                enabled = false;
            }
        }
      
        /// Called ONLY when the actual game starts.       
        public void StartMovement()
        {
            _movementAuthorized = true;

            enabled = true;
        }
       
        /// Stops the world and removes movement permission.
        
        public void StopMovement()
        {
            _movementAuthorized = false;

            enabled = false;
        }


        public void SetMoveSpeed(float speed)
        {
            moveSpeed = speed;
        }


        private void Update()
        {
            transform.Translate( Vector3.back * moveSpeed * Time.deltaTime,Space.World);
        }
    }
}