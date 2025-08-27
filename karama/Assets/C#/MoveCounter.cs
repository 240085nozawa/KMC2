using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MoveCounter : MonoBehaviour
{
    public int maxMoves = 10; // �X�e�[�W���Ƃɐݒ�\�Ȏ萔
    public int currentMoves { get; private set; }

    public TextMeshProUGUI moveText; // TextMeshPro �� UI �e�L�X�g
    public GameObject restartPanel; // �u���߂���v�{�^����\������p�l��
    public Button restartButton; // ���߂���{�^��

    public static bool isOutOfMoves = false; // �� �ǉ�


    void Start()
    {
        currentMoves = maxMoves;
        UpdateMoveUI();

        // ���߂���p�l�����\��
        if (restartPanel != null)
            restartPanel.SetActive(false);

        // ���߂���{�^���̃N���b�N�C�x���g��ݒ�
        if (restartButton != null)
            restartButton.onClick.AddListener(RestartLevel);

        
    }

    private void Update()
    {
        
    }

    public void UseMove()
    {
        if (currentMoves > 0)
        {
            currentMoves--;
            UpdateMoveUI();

            if (currentMoves == 0)
            {
                isOutOfMoves = true; // �� �萔�؂��ʒm
                //if (ArrowController.isGoal == true)
                //{

                //}
                //else
                // �Ō�ɃN���b�N���ꂽ��󂪃S�[���s�����ǂ�������
                if (ArrowController.lastClickedArrow != null &&
                    ArrowController.lastClickedArrow.isGoal)
                {

                }
                else
                {
                    ShowRestartUI();
                }
            }
        }
    }

    // UI�X�V
    private void UpdateMoveUI()
    {
        if (moveText != null)
            moveText.text = "�~ " + currentMoves;
    }

    // �萔��0�ɂȂ����烊�X�^�[�g�{�^����\��
    private void ShowRestartUI()
    {
        if (restartPanel != null)
            restartPanel.SetActive(true);
    }

    // �u���߂���v�{�^�����������Ƃ��̏���
    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
