using UnityEngine;

public class SpawnCharacter : MonoBehaviour
{
    [SerializeField] GameObject[] characterPrefabs;
    [SerializeField] Transform spawnPoint;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (characterPrefabs == null || characterPrefabs.Length == 0)
        {
            Debug.LogError("Character Prefabs belum diisi");
            return;
        }
        int id = GameManager.instance.GetCharacter();

        if (id < 0 || id >= characterPrefabs.Length)
        {
            Debug.LogWarning("Character ID invalid, fallback ke 0");
            id = 0;
        }

        Vector3 spawnPos = spawnPoint.position + Vector3.up * 0.5f;

        Instantiate(characterPrefabs[id], spawnPos, spawnPoint.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
