using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackPlayer : MonoBehaviour
{
    void Update()
    {
        if (GameObject.Find("Character") != null)
        transform.position = GameObject.Find("Character").transform.position;
    }
}
