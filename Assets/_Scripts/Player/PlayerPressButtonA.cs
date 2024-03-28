using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class PlayerPressButtonA : MonoBehaviour
{
    public SteamVR_Action_Boolean input;
    private GameObject VRCanvas;

    private void Awake()
    {
        VRCanvas = GameObject.Find("VRCanvas");
        
    }

    private void Start()
    {
        VRCanvas.SetActive(false);
    }

    private void Update()
    {
        if (input.GetStateDown(SteamVR_Input_Sources.Any))
        {
            Debug.Log("Button A is pressed");
            StartCoroutine(ShowCanvas());
        }
    }
    
    IEnumerator ShowCanvas()
    {
        VRCanvas.SetActive(true);
        yield return new WaitForSeconds(3);
        VRCanvas.SetActive(false);
    }
}
