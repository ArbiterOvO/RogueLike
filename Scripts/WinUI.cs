using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class WinUI : MonoBehaviour
{
    public Button title,exit;
    // Start is called before the first frame update
    void Start()
    {
        title.onClick.AddListener(returnTitle);
        exit.onClick.AddListener(Exit);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void returnTitle()
    {
        SceneManager.LoadScene(0);
    }

    void Exit()
    {
        Application.Quit();
        //UnityEditor.EditorApplication.isPlaying = false;
    }
}
