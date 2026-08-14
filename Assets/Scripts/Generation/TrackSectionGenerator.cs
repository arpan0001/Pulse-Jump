using System.Collections.Generic;
using PulseJump.Obstacles;
using PulseJump.Movement;
using UnityEngine;

namespace PulseJump.Generation
{
    public class TrackSectionGenerator : MonoBehaviour
    {
        [Header("References")]
        [SerializeField]
        private TrackSectionPool sectionPool;


        [Header("Generation")]
        [SerializeField]
        private int initialSectionCount = 6;

        [SerializeField]
        private TrackMovementController movementController;
        [SerializeField]
        private float firstSectionZ = 10f;
        [SerializeField]
        private float recycleZ = -20f;


        [SerializeField]
        private float sectionLength = 20f;


        private float _nextSectionZ;


        private readonly List<GameObject>
            _activeSections =
            new List<GameObject>();


        private void Start()
        {
            _nextSectionZ =
                firstSectionZ;


            GenerateInitialSections();
        }
        private void Update()
        {
            for (int i = _activeSections.Count - 1;
                 i >= 0;
                 i--)
            {
                GameObject section =
                    _activeSections[i];


                if (section == null)
                {
                    _activeSections.RemoveAt(i);
                    continue;
                }


                if (section.transform.position.z <=
                    recycleZ)
                {
                    RecycleSection(section);
                }
            }
        }

        public void RecycleSection(
    GameObject section)
        {
            if (section == null)
                return;


            movementController.UnregisterSection(
                section.transform);


            _activeSections.Remove(
                section);


            section.transform.position =
                new Vector3(
                    0f,
                    0f,
                    _nextSectionZ);


            ResetSection(section);


            _activeSections.Add(
                section);


            movementController.RegisterSection(
                section.transform);


            _nextSectionZ +=
                sectionLength;
        }

        private void ResetSection(
    GameObject section)
        {
            BarrierController[] barriers =
                section.GetComponentsInChildren<
                    BarrierController>(
                        true);


            for (int i = 0;
                 i < barriers.Length;
                 i++)
            {
                barriers[i].ResetBarrier();
            }
        }


        private void GenerateInitialSections()
        {
            for (int i = 0;
                 i < initialSectionCount;
                 i++)
            {
                SpawnSection();
            }
        }


        private void SpawnSection()
        {
            GameObject section =
                sectionPool.GetSection();


            if (section == null)
                return;


            section.transform.position =
                new Vector3(
                    0f,
                    0f,
                    _nextSectionZ);


            _activeSections.Add(
                section);


            movementController.RegisterSection(
                section.transform);


            _nextSectionZ +=
                sectionLength;
        }
    }
}   