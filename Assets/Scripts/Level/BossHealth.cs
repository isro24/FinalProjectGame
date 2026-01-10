using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public int maxHP = 500;
    int currentHP;

    void Start()
    {
        currentHP = maxHP;
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;

        if (currentHP <= 0)
            Die();
    }

    void Die()
    {
        Debug.Log("BOSS MATI");
        Destroy(gameObject);
    }
}
