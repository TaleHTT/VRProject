using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalanceTestPressButton : MonoBehaviour
{
    public void OnButtonPress()
    {
        Debug.Log("BalanceTestPressButton is Press");
        SPPBLevelManager.instance.testType = E_SPPBTestType.BalanceTest;
        SPPBLevelManager.instance.ChangeOrSetTestType();
        PlayerPressButtonA.instance.isOnBalanceTest = true;
    }
}
