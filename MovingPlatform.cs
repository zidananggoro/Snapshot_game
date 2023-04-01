using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public bool movingForward,movingUp,rotating;
    
    public float speedForward,speedUp,speedRotation;
    public float timeBetween = 4f;

    private void Start()
    {
        yes();
    }
    void Update()
    {
        if (movingForward) transform.Translate(transform.forward * Time.deltaTime * speedForward);
        else transform.Translate(-transform.forward * Time.deltaTime * speedForward);

        if (movingUp) transform.Translate(transform.up * Time.deltaTime * speedUp);
        else transform.Translate(-transform.up * Time.deltaTime * speedUp);

        if (rotating) transform.Rotate(transform.up * Time.deltaTime * speedRotation);
        else transform.Translate(-transform.up * Time.deltaTime * speedRotation);
    }
    private void yes()
    {
        movingForward = true;
        movingUp = true;
        rotating = true;

        Invoke("no", timeBetween);
    }
    private void no()
    {
        movingForward = false;
        movingUp = false;
        rotating = false;

        Invoke("yes", timeBetween);
    }
}
