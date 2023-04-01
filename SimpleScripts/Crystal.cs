using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : MonoBehaviour
{
    public GameManager gm;

    private void Update()
    {
        if (gm == null)
        {
            if (GameObject.Find("GameManager").GetComponent<GameManager>() != null)
                gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Collect();
        }
    }
    public void Collect()
    {
        Debug.Log("Collected");
        gm.crystalsCollected++;
        GameObject.Find("Audio").GetComponent<Audio>().PickupSound();
        Destroy(gameObject);
    }
}
