using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MountinTile : Node
{

    private int mountinType;

    private void Start()
    {
        mountinType = Random.Range(0,2);

        switch (mountinType)
        {
            case 0:
                terrainName = "Volcanic Mountin";
                description = terrainName + ": Volcanic Activity is rife in this area. Settling here is not possible but surrounding areas may reap extra benefits.";
                break;
            case 1:
                terrainName = "Mountains";
                description = terrainName + ": A rocky mountain area. Not good for settling but provides good cover.";
                break;

        }
    }

}
