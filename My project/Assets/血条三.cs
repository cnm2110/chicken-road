using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{
    [Header("核心组件")]
    [SerializeField] private Slider healthSlider; // 血条Slider
    [SerializeField] private Image fillImage;    // 红色填充区域
    [SerializeField] private TextMeshProUGUI hpText; // 血量文本

    [Header("血量配置")]
    public int maxHP = 100; // 最大血量
    private int currentHP;  // 当前血量

    [Header("渐变效果")]
    [SerializeField] private Color fullColor = Color.red;    // 满血颜色
    [SerializeField] private Color emptyColor = Color.gray; // 空血颜色


    void Start()
    {
        currentHP = maxHP;
        UpdateHealthBar();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            TakeDamage(10);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            Heal(10);
        }
    }


    // 更新血条和文本
    public void UpdateHealthBar()
    {
        // 计算血量比例 (0~1)
        float hpRatio = (float)currentHP / maxHP;

        // 更新Slider填充
        healthSlider.value = hpRatio;

        // 更新填充颜色渐变（从红到灰）
        fillImage.color = Color.Lerp(emptyColor, fullColor, hpRatio);

        // 更新血量文本
        hpText.text = $"{currentHP}/{maxHP}";
    }

    // 扣血方法（外部调用）
    public void TakeDamage(int damage)
    {
        currentHP = Mathf.Max(0, currentHP - damage);
        UpdateHealthBar();
    }

    // 回血方法（外部调用）
    public void Heal(int healAmount)
    {
        currentHP = Mathf.Min(maxHP, currentHP + healAmount);
        UpdateHealthBar();
    }

}

