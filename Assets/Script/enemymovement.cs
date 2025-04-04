using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemymovement : MonoBehaviour
{
    public float detectionRadius = 10f; // Radio de detección
    public float speed = 5f;            // Velocidad de movimiento

    private Transform target;           // Jugador detectado

    void Update()
    {
        DetectPlayer();

        if (target != null)
        {
            FollowTarget();
        }
    }

    void DetectPlayer()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius);

        foreach (Collider col in colliders)
        {
            if (col.CompareTag("Player"))
            {
                target = col.transform;
                return;
            }
        }

        // Si no encuentra jugador en el rango
        target = null;
    }

    void FollowTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        direction.y = 0; // Evita movimiento vertical
        transform.position += direction * speed * Time.deltaTime;
    }

    // Visualiza el área de detección en la escena
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
