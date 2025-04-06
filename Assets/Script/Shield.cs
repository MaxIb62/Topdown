using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public float MaxShield = 100f;
    public float shield;
    public GameObject ShieldCol;

    private float timeSinceLastHit = 0f;
    public float regWait = 20f;     
    public float regSpeed = 10f;     

    void Start()
    {
        shield = MaxShield;
    }

    void Update()
    {
        // Si el escudo está dañado y no ha sido golpeado recientemente
        if (shield < MaxShield)
        {
            timeSinceLastHit += Time.deltaTime;

            if (timeSinceLastHit >= regWait)
            {
                shield += regSpeed * Time.deltaTime;
                shield = Mathf.Min(shield, MaxShield);

                
                if (shield > 0f && ShieldCol != null)
                {
                    Collider colSH = ShieldCol.GetComponent<Collider>();
                    if (colSH != null && !colSH.enabled)
                    {
                        colSH.enabled = true;
                    }
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("bala"))
        {
            DamageShield(50);
            Destroy(other.gameObject);
        }
    }

    public void DamageShield(int amount)
    {
        shield -= amount;
        shield = Mathf.Max(shield, 0); 

        
        timeSinceLastHit = 0f;

        if (shield <= 0)
        {
            if (ShieldCol != null)
            {
                Collider colSH = ShieldCol.GetComponent<Collider>();
                if (colSH != null)
                {
                    colSH.enabled = false;
                }
            }
        }
    }
}
