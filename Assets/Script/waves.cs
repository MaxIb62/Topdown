using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waves : MonoBehaviour
{
    public Transform enemyPrefab;

    public Transform SpawnPoint;
    public Transform SpawnPoint2;
    public Transform SpawnPoint3;
    public Transform SpawnPoint4;

    public float timeBetweenWaves = 5f;
    private float Countdown = 2f;
    private int waveIndex = 0;

    private Transform[] SpawnPoints;
    // Start is called before the first frame update
    void Start()
    {
        SpawnPoints = new Transform[] { SpawnPoint, SpawnPoint2, SpawnPoint3, SpawnPoint4 };
    }

    // Update is called once per frame
    void Update()
    {
        if (Countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            Countdown = timeBetweenWaves;
        }

        Countdown -= Time.deltaTime;
    }

    IEnumerator SpawnWave()
    {
        for (int i = 0; i < waveIndex; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(0.5f);
        }
        waveIndex++;
    }

    void SpawnEnemy()
    {
        int randomPoint = Random.Range(0, SpawnPoints.Length);
        Transform spawn = SpawnPoints[randomPoint];

        Instantiate(enemyPrefab, spawn.position, spawn.rotation);
    }

}