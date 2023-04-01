using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RespawnLevel2 : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag ("Enemy"))
        {
            Invoke("Delay", 1f);
        }
    }

    public void Delay()
    {
        Debug.Log("Restart");
        SceneManager.LoadScene("Level2");
    }
}
