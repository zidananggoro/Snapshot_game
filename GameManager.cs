using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Guys all of this code is pretty stupid, didn't have time to make a proper gameManager :D
/// </summary>
public class GameManager : MonoBehaviour
{
    public GameObject gameOverScreen;
    public GameObject escMenu;

    public GameObject commander;
    public GameObject player;
    public PlayerMovement pm;
    public Sword s;

    public int crystalsCollected;
    public int enemiesTotal, enemiesLeft;

    public bool upgradeCityLoaded;
    public bool allCristalsCollected, enemiesDefeated;

    public int currentLevel;
    bool escapeMenuActive, mainMenuActive;
    public bool isPlayingCommander, isPlayingPlayer;

    public static GameManager instance;

    public int speedLevel, jumpForceLevel, healthLevel, damageLevel;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu") mainMenuActive = true;
        DontDestroyOnLoad(gameObject);
    }
    private void Update()
    {
        if (isPlayingCommander && player != null) player.SetActive(false);

        if (crystalsCollected == 4) allCristalsCollected = true;

        if (enemiesTotal != 0)
        if (enemiesLeft <= 25) enemiesDefeated = true;

        if (enemiesDefeated && allCristalsCollected && !upgradeCityLoaded)
        {
            if (SceneManager.GetActiveScene().name == "Level3")
            {
                GameObject.Find("EndingTrigger").GetComponent<DialogueTrigger>().EndGame();
            }
            else
            {
                upgradeCityLoaded = true;
                LoadUpgradecity();
            }
        }

        GmInput();

        //Find components automatically
        if (commander == null)
        {
            if (GameObject.Find("Commander") != null)
            commander = GameObject.Find("Commander");
        }
        if (player == null)
        {
            if (GameObject.Find("Player") != null)
            player = GameObject.Find("Player");
        }
        if (pm == null)
        {
            if (GameObject.Find("Character").GetComponent<PlayerMovement>() != null)
            pm = GameObject.Find("Character").GetComponent<PlayerMovement>();
        }
        if (s == null)
        {
            if (GameObject.Find("FullSword").GetComponent<Sword>() != null)
            s = GameObject.Find("FullSword").GetComponent<Sword>();
        }

        if (Input.GetKeyDown(KeyCode.L)) SetAllNull();
    }
    public void GmInput()
    {
        //Open escMenu
        if (Input.GetKeyDown(KeyCode.Escape) && !escapeMenuActive && !mainMenuActive)
        {
            escMenu.SetActive(true);
            StartCoroutine(ecmActive(true));

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        if (Input.GetKeyDown(KeyCode.Escape) && escapeMenuActive && !mainMenuActive)
        {
            escMenu.SetActive(false);
            StartCoroutine(ecmActive(false));

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
    private IEnumerator ecmActive(bool truee)
    {
        yield return new WaitForSeconds(0.1f);
        if (truee)
        escapeMenuActive = true;
        else
            escapeMenuActive = false;
    }
    public void StoreUpgradesData(int speedLevell, int jumpForceLevell, int healthLevell, int damageLevell)
    {
        speedLevel = speedLevell;
        jumpForceLevel = jumpForceLevell;
        healthLevel = healthLevell;
        damageLevel = damageLevell;
    }
    public void UpgradePlayer()
    {
        pm.startMaxSpeed = 20 + (2 * speedLevel);
        pm.maxSpeed = 20 + (2 * speedLevel);

        pm.jumpForce = 375 + (10*jumpForceLevel);

        pm.health = 1000 + (50 + healthLevel);
        pm.maxHealth = 1000 + (50 + healthLevel);

        s.basicDamage = 30 + (3 * damageLevel);
    }
    public void GameOver()
    {
        gameOverScreen.SetActive(true);
    }
    public void RestartLevel()
    {
        LoadLevel(currentLevel);
        crystalsCollected = 0;
    }
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");

        escMenu.SetActive(false);
        StartCoroutine(ecmActive(false));

        mainMenuActive = true;
    }
    public void LoadUpgradecity()
    {
        SceneManager.LoadScene("Upgrades2");

        crystalsCollected = 0;
        Invoke("Delay2", .5f);

        SetAllNull();
    }

    public void SetAllNull()
    {
        pm = null;
        s = null;
        player = null;
        commander = null;

        if (!mainMenuActive)
        {
            commander = GameObject.Find("Commander");
            player = GameObject.Find("Player");
            pm = GameObject.Find("Character").GetComponent<PlayerMovement>();
            s = GameObject.Find("FullSword").GetComponent<Sword>();
        }
    }
    public void LoadPlayer()
    {
        isPlayingCommander = false;
        isPlayingPlayer = true;

        commander.SetActive(false);
        player.SetActive(true);

        UpgradePlayer();

        enemiesLeft = enemiesTotal;
    }
    public void LoadTutorial()
    {
        SetAllNull();
        crystalsCollected = 0;
        SceneManager.LoadScene("Tutorial");
        mainMenuActive = false;
        isPlayingCommander = false;
        isPlayingPlayer = true;
        commander.SetActive(false);
        player.SetActive(true);

        currentLevel = 0;
    }
    public void LoadLevel(int level)
    {
        if (currentLevel == 0)
        {
            LoadTutorial();
            return;
        }

        //currentLevel = level;
        upgradeCityLoaded = false;

        SetAllNull();
        crystalsCollected = 0;
        allCristalsCollected = false;
        SceneManager.LoadScene("Level"+level.ToString());
        mainMenuActive = false;

        //Play Commander at start of every Level
        isPlayingCommander = true;
        isPlayingPlayer = false;

        Invoke("Delay", 1f);
    }
    public void Delay()
    {
        player.SetActive(false);
        commander.SetActive(true);
    }
    public void Delay2()
    {
        player.SetActive(true);
        commander.SetActive(false);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}