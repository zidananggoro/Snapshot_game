using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    public UpgradesShop us;
    public GameObject gameManager;
    private void Update()
    {
        gameManager = GameObject.Find("GameManager");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (us.unlockNextLevel)
            {
                gameManager.GetComponent<GameManager>().currentLevel += 1;
                gameManager.GetComponent<GameManager>().LoadLevel(gameManager.GetComponent<GameManager>().currentLevel);
            }
            else GameObject.Find("ErrorMessage").GetComponent<ErrorMessage>().BuyUpgrades();
        }
    }
}
