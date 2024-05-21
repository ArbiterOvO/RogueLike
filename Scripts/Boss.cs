using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    Transform Player;
    public LayerMask player;
    public GameObject fire;
    public GameObject fakeBoss;
    public GameObject FlyingEyes;
    public GameObject Gobin;
    public GameObject Mushroom;
    public GameObject Skeleton;
    [SerializeField]GameObject bossBlood;
    BoxCollider2D cl;
    Animator an;
    float speed=10f;
    [SerializeField]bool isAttacking=false;
    bool hasFake;
    int face=1;
    float attackTime=0;
    float time=0,generateTime=0;
    Vector2 yuanOffset=new Vector2(-0.348f,-1.9179f);
    Vector2 yuanSize=new Vector2(6.303f,6.303f);
    Vector2 houOffset=new Vector2(14.04f,-1.9179f);
    Vector2 houSize=new Vector2(35.083f,19.554f);
    [SerializeField]public static float blood=150f;
    [SerializeField]public float fakeBlood=30f;
    // Start is called before the first frame update
    void Start()
    {
        Player=GameObject.Find("Player").transform;
        an=GetComponent<Animator>();
        cl=GetComponent<BoxCollider2D>();
        cl.offset=yuanOffset;
        cl.size=yuanSize;
        GameObject canvas=GameObject.Find("Canvas");
        GameObject bossBlood=canvas.transform.Find("BossBlood").gameObject;
        bossBlood.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if(!hasFake)
        {
            walk();
            attack();
        }
        changeChaoXiang();
        die();
        useFire();
        shadow();
        generateEnemy();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("ZiDan")&&!hasFake)
        {
            blood-=GameManager.instance.ad;
            Destroy(other.gameObject);
        }
        if(other.CompareTag("ZiDan")&&hasFake)
        {
            fakeBlood-=GameManager.instance.ad;
            Destroy(other.gameObject);
        }
        if(other.CompareTag("Player"))
        {
            GameManager.instance.blood-=5;
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
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
        GameManager.instance.currentRoom.enemyNum--;
    }

    void walk()
    {
        if(!isAttacking&&!an.GetBool("Dead"))
        {
            transform.position=Vector2.MoveTowards(transform.position,new Vector2(Player.position.x,Player.position.y+7f),Time.deltaTime*speed);
        }
    }
    
    void attack()
    {   
        attackTime+=Time.deltaTime;
        if(Physics2D.OverlapCircle(transform.position,30f,player)&&attackTime>=2f)
        {
            isAttacking=true;
            attackTime=0;
            an.SetBool("Attack",true);
            StartCoroutine(Attack());
        }
    }

    IEnumerator Attack()
    {
        cl.offset=houOffset;
        cl.size=houSize;
        yield return new WaitForSeconds(0.7f);
        isAttacking=false;
        an.SetBool("Attack",false);
        cl.offset=yuanOffset;
        cl.size=yuanSize;
    }

    void changeChaoXiang()
    {
        if(Player.position.x-transform.position.x>=0||hasFake)
        face=1;
        else
        face=-1;
        transform.localScale=new Vector3(face,1,1);
    }

    void useFire()
    {
        time+=Time.deltaTime;
        if(blood<=100&&blood>=70&&time>=0.3f)
        {
            time=0;
            GameObject f1=Instantiate(fire,transform.position,Quaternion.identity);
            f1.transform.rotation=Quaternion.Euler(new Vector3(0,0,45));
            GameObject f2=Instantiate(fire,transform.position,Quaternion.identity);
            f2.transform.rotation=Quaternion.Euler(new Vector3(0,0,90));
            GameObject f3=Instantiate(fire,transform.position,Quaternion.identity);
            f3.transform.rotation=Quaternion.Euler(new Vector3(0,0,135));
            GameObject f4=Instantiate(fire,transform.position,Quaternion.identity);
            f4.transform.rotation=Quaternion.Euler(new Vector3(0,0,180));
            GameObject f5=Instantiate(fire,transform.position,Quaternion.identity);
            f5.transform.rotation=Quaternion.Euler(new Vector3(0,0,225));
            GameObject f6=Instantiate(fire,transform.position,Quaternion.identity);
            f6.transform.rotation=Quaternion.Euler(new Vector3(0,0,270));
            GameObject f7=Instantiate(fire,transform.position,Quaternion.identity);
            f7.transform.rotation=Quaternion.Euler(new Vector3(0,0,315));
            GameObject f8=Instantiate(fire,transform.position,Quaternion.identity);
            f8.transform.rotation=Quaternion.Euler(new Vector3(0,0,360));
        }
        if(blood<=100&&blood>=70)
        {
            an.SetBool("Idle",true);
        }
        else if(blood<70&&blood>50)
        {
            an.SetBool("Idle",false);
        }
    }

    void generateEnemy()
    {
        generateTime+=Time.deltaTime;
        if(hasFake&&generateTime>6f)
        {
            generateTime=0;
            Vector2 pos=GameManager.instance.endRoom.transform.position;
            Instantiate(FlyingEyes,pos+(new Vector2(-40f,0)),Quaternion.identity);
            Instantiate(Gobin,pos+(new Vector2(0,30f)),Quaternion.identity);
            Instantiate(Mushroom,pos+(new Vector2(40f,0)),Quaternion.identity);
            Instantiate(Skeleton,pos+(new Vector2(0,-30f)),Quaternion.identity);
        }
    }
    void shadow()
    {
        Vector2 pos=GameManager.instance.endRoom.transform.position;
        if(blood<=50&&!hasFake)
        {   
            an.SetBool("Idle",true);
            hasFake=true;
            GameObject fake1 = Instantiate(fakeBoss,pos+(new Vector2(-40f,0)),Quaternion.identity);
            GameObject fake2 = Instantiate(fakeBoss,pos+(new Vector2(0,30f)),Quaternion.identity);
            GameObject fake3 = Instantiate(fakeBoss,pos+(new Vector2(40f,0)),Quaternion.identity);
            GameObject fake4 = Instantiate(fakeBoss,pos+(new Vector2(0,-30f)),Quaternion.identity); 
            int i = Random.Range(0,4);
            switch(i)
            {
                case 0:
                transform.position=fake1.transform.position;
                Destroy(fake1);
                break;
                case 1:
                transform.position=fake2.transform.position;
                Destroy(fake2);
                break;
                case 2:
                transform.position=fake3.transform.position;
                Destroy(fake3);
                break;
                case 3:
                transform.position=fake4.transform.position;
                Destroy(fake4);
                break;
            }

        }
        if(fakeBlood<=0)
        {
            Boss.blood-=30;
        }
    }
}
