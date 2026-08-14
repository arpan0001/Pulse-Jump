using System.Collections.Generic;
using UnityEngine;

namespace PulseJump.Generation
{
    public class TrackSectionPool : MonoBehaviour
    {
        [Header("Section Prefabs")]
        [SerializeField]
        private GameObject[] sectionPrefabs;

        [Header("Pool Settings")]
        [SerializeField]
        private int sectionsPerPrefab = 3;

        private readonly List<GameObject> _available =
            new List<GameObject>();


        private void Awake()
        {
            CreatePool();
        }


        private void CreatePool()
        {
            if (sectionPrefabs == null ||
                sectionPrefabs.Length == 0)
            {
                Debug.LogError(
                    "No track section prefabs assigned.");
                return;
            }


            foreach (GameObject prefab in sectionPrefabs)
            {
                if (prefab == null)
                    continue;


                for (int i = 0;
                     i < sectionsPerPrefab;
                     i++)
                {
                    GameObject section =
                        Instantiate(
                            prefab,
                            transform);


                    section.SetActive(false);

                    _available.Add(section);
                }
            }
        }


        public GameObject GetSection()
        {
            if (_available.Count == 0)
            {
                Debug.LogWarning(
                    "Track section pool is empty.");

                return null;
            }


            int lastIndex =
                _available.Count - 1;


            GameObject section =
                _available[lastIndex];


            _available.RemoveAt(lastIndex);


            section.SetActive(true);

            return section;
        }


        public void ReturnSection(
            GameObject section)
        {
            if (section == null)
                return;


            section.SetActive(false);

            section.transform.SetParent(
                transform);


            _available.Add(section);
        }
    }
}