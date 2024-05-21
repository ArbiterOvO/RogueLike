using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
    Transform Player;
    public LayerMask player;
    BoxCollider2D cl;
    Animator an;
    float speed=16f;
    bool isAttacking=false;
    int face=3;
    float attackTime=0;
    Vector2 yuanOffset=new Vector2(-0.0786f,-0.655f);
    Vector2 yuanSize=new Vector2(2.557f,4.234f);
    Vector2 houOffset=new Vector2(1.773f,-0.655f);
    Vector2 houSize=new Vector2(6.260f,4.234f);
    [SerializeField]float blood=20f;
    // Start is called before the first frame update
    void Start()
    {
        Player=GameObject.Find("Player").transform;
        an=GetComponent<Animator>();
        cl=GetComponent<BoxCollider2D>();
        cl.offset=yuanOffset;
        cl.size=yuanSize;
    }

    // Update is called once per frame
    void Update()
    {
        die();
        walk();
        attack();
        changeChaoXiang();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("ZiDan"))
        {
            blood-=GameManager.instance.ad;
            Destroy(other.gameObject);
        }
        if(other.CompareTag("Player"))
        {
            GameManager.instance.blood-=3;
        }
        if(other.CompareTag("Bomb"))
        {
            blood-=20;
        }
    }

    void die()
    {
        if (blood<=0&&!an.GetBool("Dead"))
        {
            an.SetBool("Dead",true);
            StartCoroutine(destory());
        }
    }

    IEnumerator destory()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);
        GameManager.instance.currentRoom.enemyNum--;
    }

    void walk()
    {
        if(!isAttacking&&!an.GetBool("Dead"))
        {
            transform.position=Vector2.MoveTowards(transform.position,new Vector2(Player.position.x,Player.position.y+5f),Time.deltaTime*speed);
        }
        
    }
    
    void attack()
    {   
        attackTime+=Time.deltaTime;
        if(Physics2D.OverlapCircle(transform.position,14f,player)&&attackTime>=2f)
        {
            isAttacking=true;
            attackTime=0;
            an.SetBool("Attack",true);
            StartCoroutine(Attack());
        }
    }

    IEnumerator Attack()
    {
        yield return new WaitForSeconds(0.3f);
        cl.offset=houOffset;
        cl.size=houSize;
        yield return new WaitForSeconds(0.5f);
        isAttacking=false;
        an.SetBool("Attack",false);
        cl.offset=yuanOffset;
        cl.size=yuanSize;
    }


    void changeChaoXiang()
    {
        if(Player.position.x-transform.position.x>=0)
        face=3;
        else
        face=-3;
        transform.localScale=new Vector3(face,3,1);
    }
}
