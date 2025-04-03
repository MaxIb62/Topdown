using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rig;
    public float speed = 4;
    Vector3 LookPos;

    public GameObject bulletPrefab; // Prefab de la bala
    public Transform firePoint; // Punto de origen del disparo
    public float bulletSpeed = 10f;

    void Start()
    {
        rig = GetComponent<Rigidbody>();
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

        // Disparar con click izquierdo
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
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
}