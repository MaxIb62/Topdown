using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    Rigidbody rig;
    public float speed = 4;
    Vector3 LookPos;

    public GameObject bulletPrefab; 
    public Transform firePoint; 
    public float bulletSpeed = 10f;

    public GameObject missilPref;
    public float missileSpeed = 30f;

    public float maxHealth = 100f;
    public float maxShield = 100f;
    public float health;
    public float shield;

    public Slider healthSlider;
    public Slider shieldSlider;

    public GameObject GameOverpanel;


    void Start()
    {
        rig = GetComponent<Rigidbody>();

        transform.position = new Vector3(0, 1, 0);

        
        rig.velocity = Vector3.zero;
        rig.angularVelocity = Vector3.zero;

        health = maxHealth;
        shield = maxShield;

        if (healthSlider != null)
            healthSlider.maxValue = maxHealth;

        if (shieldSlider != null)
            shieldSlider.maxValue = maxShield;
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100))
        {
            LookPos = hit.point;
        }

        Vector3 LookDir = LookPos - transform.position;
        LookDir.y = 0;

        if (LookDir != Vector3.zero)
        {
            transform.LookAt(transform.position + LookDir, Vector3.up);
        }

        
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }

        if (Input.GetMouseButtonDown(1))
        {
            ShootMissile();
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            
            Coin.instance.TryBuyHealth(10, 30f, this);
        }

        UpdateUI();
    }

    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, 0, vertical);

        if (movement.magnitude > 1)
            movement.Normalize();

        rig.velocity = movement * speed;
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();

        Vector3 shootDirection = (LookPos - firePoint.position).normalized;
        bulletRb.velocity = shootDirection * bulletSpeed;
    }

    void ShootMissile()
    {
        GameObject missil = Instantiate(missilPref, firePoint.position, Quaternion.identity);
        Rigidbody missilRb = missil.GetComponent<Rigidbody>();

        Vector3 shootDirection = (LookPos - firePoint.position).normalized;
        missilRb.velocity = shootDirection * missileSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other. CompareTag ("bulletEnemy"))
        {
            int damage = 10;
            DamagePlayer(damage);
            Destroy(other.gameObject);
        }
    }

    void DamagePlayer(int amount)
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
            Destroy(gameObject);
            if (GameOverpanel != null)
                GameOverpanel.SetActive(true);

            Time.timeScale = 0f;
            
        }
    }

    public void Heal(float amount)
    {
        health += amount;
        if (health > maxHealth)
            health = maxHealth;
    }

    void UpdateUI()
    {

        if (healthSlider != null)
            healthSlider.value = health;

        if (shieldSlider != null)
            shieldSlider.value = shield;
    }
}