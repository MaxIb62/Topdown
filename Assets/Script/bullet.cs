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
    // void OnTriggerEnter(Collision other)
    /* {
         if (collision.gameObject.CompareTag("Player")) return; 
         Destroy(gameObject);

         if (collision.gameObject.CompareTag("Enemies"))
         {
             Destroy(collision.gameObject); 
             Destroy(gameObject); 
         }
         else
         {
             Destroy(gameObject); 
         }
     }*/

    void OnTriggerEnter(Collider other)
    {
        {
            if (other.gameObject.CompareTag("Player")) return;
            Destroy(gameObject);

            /*if (other.gameObject.CompareTag("Enemies"))
            {
                Destroy(other.gameObject);
                Destroy(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }*/
        }
    }
}
