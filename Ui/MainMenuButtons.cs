using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    public GameManager gameManager;

    public GameObject mainMenu;
    public GameObject levelMenu;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    public void LoadLevelMenu()
    {
        mainMenu.SetActive(false);
        levelMenu.SetActive(true);
    }
    public void LoadMainMenu()
    {
        mainMenu.SetActive(true);
        levelMenu.SetActive(false);
    }
    public void Test()
    {
        SceneManager.LoadScene("3D Code");
    }

    /// <summary>
    /// Haha pls don't use this code in your games... xD
    /// </summary>
    public void LoadLevel0()
    {
        gameManager.LoadLevel(0);
    }
    public void LoadTutorial()
    {
        gameManager.LoadTutorial();
    }
    public void LoadLevel1()
    {
        gameManager.currentLevel = 1;
        gameManager.LoadLevel(1);
    }
    public void LoadLevel2()
    {
        gameManager.currentLevel = 2;
        gameManager.LoadLevel(2);
    }
    public void LoadLevel3()
    {
        gameManager.currentLevel = 3;
        gameManager.LoadLevel(3);
    }
    public void LoadLevel4()
    {
        gameManager.LoadLevel(4);
    }
}
