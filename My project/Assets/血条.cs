using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [Header("血量设置")]
    public float maxHealth = 100f;          // 最大血量
    private float currentHealth;            // 当前血量

    [Header("UI 组件")]
    public Slider healthSlider;             // 血条滑动条（从场景拖入）

    [Header("受伤无敌")]
    public float invincibilityDuration = 1f; // 受伤后无敌时间（秒）
    private float invincibilityTimer = 0f;   // 无敌倒计时

    void Start()
    {
        currentHealth = maxHealth;
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }
    }

    void Update()
    {
        // 更新无敌倒计时
        if (invincibilityTimer > 0f)
        {
            invincibilityTimer -= Time.deltaTime;
        }

        // ========== 以下为测试用按键，正式版可以删除 ==========
        // 按 Q 扣血 20，按 E 回血 20（方便测试）
        if (Input.GetKeyDown(KeyCode.Q))
        {
            TakeDamage(20f);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Heal(20f);
        }
        // =================================================
    }

    // 碰撞检测（普通碰撞，非触发器）
    // 注意：必须双方都有 Collider2D，且至少一方有 Rigidbody2D
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 检查碰到的是否为障碍物（需要障碍物带有 "Obstacle" 标签）
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            // 无敌保护：只有计时器归零时才扣血
            if (invincibilityTimer <= 0f)
            {
                TakeDamage(20f);                // 每次碰撞扣 20 血，可自行修改
                invincibilityTimer = invincibilityDuration;  // 重置无敌计时器

                // 可选：碰撞后销毁障碍物（避免连续多次扣血）
                // Destroy(collision.gameObject);
            }
        }
    }

    // 公开的扣血方法
    public void TakeDamage(float damage)
    {
        if (damage <= 0) return;

        currentHealth -= damage;
        if (currentHealth < 0) currentHealth = 0;
        UpdateHealthUI();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // 回血方法
    public void Heal(float amount)
    {
        if (amount <= 0) return;

        currentHealth += amount;
        if (currentHealth > maxHealth) currentHealth = maxHealth;
        UpdateHealthUI();
    }

    // 更新血条显示
    private void UpdateHealthUI()
    {
        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;
        }
    }

    // 死亡处理
    private void Die()
    {
        Debug.Log(gameObject.name + " 已死亡");
        // 这里可以添加死亡动画、重新开始场景等逻辑
        Destroy(gameObject);
    }
}
