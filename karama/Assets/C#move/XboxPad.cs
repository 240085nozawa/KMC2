using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class XboxPad : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public RectTransform virtualCursor;
    public float cursorSpeed = 500f;
    public float cameraRotateSpeed = 100f;

    public Transform cameraTarget; // Å© Ç±Ç±Çí«â¡

    private Vector2 cursorPos;

    void Start()
    {
        cursorPos = virtualCursor.anchoredPosition;
        Cursor.visible = false;
    }

    void Update()
    {
        RotateCamera();
        MoveCursor();
        ClickCursor();
    }

    void RotateCamera()
    {
        float h = Input.GetAxis("LeftStickHorizontal");
        float v = Input.GetAxis("LeftStickVertical");

        Debug.Log("Left Stick: (" + h + ", " + v + ")");

        if (cameraTarget != null)
        {
            Vector3 rotate = new Vector3(0f, h, 0f) * cameraRotateSpeed * Time.deltaTime;
            cameraTarget.Rotate(rotate, Space.World);
        }
    }

    void MoveCursor()
    {
        float x = Input.GetAxis("RightStickHorizontal");
        float y = Input.GetAxis("RightStickVertical");

        Vector2 move = new Vector2(x, y) * cursorSpeed * Time.deltaTime;
        cursorPos += move;

        cursorPos.x = Mathf.Clamp(cursorPos.x, 0, Screen.width);
        cursorPos.y = Mathf.Clamp(cursorPos.y, 0, Screen.height);

        virtualCursor.position = cursorPos;
    }

    void ClickCursor()
    {
        if (Input.GetKeyDown(KeyCode.JoystickButton1))
        {
            PointerEventData pointerData = new PointerEventData(EventSystem.current)
            {
                position = virtualCursor.position
            };

            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerData, results);

            foreach (var result in results)
            {
                ExecuteEvents.Execute(result.gameObject, pointerData, ExecuteEvents.pointerClickHandler);
            }
        }
    }
}
