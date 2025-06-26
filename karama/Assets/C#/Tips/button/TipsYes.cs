using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipsYes : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // ヒントボタンを押すとゴールまでの指定数（tipLength）のルートを一時的にハイライト表示するスクリプト

    [Header("ヒント設定")]
    public List<GameObject> hintPath = new List<GameObject>(); // 手動でセットする正解ルート
    public int tipLength = 5;          // 表示する最大マス数
    public float tipDuration = 3f;     // 表示時間（秒）
    public GameObject hintMarkerPrefab; // 表示するマーカー（0.5マス上に表示される）

    private List<GameObject> spawnedMarkers = new List<GameObject>();

    // ボタンから呼び出す用
    public void ShowHint()
    {
        StartCoroutine(ShowHintCoroutine());
    }

    IEnumerator ShowHintCoroutine()
    {
        spawnedMarkers.Clear();

        // 指定されたマス数まで表示
        for (int i = 0; i < Mathf.Min(tipLength, hintPath.Count); i++)
        {
            GameObject tile = hintPath[i];
            Vector3 markerPos = tile.transform.position + new Vector3(0, 0.5f, 0); // タイルの上に表示
            GameObject marker = Instantiate(hintMarkerPrefab, markerPos, Quaternion.identity);
            spawnedMarkers.Add(marker);
        }

        yield return new WaitForSeconds(tipDuration);

        // マーカー削除
        foreach (GameObject marker in spawnedMarkers)
        {
            if (marker != null)
                Destroy(marker);
        }
        spawnedMarkers.Clear();
    }
}
