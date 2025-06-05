using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
//using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class CameraController : MonoBehaviour
{
    public Transform player; // �v���C���[��Transform
    public float distance = 6.0f; // ��������
    public float minDistance = 1.5f; // �ŏ��Y�[������
    public float maxDistance = 10.0f; // �ő�Y�[������
    public float zoomSpeed = 5.0f; // �Y�[�����x
    public float rotateSpeed = 5.0f; // ��]���x

    private float currentX = 0.0f;
    private float currentY = 10.0f; // �����̍����p�x

    void LateUpdate()
    {
        if (player == null) return;

        // �}�E�X�z�C�[���ŃY�[��
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        distance -= scroll * zoomSpeed;
        distance = Mathf.Clamp(distance, minDistance, maxDistance);

        // �E�N���b�N�Ŏ��_�ړ�
        if (Input.GetMouseButton(1))
        {
            currentX += Input.GetAxis("Mouse X") * rotateSpeed;
            currentY -= Input.GetAxis("Mouse Y") * rotateSpeed;
            currentY = Mathf.Clamp(currentY, 5f, 80f); // ���_�̏㉺�p�x����
        }

        // �J�����̈ʒu���v�Z
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        Vector3 offset = rotation * new Vector3(0, 0, -distance);
        transform.position = player.position + offset;
        transform.LookAt(player.position);
    }
}
