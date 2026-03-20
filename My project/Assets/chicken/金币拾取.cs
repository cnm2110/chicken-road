using UnityEngine;
using UnityEngine.UI;        // 旧版 Text
using TMPro;                  // 新版 TextMeshPro（可选的）

public class 金币拾取 : MonoBehaviour
{
    [Header("UI 设置")]
    [Tooltip("如果不拖拽，脚本会自动在场景中查找")]
    public Text coinText;          // 旧版 Text 组件
    public TMP_Text coinTMPText;   // 新版 TextMeshPro 组件（用哪个就留哪个）

    private int coinCount = 0;

    private void Start()
    {
        // 自动查找 UI 组件（如果没手动赋值）
        if (coinText == null && coinTMPText == null)
        {
            // 先尝试找旧版 Text
            coinText = GameObject.FindObjectOfType<Text>();
            // 如果没找到，再尝试找新版 TextMeshPro
            if (coinText == null)
                coinTMPText = GameObject.FindObjectOfType<TMP_Text>();

            if (coinText == null && coinTMPText == null)
                Debug.LogWarning("⚠️ 没有在场景中找到任何 Text 或 TextMeshPro 组件，金币数将不会显示。");
            else
                UpdateCoinDisplay();
        }
        else
        {
            UpdateCoinDisplay();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 用标签检测，更安全
        if (collision.CompareTag("Coin"))
        {
            coinCount++;
            UpdateCoinDisplay();
            Destroy(collision.gameObject);
        }
    }

    private void UpdateCoinDisplay()
    {
        if (coinText != null)
            coinText.text = "Coins: " + coinCount;
        else if (coinTMPText != null)
            coinTMPText.text = "Coins: " + coinCount;
    }
}
