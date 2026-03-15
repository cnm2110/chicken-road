using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMoveRigidbody2D : MonoBehaviour
{
    [Tooltip("移动速度")]
    public float speed = 5f;

    private Rigidbody2D rb;

    void Start()
    {
        // 获取刚体组件
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        // 设置刚体速度：向右移动
        rb.velocity = new Vector2(speed, rb.velocity.y);

        // 如果想保持当前垂直速度不变（比如受重力影响），只改水平方向
        // rb.velocity = new Vector2(speed, rb.velocity.y);
    }
}
