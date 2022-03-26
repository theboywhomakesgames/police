using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinnedMeshSetter : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer referenceMR;
    [SerializeField] private SkinnedMeshRenderer[] othersMR;
    [SerializeField] private SkinnedMeshRenderer[] justSetMR;
    [SerializeField] private bool randomize;

    private void Start()
    {
        foreach(SkinnedMeshRenderer mr in othersMR)
        {
            mr.bones = referenceMR.bones;
            mr.rootBone = referenceMR.rootBone;
            mr.gameObject.SetActive(false);
        }

        foreach (SkinnedMeshRenderer mr in justSetMR)
        {
            mr.bones = referenceMR.bones;
            mr.rootBone = referenceMR.rootBone;
        }

        if (randomize)
        {
            referenceMR.gameObject.SetActive(false);
            int r = Random.Range(0, othersMR.Length + 1);
            if(r == 0)
            {
                referenceMR.gameObject.SetActive(true);
            }
            else
            {
                othersMR[r - 1].gameObject.SetActive(true);
            }
        }
    }
}
