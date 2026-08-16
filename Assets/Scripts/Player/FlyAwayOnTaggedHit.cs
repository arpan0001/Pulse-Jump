using UnityEngine;

namespace PulseJump.Game
{
    [RequireComponent(typeof(Rigidbody))]
    public class FlyAwayOnTaggedHit : MonoBehaviour
    {
        [SerializeField]
        private string requiredTag = "Player";

        [SerializeField]
        private float pushForce = 10f;

        [SerializeField]
        private float upwardForce = 0f;

        [SerializeField]
        private float spinForce = 0f;


        private Rigidbody cachedRigidbody;
        private Collider cachedCollider;

        private Transform originalParent;
        private Vector3 originalLocalPosition;
        private Quaternion originalLocalRotation;
        private Vector3 originalLocalScale;

        private bool hasFlown;


        private void Awake()
        {
            cachedRigidbody = GetComponent<Rigidbody>();
            cachedCollider = GetComponent<Collider>();

            originalParent = transform.parent;
            originalLocalPosition = transform.localPosition;
            originalLocalRotation = transform.localRotation;
            originalLocalScale = transform.localScale;

            ResetForTrackReuse();
        }


        private void OnTriggerEnter(Collider other)
        {
            if (hasFlown || !other.CompareTag(requiredTag))
                return;

            hasFlown = true;

            GameObject worldObject = GameObject.Find("World");

            if (worldObject != null)
            {
                transform.SetParent(worldObject.transform, true);
            }

            cachedCollider.isTrigger = false;
            cachedRigidbody.isKinematic = false;
            cachedRigidbody.useGravity = false;

            Vector3 direction =
                transform.position - other.transform.position;

            direction.y = 0f;
            direction.Normalize();

            cachedRigidbody.AddForce(
               direction * pushForce +
           Vector3.up * upwardForce,
          ForceMode.VelocityChange);

            cachedRigidbody.AddTorque(
                Random.insideUnitSphere * spinForce,
                ForceMode.Impulse);
        }


        public void ResetForTrackReuse()
        {
            transform.SetParent(originalParent, false);

            transform.localPosition = originalLocalPosition;
            transform.localRotation = originalLocalRotation;
            transform.localScale = originalLocalScale;

            hasFlown = false;

            cachedRigidbody.isKinematic = true;
            cachedRigidbody.useGravity = false;
            cachedRigidbody.linearVelocity = Vector3.zero;
            cachedRigidbody.angularVelocity = Vector3.zero;

            cachedCollider.isTrigger = true;
        }
    }
}