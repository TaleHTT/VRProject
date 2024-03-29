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
    public SteamVR_Action_Boolean input;


    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (input.GetStateDown(SteamVR_Input_Sources.Any) && !SPPBLevelManager.instance.testIsActive)
        {
            //第一次按下
            //Debug.Log("Button A is pressed");
            SPPBLevelManager.instance.MainTest();
            SPPBLevelManager.instance.testIsActive = true;
        }
        else if(input.GetStateDown(SteamVR_Input_Sources.Any) && SPPBLevelManager.instance.testIsActive)
        {
            //第二次按下
            SPPBLevelManager.instance.testIsActive = false;
            SPPBLevelManager.SecondButtonPress();
        }
    }
}
