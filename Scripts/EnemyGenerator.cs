using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    public static EnemyGenerator instance;
    public LayerMask enemy;
    public GameObject flyingEyes;//1
    public GameObject gobin;
    public GameObject mushroom;
    public GameObject Skeleton;

    private void Awake() {
        if(instance!=null)
        {
            Destroy(this.gameObject);
            return;
        }
        instance=this;
        DontDestroyOnLoad(this);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void generateEnemy(Transform room,Transform player,int n,int enemyId)
    {
        int num=0;
        while(true)
        {
            float y=Random.Range(-40,30);
            float x=Random.Range(-70,70);
            GameObject enemyPrefab=null;
            switch(enemyId)
            {
                case 1:enemyPrefab=instance.flyingEyes;break;
                case 2:enemyPrefab=instance.gobin;break;
                case 3:enemyPrefab=instance.mushroom;break;
                case 4:enemyPrefab=instance.Skeleton;break;
            }
            GameObject newEnemy=Instantiate(enemyPrefab,room.transform.position+new Vector3(x,y),Quaternion.identity);
            num++;
            if(Physics2D.OverlapCircle(player.position,30f,instance.enemy))
            {
                Destroy(newEnemy);
                num--;
            }
            if(num>=n)
            {
                break;
            }
        }
    }


}
