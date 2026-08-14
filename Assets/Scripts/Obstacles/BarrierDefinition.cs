using UnityEngine;

namespace PulseJump.Obstacles
{
    [CreateAssetMenu(
        fileName = "BarrierDefinition",
        menuName = "Pulse Jump/Barrier Definition")]
    public class BarrierDefinition : ScriptableObject
    {
        [Header("Identity")]
        public string barrierName;

        [Header("Prefab")]
        public GameObject prefab;

        [Header("Difficulty")]
        [Range(0f, 1f)]
        public float spawnWeight = 1f;
    }
}