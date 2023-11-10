using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageDecline : MonoBehaviour
{
    GameObject parent;
    EnemyMessage em;

    // Start is called before the first frame update
    void Start()
    {
        parent = gameObject.transform.parent.gameObject;
        em = parent.GetComponent<EnemyMessage>();
    }

    public void onClick()
    {
        em.declined = true;
    }
}
