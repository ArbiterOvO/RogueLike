using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEyes : MonoBehaviour
{
    Transform Player;
    public LayerMask player;
    CircleCollider2D cl;
    Animator an;
    float speed=15f;
    bool isAttacking=false;
    int face=3;
    float attackTime=0;
    Vector2 position;//及时人物坐标
    [SerializeField]float blood=5f;
    // Start is called before the first frame update
    void Start()
    {
        Player=GameObject.Find("Player").transform;
        an=GetComponent<Animator>();
        cl=GetComponent<CircleCollider2D>();
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
            GameManager.instance.blood--;
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
        if(Physics2D.OverlapCircle(transform.position,20f,player)&&attackTime>=3f)
        {
            if(!isAttacking)
            {
                position=new Vector2(Player.position.x,Player.position.y+5f);
            }
            cl.radius=1.8f;
            isAttacking=true;
            an.SetBool("Attack",true);
            StartCoroutine(attackMove(position));
        }
        if(!isAttacking)
        {
            cl.radius=1.3f;
        }
    }

    IEnumerator attackMove(Vector2 pos)
    {
        transform.position=Vector2.MoveTowards(transform.position,pos,Time.deltaTime*30f);
        yield return new WaitForSeconds(1.5f);
        an.SetBool("Attack",false);
        isAttacking=false;
        attackTime=0;
        yield return null;
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
