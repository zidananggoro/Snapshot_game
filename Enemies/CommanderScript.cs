using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CommanderScript : MonoBehaviour
{
    public GameManager gm;
    public float speed;
    public bool gmStatsSet;

    public int normalBotsLeft, rangedBotsLeft;
    public int maxBotsInOnePlace = 3;

    //Look
    public Transform commanderCam, orientation;
    private float xRotation;
    private float sensitivity = 50f;
    private float sensMultiplier = 1f;

    //Select bots
    public bool normalSelected, rangedSelected;
    public bool blueNext;

    //Bots
    public GameObject normalBot, normalBotViolet;
    public GameObject rangedBot, rangedBotViolet;

    //SetText
    public TextMeshProUGUI nornalBLeftTxt,rangedBLeftTxt;

    public LayerMask whatIsEnemies;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void Update()
    {
        if (gm == null)
        {
            if (GameObject.Find("GameManager") != null)
            gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        }

        //Set maxEnemies variable of gm
        if (gm != null && !gmStatsSet)
        {
            gmStatsSet = true;
            gm.enemiesTotal = normalBotsLeft + rangedBotsLeft;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0)) PlaceBot();

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            normalSelected = true;
            rangedSelected = false;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            normalSelected = false;
            rangedSelected = true;
        }

        //Check if all bots are placed
        if (normalBotsLeft <= 0 && rangedBotsLeft <= 0) Done();

        nornalBLeftTxt.SetText(normalBotsLeft.ToString());
        rangedBLeftTxt.SetText(rangedBotsLeft.ToString());

        Movement();
        Look();
    }
    private void PlaceBot()
    {
        RaycastHit hit;
        if (Physics.Raycast(commanderCam.position, commanderCam.forward, out hit, 200))
        {
            if (normalSelected && normalBotsLeft > 0)
            {
                //Check if there are already robots
                if (Physics.OverlapSphere(hit.point,10,whatIsEnemies).Length > 3)
                {
                    Debug.Log("There are too many bots!");
                    GameObject.Find("ErrorMessage").GetComponent<ErrorMessage>().RobotsToNear();
                    return;
                }

                normalBotsLeft--;

                if (blueNext)
                    Instantiate(normalBot, hit.point, Quaternion.identity);
                if (!blueNext)
                    Instantiate(normalBotViolet, hit.point, Quaternion.identity);
            }
            if (rangedSelected && rangedBotsLeft > 0)
            {
                //Check if there are already robots
                if (Physics.OverlapSphere(hit.point, 10, whatIsEnemies).Length > 3)
                {
                    Debug.Log("There are too many bots!");
                    return;
                }

                rangedBotsLeft--;

                if (blueNext)
                    Instantiate(rangedBot, hit.point, Quaternion.identity);
                if (!blueNext)
                    Instantiate(rangedBotViolet, hit.point, Quaternion.identity);
            }
        }

        //Switch color
        blueNext = !blueNext;
    }

    float desiredX;

    private void Done()
    {
        gm.LoadPlayer();
    }
    private void Look()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.fixedDeltaTime * sensMultiplier;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.fixedDeltaTime * sensMultiplier;

        //Find current look rotation
        Vector3 rot = commanderCam.transform.localRotation.eulerAngles;
        desiredX = rot.y + mouseX;

        //Rotate, and also make sure we dont over- or under-rotate.
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        //Perform the rotations
        commanderCam.transform.localRotation = Quaternion.Euler(xRotation, desiredX, 0);
        orientation.transform.localRotation = Quaternion.Euler(0, desiredX, 0);
    }
    private void Movement()
    {
        transform.Translate(orientation.forward * Input.GetAxis("Vertical") * Time.deltaTime * speed);
        transform.Translate(orientation.right * Input.GetAxis("Horizontal") * Time.deltaTime * speed);
        if (Input.GetKey(KeyCode.Space)) transform.Translate(orientation.up * Time.deltaTime * speed);
        if (Input.GetKey(KeyCode.LeftShift)) transform.Translate(-orientation.up * Time.deltaTime * speed);
    }
}
