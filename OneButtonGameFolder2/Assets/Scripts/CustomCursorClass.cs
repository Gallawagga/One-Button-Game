using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCursorClass : MonoBehaviour
{
    public Texture2D customCursor;

    void Start()
    {
        Cursor.SetCursor(customCursor, Vector2.zero, CursorMode.ForceSoftware);
    }

}
