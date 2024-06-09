using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class AutoSetCheckpoints : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        if(GetComponent<CheckpointLogic>().index == -1)
        {
            Gamemanager gm = GameObject.FindGameObjectWithTag("GM").GetComponent<Gamemanager>();

            GetComponent<CheckpointLogic>().index = gm.OnAddCheckpoint(GetComponent<CheckpointLogic>());

        }
    }
}
