using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTurnButton : MonoBehaviour
{
    [SerializeField] TurnManagemet tM;
    [SerializeField] TerrainGeneration tg;
    PlayerCompany pc;
    GameManager gm;
    [SerializeField] List<CanvasGroup> windows = new List<CanvasGroup>();
    Canvas canvas;

    void Start()
    {
        GameObject manager = GameObject.FindGameObjectWithTag("Manager");

        tM = manager.GetComponent<TurnManagemet>();
        pc = manager.GetComponent<PlayerCompany>();
        gm = manager.GetComponent<GameManager>();
        tg = manager.GetComponent<TerrainGeneration>();
        canvas = (Canvas)GameObject.FindObjectsOfType(typeof(Canvas))[1];

        windows.Add(canvas.transform.Find("InfoWindow").GetComponent<CanvasGroup>());
        windows.Add(canvas.transform.Find("VistionBoard_Window").GetComponent<CanvasGroup>());
        windows.Add(canvas.transform.Find("Marketing_Window").GetComponent<CanvasGroup>());
        windows.Add(canvas.transform.Find("UpgradeWindow").GetComponent<CanvasGroup>());
        windows.Add(canvas.transform.Find("CompanyProfile_Window").GetComponent<CanvasGroup>());
        windows.Add(canvas.transform.Find("Income_Window").GetComponent<CanvasGroup>());
        windows.Add(canvas.transform.Find("EnemyProfile_Window").GetComponent<CanvasGroup>());
        windows.Add(canvas.transform.Find("Trading_Window").GetComponent<CanvasGroup>());
        windows.Add(canvas.transform.Find("Com_Window").GetComponent<CanvasGroup>());
        windows.Add(canvas.transform.Find("Request_Window").GetComponent<CanvasGroup>());
    }

    public void onClick()
    {
        if (gm.getLoseOfFood() == true || tM.currentTurn == tg.maxTurns + 1)
        {
            return;
        }

        pc.playerBuilding.idle = false;
        tM.endTurn();

        foreach(CanvasGroup w in windows)
        {
            if (w.alpha == 1)
            {
                w.alpha = 0;
                w.interactable = false;
                w.blocksRaycasts = false;
            }
        }
    }
}
