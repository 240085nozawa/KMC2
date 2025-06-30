using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal2 : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [Header("ゴール設定")]
    public Vector3 goalPosition = new Vector3(20, 1, 20); // ゴール座標（インスペクターで設定）
    public float waitTime = 1.0f;                         // シーン遷移までの待機時間

    private float stayTime = 0f;          // ゴール地点に留まった時間
    private bool playerAtGoal = false;    // プレイヤーがゴールにいるかどうか
    private Transform playerTransform;    // プレイヤーの Transform

    void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
        else
        {
            Debug.LogError("[Goal2] Playerタグのついたオブジェクトが見つかりません");
        }
    }

    void Update()
    {
        if (playerTransform == null) return;

        if (IsAtGoal(playerTransform.position))
        {
            stayTime += Time.deltaTime;
            Debug.Log($"[Goal2] ゴール内に滞在中: {stayTime:F2}/{waitTime}");

            if (!playerAtGoal && stayTime >= waitTime)
            {
                Debug.Log("[Goal2] シーン移動実行：Clear");
                SceneManager.LoadScene("Clear");
                playerAtGoal = true;
            }
        }
        else
        {
            stayTime = 0f;
            playerAtGoal = false;
        }
    }

    // プレイヤーがゴール位置にいるかの判定（XZ誤差0.5以内、Yは完全一致）
    private bool IsAtGoal(Vector3 pos)
    {
        Vector2 playerXZ = new Vector2(pos.x, pos.z);
        Vector2 goalXZ = new Vector2(goalPosition.x, goalPosition.z);
        return Vector2.Distance(playerXZ, goalXZ) < 0.5f && Mathf.Approximately(pos.y, goalPosition.y);
    }
}
