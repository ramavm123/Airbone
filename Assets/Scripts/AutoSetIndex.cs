using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[ExecuteInEditMode]
public class AutoSetIndex : MonoBehaviour
{
    public int index = -1;
    [SerializeField] private Component componentToCheck;
    // Start is called before the first frame update
    void Awake()
    {
        if(index == -1)
        {
            int searchIndex = -1;
            foreach (MonoBehaviour _o in GameObject.FindObjectsOfType(componentToCheck.GetType()))
            {
                
                int objectiveIndex = _o.gameObject.GetComponent<AutoSetIndex>().index;
                if (objectiveIndex > searchIndex)
                {
                    searchIndex = objectiveIndex;
                }
            }
            index = searchIndex + 1;
        }
    }
}
