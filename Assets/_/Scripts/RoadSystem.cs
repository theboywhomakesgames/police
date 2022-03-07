using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DB.Police
{
    public class RoadSystem : MonoBehaviour
    {
        public static RoadSystem instance;

        Vector3 lastPos;

        [Button]
        public void Test()
        {
            Vector3 tmp = testT.position;
            testT.position = GetNeighbour(testT.position, lastPos);
            lastPos = tmp;
        }

        public void AddLocation(RoadLocation loc)
        {
            locations.Add(loc);
        }

        public Vector3 GetNeighbour(Vector3 position, Vector3 lastPos)
        {
            Vector3 fallback = Vector3.zero;

            SortedList<float, RoadLocation> list = new SortedList<float, RoadLocation>();
            foreach (RoadLocation loc in locations)
            {
                float distance = (loc.transform.position - position).magnitude;
                float lpDistance = (loc.transform.position - lastPos).magnitude;

                if (distance < thresholdCeiling && distance > thresholdFloor)
                {
                    if (lpDistance > thresholdFloor)
                    {
                        list.Add(distance, loc);
                    }
                    else
                    {
                        fallback = loc.transform.position;
                    }
                }
            }

            int rndIdx = Random.Range(0, 4);
            IList<RoadLocation> locs = list.Values;
            for (int i = rndIdx; i >= 0; i--)
            {
                if (list.Count > i)
                {
                    return locs[i].transform.position;
                }
            }

            return fallback;
        }

        public Vector3 GetNeighbour(Vector3 position)
        {
            SortedList<float, RoadLocation> list = new SortedList<float, RoadLocation>();
            foreach (RoadLocation loc in locations)
            {
                float distance = (loc.transform.position - position).magnitude;

                if (distance < thresholdCeiling && distance > thresholdFloor)
                {
                    list.Add(distance, loc);
                }
            }

            int rndIdx = Random.Range(0, 4);
            IList<RoadLocation> locs = list.Values;
            for(int i = rndIdx; i >= 0; i--)
            {
                if(list.Count > i)
                {
                    return locs[i].transform.position;
                }
            }

            return Vector3.zero;
        }

        [SerializeField] private List<RoadLocation> locations;
        [SerializeField] private float thresholdCeiling = 20, thresholdFloor = 1;
        [SerializeField] private Transform testT;

        private void Awake()
        {
            instance = this;
        }
    }
}