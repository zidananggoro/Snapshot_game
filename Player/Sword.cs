using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public bool blockInput;

    public Animator animator;
    public Transform fpsCam;
    public Rigidbody rb;
    public UiBar blockBar;
    public CamShake camShake;
    public ParticleSpawner particleSpawner;

    public Transform attackPoint, feet;
    public bool readyToAttack, attacking;
    public float timeBetweenAttacks;
    public int basicDamage;
    public float attackRange;

    //Anim
    public bool Trigger1;

    //Blocking
    public float maxDamageBlocked, damageToBlockLeft, repairSpeed;
    public int damageOnBreak;
    public bool blocking, blockBroken;

    //Charage Sword
    public float swordCharge, maxSwordCharge;

    //SwordStomp
    public int stompDamage;
    public float stompRange, stompCooldown, minHeight;
    public bool readyToStomp, stompInCooldown;

    //Shockwave ability
    public float shockwaveForce, shockwaveCooldown, shockwaveRange;
    bool readyToShockwave;

    //Hook ability
    public float hookForce, hookRange, hookCooldown;
    bool readyToHook, hooking, hookHitEnemy;
    Vector3 hookDirection, posToHook;

    //Stone platform ability
    public GameObject stonePlatform;
    public Transform spSpawnPoint;
    public bool readyToSpawnStone;
    public float stoneCooldown;

    public bool disableMovement;
    public LayerMask whatIsEnemies;
    public LayerMask whatIsPlayer;

    private void Start()
    {
        readyToShockwave = true;
        readyToHook = true;
    }
    void Update()
    {
        MyInput();

        if (hooking) Hooking();

        //Execute Stomp attack
        if (rb.GetComponent<PlayerMovement>().grounded && readyToStomp) ExecuteStomp();

        //Update energyBar
        blockBar.UpdateBar(damageToBlockLeft, maxDamageBlocked);
    }
    private void MyInput()
    {
        if (Input.GetKeyUp(KeyCode.Mouse0) && readyToAttack && swordCharge <= 0.2f && !blocking && !blockInput)
        {
            BasicAttack();
        }

        //Block
        if (Input.GetKey(KeyCode.Mouse1) && !blockBroken) blocking = true;
        if (!Input.GetKey(KeyCode.Mouse1)) blocking = false;
        if (blockBroken) blocking = false;
        ///Animator
        if (blocking) animator.SetBool("blocking", true);
        else animator.SetBool("blocking", false);

        //Slowly repair block
        if (!blocking && damageToBlockLeft < maxDamageBlocked)
            damageToBlockLeft += repairSpeed * Time.deltaTime;

        //Restore block after it broke
        if (damageToBlockLeft >= maxDamageBlocked) blockBroken = false;

        //Charge
        if (Input.GetKey(KeyCode.Mouse0) && readyToAttack && !blockInput) ChargeAttack();
        if (swordCharge >= 0.2f) animator.SetBool("charging", true);
        //Fire
        if (Input.GetKeyUp(KeyCode.Mouse0) && swordCharge >= 0.2f) FireChargedAttack();

        //if (Input.GetKeyDown(KeyCode.R) && readyToHook) ShootHook();

        //If falling down and crouching
        if (rb.velocity.y < 0 && rb.GetComponent<PlayerMovement>().crouching && !stompInCooldown && transform.position.y >= minHeight) readyToStomp = true;

        //Press Q for shockwave
        if (Input.GetKeyDown(KeyCode.Q) && readyToShockwave) Shockwave();

        //Create stone platform
        if (Input.GetKeyDown(KeyCode.E) && readyToSpawnStone)
        {
            Instantiate(stonePlatform, spSpawnPoint.position,Quaternion.Euler(-90,0,0));
            readyToSpawnStone = false;
            Invoke("ResetStoneAbility", stoneCooldown);
        }
    }
    private void ResetStoneAbility() 
    {
        readyToSpawnStone = true;
    }
    private void BasicAttack()
    {
        camShake.StopAllCoroutines();
        camShake.StartCoroutine(camShake.Shake(0.15f, 0.075f));

        if (Trigger1)
        {
            //Audio
            GameObject.Find("Audio").GetComponent<Audio>().AttackSound(true);

            animator.SetTrigger("BasicAttack1");
        }
        else
        {
            animator.SetTrigger("BasicAttack2");
            GameObject.Find("Audio").GetComponent<Audio>().AttackSound(false);
        }

        //animator.SetTrigger("Attack");
        readyToAttack = false;
        attacking = true;

        //Checks for enemies and Damages them
        Collider[] enemiesInRange = Physics.OverlapSphere(attackPoint.position, attackRange, whatIsEnemies);
        for (int i = 0; i < enemiesInRange.Length; i++)
        {
            if (enemiesInRange[i].GetComponent<Enemy>() != null)
                enemiesInRange[i].GetComponent<Enemy>().TakeDamage(basicDamage);
            if (enemiesInRange[i].GetComponent<RangedEnemy>() != null)
                enemiesInRange[i].GetComponent<RangedEnemy>().TakeDamage(basicDamage);
        }

        Invoke("ResetAttack", timeBetweenAttacks);

        swordCharge = 0;
    }
    private void ResetAttack()
    {
        Trigger1 = !Trigger1;

        animator.ResetTrigger("BasicAttack1");
        animator.ResetTrigger("BasicAttack2");
        readyToAttack = true;
        attacking = false;
    }
    public void TakeBlockDamage(int blockDamage)
    {
        GameObject.Find("Audio").GetComponent<Audio>().BlockSound();
        damageToBlockLeft -= blockDamage;

        //Break block if needed
        if (damageToBlockLeft <= 0) BlockBreak();
    }
    public void BlockBreak()
    {
        blockBroken = true;
        blocking = false;
        rb.GetComponent<PlayerMovement>().TakeDamage(damageOnBreak);
        Debug.Log("BlockBroken");
    }
    private void ChargeAttack()
    {
        if (swordCharge < maxSwordCharge)
        swordCharge += Time.deltaTime;

        if (swordCharge >= 0.3f)
        GameObject.Find("Audio").GetComponent<Audio>().ChargeSound(true);
        camShake.StartCoroutine(camShake.Shake(3f, 0.02f));
    }
    private void FireChargedAttack()
    {
        GameObject.Find("Audio").GetComponent<Audio>().ChargeSound(false);
        GameObject.Find("Audio").GetComponent<Audio>().AttackSound(true);

        camShake.StopAllCoroutines();
        camShake.StartCoroutine(camShake.Shake(0.15f, 0.1f));

        //Attack and damage
        animator.SetBool("charging", false);

        Collider[] enemiesInRange = Physics.OverlapSphere(attackPoint.position, attackRange, whatIsEnemies);
        for (int i = 0; i < enemiesInRange.Length; i++)
        {
            if (enemiesInRange[i].GetComponent<Enemy>() != null)
            enemiesInRange[i].GetComponent<Enemy>().TakeDamage(basicDamage * Mathf.RoundToInt(swordCharge));
            if (enemiesInRange[i].GetComponent<RangedEnemy>() != null)
                enemiesInRange[i].GetComponent<RangedEnemy>().TakeDamage(basicDamage * Mathf.RoundToInt(swordCharge));
        }

        swordCharge = 0;

        readyToAttack = false;
        Invoke("ResetAttack", timeBetweenAttacks);
    }
    private void ExecuteStomp()
    {
        camShake.StartCoroutine(camShake.Shake(0.25f, 0.15f));
        Debug.Log("StompExecuted");

        particleSpawner.InstantiateStompParticles(feet.position);

        // Checks for enemies and Damages them
        Collider[] enemiesInRange = Physics.OverlapSphere(attackPoint.position, stompRange, whatIsEnemies);
        for (int i = 0; i < enemiesInRange.Length; i++)
        {
            if (enemiesInRange[i].GetComponent<Enemy>() != null)
                enemiesInRange[i].GetComponent<Enemy>().TakeDamage(stompDamage);
            if (enemiesInRange[i].GetComponent<RangedEnemy>() != null)
                enemiesInRange[i].GetComponent<RangedEnemy>().TakeDamage(stompDamage);
        }

        readyToStomp = false;
        stompInCooldown = true;
        Invoke("ResetStomp", stompCooldown);
    }
    private void ResetStomp()
    {
        stompInCooldown = false;
    }
    private void Shockwave()
    {
        camShake.StartCoroutine(camShake.Shake(0.15f, 0.075f));
        readyToShockwave = false;

        Collider[] enemiesInRange = Physics.OverlapSphere(attackPoint.position, shockwaveRange, whatIsEnemies);
        for (int i = 0; i < enemiesInRange.Length; i++)
        {
            enemiesInRange[i].GetComponent<Rigidbody>().AddExplosionForce(shockwaveForce, attackPoint.position, shockwaveRange);
            Debug.Log("ShockwaveDone");
            if (enemiesInRange[i].GetComponent<RangedEnemy>() != null)
                enemiesInRange[i].GetComponent<RangedEnemy>().TakeDamage(basicDamage * Mathf.RoundToInt(swordCharge));
        }

        Invoke("ResetShockwave", shockwaveCooldown);
    }
    private void ResetShockwave()
    {
        readyToShockwave = true;
    }
    private void ShootHook()
    {
        hooking = true;
        disableMovement = true;
        readyToHook = false;

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.position, fpsCam.forward, out hit, hookRange))
        {
            //SetPos
            posToHook = hit.point;

            //Add start force
            rb.useGravity = false;
            rb.GetComponent<PlayerMovement>().grounded = false;
            rb.velocity = Vector3.zero;
            hookDirection = hit.point - rb.transform.position;
            rb.AddForce(hookDirection.normalized * hookForce, ForceMode.Impulse);

            //Enemy hit?
            if (hit.collider.CompareTag("Enemy")) hookHitEnemy = true;
        }

        Invoke("ResetHook", hookCooldown);
    }
    private void Hooking()
    {
        rb.AddForce(hookDirection.normalized * (hookForce / 2) * Time.deltaTime,ForceMode.Impulse);

        //StopHook
        if (Physics.CheckSphere(posToHook, 5, whatIsPlayer))
        {
            StopHook();
        }
    }
    public void StopHook()
    {
        hooking = false;
        rb.useGravity = true;
        disableMovement = false;

        if (hookHitEnemy)
        {
            BasicAttack();
            rb.velocity = Vector3.zero;
            rb.AddForce(-hookDirection.normalized * hookForce);
        }

        hookHitEnemy = false;
    }
    private void ResetHook()
    {
        readyToHook = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().TakeDamage(basicDamage);
        }
    }

    //Debugging, attack Range Check
    void OnDrawGizmosSelected()
    {
        // Draw a red sphere at the attackPoint's position
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
