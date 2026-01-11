using UnityEngine;

public class BossChase : MonoBehaviour
{
    public float moveSpeed = 1.5f;
    public float stopDistance = 2.2f;

    Transform player;
    Rigidbody rb;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        rb = GetComponent<Rigidbody>();

        if (player == null)
            Debug.LogError("[BossChase] PLAYER TIDAK DITEMUKAN");
    }

    void FixedUpdate()
    {
        if (player == null) return;

        float dist = Vector3.Distance(transform.position, player.position);
        if (dist <= stopDistance) return;

        Vector3 dir = (player.position - transform.position).normalized;
        Vector3 move = dir * moveSpeed * Time.fixedDeltaTime;

        rb.MovePosition(rb.position + move);
        transform.forward = dir;
    }
}
