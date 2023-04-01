using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSpawner : MonoBehaviour
{
    public GameObject stompParticles;
    public void InstantiateStompParticles(Vector3 position)
    {
        Instantiate(stompParticles, position, Quaternion.identity);
    }
}
