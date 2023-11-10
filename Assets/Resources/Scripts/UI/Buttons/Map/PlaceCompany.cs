using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaceCompany : MonoBehaviour
{
    TerrainGeneration TG;
    [SerializeField] Transform planetText;
    [SerializeField] Canvas canvas;
    Text txt;
    int count = 0;

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

        TG = GameObject.FindGameObjectWithTag("Manager").GetComponent<TerrainGeneration>();

        if (planetText == null)
            planetText = canvas.transform.Find("CurrentPlanet_Background");

        txt = planetText.Find("CurrentPlanet_txt").GetComponent<Text>();

    }

    public void onClick()
    {
        if(TG.pCOnMap == false)
        {
            TG.setPCPlaced(true);
            txt.text = "Place Your Company";
            count++;
        }
    }
}
