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


        public BarrierDefinition GetRandomBarrier()
        {
            if (barriers == null ||
                barriers.Count == 0)
            {
                return null;
            }


            int index =
                Random.Range(
                    0,
                    barriers.Count);


            return barriers[index];
        }
    }
}