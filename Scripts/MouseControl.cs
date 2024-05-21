using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseControl : MonoBehaviour
{

    public Texture2D shoot;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.SetCursor(shoot,new Vector3(250f,250f),CursorMode.Auto);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
