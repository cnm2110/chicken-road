using UnityEngine;
using UnityEngine.UI;        // 旧版 Text
using TMPro;                  // 新版 TextMeshPro（可选的）


public class 金币拾取 : MonoBehaviour
{
    [Header("UI 设置")]
    [Tooltip("请手动拖拽显示金币数的 TextMeshPro 组件")]
    public TMP_Text coinText;   // 不再自动查找，需要手动拖拽

    private int coinCount = 0;

    private void Start()
    {
        // 检查是否已指定，未指定则给出警告
        if (coinText == null)
        {
            Debug.LogWarning("金币拾取脚本：未指定 coinText，请在 Inspector 中手动拖拽金币文本组件！");
        }
        else
        {
            UpdateCoinDisplay();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coin"))   // 注意标签大小写，建议用 "Coin"
        {
            coinCount++;
            UpdateCoinDisplay();
            Destroy(collision.gameObject);
        }
    }

    private void UpdateCoinDisplay()
    {
        if (coinText != null)
        {
            coinText.text = "Coins: " + coinCount;
        }
    }
}
