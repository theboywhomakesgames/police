using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;
using Sirenix.OdinInspector;
using DG.Tweening;
using DB.Utils;
using UnityEngine.Events;

[Serializable]
public class Floor
{
    public static void DisableGO(GameObject go)
    {
        if (!go.activeSelf)
            return;

        go.transform.DOMoveY(20, 0.5f).SetRelative(true).OnComplete(() =>
        {
            Vector3 pos = go.transform.position;
            pos.y -= 20;
            go.transform.position = pos;
            go.SetActive(false);
        });
    }

    public static void EnableGO(GameObject go)
    {
        if (go.activeSelf)
            return;

        go.SetActive(true);

        Vector3 pos = go.transform.position;
        pos.y += 20;
        go.transform.position = pos;

        go.transform.DOMoveY(-20, 0.5f).SetRelative(true);
    }

    public CinemachineVirtualCamera[] cams;
    public GameObject[] walls, junk;
    public GameObject floor;
    public int idx = 0;

    public void SetActive(bool input)
    {
        foreach (GameObject go in walls)
        {
            go.SetActive(input);
        }

        floor.SetActive(input);
    }

    public void UpdateWalls()
    {
        int tmpidx = idx - 1;
        if(tmpidx < 0)
        {
            tmpidx = walls.Length - 1;
        }

        DisableGO(walls[idx]);
        DisableGO(walls[tmpidx]);
        //walls[idx].SetActive(false);
        //walls[tmpidx].SetActive(false);

        for(int i = 0; i < walls.Length; i++)
        {
            if(i != tmpidx && i != idx)
            {
                EnableGO(walls[i]);
            }
        }
    }

    public void Activate()
    {
        cams[idx].Priority = 20;

        foreach(GameObject go in junk)
        {
            go.SetActive(false);
        }

        UpdateWalls();
    }

    public void Deactivate()
    {
        cams[idx].Priority = 0;

        foreach (GameObject go in junk)
        {
            go.SetActive(true);
        }

        UpdateWalls();
    }

    public void RollRight() 
    {
        cams[idx--].Priority = 0;
        if(idx < 0)
        {
            idx = cams.Length - 1;
        }

        cams[idx].Priority = 20;

        UpdateWalls();
    }

    public void RollLeft() 
    {
        cams[idx++].Priority = 0;
        idx %= cams.Length;
        cams[idx].Priority = 20;

        UpdateWalls();
    }
}

public class Building : MonoBehaviour
{
    public UnityEvent OnActivation, OnDeactivation;

    [SerializeField] private Floor[] floors;
    [SerializeField] private GameObject junk;
    [SerializeField] private int idx;
    [SerializeField] private CinemachineVirtualCamera mainCam;

    [Button]
    public void GoIn()
    {
        junk.SetActive(false);
        floors[0].SetActive(true);
        floors[0].Activate();

        for (int i = 1; i < floors.Length; i++)
        {
            floors[i].SetActive(false);
        }

        BackStackButton.instance.RegisterAction(GoOut);
        OnActivation?.Invoke();
    }

    private void GoOut()
    {
        foreach(CinemachineVirtualCamera vc in GetComponentsInChildren<CinemachineVirtualCamera>())
        {
            vc.Priority = 0;
        }

        mainCam.Priority = 20;
        OnDeactivation?.Invoke();
    }

    [Button]
    private void GoLeft()
    {
        floors[idx].RollLeft();
    }

    [Button]
    private void GoRight()
    {
        floors[idx].RollRight();
    }
}