using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float moveDuration = 0.5f; // �ړ�����
    private float gridSize = 1.0f; // 1�}�X�̃X�P�[��
    public LayerMask tileLayer; // �^�C���̃��C���[�}�X�N
    private bool isMoving = false; // �ړ������ǂ����̃t���O

    public GameObject arrowPrefab; //���̃v���n�u
    //private GameObject[] arrows = new GameObject[4]; // �����Ǘ�
    private List<GameObject> arrows = new List<GameObject>(); // ��󃊃X�g

    private Dictionary<Vector3, Color> faceColors = new Dictionary<Vector3, Color>();
    private Renderer cubeRenderer;

    private MoveCounter moveCounter; // MoveCounter�̎Q��


    Dictionary<string, string> faceTextures = new Dictionary<string, string>
    {
        { "Top", "PurpleTairu" },    // ���
        { "Bottom", "BlueTairu" },   // ����
        { "Front", "RedTairu" },     // �O��
        { "Back", "GreenTairu" },    // ���
        { "Left", "BlackTairu" },    // ����
        { "Right", "YellowTairu" }   // �E��
    };

    void Start()
    {
        Application.targetFrameRate = 60;

        moveCounter = FindObjectOfType<MoveCounter>(); // MoveCounter��T��

        // ����̖��\���X�V
        UpdateArrowVisibility();

        // ���b UpdateArrowVisibility ���Ăяo���i�^�C���̐F�ω��ɑΉ��j
        InvokeRepeating(nameof(UpdateArrowVisibility), 1f, 1f);
    }
    void Update()
    {
        
    }
    

    public void TryMove(Vector3 direction)
    {
        if (isMoving) return;

        Vector3 targetPos = transform.position + direction;
        targetPos.y = 0; // �^�C����Y���W�ɍ��킹��

        if (IsTilePresent(targetPos))
        {
            string tileTexture = GetTileTextureName(targetPos);
            string cubeTexture = GetCubeFaceTexture(direction);

            ////UD_Tile ����F�}�e���A������ "_UD" ���܂܂�Ă�����㉺�ړ�
            //if (tileTexture.ToLower().Contains("_ud"))
            //{
            //    if (moveCounter != null && moveCounter.currentMoves > 0)
            //    {
            //        moveCounter.UseMove();
            //        StartCoroutine(Roll(direction, true)); // UD�^�C���ւ̈ړ��͓��ʈ���
            //    }
            //    return;
            //}

            //WhiteTairu �Ȃ��v�s�v
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
        isMoving = true; // �ړ��J�n

        // �ړ��J�n���ɖ����\���ɂ���
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

        isMoving = false; // �ړ��I��


        // **�ړ�������̃L���[�u�̖ʂ̃e�N�X�`�����m�F**
        //Debug.Log($"�ړ�������̃L���[�u�ʂ̃e�N�X�`��: {GetCubeFaceTexture(Vector3.forward)}");

        //if (checkLiftAfter) CheckAndStartLift(); //������UD�^�C�����~���������s
        UpdateArrowVisibility(); //�ړ���ɖ��̕\�����X�V
                                 // ��]�ړ���̏�����
    }

    // �^�C�������݂��邩�`�F�b�N
    bool IsTilePresent(Vector3 position)
    {
        Collider[] hitColliders = Physics.OverlapSphere(position, 0.6f, tileLayer); // ���a���g��
        Debug.Log($"Checking tile at {position}, found: {hitColliders.Length}");
        return hitColliders.Length > 0;
    }

    // �w��ʒu�̃^�C���̃e�N�X�`�����i�F�j���擾
    string GetTileTextureName(Vector3 position)
    {
        Collider[] hitColliders = Physics.OverlapSphere(position, 0.6f, tileLayer);
        if (hitColliders.Length > 0)
        {
            Renderer tileRenderer = hitColliders[0].GetComponent<Renderer>();
            if (tileRenderer != null && tileRenderer.material != null && tileRenderer.material.mainTexture != null)
            {
                string textureName = tileRenderer.material.mainTexture.name;
                //Debug.Log($"�^�C���̈ʒu: {position}, �e�N�X�`����: {textureName}"); // �� �e�N�X�`���������O�o��
                return tileRenderer.material.mainTexture.name;
            }
        }
        return "";
    }

    // �i�s�����ɉ����ăv���C���[�̃L���[�u�ʂ̐F���擾
    string GetCubeFaceTexture(Vector3 direction)
    {
        if (direction == Vector3.forward) return faceTextures["Front"];
        if (direction == Vector3.back) return faceTextures["Back"];
        if (direction == Vector3.right) return faceTextures["Right"];
        if (direction == Vector3.left) return faceTextures["Left"];
        return "";
    }

    // �L���[�u�̖ʂ̏�Ԃ���]�ɉ����čX�V
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

        //Debug.Log($"�ړ�������̃L���[�u�ʂ̃e�N�X�`��: �O��={faceTextures["Front"]}, �E��={faceTextures["Right"]}");
    }


    // �ړ��\�ȕ����ɖ���\��
    void UpdateArrowVisibility()
    {
        if (isMoving) return; //�ړ����͖����X�V���Ȃ�

        ClearArrows(); // �����̖����폜

        Vector3[] directions = { Vector3.forward, Vector3.back, Vector3.right, Vector3.left };
        Quaternion[] rotations = {
            Quaternion.Euler(0, 0, 0),   // �O��
            Quaternion.Euler(0, 180, 0), // ���
            Quaternion.Euler(0, 90, 0),  // �E
            Quaternion.Euler(0, -90, 0)  // ��
        };

        foreach (Vector3 dir in directions)
        {
            Vector3 targetPos = transform.position + dir;
            targetPos.y = 0;

            if (IsTilePresent(targetPos))
            {
                string tileTexture = GetTileTextureName(targetPos);
                string cubeTexture = GetCubeFaceTexture(dir);

                //Debug.Log($"����: {dir}, �^�C���̃e�N�X�`��: {tileTexture}, �L���[�u�ʂ̃e�N�X�`��: {cubeTexture}");


                //WhiteTairu �͏�ɒʍs�\�Ƃ���     //���O��_UD���܂܂�Ă��邩    //�������ɓ��ꂵ�Ĕ�r��   
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
                    //Debug.Log($"�F����v���Ȃ����ߖ���\��: {targetPos}");
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
    // UD_Tile �p�̏㉺�W�����v����
    //IEnumerator JumpVertical()
    //{
    //    isMoving = true;
    //    HideArrows();

    //    float targetY = transform.position.y == 0 ? 10 : 0; // ���ɂ������ցA��ɂ����牺��

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
    //    UpdateArrowVisibility(); // ���X�V
    //}
    //void CheckAndStartLift()
    //{
    //    Vector3 checkPos = transform.position;
    //    checkPos.y = 0f; // ���w���UD_Tile�ɔz�u

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

    //// ��]��ɏ����҂��Ă��珸�~�J�n�i0.5�b�ҋ@�Ȃǁj
    //IEnumerator StartLiftWithDelay(UD_Tile udTile)
    //{
    //    yield return new WaitForSeconds(0.5f); // �K�v�ɉ����Ē����\
    //    udTile.StartLift(this.gameObject);
    //}
}