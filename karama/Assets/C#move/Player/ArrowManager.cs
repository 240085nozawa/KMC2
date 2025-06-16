using UnityEngine;
using System.Collections.Generic;

// 矢印オブジェクトの表示・非表示・削除を管理するスクリプト

public class ArrowManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private List<GameObject> arrows = new List<GameObject>();
    private GameObject arrowPrefab;
    private Transform playerTransform;
    private LayerMask tileLayer;
    private CubeFaceManager faceManager;

    public ArrowManager(GameObject prefab, Transform player, LayerMask layer, CubeFaceManager faceMgr)
    {
        arrowPrefab = prefab;
        playerTransform = player;
        tileLayer = layer;
        faceManager = faceMgr;
    }

    // 矢印を表示する処理（4方向チェック）
    public void ShowArrows()
    {
        ClearArrows();

        Vector3[] directions = { Vector3.forward, Vector3.back, Vector3.right, Vector3.left };

        foreach (Vector3 dir in directions)
        {
            Vector3 targetPos = playerTransform.position + dir;
            targetPos.y = Mathf.Floor(playerTransform.position.y) - 1f;

            if (IsTilePresent(targetPos))
            {
                string tileTexture = GetTileTextureName(targetPos).ToLower();
                string cubeTexture = faceManager.GetFaceForDirection(dir).ToLower();

                if (tileTexture == "whitetairu" || tileTexture.Contains("_ud") || tileTexture == cubeTexture)
                {
                    Vector3 arrowPos = playerTransform.position + dir * 1.2f;
                    GameObject arrow = GameObject.Instantiate(arrowPrefab, arrowPos, Quaternion.identity);
                    arrow.transform.LookAt(playerTransform.position);
                    arrow.GetComponent<ArrowController>().SetDirection(playerTransform.GetComponent<PlayerController>(), dir);
                    arrows.Add(arrow);
                }
            }
        }
    }

    // 矢印をすべて非表示にする（SetActive false）
    public void HideArrows()
    {
        foreach (GameObject arrow in arrows)
        {
            if (arrow != null)
                arrow.SetActive(false);
        }
    }

    // 矢印をすべて削除
    public void ClearArrows()
    {
        foreach (GameObject arrow in arrows)
        {
            if (arrow != null)
                GameObject.Destroy(arrow, 0.1f); // 音再生のため少し遅延
        }
        arrows.Clear();
    }

    // 指定位置にタイルが存在するか判定
    private bool IsTilePresent(Vector3 position)
    {
        Collider[] hitColliders = Physics.OverlapSphere(position, 0.6f, tileLayer);
        return hitColliders.Length > 0;
    }

    // 指定位置のタイルのテクスチャ名を取得
    private string GetTileTextureName(Vector3 position)
    {
        Collider[] hits = Physics.OverlapSphere(position, 0.6f, tileLayer);
        if (hits.Length > 0)
        {
            Renderer tileRenderer = hits[0].GetComponent<Renderer>();
            if (tileRenderer?.material?.mainTexture != null)
            {
                return tileRenderer.material.mainTexture.name;
            }
        }
        return "";
    }
}
