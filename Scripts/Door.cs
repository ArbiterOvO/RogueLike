using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    // Start is called before the first frame update
    float yOffset=50f;
    float xOffset=60f;
    public GameObject player;
    public GameObject c;

    bool hasChanged=false;
    private void Start() {
        player=GameObject.Find("Player");
        c=GameObject.Find("Main Camera");
    }

    IEnumerator change(float px,float py,float rx,float ry)
    {
        //设置房间已改变
        hasChanged=true;
        //切换动画
        GameManager.instance.sceneFader.FadeOut();
        yield return new WaitForSeconds(1f);
        Debug.Log("1");
        //切换角色
        player.transform.position=new Vector2(player.transform.position.x+px,player.transform.position.y+py);
        //切换摄像机
        c.transform.position=new Vector3(c.transform.position.x+rx,c.transform.position.y+ry,c.transform.position.z);
        //初始化
        hasChanged=false;
        yield return null;
    }

    private void OnTriggerStay2D(Collider2D other) {

        if(other.gameObject.tag.Equals("Player")&&!hasChanged&&GameManager.instance.currentRoom.hasChoose)
        {
            switch(this.gameObject.tag)
            {
                case "TopDoor":
                    StartCoroutine(change(0,yOffset,0,120f));
                    //changeRoom(0,yOffset,0,120f);
                    break;
                case "RightDoor":
                    StartCoroutine(change(xOffset,0,220f,0));
                    //changeRoom(xOffset,0,220f,0);
                    break;
                case "BottomDoor":
                    StartCoroutine(change(0,-yOffset,0,-120f));
                    //changeRoom(0,-yOffset,0,-120f);
                    break;
                case "LeftDoor":
                    StartCoroutine(change(-xOffset,0,-220f,0));
                    //changeRoom(-xOffset,0,-220f,0);
                    break;
            }
        }
    }
}
