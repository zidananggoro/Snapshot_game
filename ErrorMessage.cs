using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErrorMessage : MonoBehaviour
{
    public GameObject robotsToNear, buyUpgrades,notEnoughtPoints;

    public void RobotsToNear()
    {
        robotsToNear.SetActive(true);
        Invoke("Delay", 1f);
    }
    public void BuyUpgrades()
    {
        buyUpgrades.SetActive(true);
        Invoke("Delay", 1f);
    }
    public void NotEnoughtPoints()
    {
        notEnoughtPoints.SetActive(true);
        Invoke("Delay", 1f);
    }
    public void Delay()
    {
        notEnoughtPoints.SetActive(false);
        buyUpgrades.SetActive(false);
        robotsToNear.SetActive(false);
    }
}
