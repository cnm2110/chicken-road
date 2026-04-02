using UnityEngine;
using System.Collections; // 必须引入，用于协程
using UnityEngine.SceneManagement;
public class ShopShelfController : MonoBehaviour
{
    [Tooltip("空槽")]
    public GameObject shelfObject;
    // 用于控制自动关闭的协程，防止重复触发
    private Coroutine autoCloseCoroutine;
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

            // 如果已经有倒计时在跑，先停掉，避免重复计时
            if (autoCloseCoroutine != null)
            {
                StopCoroutine(autoCloseCoroutine);
            }
            // 显示货架、暂停游戏
            shelfObject.SetActive(true);
            Time.timeScale = 0f;
            // 启动2秒后自动关闭的协程
            autoCloseCoroutine = StartCoroutine(AutoCloseShopAfterDelay(2f));
        }
    }
    // 协程：延时后自动关闭商店
    IEnumerator AutoCloseShopAfterDelay(float delaySeconds)
    {
        // WaitForSecondsRealtime 不受 Time.timeScale=0 影响，是关键！
        yield return new WaitForSecondsRealtime(delaySeconds);
        // 关闭商店、恢复游戏
        CloseShop();
    }
    // 统一的关闭商店方法，方便复用
    public void CloseShop()
    {
        shelfObject.SetActive(false);
        Time.timeScale = 1f;
        // 清空协程引用
        autoCloseCoroutine = null;
        Debug.Log("商店自动关闭，游戏恢复");
    }
}