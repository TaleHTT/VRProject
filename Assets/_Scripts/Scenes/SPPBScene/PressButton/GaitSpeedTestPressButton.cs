using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GaitSpeedTestPressButton : MonoBehaviour
{
    public void OnButtonPress()
    {
        Debug.Log("GaitSpeedTestPressButton is Press");
        SPPBLevelManager.instance.testType = E_SPPBTestType.GaitSpeedTest;
        SPPBLevelManager.instance.ChangeOrSetTestType();
    }
}
