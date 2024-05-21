using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RommGenerator : MonoBehaviour
{

    public enum Direction{up,down,left,right};
    public Direction direction;

    [Header("房间信息")]
    public GameObject roomPrefab;
    public int roomNumber=9;
    // public Color startColor,endColor,l;
    public GameObject endRoom;

    [Header("位置控制")]
    public Transform generatorPoint;
    public float xOffset=220f;
    public float yOffset=120f;
    public LayerMask roomLayer;

    public List<Room> rooms=new List<Room>();
    void Start()
    {

        for (int i = 0; i < roomNumber-1; i++)
        {
            rooms.Add(Instantiate(roomPrefab,generatorPoint.position,Quaternion.identity).GetComponent<Room>());

            //改变point位置
            ChangePointPos();
        }
        //第一个房间
        // rooms[0].GetComponent<SpriteRenderer>().color=startColor;
        //最远的房间
        endRoom=rooms[0].gameObject;
        foreach (var room in rooms)
        {
            if(room.transform.position.sqrMagnitude>endRoom.transform.position.sqrMagnitude)
            {
                endRoom=room.gameObject;
            }
            SetupDoor(room,room.transform.position);
        }
        // endRoom.GetComponent<SpriteRenderer>().color=l;
        //最后房间的生成
        if(Physics2D.OverlapCircle(new Vector2(endRoom.transform.position.x,endRoom.transform.position.y+yOffset),0.2f,roomLayer))//上
        {
            rooms.Add(Instantiate(roomPrefab,new Vector2(endRoom.transform.position.x,endRoom.transform.position.y-yOffset),Quaternion.identity).GetComponent<Room>());
            rooms[8].upRoom=true;
            endRoom.GetComponent<Room>().downRoom=true;
            Debug.Log("上");
        }
        else if(Physics2D.OverlapCircle(new Vector2(endRoom.transform.position.x+xOffset,endRoom.transform.position.y),0.2f,roomLayer))//右
        {
            rooms.Add(Instantiate(roomPrefab,new Vector2(endRoom.transform.position.x-xOffset,endRoom.transform.position.y),Quaternion.identity).GetComponent<Room>());
            rooms[8].rightRoom=true;
            endRoom.GetComponent<Room>().leftRoom=true;
            Debug.Log("右");
        }
        else if(Physics2D.OverlapCircle(new Vector2(endRoom.transform.position.x,endRoom.transform.position.y-yOffset),0.2f,roomLayer))//下
        {
            rooms.Add(Instantiate(roomPrefab,new Vector2(endRoom.transform.position.x,endRoom.transform.position.y+yOffset),Quaternion.identity).GetComponent<Room>());
            rooms[8].downRoom=true;
            endRoom.GetComponent<Room>().upRoom=true;
            Debug.Log("下");
        }
        else
        {
            rooms.Add(Instantiate(roomPrefab,new Vector2(endRoom.transform.position.x+xOffset,endRoom.transform.position.y),Quaternion.identity).GetComponent<Room>());
            rooms[8].leftRoom=true;
            endRoom.GetComponent<Room>().rightRoom=true;
            Debug.Log("左");
        }
        
        
        endRoom=rooms[8].gameObject;
        // endRoom.GetComponent<SpriteRenderer>().color=endColor;
        GameManager.instance.endRoom=endRoom;
    }

    void Update()
    {
        
    }

    public void ChangePointPos()
    {
        while (Physics2D.OverlapCircle(generatorPoint.position,0.2f,roomLayer))
        {
            direction= (Direction)Random.Range(0,4);
            switch (direction)
            {
                case Direction.up:
                    generatorPoint.Translate(new Vector3(0,yOffset,0),Space.Self);
                    break;
                case Direction.down:
                    generatorPoint.Translate(new Vector3(0,-yOffset,0),Space.Self);
                    break;
                case Direction.left:
                    generatorPoint.Translate(new Vector3(-xOffset,0,0),Space.Self);
                    break;
                case Direction.right:
                    generatorPoint.Translate(new Vector3(xOffset,0,0),Space.Self);
                    break;
            }
        }
        
    }

    public void SetupDoor(Room newRoom,Vector3 roomPos)
    {
        newRoom.upRoom=Physics2D.OverlapCircle(roomPos+new Vector3(0,yOffset,0),0.2f,roomLayer);
        newRoom.downRoom=Physics2D.OverlapCircle(roomPos+new Vector3(0,-yOffset,0),0.2f,roomLayer);
        newRoom.leftRoom=Physics2D.OverlapCircle(roomPos+new Vector3(-xOffset,0,0),0.2f,roomLayer);
        newRoom.rightRoom=Physics2D.OverlapCircle(roomPos+new Vector3(xOffset,0,0),0.2f,roomLayer);
    }
}
