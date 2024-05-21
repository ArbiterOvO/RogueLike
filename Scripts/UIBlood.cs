using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBlood : MonoBehaviour
{
    public Image mask;
    float originalSize;
    
    // Start is called before the first frame update
    void Start()
    {
        originalSize=mask.rectTransform.rect.width;
    }

    // Update is called once per frame
    void Update()
    {
        mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal,
        originalSize * GameManager.instance.blood/GameManager.instance.maxBlood);
    }
}
