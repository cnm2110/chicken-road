using UnityEngine;
using System.Collections;
public class CoinSpawner : MonoBehaviour
{
    [Header("金币设置")]
    [Tooltip("金币预制体")]
    public GameObject coinPrefab;
    [Tooltip("同时存在的最大金币数量")]
    public int maxCoinCount = 5;
    [Tooltip("每次刷新的金币数量")]
    public int spawnPerTime = 2;
    [Header("刷新范围设置")]
    [Tooltip("刷新区域中心（默认是脚本挂载物体的位置）")]
    public Vector2 spawnAreaCenter;
    [Tooltip("刷新区域宽度（X轴范围）")]
    public float spawnAreaWidth = 10f;
    [Tooltip("刷新区域高度（Y轴范围）")]
    public float spawnAreaHeight = 5f;
    [Header("刷新间隔设置")]
    [Tooltip("两次刷新的间隔时间（秒）")]
    public float spawnInterval = 3f;
    [Tooltip("金币生成后多久自动消失（秒，0为不消失）")]
    public float coinLifeTime = 10f;
    [Header("防重叠设置")]
    [Tooltip("金币之间的最小距离，避免堆叠")]
    public float minDistanceBetweenCoins = 1f;
    [Tooltip("生成时的最大尝试次数，避免死循环")]
    public int maxSpawnAttempts = 10;
    // 当前场景中的金币列表
    private System.Collections.Generic.List<GameObject> currentCoins = new System.Collections.Generic.List<GameObject>();
    private void Start()
    {
        // 如果没手动设置中心，默认用脚本挂载物体的位置
        if (spawnAreaCenter == Vector2.zero)
        {
            spawnAreaCenter = transform.position;
        }
        // 启动自动刷新协程
        StartCoroutine(AutoSpawnCoins());
    }
    // 自动定时刷新协程
    private IEnumerator AutoSpawnCoins()
    {
        while (true)
        {
            // 清理已经被销毁的金币
            currentCoins.RemoveAll(coin => coin == null);
            // 如果当前金币数没到上限，就刷新
            if (currentCoins.Count < maxCoinCount)
            {
                SpawnCoins(spawnPerTime);
            }
            // 等待指定间隔后再刷新
            yield return new WaitForSeconds(spawnInterval);
        }
    }
    // 生成指定数量的金币
    public void SpawnCoins(int amount)
    {
        int spawned = 0;
        int attempts = 0;
        while (spawned < amount && attempts < maxSpawnAttempts * amount)
        {
            // 生成随机坐标
            Vector2 randomPos = GetRandomPositionInArea();
            // 检查是否和已有金币重叠
            if (!IsPositionOverlap(randomPos))
            {
                // 实例化金币
                GameObject newCoin = Instantiate(coinPrefab, randomPos, Quaternion.identity);
                currentCoins.Add(newCoin);
                // 如果设置了生命周期，自动销毁
                if (coinLifeTime > 0)
                {
                    Destroy(newCoin, coinLifeTime);
                }
                spawned++;
            }
            attempts++;
        }
    }
    // 在范围内生成随机坐标
    private Vector2 GetRandomPositionInArea()
    {
        float randomX = Random.Range(
            spawnAreaCenter.x - spawnAreaWidth / 2,
            spawnAreaCenter.x + spawnAreaWidth / 2
        );
        float randomY = Random.Range(
            spawnAreaCenter.y - spawnAreaHeight / 2,
            spawnAreaCenter.y + spawnAreaHeight / 2
        );
        return new Vector2(randomX, randomY);
    }
    // 检查坐标是否和已有金币重叠
    private bool IsPositionOverlap(Vector2 pos)
    {
        foreach (GameObject coin in currentCoins)
        {
            if (coin != null)
            {
                float distance = Vector2.Distance(pos, coin.transform.position);
                if (distance < minDistanceBetweenCoins)
                {
                    return true; // 重叠，不能生成
                }
            }
        }
        return false; // 不重叠，可以生成
    }
    // 🔹 编辑器可视化：在Scene窗口画刷新范围
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        // 画矩形范围
        Gizmos.DrawWireCube(
            spawnAreaCenter,
            new Vector3(spawnAreaWidth, spawnAreaHeight, 0)
        );
    }
    // 🔹 手动触发刷新（可绑定按钮）
    public void ManualSpawn()
    {
        SpawnCoins(spawnPerTime);
    }
    // 🔹 清空所有金币
    public void ClearAllCoins()
    {
        foreach (GameObject coin in currentCoins)
        {
            if (coin != null)
            {
                Destroy(coin);
            }
        }
        currentCoins.Clear();
    }
}