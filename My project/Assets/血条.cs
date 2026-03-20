using UnityEngine;
using UnityEngine.UI;
using TMPro; // 注意：原代码缺少这行，需要补上才能使用 TMP_Text

public class HealthLogic : MonoBehaviour
{
    // 血条红色填充部分（Image组件）
    public Image RedBar;
    // 最大血量与当前血量
    private float maxHealth, currentHealth;
    // 显示血量文字的 TextMeshPro 组件
    public TMP_Text healthText;

    void Start()
    {
        // 初始化最大血量为100
        maxHealth = 100f;
        // 当前血量等于最大血量
        currentHealth = maxHealth;
        // 初始化血量文字显示为 "100/100"
        healthText.text = $"{maxHealth}/{maxHealth}";
    }

    void Update()
    {
        // 按下 Q 键减少20点血量
        if (Input.GetKeyDown(KeyCode.Q)) ChangeHealth(-20f);
        // 按下 R 键增加30点血量
        if (Input.GetKeyDown(KeyCode.R)) ChangeHealth(30f);
    }

    /// <summary>
    /// 改变血量的核心方法
    /// </summary>
    /// <param name="delta">血量变化值（负数为扣血，正数为回血）</param>
    void ChangeHealth(float delta)
    {
        // 限制血量在 0 ~ maxHealth 之间，防止溢出
        currentHealth = Mathf.Clamp(currentHealth + delta, 0, maxHealth);
        // 更新血条填充比例
        RedBar.fillAmount = currentHealth / maxHealth;
        // 更新血量文字显示
        healthText.text = $"{currentHealth}/{maxHealth}";
    }
}