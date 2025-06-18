using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    private PlayerController playerController;
    private Vector3 moveDirection;

    //private AudioSource audioSource; //AudioSource 参照
    public GameObject clickSoundPrefab; // インスペクターで ClickSoundPlayer をセットする

    // 矢印の移動方向とプレイヤーの参照をセットする
    public void SetDirection(PlayerController controller, Vector3 direction)
    {
        playerController = controller;
        moveDirection = direction;
    }
    void Start()
    {
        //AudioSource を取得（AudioClip はPrefabに設定済みと想定）
        //audioSource = GetComponent<AudioSource>();
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
                    OnMouseDown();
                    playerController.TryMove(moveDirection);
                    break;
                }
            }
        }
    }
    void OnMouseDown()
    {
        if (clickSoundPrefab != null)
        {
            GameObject sound = Instantiate(clickSoundPrefab);
            AudioSource source = sound.GetComponent<AudioSource>();
            if (source != null) source.Play();

            Destroy(sound, 2f); // 音が再生し終わったら自動で削除
        }

        //playerController?.TryMove(moveDirection); // 通常の移動
    }
}

