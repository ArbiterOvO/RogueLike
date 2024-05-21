using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI blood;
    public Text ad,shootSpeed,bomb;
    public Button esc,backGame,exitGame;
    public Slider music;
    public Toggle t;
    public AudioSource m;
    public GameObject Panel;
    bool isTest;
    void Start()
    {
        reSet();
        esc.onClick.AddListener(Esc);
        backGame.onClick.AddListener(back);
        exitGame.onClick.AddListener(exit);
        m=GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        updateUI();
        getEsc();
        updateMusic();
        if(GameManager.instance.stop)
        {
            Time.timeScale = 0;
            Panel.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            Panel.SetActive(false);
        }
        die();
        win();
        test();
    }

    void die()
    {
        if(GameManager.instance.blood<=0)
        {
            SceneManager.LoadScene(2);
        }
    }
    void updateUI()
    {
        blood.text=Convert.ToString(GameManager.instance.blood)+"/"+Convert.ToString(GameManager.instance.maxBlood);
        ad.text="攻击力："+Convert.ToString(GameManager.instance.ad);
        shootSpeed.text="射速："+(1.0f/GameManager.instance.shootSpeed).ToString("#0.00");
        bomb.text="手雷数："+GameManager.instance.BoomNum.ToString();
    }

    void Esc()
    {
        GameManager.instance.stop=true;
    }

    void getEsc()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(!GameManager.instance.stop)
            Esc();
            else
            GameManager.instance.stop=false;
        }
    }

    void back()
    {
        GameManager.instance.stop=false;
    }

    void exit()
    {
        Application.Quit();
        //UnityEditor.EditorApplication.isPlaying = false;
    }

    void updateMusic()
    {
        m.volume=music.value;
    }

    void reSet()
    {
        Boss.blood=150f;
        GameManager.instance.blood=10;
        GameManager.instance.maxBlood=10;
        GameManager.instance.level=0;
        GameManager.instance.ad=1;
        GameManager.instance.shootSpeed=0.3f;
        GameManager.instance.BoomNum=0;
        GameManager.instance.stop=false;
    }
    void test()
    {
        if(t.isOn&&!isTest)
        {
            isTest=true;
            GameManager.instance.blood=100;
            GameManager.instance.maxBlood=100;
            GameManager.instance.shootSpeed=0.1f;
        }
    }
    void win()
    {
        if(Boss.blood<=0)
        StartCoroutine(youwin());
    }

    IEnumerator youwin()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(3);
    }
}
