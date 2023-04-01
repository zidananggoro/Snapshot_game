using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradesShop : MonoBehaviour
{
    public bool unlockNextLevel;

    public GameManager gm;
    public GameObject shopUi;
    public Sword s;
    public PlayerMovement pm;

    //All the text stuff
    public TextMeshProUGUI healthTxtPlayer, speedTxtPlayer, damageTxtPlayer, jumpForceTxtPlayer, pointsLeftPlayer;
    public TextMeshProUGUI healthTxtEnemy, speedTxtEnemy, damageTxtEnemy, rangeTxtEnemy, pointsLeftEnemy;

    //Variables to level up (Player)
    public bool statsRecovered;
    public int pointsGivenAtStart = 4;
    public int pointsToSpend;
    public int damageLevel, speedLevel, healthLevel, jumpForceLevel;
    public int maxLevel = 10;

    //Variables to level up (Enemy)
    public int pointsToSpendEnemy;
    public int damageLevelEnemy, speedLevelEnemy, healthLevelEnemy, rangeLevelEnemy;

    private void Start()
    {
        //Give some points to spend
        pointsToSpend = pointsGivenAtStart;
        pointsToSpendEnemy = pointsGivenAtStart;
    }
    private void Update()
    {
        if (gm == null)
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();

        if (gm != null && !statsRecovered)
        {
            damageLevel = gm.damageLevel;
            speedLevel = gm.speedLevel;
            jumpForceLevel = gm.jumpForceLevel;
            healthLevel = gm.healthLevel;

            statsRecovered = true;
        }

        if (pointsToSpend <= 0) Finished();

        healthTxtPlayer.SetText("Health (" + healthLevel + "/10)");
        speedTxtPlayer.SetText("Speed (" + speedLevel + "/10)");
        damageTxtPlayer.SetText("Damage (" + damageLevel + "/10)");
        jumpForceTxtPlayer.SetText("Jump (" + jumpForceLevel + "/10)");

        healthTxtEnemy.SetText("Health (" + healthLevelEnemy + "/10)");
        speedTxtEnemy.SetText("Speed (" + speedLevelEnemy + "/10)");
        damageTxtEnemy.SetText("Damage (" + damageLevelEnemy + "/10)");
        rangeTxtEnemy.SetText("Range (" + rangeLevelEnemy + "/10)");

        pointsLeftPlayer.SetText("Points Left " + pointsToSpend);
        pointsLeftEnemy.SetText("Points Left " + pointsToSpendEnemy);
    }

    /// <summary>
    /// More pretty stupid code :D Get used to it^^
    /// </summary>
    public void IncreaseSpeedLevel()
    {
        //Make sure level doesn't go over max, and points are needed to upgrade
        if (speedLevel >= maxLevel || pointsToSpend <= 0) return;

        //Audio
        GameObject.Find("Audio").GetComponent<Audio>().UpgradeSound();

        speedLevel++;
        pointsToSpend--;
    }
    public void IncreaseJumpForceLevel()
    {
        //Make sure level doesn't go over max, and points are needed to upgrade
        if (jumpForceLevel >= maxLevel || pointsToSpend <= 0) return;

        //Audio
        GameObject.Find("Audio").GetComponent<Audio>().UpgradeSound();

        jumpForceLevel++;
        pointsToSpend--;
    }
    public void IncreaseHealthLevel()
    {
        //Make sure level doesn't go over max, and points are needed to upgrade
        if (healthLevel >= maxLevel || pointsToSpend <= 0) return;

        //Audio
        GameObject.Find("Audio").GetComponent<Audio>().UpgradeSound();

        healthLevel++;
        pointsToSpend--;
    }
    public void IncreaseDamageLevel()
    {
        //Make sure level doesn't go over max, and points are needed to upgrade
        if (damageLevel >= maxLevel || pointsToSpend <= 0) return;

        //Audio
        GameObject.Find("Audio").GetComponent<Audio>().UpgradeSound();

        damageLevel++;
        pointsToSpend--;
    }
    public void IncreaseSpeedLevelEnemy()
    {
        //Make sure level doesn't go over max, and points are needed to upgrade
        if (speedLevelEnemy >= maxLevel || pointsToSpendEnemy <= 0) return;

        //Audio
        GameObject.Find("Audio").GetComponent<Audio>().UpgradeSound();

        speedLevelEnemy++;
        pointsToSpendEnemy--;
    }
    public void IncreaseJumpForceLevelEnemy()
    {
        //Make sure level doesn't go over max, and points are needed to upgrade
        if (speedLevelEnemy >= maxLevel || pointsToSpendEnemy <= 0) return;

        //Audio
        GameObject.Find("Audio").GetComponent<Audio>().UpgradeSound();

        rangeLevelEnemy++;
        pointsToSpendEnemy--;
    }
    public void IncreaseHealthLevelEnemy()
    {
        //Make sure level doesn't go over max, and points are needed to upgrade
        if (speedLevelEnemy >= maxLevel || pointsToSpendEnemy <= 0) return;

        //Audio
        GameObject.Find("Audio").GetComponent<Audio>().UpgradeSound();

        healthLevelEnemy++;
        pointsToSpendEnemy--;
    }
    public void IncreaseDamageLevelEnemy()
    {
        //Make sure level doesn't go over max, and points are needed to upgrade
        if (speedLevelEnemy >= maxLevel || pointsToSpendEnemy <= 0) return;

        //Audio
        GameObject.Find("Audio").GetComponent<Audio>().UpgradeSound();

        damageLevelEnemy++;
        pointsToSpendEnemy--;
    }
    public void Finished()
    {
        gm.StoreUpgradesData(speedLevel, jumpForceLevel, healthLevel, damageLevel);

        unlockNextLevel = true;
    }

    // open shop when entering the shop, great sentence btw.
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            shopUi.SetActive(true);

            //Block movement
            s.blockInput = true;
            pm.blockCameraMovement = true;

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            shopUi.SetActive(false);

            //Allow movement
            s.blockInput = false;
            pm.blockCameraMovement = false;

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
