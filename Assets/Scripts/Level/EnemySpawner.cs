using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;

    public int maxEnemy = 20;          // batas total enemy
    public int startAmount = 2;        // wave awal
    public float spawnInterval = 2f;   // jeda antar wave
    public float spawnRadius = 15f;

    int spawnedCount = 0;
    int currentWaveAmount;

    void Start()
    {
        currentWaveAmount = startAmount;
        EnemyManager.instance.Init(maxEnemy);
        StartCoroutine(SpawnWave());
    }

    IEnumerator SpawnWave()
    {
        while (spawnedCount < maxEnemy)
        {
            int spawnThisWave = Mathf.Min(currentWaveAmount, maxEnemy - spawnedCount);

            for (int i = 0; i < spawnThisWave; i++)
            {
                Vector3 randomXZ = transform.position + new Vector3(
                    Random.Range(-spawnRadius, spawnRadius),
                    10f,
                    Random.Range(-spawnRadius, spawnRadius)
                );

                if (Physics.Raycast(randomXZ, Vector3.down, out RaycastHit hit, 50f))
                {
                    Vector3 spawnPos = hit.point;
                    spawnPos.y += 1f; 

                    GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);

                    EnemyManager.instance.RegisterEnemy();

                    EnemyAI ai = enemy.GetComponent<EnemyAI>();
                    if (ai != null)
                    {
                        ai.formationIndex = spawnedCount % ai.formationTotal;
                    }

                    spawnedCount++;
                }
            }

            currentWaveAmount *= 2;
            yield return new WaitForSeconds(spawnInterval);
        }
    }

}
