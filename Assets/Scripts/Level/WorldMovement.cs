using UnityEngine;

namespace PulseJump.Level
{
    public class WorldMovement : MonoBehaviour
    {
        [Header("Movement")]
        [SerializeField]
        private float moveSpeed = 5f;

        private bool _isMoving;


        public float MoveSpeed => moveSpeed;


        public void StartMovement()
        {
            _isMoving = true;
        }


        public void StopMovement()
        {
            _isMoving = false;
        }


        private void Update()
        {
            if (!_isMoving)
                return;

            transform.Translate( Vector3.down *  moveSpeed *Time.deltaTime, Space.World);
        }
    }
}