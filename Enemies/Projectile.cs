using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage;
    public bool dontDestroy = false;
    public float speed;

    Vector3 direction;
    private void Start()
    {
        direction = GameObject.Find("TrackPlayer").transform.position - transform.position;
    }
    private void Update()
    {
        transform.Translate(direction.normalized * Time.deltaTime * speed);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerMovement>().TakeDamage(damage);
            SelfDestruction();
        }
    }
    public void SelfDestruction()
    {
        if (!dontDestroy)
        Destroy(gameObject);
    }
}
