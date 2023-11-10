using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseTips : MonoBehaviour
{
    [SerializeField] CanvasGroup tips;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void onCLick()
    {
        tips.alpha = 0;
        tips.interactable = false;
        tips.blocksRaycasts = false;
    }
}
