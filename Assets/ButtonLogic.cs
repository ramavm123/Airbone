using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonLogic : MonoBehaviour
{
    bool pressed = false;
    public void OnButtonPressed()
    {
        if (!pressed)
        {
            pressed = true;
            GetComponent<Animator>().SetTrigger("OnButtonPress");
            GameObject.FindGameObjectWithTag("GM").GetComponent<Gamemanager>().OnRespawn();

        }
    }
}
