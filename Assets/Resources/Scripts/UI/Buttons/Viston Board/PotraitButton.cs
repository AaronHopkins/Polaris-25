using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotraitButton : MonoBehaviour
{
    [SerializeField] Transform vistionBoard;
    [SerializeField] VistonBoardSave VS;
    private int portraitNum;

    private void Awake()
    {
        if (vistionBoard == null)
        {
            Transform perant = transform.parent;
            while (perant == null)
            {
                vistionBoard = perant.transform;
                if (vistionBoard == null)
                {
                    break;
                }
                perant = perant.parent;
            }
        }

        if (VS == null)
            VS = vistionBoard.Find("SaveButton").GetComponent<VistonBoardSave>();

        switch (this.gameObject.name)
        {
            case "PortraitOne":
                portraitNum = 1;
                break;
            case "PortraitTwo":
                portraitNum = 2;
                break;
            case "PortraitThree":
                portraitNum = 3;
                break;
            case "PortraitFour":
                portraitNum = 4;
                break;
        }
    }

    public void onClick()
    {
        VS.portrait = portraitNum;
    }
}
