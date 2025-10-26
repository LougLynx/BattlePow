using UnityEngine;

public class CursorManage : MonoBehaviour
{
    [SerializeField] private Texture2D cursorNomal;
    [SerializeField] private Texture2D cursorShoot;
    [SerializeField] private Texture2D cursorReload;
    private Vector2 hotspot = new Vector2(32, 32);
    void Start()
    {
        Cursor.SetCursor(cursorNomal, hotspot, CursorMode.Auto);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Cursor.SetCursor(cursorShoot, hotspot, CursorMode.Auto);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            Cursor.SetCursor(cursorNomal, hotspot, CursorMode.Auto);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Cursor.SetCursor(cursorReload, hotspot, CursorMode.Auto);
        }
        else if (Input.GetKeyUp(KeyCode.R))
        {
            Cursor.SetCursor(cursorNomal, hotspot, CursorMode.Auto);
        }
    }

}
