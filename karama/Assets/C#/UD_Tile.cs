using UnityEngine;
using System.Collections;

public class UD_Tile : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float moveTime = 2f;         // 上昇・下降にかける時間（変更可）
    public float upperY = 10f;          // 上階のY座標
    public float lowerY = 0f;           // 下階のY座標

    private bool isMoving = false;

    // プレイヤーを受け取って一緒に移動開始
    public void StartLift(GameObject player)
    {
        if (!isMoving)
        {
            StartCoroutine(LiftRoutine(player));
        }
    }

    IEnumerator LiftRoutine(GameObject player)
    {
        isMoving = true;

        // プレイヤーをこのタイルの子に設定
        player.transform.SetParent(transform);

        float startY = transform.position.y;
        float targetY = (startY < (upperY + lowerY) / 2f) ? upperY : lowerY;

        Vector3 startPos = transform.position;
        Vector3 endPos = new Vector3(startPos.x, targetY, startPos.z);

        float elapsed = 0f;

        while (elapsed < moveTime)
        {
            transform.position = Vector3.Lerp(startPos, endPos, elapsed / moveTime);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = endPos;

        // 子オブジェクト解除
        player.transform.SetParent(null);

        isMoving = false;
    }
}
