using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneFader : MonoBehaviour
{
    Animator an;
    int faderId;

    private void Start() {
        an=GetComponent<Animator>();
        faderId=Animator.StringToHash("Fade");
        GameManager.RegisterSceneFader(this);
    }
    public void FadeOut()
    {
        an.SetTrigger(faderId);
    }
}
