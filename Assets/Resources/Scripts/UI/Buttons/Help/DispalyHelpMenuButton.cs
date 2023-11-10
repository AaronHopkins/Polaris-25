using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DispalyHelpMenuButton : MonoBehaviour
{
    [SerializeField] CanvasGroup helpMenu;

    public void onClick()
    {
        helpMenu.alpha = 1;
        helpMenu.interactable = true;
        helpMenu.blocksRaycasts = true;
    }
}
