using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    private PlayerController playerController;
    private Vector3 moveDirection;

    private AudioSource audioSource; //AudioSource 参照

    // 矢印の移動方向とプレイヤーの参照をセットする
    public void SetDirection(PlayerController controller, Vector3 direction)
    {
        playerController = controller;
        moveDirection = direction;
    }
    void Start()
    {
        //AudioSource を取得（AudioClip はPrefabに設定済みと想定）
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))  // 左クリック
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit[] hits = Physics.RaycastAll(ray); // ← 複数ヒットを取得

            foreach (var hit in hits)
            {
                if (hit.collider != null && hit.collider.gameObject == gameObject)// 矢印がクリックされた
                {
                    //Debug.Log("矢印クリック成功: " + moveDirection);
                    playerController.TryMove(moveDirection);
                    break;
                }
            }
        }
    }
}

