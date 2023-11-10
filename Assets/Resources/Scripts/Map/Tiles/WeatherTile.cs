using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherTile : Node
{

    public int weatherType;
    public int duration;

    private void Awake()
    {
        switch (weatherType)
        {
            case 1:

                break;
            case 2:

                break;
        }
    }

    private void Start()
    {
        weatherType = Random.Range(0, 2);

        switch (weatherType)
        {
            case 0:
                terrainName = "Lightning Storm";
                description = terrainName + ": A storm characterized by the presence of lightning and its acoustic effect on the planet's atmosphere";
                break;
            case 1:
                terrainName = "Dust Wave";
                description = terrainName + ": A strom that contains the dust and small rocks that will slowly move through terrain";
                break;
            case 2:
                terrainName = "Typhon";
                description = terrainName + ": A mountains are formed by the effects of folding on layers within the upper part of the planet's crust.";
                break;
        }
    }

    private void Update()
    {
        if (duration == 0)
        {

        }
    }

    public void setWeatherTiles()
    {
        //set the tiles around this one to weather tiles
    }
}
