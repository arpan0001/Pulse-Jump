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


        private void Awake()
        {
            // Always start disabled.
            _movementAuthorized = false;
            enabled = false;
        }


        private void OnEnable()
        {
            // If something else tries to enable
            // this component before the game starts,
            // immediately disable it again.

            if (!_movementAuthorized)
            {
                enabled = false;
            }
        }


        /// <summary>
        /// Called ONLY when the actual game starts.
        /// </summary>
        public void StartMovement()
        {
            _movementAuthorized = true;

            enabled = true;
        }


        /// <summary>
        /// Stops the world and removes movement permission.
        /// </summary>
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
            transform.Translate(
                Vector3.back *
                moveSpeed *
                Time.deltaTime,
                Space.World);
        }
    }
}