using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustLeave : MonoBehaviour
{
    public bool noPerformance;
    public bool lowPerformance;
    public bool averagePerformance;
    public bool fullPerformance;

    bool truee;

    void Start()
    {
        AdjustLeaves();
    }
    public void Reset()
    {
        MeshRenderer[] children = GetComponentsInChildren<MeshRenderer>();

        for (int i = 0; i < 300; i += 1)
        {
            if (children[i] != null)
                children[i].enabled = true;
        }
        AdjustLeaves();
    }
    public void AdjustLeaves()
    {
        if (noPerformance)
        {
            MeshRenderer[] children = GetComponentsInChildren<MeshRenderer>();

            for (int i = 0; i < 300; i += 1)
            {
                if (children[i] != null)
                    children[i].enabled = false;
            }
        }
        if (lowPerformance)
        {
            MeshRenderer[] children = GetComponentsInChildren<MeshRenderer>();

            for (int i = 1; i < 300; i += 1)
            {
                if (children[i] != null)
                    children[i].enabled = false;

                if (truee)
                    i++;

                truee = !truee;
            }
        }
        if (averagePerformance || lowPerformance)
        {
            MeshRenderer[] children = GetComponentsInChildren<MeshRenderer>();

            for (int i = 0; i < 300; i += 2)
            {
                if (children[i] != null)
                    children[i].enabled = false;
            }
        }
    }
}
