using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DB.Police
{
    public class WalkableBlock : MonoBehaviour
    {
        [SerializeField] private GameObject stickmanPrefab;
        [SerializeField] private Transform[] wps;

        private void Start()
        {
            int r = Random.Range(1, wps.Length - 1);
            int baseR = Random.Range(0, wps.Length);
            for(int i = 0; i < r; i++)
            {
                int ii = i + baseR;
                ii %= wps.Length;
                GameObject go = Instantiate(stickmanPrefab);
                go.transform.position = wps[ii].transform.position;
                BlockWalker bw = go.GetComponentInChildren<BlockWalker>();
                bw.wps = wps;
                bw.Activate();
            }
        }
    }
}