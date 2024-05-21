using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    float speed=100f;
    float face=Gun.face;
    void Update()
    {
        transform.Translate(new Vector3(speed*Time.deltaTime,0,0),Space.Self);
        destroyZiDan();
    }

    void destroyZiDan()
    {
        Vector3 sp=Camera.main.WorldToScreenPoint(transform.position);
        if(sp.x<0||sp.x>Screen.width)
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player"))
        {
            GameManager.instance.blood--;
            Destroy(this.gameObject);
        }
    }
}
