using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]

public class CombineMeshes : MonoBehaviour
{
    void Start()
        {
        Vector3 startPos = transform.position;
        Vector3 startScale = transform.localScale;
        transform.position = Vector3.zero;

            MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>();
            CombineInstance[] combine = new CombineInstance[meshFilters.Length];

            int i = 0;
            while (i < meshFilters.Length)
            {
                combine[i].mesh = meshFilters[i].sharedMesh;
                combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
                meshFilters[i].gameObject.SetActive(false);

                i++;
            }
            transform.GetComponent<MeshFilter>().mesh = new Mesh();
            transform.GetComponent<MeshFilter>().mesh.CombineMeshes(combine,true,true);
            transform.gameObject.SetActive(true);

        transform.position = startPos;
        transform.localScale = startScale;
        }
}
