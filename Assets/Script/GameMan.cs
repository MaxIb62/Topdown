using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMan : MonoBehaviour
{
    public GameObject playerPrefab; 
    public Transform spawnPoint;

    public static GameMan instance;

    public int DamageNumber = 50;
    public int MinDmg = 5;
    public float IntervalDmg = 9f;
    private float timer = 0f;

    void Start()
    {
        if (playerPrefab != null && spawnPoint != null)
        {
            Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
        }
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= IntervalDmg)
        {
            DamageNumber = Mathf.Max(MinDmg, DamageNumber - 5);
            timer = 0f;
        }
    }

    public int GetNumberDamage()
    {
        return DamageNumber;
    }
}
