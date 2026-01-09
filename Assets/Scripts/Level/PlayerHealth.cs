using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHP = 100;
    int currentHP;

    public int CurrentHP => currentHP;

    void Start()
    {
        currentHP = maxHP;
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        currentHP = Mathf.Max(0, currentHP);
    }
}
