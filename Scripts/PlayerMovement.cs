using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D cl;
    private Animator an;
    public GameObject bomb;
    [Header("移动参数")]
    public float xSpeed=30f;
    public float ySpeed=25f;
    public float xOffset=110f;
    public float yOffset=60f;
    [Header("状态")]
    public float chaoxiang=1f;
    [Header("按键映射")]
    bool isE;
    public static float Horizontal;
    float Vertical;
    [Header("武器")]
    public Transform gun;

    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
        cl=GetComponent<BoxCollider2D>();
        an=GetComponent<Animator>();
    }

    
    void Update()
    {
        useBomb();
    }

    void FixedUpdate()
    {
        walk();
        changeChaoXiang();
    }

    void walk()
    {
        Horizontal=Input.GetAxis("Horizontal");
        Vertical=Input.GetAxis("Vertical");
        if(Horizontal!=0)
        {
            //人物动画
            an.SetBool("walkUp",false); 
            an.SetBool("walkDown",false); 
            an.SetBool("walkLeftOrRight",true);   
        }
        else if(Horizontal==0&&Vertical>0)
        {
            //人物动画
            an.SetBool("walkUp",true); 
            an.SetBool("walkDown",false); 
            an.SetBool("walkLeftOrRight",false);   
        }
        else if(Horizontal==0&&Vertical<0)
        {
            //人物
            an.SetBool("walkUp",false); 
            an.SetBool("walkDown",true); 
            an.SetBool("walkLeftOrRight",false); 
        }
        else if(Horizontal==0&&Vertical==0)
        {
            //人物
            an.SetBool("walkUp",false); 
            an.SetBool("walkDown",false); 
            an.SetBool("walkLeftOrRight",false); 
        }
        rb.velocity=new Vector2(Horizontal*xSpeed,Vertical*ySpeed);
    }

    void changeChaoXiang()
    {
        if(Horizontal>=0)
        chaoxiang=-1f; 
        else 
        {
            chaoxiang=1f;
        }
        transform.localScale=new Vector3(chaoxiang,1,1);
    }

    void useBomb()
    {
        if(Input.GetButtonDown("use")&&GameManager.instance.BoomNum>0)
        {
            Instantiate(bomb,transform.position,Quaternion.identity);
            GameManager.instance.BoomNum--;
        }
    }
}
