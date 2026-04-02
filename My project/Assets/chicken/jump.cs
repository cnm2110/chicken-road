using UnityEngine;

public class Jump : MonoBehaviour
{
    public float jumpForce = 7f;
    public int maxJumps = 2;
    private int jumpsLeft;
    private Rigidbody2D rb;
    private bool hasResetBelow = false;  // 是否已经在下方重置过

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        jumpsLeft = maxJumps;
    }

    void Update()
    {
        // 当角色 Y 坐标低于 -3.5 且还未重置过时，重置跳跃次数
        if (transform.position.y <= -3.5f && !hasResetBelow)
        {
            jumpsLeft = maxJumps;
            hasResetBelow = true;  // 标记已重置，避免每帧重置
        }

        // 如果角色重新上升到 -3.5 以上，重置标志，允许下次下落时再次重置
        if (transform.position.y > -3.5f)
        {
            hasResetBelow = false;
        }

        if (Input.GetKeyDown(KeyCode.Space) && jumpsLeft > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpsLeft--;
        }
    }
}