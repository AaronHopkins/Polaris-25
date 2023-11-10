using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RerollMap : MonoBehaviour
{

    TerrainGeneration TG;
    TurnManagemet tm;
    EnemyInfo ei;
    [SerializeField] Transform planetText;
    [SerializeField] Canvas canvas;

    Camera mainCam;
    Text txt;

    private void Awake()
    {
        if (canvas == null)
        {
            Transform testCanvasTransform = transform.parent;
            while (testCanvasTransform == null)
            {
                canvas = testCanvasTransform.GetComponent<Canvas>();
                if (canvas != null)
                {
                    break;
                }
                testCanvasTransform = testCanvasTransform.parent;
            }
        }

        tm = GameObject.FindGameObjectWithTag("Manager").GetComponent<TurnManagemet>();
        TG = GameObject.FindGameObjectWithTag("Manager").GetComponent<TerrainGeneration>();
        ei = GameObject.FindGameObjectWithTag("Manager").GetComponent<EnemyInfo>();

        mainCam = Camera.main;

        if (planetText == null)
            planetText = canvas.transform.Find("CurrentPlanet_Background");

        txt = planetText.Find("CurrentPlanet_txt").GetComponent<Text>();
    }

    public void onClick()
    {
        switch (TG.currentPlanet)
        {
            case "Mars":
                mainCam.orthographicSize = 35;
                break;
            case "Moon":
                mainCam.orthographicSize = 20;
                break;
            case "Jupiter":
                mainCam.orthographicSize = 55;                
                break;
        }

        

        txt.text = "Current: " +  TG.currentPlanet;
        if(TG.mapActive == true)
        {
            ei.refresh();
            tm.reloadMap();
            TG.rerollMap();
            TG.gameObject.GetComponent<PlayerCompany>().deleteOutline();
            TG.gameObject.GetComponent<PlayerCompany>().createOutlinePerant();
        }
    }
}
