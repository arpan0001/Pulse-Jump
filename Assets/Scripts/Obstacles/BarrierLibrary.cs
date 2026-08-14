using System.Collections.Generic;
using UnityEngine;

namespace PulseJump.Obstacles
{
    [CreateAssetMenu(
        fileName = "BarrierLibrary",
        menuName = "Pulse Jump/Barrier Library")]
    public class BarrierLibrary : ScriptableObject
    {
        [Header("Available Barriers")]
        public List<BarrierDefinition> barriers =
            new List<BarrierDefinition>();
    }
}