using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{




    public void changePos(Vector2 pos)
    {
        gameObject.transform.position=pos;
    }
}
