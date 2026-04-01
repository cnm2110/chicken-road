using UnityEngine;
using UnityEngine.SceneManagement; // 必须引入场景管理命名空间

public class ShopShelfController : MonoBehaviour
{
    [Tooltip("空槽")]
    public GameObject shelfObject;
    private void Start()
    {
        // 初始状态：隐藏货架
        shelfObject.SetActive(false);
    }
    // 触发检测（2D）
    private void OnTriggerEnter2D(Collider2D other)
    {
        // 检测是否是玩家
        if (other.CompareTag("player"))
        {
            Debug.Log("玩家触发商店，显示货架！");
            // 显示货架
            shelfObject.SetActive(true);
        }
    }
}