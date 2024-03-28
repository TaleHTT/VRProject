using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR;

public class PlayerPressButtonB : MonoBehaviour
{
    public SteamVR_Action_Boolean input;

    private void Update()
    {
        if (input.GetStateDown(SteamVR_Input_Sources.Any))
        {
            Debug.Log("Button B is pressed");
            SceneManager.LoadScene("StartScene");
        }
    }
}
