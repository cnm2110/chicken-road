
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMoveRigidbody2D : MonoBehaviour
{
    [Tooltip("移动速度")]
    public float speed = 5f;
    [Tooltip("终点X坐标（到达此位置返回起点）")]
    public float endX = 110.5f; // 替换为你场景的实际终点X值
    [Tooltip("起点X坐标（重置位置）")]
    public float startX = -20.4f; // 替换为你场景的实际起点X值

    private Rigidbody2D rb;

    void Start()
    {
        // 获取刚体组件
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // 1. 正常移动（向右）
        rb.velocity = new Vector2(speed, rb.velocity.y);

        // 2. 检测是否到达终点
        if (transform.position.x >= endX)
        {
            // 3. 重置到起点（位置+速度同时重置）
            transform.position = new Vector2(startX, transform.position.y);
            rb.velocity = new Vector2(speed, rb.velocity.y); // 保持移动方向，避免停滞
        }
    }
}
