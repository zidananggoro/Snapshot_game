using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    public GameObject music1;
    public GameObject footSteps, attack1,attack2, charge, shoot, upgrade, pickup, block;

    public void WalkingSound(bool turnOn)
    {
        if (turnOn) footSteps.SetActive(true);
        else footSteps.SetActive(false);
    }
    public void AttackSound(bool isAttack1)
    {
        if (isAttack1) attack1.SetActive(true);
        else attack2.SetActive(true);

        if (isAttack1)
        StartCoroutine(Deactivate(attack1));
        else
        StartCoroutine(Deactivate(attack2));
    }
    public void ChargeSound(bool turnOn)
    {
        if (turnOn) charge.SetActive(true);
        else charge.SetActive(false);
    }
    public void ShootSound()
    {
        shoot.SetActive(true);
        StartCoroutine(Deactivate(shoot));
    }
    public void UpgradeSound()
    {
        upgrade.SetActive(true);
        StartCoroutine(Deactivate(upgrade));
    }
    public void PickupSound()
    {
        pickup.SetActive(true);
        StartCoroutine(Deactivate(pickup));
    }
    public void BlockSound()
    {
        pickup.SetActive(true);
        StartCoroutine(Deactivate(block));
    }
    public IEnumerator Deactivate(GameObject go)
    {
        yield return new WaitForSeconds(0.25f);
        go.SetActive(false);
    }
}
