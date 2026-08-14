using System.Collections.Generic;
using UnityEngine;

namespace PulseJump.Movement
{
    public class TrackMovementController : MonoBehaviour
    {
        [SerializeField]
        private float speed = 5f;


        private readonly List<Transform>
            _sections =
                new List<Transform>();


        public float Speed => speed;


        public void RegisterSection(
            Transform section)
        {
            if (section == null)
                return;


            if (!_sections.Contains(section))
            {
                _sections.Add(section);
            }
        }


        public void UnregisterSection(
            Transform section)
        {
            if (section == null)
                return;


            _sections.Remove(section);
        }


        private void Update()
        {
            float movement =
                speed *
                Time.deltaTime;


            Vector3 delta =
                Vector3.back *
                movement;


            for (int i = 0;
                 i < _sections.Count;
                 i++)
            {
                Transform section =
                    _sections[i];


                if (section == null)
                    continue;


                section.position += delta;
            }
        }
    }
}