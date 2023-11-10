using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageAccept : MonoBehaviour
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
        em.accepted = true;
    }
}
