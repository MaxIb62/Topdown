using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public float lifetime = 3f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    // Update is called once per frame
   void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")) return; // Evitar autodestrucción al tocar al jugador
        Destroy(gameObject);
    }
}
