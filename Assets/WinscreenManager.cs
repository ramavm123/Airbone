using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WinscreenManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI DeathsText;

    public void SetUI(int Deaths)
    {
        DeathsText.text = new string("Deaths: "+Deaths);
    }
}
