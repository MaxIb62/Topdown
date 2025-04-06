using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class enemymovement : MonoBehaviour
{
    public float FollowRad = 10f;
    public float speed = 5f;

    private Transform target;

    public float HealthMax = 100f;
    public float health;

    public float MaxShield = 100f;
    public float shield;

    public Slider healthSlider;
    public Slider shieldSlider;

    public GameObject coinPref;
    [Range(0f, 1f)]
    public float posibilityDrop = 0.2f;

    public GameObject bulletEnemy;
    public Transform FirePoint;
    public float bulletSpeed = 10f;
    public float shootInterval = 1.5f;

    private float ShootTimer = 0f;


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

        if (target != null)
        {
            FollowTarget();

            ShootTimer += Time.deltaTime;
            if(ShootTimer >= shootInterval)
            {
                shootPlayer();
                ShootTimer = 0f;
            }
        }

        UpdateUI();
    }

    void shootPlayer()
    {
        if (bulletEnemy != null && FirePoint != null && target != null)
        {
            GameObject bulletEnem = Instantiate(bulletEnemy, FirePoint.position, Quaternion.identity);
            Rigidbody RB = bulletEnem.GetComponent<Rigidbody>();

            Vector3 shootDir = (target.position - FirePoint.position).normalized;
            RB.velocity = shootDir * bulletSpeed;

            bulletEnem.transform.forward = shootDir;
        }
    }

    void DetectPlayer()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, FollowRad);

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
        direction.y = 0; 
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

    public void Damage(int amount)
    {
        if (shield > 0)
        {
            shield -= amount;
            if (shield < 0)
            { 
                health += shield; 
                shield = 0;
            }
        }
        else
        {
            health -= amount;
        }

        if (health <= 0)
        {
            DropCoin();
            Destroy(gameObject);
        }
    }

    void DropCoin()
    {
        float randm = Random.value;
        if (randm <= posibilityDrop && coinPref !=null)
        {
            Instantiate(coinPref, transform.position + Vector3.up * 0.5f, Quaternion.identity);
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
        Gizmos.DrawWireSphere(transform.position, FollowRad);
    }
}
