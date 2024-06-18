using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyLogic : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player") GameObject.FindGameObjectWithTag("GM").GetComponent<Gamemanager>().OnPlayerKeyCollection(GetComponent<AutoSetIndex>().index);
    }
}
