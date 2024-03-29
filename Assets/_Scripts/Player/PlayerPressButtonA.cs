using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;


public class PlayerPressButtonA : MonoBehaviour
{
    public static PlayerPressButtonA instance{ get; set; }
    public static Action SPPBTestStart;
    public SteamVR_Action_Boolean input;
    private GameObject VRCanvas;

    private void Awake()
    {
        VRCanvas = GameObject.Find("VRCanvas");
        instance = this;
        
    }

    private void Start()
    {
        VRCanvas.SetActive(false);
        SPPBTestStart += ShowCanvas;
    }

    private void Update()
    {
        if (input.GetStateDown(SteamVR_Input_Sources.Any))
        {
            Debug.Log("Button A is pressed");
            SPPBTestStart();
        }
    }
    
    public void ShowCanvas()
    {
        StartCoroutine(IE_ShowCanvas());
    }

    IEnumerator IE_ShowCanvas()
    {
        VRCanvas.SetActive(true);
        yield return new WaitForSeconds(3);
        VRCanvas.SetActive(false);
    }
}
