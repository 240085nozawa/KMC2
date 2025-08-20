using UnityEngine;
using System.Collections;

public class GoalCubeJump : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnTriggerEnter(Collider other)
    {
        // Arrowタグのオブジェクトとぶつかったか判定
        if (other.CompareTag("Arrow"))
        {
            // 現在のX,Z座標はそのまま、Yだけ1にする
            Vector3 pos = transform.position;
            pos.y = 1.5f;
            transform.position = pos;
        }
    }

}
