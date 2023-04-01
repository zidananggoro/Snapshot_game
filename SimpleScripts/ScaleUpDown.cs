using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleUpDown : MonoBehaviour
{
    public float scaleSpeed;
    public float timeBetween;
    public bool scalingUp;
    private void Start()
    {
        yes();
    }
    private void Update()
    {
        if (scalingUp) transform.localScale += Vector3.one * Time.deltaTime * scaleSpeed;
        else transform.localScale -= Vector3.one * Time.deltaTime * scaleSpeed;
    }
    private void yes()
    {
        scalingUp = true;
        Invoke("no", timeBetween);
    }
    private void no()
    {
        scalingUp = false;
        Invoke("yes", timeBetween);
    }
}
