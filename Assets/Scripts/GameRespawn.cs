using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRespawn : MonoBehaviour
{
    public float threshold;
    public float respawnDelay = 2.0f; 
    private Vector3 lastCheckpointPosition;

    void Start()
    {
        lastCheckpointPosition = transform.position;
    }

    void FixedUpdate()
    {
        if (transform.position.y < threshold)
        {
            StartCoroutine(RespawnCoroutine());
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CheckPoint"))
        {
            lastCheckpointPosition = other.transform.position;
        }
    }

    IEnumerator RespawnCoroutine()
    {
        yield return new WaitForSeconds(respawnDelay); 
        Respawn();
    }

    void Respawn()
    {
        transform.position = lastCheckpointPosition;
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
}
