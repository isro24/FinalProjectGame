using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHP = 50;
    int currentHP;

    EnemyAttack enemyAttack;

    void Start()
    {
        currentHP = maxHP;
        enemyAttack = GetComponent<EnemyAttack>(); // ambil EnemyAttack di root
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;

        if (currentHP <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // 🔑 LEPAS SLOT ATTACK JIKA ENEMY MATI SAAT MENYERANG
        if (enemyAttack != null && EnemyAttackManager.instance != null)
        {
            EnemyAttackManager.instance.FinishAttack(enemyAttack);
        }

        Destroy(gameObject);
    }
}
