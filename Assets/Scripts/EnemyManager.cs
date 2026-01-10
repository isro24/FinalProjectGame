using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;

    public GameObject nextLevelTrigger;

    int aliveEnemy;
    int totalSpawned;
    int maxEnemy;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        nextLevelTrigger.SetActive(false);
    }

    public void Init(int max)
    {
        maxEnemy = max;
        aliveEnemy = 0;
        totalSpawned = 0;
    }

    public void RegisterEnemy()
    {
        aliveEnemy++;
        totalSpawned++;

        Debug.Log($"Register | Alive: {aliveEnemy} | Spawned: {totalSpawned}");
    }

    public void UnregisterEnemy()
    {
        aliveEnemy--;
        Debug.Log($"Unregister | Alive: {aliveEnemy}");

        CheckClear();
    }

    void CheckClear()
    {
        if (totalSpawned >= maxEnemy && aliveEnemy <= 0)
        {
            Debug.Log("ARENA CLEAR → SHOW TELEPORT");
            nextLevelTrigger.SetActive(true);
            Debug.Log("Teleport active = " + nextLevelTrigger.activeSelf);

        }
    }
}
