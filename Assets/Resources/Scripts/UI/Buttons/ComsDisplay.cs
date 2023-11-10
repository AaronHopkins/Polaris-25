using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComsDisplay : MonoBehaviour
{
    [SerializeField] CanvasGroup comsBoard;
    [SerializeField] TerrainGeneration tg;
    [SerializeField] CanvasGroup tips;
    [SerializeField] Canvas canvas;

    int tipCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (canvas == null)
            canvas = canvas = (Canvas)GameObject.FindObjectsOfType(typeof(Canvas))[1];

        if (tg == null)
            tg = GameObject.FindGameObjectWithTag("Manager").GetComponent<TerrainGeneration>();

        if (comsBoard == null)
            comsBoard = canvas.transform.Find("Com_Window").GetComponent<CanvasGroup>();
    }

    public void onClick()
    {

        if (tg.currentPlanet == "Tutorial: Moon" && tipCount == 0)
        {
            tips.alpha = 1;
            tips.interactable = true;
            tips.blocksRaycasts = true;
            tipCount++;
        }

        comsBoard.alpha = 1;
        comsBoard.blocksRaycasts = true;
        comsBoard.interactable = true;
    }
}
