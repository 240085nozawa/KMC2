using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    public Vector3 goalPosition = new Vector3(20, 1, 20); // ゴール座標（変更可能）
    public float requiredTime = 1.0f; // 何秒経過したらシーン移動するか（変更可能）

    private float stayTime = 0f; // プレイヤーがゴールに留まった時間
    private bool playerAtGoal = false; // プレイヤーがゴールにいるか
    private Transform playerTransform; // プレイヤーのTransform

    // Start is called before the first frame update
    void Start()
    {
        // プレイヤーオブジェクトを探して Transform を取得
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
        else
        {
            Debug.LogError("[Goal] Playerタグのついたオブジェクトが見つかりません！");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // プレイヤーオブジェクトを探して Transform を取得
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
        else
        {
            Debug.LogError("[Goal] Playerタグのついたオブジェクトが見つかりません！");
        }

        if (playerTransform == null) return; // プレイヤーが見つからなければ処理しない

        if (IsAtGoal(playerTransform.position))
        {
            if (!playerAtGoal)
            {
                Debug.Log("[Goal] プレイヤーがゴール座標に到達！");
                playerAtGoal = true;
                stayTime = 0f; // カウントリセット
            }

            stayTime += Time.deltaTime;
            Debug.Log($"[Goal] 現在の滞在時間: {stayTime}/{requiredTime}");

            if (stayTime >= requiredTime)
            {
                StageTracker.LastPlayedStage = SceneManager.GetActiveScene().name;

                Debug.Log("[Goal] シーン移動を実行！");
                SceneManager.LoadScene("Clear"); // シーン移動（シーン名は変更可）
            }
        }
        else
        {
            playerAtGoal = false;
            stayTime = 0f; // ゴールから出たらリセット
        }
    }

    // プレイヤーがゴール座標にいるか判定
    private bool IsAtGoal(Vector3 playerPos)
    {
        return Vector3.Distance(playerPos, goalPosition) < 0.5f; // 誤差を考慮
    }
}
