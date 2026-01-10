using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHP = 50;
    int currentHP;

    public int CurrentHP => currentHP;

    EnemyAttack enemyAttack;

    void Start()
    {
        currentHP = maxHP;
        enemyAttack = GetComponent<EnemyAttack>();
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        if (currentHP <= 0)
            Die();
    }

    public bool IsLowHP()
    {
        return currentHP <= maxHP * 0.3f;
    }


    void Die()
    {
        if (EnemyManager.instance != null)
            EnemyManager.instance.UnregisterEnemy();

        Destroy(gameObject);
    }

}
