using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpikeDamage : MonoBehaviour
{
    [SerializeField] private int damagePerTouch = 30; // 每次触碰的伤害值

    // 当有物体进入触发区域时调用
    private void OnTriggerEnter2D(Collider2D other)
    {
        // 尝试获取目标物体上的 Health 脚本
        Health targetHealth = other.GetComponent<Health>();

        if (targetHealth != null)
        {
            // 调用扣血方法
            targetHealth.TakeDamage(damagePerTouch);
        }
    }
}

