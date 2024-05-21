using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public GameObject leftDoor,rightDoor,upDoor,downDoor;
    public GameObject boss;

    public bool leftRoom,rightRoom,upRoom,downRoom;
    public bool isClear=false;
    public bool hasChoose=false;
    [SerializeField]public int enemyNum;
    void Start()
    {
        leftDoor.SetActive(leftRoom);
        rightDoor.SetActive(rightRoom);
        upDoor.SetActive(upRoom);
        downDoor.SetActive(downRoom); 
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.instance.endRoom!=GameManager.instance.currentRoom)
        clearRoom();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")&&!isClear&&enemyNum==0&&this.gameObject!=GameManager.instance.endRoom)
        {
            GameManager.instance.currentRoom=this;
            enemyNum=GameManager.instance.level+3;
            if(enemyNum>8)
            enemyNum=8;
            if(!GameManager.instance.currentRoom.isClear)
            EnemyGenerator.generateEnemy(this.transform,other.transform,enemyNum,GameManager.instance.level/2+1);
        }
        if(other.CompareTag("Player")&&this.gameObject==GameManager.instance.endRoom)
        {
            GameManager.instance.currentRoom=this;
            generateBoss();
        }
    }

    void clearRoom()
    {
        if(enemyNum<=0&&GameManager.instance.currentRoom==this)
        isClear=true;
    }

    void generateBoss()
    {
        Instantiate(boss,transform.position,Quaternion.identity);
    }
}
