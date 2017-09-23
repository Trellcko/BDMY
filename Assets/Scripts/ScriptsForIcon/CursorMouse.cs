using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorMouse : MonoBehaviour {
    public Texture2D Cursor;

    private void OnGUI()
    {
        Vector2 mp = Input.mousePosition;
        mp.y = Screen.height - mp.y;
        GUI.DrawTexture(new Rect(mp.x-25, mp.y-20, 60, 78),Cursor);
 
    }
  
}
