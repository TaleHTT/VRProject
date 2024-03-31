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

    public bool isOnBalanceTest = false;


    private void Awake()
    {
        instance = this;
    }

    public bool firstTimeEnterBalanceTest = true;
    private void Update()
    {
        if (isOnBalanceTest)
        {
            if (input.GetStateDown(SteamVR_Input_Sources.Any) && BalanceTestManager.instance.pressCount == 0)
            {
                SPPBLevelManager.instance.MainTest();
                firstTimeEnterBalanceTest = false;
                SPPBLevelManager.BalanceTestAction();
                BalanceTestManager.instance.pressCount++;
            }
            else if(input.GetStateDown(SteamVR_Input_Sources.Any))
            {
                BalanceTestManager.instance.pressCount++;
            }
        }
        else
        {
            if (input.GetStateDown(SteamVR_Input_Sources.Any) && !SPPBLevelManager.instance.testIsActive)
            {
                //第一次按下
                //Debug.Log("Button A is pressed");
                SPPBLevelManager.instance.MainTest();
                SPPBLevelManager.instance.testIsActive = true;
            }
            else if (input.GetStateDown(SteamVR_Input_Sources.Any) && SPPBLevelManager.instance.testIsActive)
            {
                //第二次按下
                SPPBLevelManager.instance.testIsActive = false;
                SPPBLevelManager.SecondButtonPress();
            }
        }
        
    }
}
