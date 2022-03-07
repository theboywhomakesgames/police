using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DB.Police
{
    public class Squad : MonoBehaviour
    {
        public void AddMember(GoalChaser gc)
        {
            squadMembers.Add(gc);
            SortSquad();
        }

        public void SortSquad()
        {
            if (autosort)
            {
                AutoSort();
            }
            else
            {
                if(poses.Count >= squadMembers.Count - 1)
                {
                    for(int i = 0; i < squadMembers.Count; i++)
                    {
                        squadMembers[i].goalT.parent = poses[i];
                        squadMembers[i].goalT.localPosition = Vector3.zero;
                    }
                }
            }
        }

        private void AutoSort()
        {
            int base_ = 0;
            while (true)
            {
                bool flag = false;

                for (int i = 0; i < width; i++)
                {
                    int idx = base_ * width + i;
                    if (idx == 0) continue;

                    if (idx < squadMembers.Count)
                    {
                        if (base_ == 0)
                        {
                            squadMembers[idx].goalT.parent = squadMembers[0].transform;
                        }
                        else
                        {
                            int parIdx = (base_ - 1) * width + i;
                            squadMembers[idx].goalT.parent = squadMembers[parIdx].transform;
                        }
                    }
                    else
                    {
                        flag = true;
                        break;
                    }
                }

                base_++;
                if (flag)
                {
                    break;
                }
            }
        }

        [SerializeField] private List<GoalChaser> squadMembers;
        [SerializeField] private List<Transform> squadTransforms;
        [SerializeField] private int width = 5;

        [SerializeField] private bool autosort = true;
        [SerializeField] private List<Transform> poses;

        private void Start()
        {
            foreach(Transform t in squadTransforms)
            {
                squadMembers.Add(t.gameObject.GetComponentInChildren<GoalChaser>());
            }

            SortSquad();
        }
    }
}