using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
//using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class CameraController : MonoBehaviour
{
    public Transform player; // プレイヤーのTransform
    public float distance = 6.0f; // 初期距離
    public float minDistance = 1.5f; // 最小ズーム距離
    public float maxDistance = 10.0f; // 最大ズーム距離
    public float zoomSpeed = 5.0f; // ズーム速度
    public float rotateSpeed = 5.0f; // 回転速度

    private float currentX = 0.0f;
    private float currentY = 10.0f; // 初期の高さ角度

    void LateUpdate()
    {
        if (player == null) return;

        // マウスホイールでズーム
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        distance -= scroll * zoomSpeed;
        distance = Mathf.Clamp(distance, minDistance, maxDistance);

        // 右クリックで視点移動
        if (Input.GetMouseButton(1))
        {
            currentX += Input.GetAxis("Mouse X") * rotateSpeed;
            currentY -= Input.GetAxis("Mouse Y") * rotateSpeed;
            currentY = Mathf.Clamp(currentY, 5f, 80f); // 視点の上下角度制限
        }

        // カメラの位置を計算
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        Vector3 offset = rotation * new Vector3(0, 0, -distance);
        transform.position = player.position + offset;
        transform.LookAt(player.position);
    }
}
