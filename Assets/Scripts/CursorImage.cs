using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorImage : MonoBehaviour
{
    public Texture2D cursorImage;
    void LateUpdate()
    {
        Cursor.SetCursor(cursorImage, Vector2.zero, CursorMode.ForceSoftware);
    }


    private void Start()
    {
       
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
