using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrollContent : MonoBehaviour
{
    public float minX, maxX, scrollSpeed;
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow) && transform.position.x > minX) transform.Translate(Vector3.left * scrollSpeed * Time.deltaTime);
        if (Input.GetKey(KeyCode.RightArrow) && transform.position.x < maxX) transform.Translate(Vector3.right * scrollSpeed * Time.deltaTime);
    }
}
