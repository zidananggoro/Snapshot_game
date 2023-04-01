using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    public GameManager gm;
    public bool dead;
    public Animator animator;

    public int health, damage;
    public float attackRange, sightRange, timeBetweenAttacks;
    public bool playerInAttackRange, playerInSightRange, readyToAttack;

    public Rigidbody rb;
    public Transform player, attackPos,feet;
    public GameObject projectile;
    public float speed, maxSpeed;

    public LayerMask whatIsPlayer;
    public Transform[] rbArray;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        //Get Character component
        if (player == null)
        {
            if (GameObject.Find("Character") != null)
                player = GameObject.Find("Character").transform;
        }

        if (gm == null)
        {
            if (GameObject.Find("GameManager").GetComponent<GameManager>() != null)
                gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        }

        if (!dead)
        {
            if (health <= 0) Die();

            playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
            playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

            if (playerInSightRange && !playerInAttackRange) Chase();
            if (playerInSightRange && playerInAttackRange) Attack();

            if (!playerInAttackRange && !playerInSightRange) animator.SetBool("Chasing", false);
        }
    }
    private void Chase()
    {
        animator.SetBool("Chasing", true);

        //Calculate direction to player
        Vector3 playerPos = new Vector3(player.position.x, transform.position.y, player.position.z);
        Vector3 direction = playerPos - transform.position;

        //AddForce
        if (rb.velocity.magnitude < maxSpeed)
            rb.AddForce(direction.normalized * speed * Time.deltaTime);

        //Look at player
        transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
    }
    private void Attack()
    {
        if (dead) return;

        //Audio
        GameObject.Find("Audio").GetComponent<Audio>().ShootSound();

        //Instantiate Projectile
        attackPos.LookAt(GameObject.Find("TrackPlayer").transform);
        if (readyToAttack)
        {
        Instantiate(projectile, attackPos.position, Quaternion.identity);
        readyToAttack = false;
        }

        if (!IsInvoking("ResetAttack"))
            Invoke("ResetAttack", timeBetweenAttacks);

        //Look at player
        transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
    }
    private void ResetAttack()
    {
        readyToAttack = true;
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
    }
    private void Die()
    {
        dead = true;
        animator.SetBool("Dead", true);

        Invoke("DestroyParts", 2);
    }
    private void DestroyParts()
    {
        //Say gm that one enemy has been defeated
        gm.enemiesLeft--;
        Destroy(gameObject);
    }

    //Debugging
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, sightRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
