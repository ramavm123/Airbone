using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyLogic : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameObject.FindGameObjectWithTag("GM").GetComponent<Gamemanager>().OnPlayerKeyCollection(GetComponent<AutoSetIndex>().index);
            gameObject.SetActive(false);
        }
    }
}
