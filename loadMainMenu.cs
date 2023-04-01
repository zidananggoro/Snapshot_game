using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loadMainMenu : MonoBehaviour
{
    private void Start()
    {
        GameObject.Find("GameManager").GetComponent<GameManager>().LoadMainMenu();
    }
}
