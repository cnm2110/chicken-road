using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Coin : MonoBehaviour
{
    [Header("金币基础设置")]
    [Tooltip("金币被拾取后消失的时间（秒）")]
    public float respawnTime = 5f;

    [Tooltip("金币是否可以被拾取")]
    private bool isCollectible = true;

    private SpriteRenderer spriteRenderer;
    private Collider2D coinCollider;

    public static int coinCount = 0;
    // 新增：引用UI文本组件（显示coins:0）
    public TMP_Text coinText; // 若使用TextMeshPro，请改为 public TMP_Text coinText;

    private void Awake()
    {
        // 获取组件
        spriteRenderer = GetComponent<SpriteRenderer>();
        coinCollider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 只响应玩家触发（请确保玩家Tag为"Player"）
        if (other.CompareTag("Player") && isCollectible)
        {
            CollectCoin();
        }
    }

    /// <summary>
    /// 拾取金币逻辑
    /// </summary>
    private void CollectCoin()
    {
        // 标记为不可拾取
        isCollectible = false;
        // 隐藏金币
        spriteRenderer.enabled = false;
        // 关闭碰撞体，避免重复触发
        coinCollider.enabled = false;
        // 新增：金币数+1
        coinCount++;
        // 新增：更新UI显示
        UpdateCoinUI();

        // 调用重生方法
        Invoke(nameof(RespawnCoin), respawnTime);
    }

    /// <summary>
    /// 金币重生逻辑
    /// </summary>
    private void RespawnCoin()
    {
        // 恢复显示
        spriteRenderer.enabled = true;
        // 恢复碰撞体
        coinCollider.enabled = true;
        // 标记为可拾取
        isCollectible = true;
    }
    private void UpdateCoinUI()
    {
        if (coinText != null)
        {
            coinText.text = $"coins:{coinCount}";
        }
    }
}