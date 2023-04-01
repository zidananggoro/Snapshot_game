using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    public float destroyTimer = 2f;
    void Update()
    {
        destroyTimer -= Time.deltaTime;
        if (destroyTimer < 0) Destroy(gameObject);
    }
}
