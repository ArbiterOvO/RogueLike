using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartUI : MonoBehaviour
{
    public Button StartButton,AboutButton,QuitButton,Back;
    public GameObject panel;
    // Start is called before the first frame update
    void Start()
    {
        StartButton.onClick.AddListener(start);
        QuitButton.onClick.AddListener(quit);
        AboutButton.onClick.AddListener(about);
        Back.onClick.AddListener(back);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void start()
    {
        SceneManager.LoadScene (1);
    }
    void quit()
    {
        Application.Quit();
        //UnityEditor.EditorApplication.isPlaying = false;
    }
    void about()
    {
        panel.SetActive(true);
    }

    void back()
    {
        panel.SetActive(false);
    }
}
