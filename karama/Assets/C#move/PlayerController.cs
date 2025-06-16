using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float moveDuration = 0.5f; // 移動時間
    private float gridSize = 1.0f; // 1マスのスケール
    public LayerMask tileLayer; // タイルのレイヤーマスク
    private bool isMoving = false; // 移動中かどうかのフラグ

    public GameObject arrowPrefab; //矢印のプレハブ
    //private GameObject[] arrows = new GameObject[4]; // 矢印を管理
    private List<GameObject> arrows = new List<GameObject>(); // 矢印リスト

    private Dictionary<Vector3, Color> faceColors = new Dictionary<Vector3, Color>();
    private Renderer cubeRenderer;

    private MoveCounter moveCounter; // MoveCounterの参照


    Dictionary<string, string> faceTextures = new Dictionary<string, string>
    {
        { "Top", "PurpleTairu" },    // 上面
        { "Bottom", "BlueTairu" },   // 下面
        { "Front", "RedTairu" },     // 前面
        { "Back", "GreenTairu" },    // 後面
        { "Left", "BlackTairu" },    // 左面
        { "Right", "YellowTairu" }   // 右面
    };

    void Start()
    {
        Application.targetFrameRate = 60;

        moveCounter = FindObjectOfType<MoveCounter>(); // MoveCounterを探す

        // 初回の矢印表示更新
        UpdateArrowVisibility();

        // 毎秒 UpdateArrowVisibility を呼び出す（タイルの色変化に対応）
        InvokeRepeating(nameof(UpdateArrowVisibility), 1f, 1f);
    }
    void Update()
    {
        
    }
    

    public void TryMove(Vector3 direction)
    {
        if (isMoving) return;

        Vector3 targetPos = transform.position + direction;
        targetPos.y = 0; // タイルのY座標に合わせる

        if (IsTilePresent(targetPos))
        {
            string tileTexture = GetTileTextureName(targetPos);
            string cubeTexture = GetCubeFaceTexture(direction);

            ////UD_Tile 判定：マテリアル名に "_UD" が含まれていたら上下移動
            //if (tileTexture.ToLower().Contains("_ud"))
            //{
            //    if (moveCounter != null && moveCounter.currentMoves > 0)
            //    {
            //        moveCounter.UseMove();
            //        StartCoroutine(Roll(direction, true)); // UDタイルへの移動は特別扱い
            //    }
            //    return;
            //}

            //WhiteTairu なら一致不要
            if (tileTexture.ToLower() == "whitetairu"
                || tileTexture.ToLower() == cubeTexture.ToLower())
            {
                if (moveCounter != null && moveCounter.currentMoves > 0)
                {
                    moveCounter.UseMove();
                    StartCoroutine(Roll(direction, false));
                }
            }
        }
        else
        {
            //Debug.Log("No tile found at " + targetPos);
        }
    }

    

    IEnumerator Roll(Vector3 direction , bool checkLiftAfter)
    {
        isMoving = true; // 移動開始

        // 移動開始時に矢印を非表示にする
        HideArrows();

        Vector3 anchor = transform.position + (Vector3.down * gridSize * 0.5f) + (direction * 0.5f);
        Vector3 axis = Vector3.Cross(Vector3.up, direction);

        for (int i = 0; i < 90; i += 15)
        {
            transform.RotateAround(anchor, axis, 15);
            yield return new WaitForSeconds(moveDuration / 6);
        }

        transform.position = new Vector3(
            Mathf.Round(transform.position.x),
            Mathf.Round(transform.position.y),
            Mathf.Round(transform.position.z)
        );

        UpdateCubeFaces(direction);

        isMoving = false; // 移動終了


        // **移動完了後のキューブの面のテクスチャを確認**
        //Debug.Log($"移動完了後のキューブ面のテクスチャ: {GetCubeFaceTexture(Vector3.forward)}");

        //if (checkLiftAfter) CheckAndStartLift(); //ここでUDタイル昇降処理を実行
        UpdateArrowVisibility(); //移動後に矢印の表示を更新
                                 // 回転移動後の処理内
    }

    // タイルが存在するかチェック
    bool IsTilePresent(Vector3 position)
    {
        Collider[] hitColliders = Physics.OverlapSphere(position, 0.6f, tileLayer); // 半径を拡大
        Debug.Log($"Checking tile at {position}, found: {hitColliders.Length}");
        return hitColliders.Length > 0;
    }

    // 指定位置のタイルのテクスチャ名（色）を取得
    string GetTileTextureName(Vector3 position)
    {
        Collider[] hitColliders = Physics.OverlapSphere(position, 0.6f, tileLayer);
        if (hitColliders.Length > 0)
        {
            Renderer tileRenderer = hitColliders[0].GetComponent<Renderer>();
            if (tileRenderer != null && tileRenderer.material != null && tileRenderer.material.mainTexture != null)
            {
                string textureName = tileRenderer.material.mainTexture.name;
                //Debug.Log($"タイルの位置: {position}, テクスチャ名: {textureName}"); // ← テクスチャ名をログ出力
                return tileRenderer.material.mainTexture.name;
            }
        }
        return "";
    }

    // 進行方向に応じてプレイヤーのキューブ面の色を取得
    string GetCubeFaceTexture(Vector3 direction)
    {
        if (direction == Vector3.forward) return faceTextures["Front"];
        if (direction == Vector3.back) return faceTextures["Back"];
        if (direction == Vector3.right) return faceTextures["Right"];
        if (direction == Vector3.left) return faceTextures["Left"];
        return "";
    }

    // キューブの面の状態を回転に応じて更新
    void UpdateCubeFaces(Vector3 direction)
    {
        string temp;

        if (direction == Vector3.forward)
        {
            temp = faceTextures["Top"];
            faceTextures["Top"] = faceTextures["Back"];
            faceTextures["Back"] = faceTextures["Bottom"];
            faceTextures["Bottom"] = faceTextures["Front"];
            faceTextures["Front"] = temp;
        }
        else if (direction == Vector3.back)
        {
            temp = faceTextures["Top"];
            faceTextures["Top"] = faceTextures["Front"];
            faceTextures["Front"] = faceTextures["Bottom"];
            faceTextures["Bottom"] = faceTextures["Back"];
            faceTextures["Back"] = temp;
        }
        else if (direction == Vector3.right)
        {
            temp = faceTextures["Top"];
            faceTextures["Top"] = faceTextures["Left"];
            faceTextures["Left"] = faceTextures["Bottom"];
            faceTextures["Bottom"] = faceTextures["Right"];
            faceTextures["Right"] = temp;
        }
        else if (direction == Vector3.left)
        {
            temp = faceTextures["Top"];
            faceTextures["Top"] = faceTextures["Right"];
            faceTextures["Right"] = faceTextures["Bottom"];
            faceTextures["Bottom"] = faceTextures["Left"];
            faceTextures["Left"] = temp;
        }

        //Debug.Log($"移動完了後のキューブ面のテクスチャ: 前面={faceTextures["Front"]}, 右面={faceTextures["Right"]}");
    }


    // 移動可能な方向に矢印を表示
    void UpdateArrowVisibility()
    {
        if (isMoving) return; //移動中は矢印を更新しない

        ClearArrows(); // 既存の矢印を削除

        Vector3[] directions = { Vector3.forward, Vector3.back, Vector3.right, Vector3.left };
        Quaternion[] rotations = {
            Quaternion.Euler(0, 0, 0),   // 前方
            Quaternion.Euler(0, 180, 0), // 後方
            Quaternion.Euler(0, 90, 0),  // 右
            Quaternion.Euler(0, -90, 0)  // 左
        };

        foreach (Vector3 dir in directions)
        {
            Vector3 targetPos = transform.position + dir;
            targetPos.y = 0;

            if (IsTilePresent(targetPos))
            {
                string tileTexture = GetTileTextureName(targetPos);
                string cubeTexture = GetCubeFaceTexture(dir);

                //Debug.Log($"方向: {dir}, タイルのテクスチャ: {tileTexture}, キューブ面のテクスチャ: {cubeTexture}");


                //WhiteTairu は常に通行可能とする     //名前に_UDが含まれているか    //小文字に統一して比較す   
                if (tileTexture.ToLower() == "whitetairu"
                    || tileTexture.ToLower().Contains("_ud")
                    || tileTexture.ToLower() == cubeTexture.ToLower())
                {
                    GameObject arrow = Instantiate(arrowPrefab, transform.position + dir * 1.2f, Quaternion.identity);
                    arrow.transform.LookAt(transform.position);
                    arrow.GetComponent<ArrowController>().SetDirection(this, dir);
                    arrows.Add(arrow);
                }
                else
                {
                    //Debug.Log($"色が一致しないため矢印非表示: {targetPos}");
                }
            }
        }
    }

    

    void ClearArrows()
    {
        foreach (GameObject arrow in arrows)
        {
            Destroy(arrow);
        }
        arrows.Clear();
    }
    void HideArrows()
    {
        foreach (GameObject arrow in arrows)
        {
            arrow.SetActive(false);
        }
    }
    // UD_Tile 用の上下ジャンプ処理
    //IEnumerator JumpVertical()
    //{
    //    isMoving = true;
    //    HideArrows();

    //    float targetY = transform.position.y == 0 ? 10 : 0; // 下にいたら上へ、上にいたら下へ

    //    Vector3 start = transform.position;
    //    Vector3 end = new Vector3(start.x, targetY, start.z);

    //    float elapsed = 0f;

    //    while (elapsed < moveDuration)
    //    {
    //        transform.position = Vector3.Lerp(start, end, elapsed / moveDuration);
    //        elapsed += Time.deltaTime;
    //        yield return null;
    //    }

    //    transform.position = end;

    //    isMoving = false;
    //    UpdateArrowVisibility(); // 矢印更新
    //}
    //void CheckAndStartLift()
    //{
    //    Vector3 checkPos = transform.position;
    //    checkPos.y = 0f; // 下層基準でUD_Tileに配置

    //    Collider[] hits = Physics.OverlapSphere(checkPos, 0.1f);
    //    foreach (var hit in hits)
    //    {
    //        Renderer rend = hit.GetComponent<Renderer>();
    //        if (rend != null && rend.material.mainTexture != null)
    //        {
    //            string texName = rend.material.mainTexture.name.ToLower();
    //            if (texName.Contains("_ud"))
    //            {
    //                UD_Tile udTile = hit.GetComponent<UD_Tile>();
    //                if (udTile != null)
    //                {
    //                    StartCoroutine(StartLiftWithDelay(udTile));
    //                }
    //            }
    //        }
    //    }
    //}

    //// 回転後に少し待ってから昇降開始（0.5秒待機など）
    //IEnumerator StartLiftWithDelay(UD_Tile udTile)
    //{
    //    yield return new WaitForSeconds(0.5f); // 必要に応じて調整可能
    //    udTile.StartLift(this.gameObject);
    //}
}