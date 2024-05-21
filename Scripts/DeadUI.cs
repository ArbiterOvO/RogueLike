using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeadUI : MonoBehaviour
{
    // Start is called before the first frame update
    public Button reStart,quit;
    void Start()
    {
        reStart.onClick.AddListener(ReStart);
        quit.onClick.AddListener(Quit);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ReStart()
    {
        SceneManager.LoadScene (1);
    }
    void Quit()
    {
        Application.Quit();
        //UnityEditor.EditorApplication.isPlaying = false;
    }
}
