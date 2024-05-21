using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public static float face;
    public GameObject ZiDanprefab;
    public Transform player;
    float time=0;
    // Start is called before the first frame update
    void Start()
    {
        player=GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        shoot();
        DiretionChange();
        if(transform.localScale.x>0)
        face=1;
        else
        face=-1;
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

        if(horizontal<0)
        {
            transform.localScale=new Vector3(-1*transform.localScale.x,1,1);
        }
         transform.position=new Vector3(player.position.x+10f,transform.position.y,0);
        
        
        //朝向鼠标方向
        transform.rotation = Quaternion.Euler(new Vector3(0,0,Angle));
    }

    void shoot()
    {
        time+=Time.deltaTime;
        if(Input.GetMouseButton(0)&&time>=GameManager.instance.shootSpeed)
        {
            time=0;
            GameObject zd=Instantiate(ZiDanprefab,transform.position+new Vector3(0,0,0),Quaternion.identity);
        }
    }
}
