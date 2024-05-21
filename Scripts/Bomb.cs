using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    Animator an;
    CircleCollider2D cl;
    float time=0;
    // Start is called before the first frame update
    void Start()
    {
        an=GetComponent<Animator>();
        cl=GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        bombforTimeOut();
    }

    void bombforTimeOut()
    {
        time+=Time.deltaTime;
        if(time>5f)
        {
            bomb();
        }
    }

    void bomb()
    {
        an.SetBool("Bomb",true);
        StartCoroutine(fire());
    }

    
    IEnumerator fire()
    {
        yield return new WaitForSeconds(1.25f);
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Enemy"))
        {
            bomb();
        }
    }
}
