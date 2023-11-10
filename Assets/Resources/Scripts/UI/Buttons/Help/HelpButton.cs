using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpButton : MonoBehaviour
{
    [SerializeField] Sprite content;
    [SerializeField] GameObject scrollView;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void onClick()
    {
        scrollView.GetComponent<Image>().sprite = content;
        scrollView.GetComponent<Image>().SetNativeSize();
    }
}
