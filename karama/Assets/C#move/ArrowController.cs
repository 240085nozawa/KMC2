using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    private PlayerController playerController;
    private Vector3 moveDirection;

    private AudioSource audioSource; //AudioSource �Q��

    // ���̈ړ������ƃv���C���[�̎Q�Ƃ��Z�b�g����
    public void SetDirection(PlayerController controller, Vector3 direction)
    {
        playerController = controller;
        moveDirection = direction;
    }
    void Start()
    {
        //AudioSource ���擾�iAudioClip ��Prefab�ɐݒ�ς݂Ƒz��j
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))  // ���N���b�N
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit[] hits = Physics.RaycastAll(ray); // �� �����q�b�g���擾

            foreach (var hit in hits)
            {
                if (hit.collider != null && hit.collider.gameObject == gameObject)// ��󂪃N���b�N���ꂽ
                {
                    //Debug.Log("���N���b�N����: " + moveDirection);
                    playerController.TryMove(moveDirection);
                    break;
                }
            }
        }
    }
}

