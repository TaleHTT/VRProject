using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.Newtonsoft.Json.Bson;
using Valve.VR.InteractionSystem;

public class PressButton : MonoBehaviour
{

    public virtual void OnButtonPress(Hand hand)
    {
        Debug.Log("OnButton is Press");
    }
}
