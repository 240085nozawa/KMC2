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
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == gameObject) // 矢印がクリックされた
                {
                    Debug.Log("矢印クリック: " + moveDirection);
                    playerController.TryMove(moveDirection);
                }
            }
        }
    }
}

