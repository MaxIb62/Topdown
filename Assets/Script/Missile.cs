using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public float DamageRadio = 5f;
    public int Damage = 30;

    private void OnCollisionEnter(Collision collision)
    {
        Collider[] hitCol = Physics.OverlapSphere(transform.position, DamageRadio);
        foreach (Collider close in hitCol)
        {
            if (close.CompareTag("Enemies"))
            {
                enemymovement enemie = close.GetComponent<enemymovement>();
                if(enemie != null)
                {
                    enemie.Damage(Damage);
                }
            }
        }

        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Player")) return;
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere (transform.position, DamageRadio);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
