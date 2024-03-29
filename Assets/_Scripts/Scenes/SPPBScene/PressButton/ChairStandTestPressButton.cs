using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChairStandTestPressButton : MonoBehaviour
{
    public void OnButtonPress()
    {
        Debug.Log("ChairStandTestPressButton is Press");
        SPPBLevelManager.instance.testType = E_SPPBTestType.ChairStandTest;
        SPPBLevelManager.instance.ChangeOrSetTestType();
    }
}
