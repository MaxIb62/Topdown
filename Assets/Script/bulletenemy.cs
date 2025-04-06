using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletenemy : MonoBehaviour
{
    public float lifetime = 3f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
