using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public float maxHealth = 100f;     // 最大血量
    private float currentHealth;        // 当前血量

    public Slider healthSlider;         // 把场景里的 Slider 拖进来

    void Start()
    {
        currentHealth = maxHealth;      // 一开始满血
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }
    }

    void Update()
    {
        // 按 Q 键扣血 20
        if (Input.GetKeyDown(KeyCode.Q))
        {
            TakeDamage(20f);
        }

        // 按 E 键回血 20
        if (Input.GetKeyDown(KeyCode.E))
        {
            Heal(20f);
        }
    }

    // 扣血方法
    public void TakeDamage(float damage)
    {
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
        currentHealth += amount;
        if (currentHealth > maxHealth) currentHealth = maxHealth;
        UpdateHealthUI();
    }

    // 更新血条显示
    void UpdateHealthUI()
    {
        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;
        }
    }

    // 死亡处理
    void Die()
    {
        // 可以改成你需要的效果，比如播放动画、显示文字等
        Debug.Log(gameObject.name + " 已死亡");
        Destroy(gameObject);
    }
}
