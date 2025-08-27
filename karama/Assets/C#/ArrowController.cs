using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    private PlayerController playerController;
    private Vector3 moveDirection;

    //private AudioSource audioSource; //AudioSource �Q��
    public GameObject clickSoundPrefab; // �C���X�y�N�^�[�� ClickSoundPlayer ���Z�b�g����


    public bool isGoal = false;        //�S�[���ɍs����󂩂ǂ���

    public static ArrowController lastClickedArrow; // �� �ǉ�


    // ���̈ړ������ƃv���C���[�̎Q�Ƃ��Z�b�g����
    public void SetDirection(PlayerController controller, Vector3 direction)
    {
        playerController = controller;
        moveDirection = direction;
    }
    void Start()
    {
        //AudioSource ���擾�iAudioClip ��Prefab�ɐݒ�ς݂Ƒz��j
        //audioSource = GetComponent<AudioSource>();
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
                    // ��󂪃N���b�N���ꂽ�玩�����L�^
                    lastClickedArrow = this;

                    //Debug.Log("���N���b�N����: " + moveDirection);
                    OnMouseDown();
                    playerController.TryMove(moveDirection);

                    //�S�[���ɍs�����Ȃ�
                    if(isGoal == true)
                    {
                        //�q���̃A�j���[�V����
                        GameObject.Find("GoalJunp").GetComponent<Animator>().SetTrigger("Jump");

                    }
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

            Destroy(sound, 2f); // �����Đ����I������玩���ō폜
        }

        //playerController?.TryMove(moveDirection); // �ʏ�̈ړ�
    }

    //�S�[���ɐG�ꂽ��
    private void OnTriggerEnter(Collider other)
    {
        isGoal = true;
    }
}

