using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameManager instance;
    public SceneFader sceneFader;
    public Room currentRoom;
    public GameObject endRoom;
    [Header("power")]
    public int blood=10;
    public int maxBlood=10;
    public int level=0;
    public float ad=1;
    public float shootSpeed=0.3f;//间隔
    public int BoomNum=0;
    public bool stop;


    private void Awake() {
        if(instance!=null)
        {
            Destroy(this.gameObject);
            return;
        }
        instance=this;
        DontDestroyOnLoad(this);
        
    }
    public static void RegisterSceneFader(SceneFader obj)
    {
        instance.sceneFader=obj;
    }



}
