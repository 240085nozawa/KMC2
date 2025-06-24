using UnityEngine;
using System.Collections;

public class UD_Tile : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float moveTime = 2f;         // �㏸�E���~�ɂ����鎞�ԁi�ύX�j
    public float upperY = 10f;          // ��K��Y���W
    public float lowerY = 0f;           // ���K��Y���W

    private bool isMoving = false;

    // �v���C���[���󂯎���Ĉꏏ�Ɉړ��J�n
    public void StartLift(GameObject player)
    {
        if (!isMoving)
        {
            StartCoroutine(LiftRoutine(player));
        }
    }

    IEnumerator LiftRoutine(GameObject player)
    {
        isMoving = true;

        // �v���C���[�����̃^�C���̎q�ɐݒ�
        player.transform.SetParent(transform);

        float startY = transform.position.y;
        float targetY = (startY < (upperY + lowerY) / 2f) ? upperY : lowerY;

        Vector3 startPos = transform.position;
        Vector3 endPos = new Vector3(startPos.x, targetY, startPos.z);

        float elapsed = 0f;

        while (elapsed < moveTime)
        {
            transform.position = Vector3.Lerp(startPos, endPos, elapsed / moveTime);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = endPos;

        // �q�I�u�W�F�N�g����
        player.transform.SetParent(null);

        isMoving = false;
    }
}
