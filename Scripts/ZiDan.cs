using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZiDan : MonoBehaviour
{

    float speed=150f;
    float face=Gun.face;
    // Start is called before the first frame update
    void Start()
    {
        DiretionChange();
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.localScale.x>0)
        transform.Translate(new Vector3(speed*Time.deltaTime,0,0),Space.Self);
        else if(transform.localScale.x<0)
        transform.Translate(new Vector3(-speed*Time.deltaTime,0,0),Space.Self);
        destroyZiDan();
    }

    void destroyZiDan()
    {
        Vector3 sp=Camera.main.WorldToScreenPoint(transform.position);
        if(sp.x<0||sp.x>Screen.width)
        Destroy(this.gameObject);
    }

    void DiretionChange()
    {
        float horizontal=PlayerMovement.Horizontal;
        //获取鼠标在游戏中的世界坐标
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    
        //获取gun坐标和鼠标的世界坐标形成的向量的角度
        float Angle= Mathf.Atan2(mousePosition.y - transform.position.y, mousePosition.x-transform.position.x) * Mathf.Rad2Deg;
        if(Angle>90)
        {
            Angle=Angle-180;
            transform.localScale=new Vector3(-1,1,1);
        }
        else if(Angle<-90)
        {
            Angle=Angle+180;
            transform.localScale=new Vector3(-1,1,1);
        }
        else
        {
            transform.localScale=new Vector3(1,1,1);
        }

        //朝向鼠标方向
        transform.rotation = Quaternion.Euler(new Vector3(0,0,Angle));
    }


}
