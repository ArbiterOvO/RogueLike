using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoosePower : MonoBehaviour
{
    public Button b1,b2,b3;
    public GameObject powerCanvas;
    // Start is called before the first frame update
    void Start()
    {
        b1.onClick.AddListener(adUp);
        b2.onClick.AddListener(shootSpeedUp);
        b3.onClick.AddListener(getBoom);
    }

    // Update is called once per frame
    void Update()
    {
        showUI();
    }

    void showUI()
    {
        if(GameManager.instance.currentRoom!=null&&GameManager.instance.currentRoom.gameObject!=GameManager.instance.endRoom)
        {
            if(GameManager.instance.currentRoom.isClear&&!GameManager.instance.currentRoom.hasChoose)
            {
                powerCanvas.SetActive(true);
            }
        }
        
    }

    void adUp()
    {
        GameManager.instance.ad+=0.5f;
        addBlood();
        powerCanvas.SetActive(false);
        GameManager.instance.currentRoom.hasChoose=true;
    }

    void shootSpeedUp()
    {
        if(GameManager.instance.shootSpeed>0.05f)
        GameManager.instance.shootSpeed-=0.05f;
        addBlood();
        powerCanvas.SetActive(false);
        GameManager.instance.currentRoom.hasChoose=true;
    }

    void addBlood()
    {
        GameManager.instance.maxBlood+=1;
        GameManager.instance.blood=GameManager.instance.maxBlood;
        GameManager.instance.level++;
        powerCanvas.SetActive(false);
        GameManager.instance.currentRoom.hasChoose=true;
    }

    void getBoom()
    {
        GameManager.instance.BoomNum+=1;
        addBlood();
        powerCanvas.SetActive(false);
        GameManager.instance.currentRoom.hasChoose=true;
    }
    
}
