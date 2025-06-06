using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    private PlayerController playerController;
    private Vector3 moveDirection;

    public void SetDirection(PlayerController controller, Vector3 direction)
    {
        playerController = controller;
        moveDirection = direction;
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
                    Debug.Log("矢印クリック成功: " + moveDirection);
                    playerController.TryMove(moveDirection);
                    break;
                }
            }
        }
    }
}

