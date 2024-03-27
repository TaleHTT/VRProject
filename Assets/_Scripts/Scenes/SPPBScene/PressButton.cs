using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.Newtonsoft.Json.Bson;
using Valve.VR.InteractionSystem;

public class PressButton : MonoBehaviour
{
    GameObject SPPBStartHint;

    private void Awake()
    {
        SPPBStartHint = GameObject.Find("SPPBStartHint");
        SPPBStartHint.SetActive(false);
    }

    public void OnButtonPress()
    {
        //SPPBStartHint = GameObject.Find("SPPBSTartHint");
        SPPBStartHint.gameObject.SetActive(true);
        Debug.Log("OnButton is Press");
    }
}
