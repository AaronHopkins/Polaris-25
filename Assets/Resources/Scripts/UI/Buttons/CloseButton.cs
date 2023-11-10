using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseButton : MonoBehaviour
{

    [SerializeField] CanvasGroup window;

    private void Awake()
    {
        if (window == null)
        {
            Transform perant = transform.parent;
            while (perant == null)
            {
                window = perant.GetComponent<CanvasGroup>();
                if (window == null)
                {
                    break;
                }
                perant = perant.parent;
            }
        }
    }

    public void onClick()
    {
        window.alpha = 0;
        window.blocksRaycasts = false;
        window.interactable = true;
    }
}
