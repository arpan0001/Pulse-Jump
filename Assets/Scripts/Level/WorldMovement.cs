using UnityEngine;

namespace PulseJump.Level
{
    public class WorldMovement : MonoBehaviour
    {
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


        public void SetMoveSpeed(float speed)
        {
            moveSpeed = speed;
        }


        private void Update()
        {
            if (!_isMoving)
                return;


            transform.Translate(
                Vector3.back *
                moveSpeed *
                Time.deltaTime,
                Space.World);
        }
    }
}