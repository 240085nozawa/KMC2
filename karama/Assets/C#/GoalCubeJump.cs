using UnityEngine;
using System.Collections;

public class GoalCubeJump : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnTriggerEnter(Collider other)
    {
        // Arrow�^�O�̃I�u�W�F�N�g�ƂԂ�����������
        if (other.CompareTag("Arrow"))
        {
            // ���݂�X,Z���W�͂��̂܂܁AY����1�ɂ���
            Vector3 pos = transform.position;
            pos.y = 1.5f;
            transform.position = pos;
        }
    }

}
