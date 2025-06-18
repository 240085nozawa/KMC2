using UnityEngine;

public class Camera2 : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    // 回転の中心となる座標（インスペクターで設定可能）
    public Vector3 targetPosition = Vector3.zero;

    // 距離（ズーム距離）
    public float distance = 10.0f;

    // マウス感度
    public float xSpeed = 120.0f;
    public float ySpeed = 120.0f;

    // 上下の角度制限
    public float yMinLimit = -20f;
    public float yMaxLimit = 80f;

    // 現在の角度
    private float x = 0.0f;
    private float y = 0.0f;

    public float minDistance = 1.5f; // 最小ズーム距離
    public float maxDistance = 10.0f; // 最大ズーム距離
    public float zoomSpeed = 5.0f; // ズーム速度

    void Start()
    {
        // 初期角度の取得
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;

        // カーソルロックは必要に応じて
        // Cursor.lockState = CursorLockMode.Locked;
    }

    void LateUpdate()
    {
        // マウスホイールでズーム
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        distance -= scroll * zoomSpeed;
        distance = Mathf.Clamp(distance, minDistance, maxDistance);

        // 右クリックしている間だけ回転
        if (Input.GetMouseButton(1))
        {
            x += Input.GetAxis("Mouse X") * xSpeed * Time.deltaTime;
            y -= Input.GetAxis("Mouse Y") * ySpeed * Time.deltaTime;

            y = ClampAngle(y, yMinLimit, yMaxLimit);
        }

        // 回転計算
        Quaternion rotation = Quaternion.Euler(y, x, 0);
        Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
        Vector3 position = rotation * negDistance + targetPosition;

        transform.rotation = rotation;
        transform.position = position;
    }

    // 角度制限関数
    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F) angle += 360F;
        if (angle > 360F) angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }
}
