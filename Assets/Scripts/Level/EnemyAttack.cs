using System.Collections;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public int damage = 10;
    public float attackCooldown = 2f;
    public float attackRange = 1.5f;
    public float requestDelay = 1.0f; // ⬅ delay sebelum boleh request

    public GameObject attackIndicator;
    public AudioSource attackAudio;

    PlayerHealth player;
    bool isAttacking;
    bool canRequest = false;

    void Start()
    {
        player = FindFirstObjectByType<PlayerHealth>();

        attackAudio = GetComponent<AudioSource>(); // ⬅ WAJIB

        if (attackIndicator != null)
            attackIndicator.SetActive(false);

        StartCoroutine(EnableRequestAfterDelay());
    }


    IEnumerator EnableRequestAfterDelay()
    {
        yield return new WaitForSeconds(requestDelay);
        canRequest = true;
    }

    void Update()
    {
        if (!canRequest || isAttacking || player == null) return;

        float dist = Vector3.Distance(transform.position, player.transform.position);

        if (dist <= attackRange)
        {
            TryRequestAttack();
        }
    }

    void TryRequestAttack()
    {
        // ❗ request hanya SEKALI sampai selesai attack
        canRequest = false;

        if (EnemyAttackManager.instance.RequestAttack(this))
        {
            StartCoroutine(AttackRoutine());
        }
        else
        {
            // gagal → coba lagi nanti
            StartCoroutine(EnableRequestAfterDelay());
        }
    }

    IEnumerator AttackRoutine()
    {
        isAttacking = true;

        if (attackIndicator != null)
            attackIndicator.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        if (attackAudio != null)
            attackAudio.Play();

        if (player != null)
            player.TakeDamage(damage);

        yield return new WaitForSeconds(attackCooldown);

        if (attackIndicator != null)
            attackIndicator.SetActive(false);

        EnemyAttackManager.instance.FinishAttack(this);

        isAttacking = false;

        // ⏱ setelah attack selesai, baru boleh request lagi
        StartCoroutine(EnableRequestAfterDelay());
    }
}
