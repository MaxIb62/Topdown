using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine   .UI;


public class enemymovement : MonoBehaviour

{
    public float detectionRadius = 10f;
    public float speed = 5f;

    private Transform target;

    public float HealthMax = 100f;
    public float health;

    public float MaxShield = 100f;
    public float shield;

    public Slider healthSlider;   
    public Slider shieldSlider;
    void Start()
    {
        health = HealthMax;
        shield = MaxShield;

        if (healthSlider != null)
            healthSlider.maxValue = HealthMax;

        if (shieldSlider != null)
            shieldSlider.maxValue = MaxShield;
    }
    void Update()
    {
        DetectPlayer();

        if (target != null)
        {
            FollowTarget();
        }

        UpdateUI();
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


        target = null;
    }

    void FollowTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        direction.y = 0; // Evita movimiento vertical
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("bala"))
        {
            int damage = GameMan.instance.GetNumberDamage();
            Damage(damage);
            Destroy(other.gameObject);
        }
    }

    void Damage(int amount)
    {
        if (shield > 0)
        {
            shield -= amount;
            if (shield < 0)
            {
                // Si sobra daño después de que el escudo llega a 0, se aplica a la vida
                health += shield; // shield es negativo aquí
                shield = 0;
            }
        }
        else
        {
            health -= amount;
        }

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    void UpdateUI()
    {
   
        if (healthSlider != null)
            healthSlider.value = health;

        if (shieldSlider != null)
            shieldSlider.value = shield;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
