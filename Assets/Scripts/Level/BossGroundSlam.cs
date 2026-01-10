using System.Collections;
using UnityEngine;

public class BossGroundSlam : MonoBehaviour
{
    [Header("AOE Settings")]
    public float slamRadius = 3f;
    public int slamDamage = 30;
    public float slamCooldown = 4f;

    [Header("Indicator")]
    public GameObject slamIndicator;
    public float warnDuration = 1.5f;
    public float blinkSpeed = 0.2f;

    [Header("Audio")]
    public AudioSource slamAudio;

    bool canSlam = true;
    Transform player;

    void Start()
    {
        player = FindFirstObjectByType<PlayerHealth>()?.transform;

        if (slamIndicator != null)
            slamIndicator.SetActive(false);
    }

    void Update()
    {
        if (!canSlam || player == null) return;

        float dist = Vector3.Distance(transform.position, player.position);
        if (dist <= slamRadius + 1f)
        {
            StartCoroutine(SlamRoutine());
        }
    }

    IEnumerator SlamRoutine()
    {
        canSlam = false;

        // POSISIKAN INDICATOR DI BAWAH PLAYER
        slamIndicator.transform.position =
            new Vector3(player.position.x, 0.01f, player.position.z);

        slamIndicator.SetActive(true);

        // BLINK WARNING
        float t = 0f;
        while (t < warnDuration)
        {
            slamIndicator.SetActive(!slamIndicator.activeSelf);
            yield return new WaitForSeconds(blinkSpeed);
            t += blinkSpeed;
        }

        slamIndicator.SetActive(true);

        // SLAM!
        if (slamAudio != null)
            slamAudio.Play();

        DealDamage();

        yield return new WaitForSeconds(0.3f);
        slamIndicator.SetActive(false);

        yield return new WaitForSeconds(slamCooldown);
        canSlam = true;
    }

    void DealDamage()
    {
        Collider[] hits = Physics.OverlapSphere(
            slamIndicator.transform.position,
            slamRadius
        );

        foreach (Collider hit in hits)
        {
            PlayerHealth player = hit.GetComponent<PlayerHealth>();
            if (player != null)
            {
                player.TakeDamage(slamDamage);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, slamRadius);
    }
}
