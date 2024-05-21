using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeBoss : MonoBehaviour
{
    // Start is called before the first frame update
    float blood=30f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        die();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("ZiDan"))
        {
            blood-=GameManager.instance.ad;
            Destroy(other.gameObject);
        }
        if(other.CompareTag("Player"))
        {
            GameManager.instance.blood-=5;
        }
        if(other.CompareTag("Bomb"))
        {
            blood-=20;
        }
    }

    void die()
    {
        if(blood<=0)
        {
            Destroy(gameObject);
        }
    }
}
