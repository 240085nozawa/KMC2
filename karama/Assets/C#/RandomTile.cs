using System.Collections.Generic;
using UnityEngine;

public class RandomTile : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    // 色付きタイル用のマテリアルをInspectorから割り当て
    public float changeInterval = 2f; // 色の変化間隔
    public LayerMask tileLayer; // タイル判定用レイヤー（Playerと同じ）

    private Renderer tileRenderer;

    // マテリアル一覧（全色分をここでマッピング）
    public List<Material> allMaterials;

    // 開始時に初期化
    void Start()
    {
        tileRenderer = GetComponent<Renderer>();

        if (allMaterials == null || allMaterials.Count == 0)
        {
            Debug.LogError("[RandomTile] マテリアルが設定されていません！");
            enabled = false;
            return;
        }

        InvokeRepeating(nameof(ChangeColorFromNeighbors), 1f, changeInterval);
    }

    // 周囲のタイルの色を取得し、そこからランダムで変化
    void ChangeColorFromNeighbors()
    {
        List<string> neighborTextureNames = new List<string>();

        Vector3[] directions = {
            Vector3.forward,
            Vector3.back,
            Vector3.left,
            Vector3.right
        };

        foreach (Vector3 dir in directions)
        {
            Vector3 checkPos = transform.position + dir;
            Collider[] hits = Physics.OverlapSphere(checkPos, 0.4f, tileLayer);

            foreach (var hit in hits)
            {
                Renderer neighborRenderer = hit.GetComponent<Renderer>();
                if (neighborRenderer != null && neighborRenderer.material.mainTexture != null)
                {
                    string texName = neighborRenderer.material.mainTexture.name;
                    if (!neighborTextureNames.Contains(texName))
                    {
                        neighborTextureNames.Add(texName);
                    }
                }
            }
        }

        if (neighborTextureNames.Count == 0) return;

        // 現在の色
        string currentTex = tileRenderer.material.mainTexture.name;
        int currentIndex = neighborTextureNames.IndexOf(currentTex);

        // 次のインデックス（最後なら0に戻る）
        int nextIndex = (currentIndex + 1) % neighborTextureNames.Count;
        string nextTexName = neighborTextureNames[nextIndex];

        // 一致するマテリアルを探して適用
        Material nextMat = allMaterials.Find(mat => mat.mainTexture != null && mat.mainTexture.name == nextTexName);

        if (nextMat != null)
        {
            tileRenderer.material = nextMat;
            Debug.Log($"[RandomTile] 色変更 → {nextMat.name}");
        }

    }
}
