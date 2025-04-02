using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rig;

    public float speed = 4;

    Vector3 LookPos;
    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody>();
        
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100))
        {
            LookPos = hit.point;
        }

        Vector3 LookDir = LookPos - transform.position;
        LookDir.y = 0;

        transform.LookAt(transform.position + LookDir, Vector3.up);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, 0, vertical);

        rig.velocity = (movement * speed / Time.deltaTime);
    }
}
