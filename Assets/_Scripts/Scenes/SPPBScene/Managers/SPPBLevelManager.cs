using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public enum E_SPPBTestType
{
    None,
    GaitSpeedTest,
    ChairStandTest,
    BalanceTest
}

public class SPPBLevelManager : MonoBehaviour
{
    public static SPPBLevelManager instance;
    GameObject VRCamera;
    GameObject EndPoint;
    GameObject SPPBTestStartPanel;
    GameObject SPPBTestStartPanelBG;

    public bool testIsActive = false;

    //private GameObject VRCanvas;
    public float testBeginTime;
    public float testEndTime;

    public static Action GaitSpeedTestActionStart;
    public static Action GaitSpeedTestActionEnd;
    public static Action ChairStandTestActionStart;
    public static Action ChairStandTestActionEnd;

    public static Action SecondButtonPress;

    public bool isGaitSpeedTest;
    public bool isChairStandTest;
    public bool isBalanceTest;

    public E_SPPBTestType testType = 0;

    private GameObject VRInfoCanvas;

    private GameObject BalanceTestText;
    private GameObject ChairStandTestText;
    private GameObject GaitSpeedTestText;
    private GameObject NoneText;

    public bool changeOrSetButton = false;
    public Action ChangeOrSetTestType;

    private float waitTime = 0.5f;
    

    private void Awake()
    {
        Init();
        ChangeOrSetTestType += ChangeType;
    }

    private void Init()
    {
        instance = this;

        BalanceTestText = GameObject.Find("BalanceTestText");
        BalanceTestText.SetActive(false);
        ChairStandTestText = GameObject.Find("ChairStandTestText");
        ChairStandTestText.SetActive(false);
        GaitSpeedTestText = GameObject.Find("GaitSpeedTestText");
        GaitSpeedTestText.SetActive(false);
        NoneText = GameObject.Find("NoneText");
        NoneText.SetActive(false);
        VRInfoCanvas = GameObject.Find("VRInfoCanvas");
        VRInfoCanvas.SetActive(false);

        //VRCanvas = GameObject.Find("VRCanvas");
        VRCamera = GameObject.Find("VRCamera");
        EndPoint = GameObject.Find("EndPoint");

        SPPBTestStartPanel = GameObject.Find("SPPBTestStartPanel");
        SPPBTestStartPanelBG = GameObject.Find("SPPBTestStartPanelBG");
    }

    private void Update()
    {
        GaitSpeedTestEndDetect();
    }

    public void MainTest()
    {
        switch (testType)
        {
            case E_SPPBTestType.BalanceTest:
                BalanceTest();
                break;
            case E_SPPBTestType.ChairStandTest:
                ChairStandTest(); 
                break;
            case E_SPPBTestType.GaitSpeedTest:
                GaitSpeedTest();
                break;
            case E_SPPBTestType.None:
                None();
                break;
        }
    }

    #region TestDetail

    public void None()
    {
        StartCoroutine(IE_None());
    }

    IEnumerator IE_None()
    {
        NoneText.SetActive(true);
        yield return new WaitForSeconds(waitTime);
        NoneText.SetActive(false);
    }

    public void BalanceTest()
    {
        StartCoroutine(IE_BalanceTest());
    }

    IEnumerator IE_BalanceTest()
    {
        BalanceTestText.SetActive(true);
        yield return new WaitForSeconds(waitTime);
        BalanceTestText.SetActive(false);
    }

    public void ChairStandTest()
    {
        SecondButtonPress += ChairStandTestSecondButtonPress;
        StartCoroutine(IE_ChairStandTest());
    }

    //ChairStand测试中第二次按下为测试结束标志
    private void ChairStandTestSecondButtonPress()
    {
        SecondButtonPress -= ChairStandTestSecondButtonPress;
        ChairStandTestManager.instance.endTime = Time.time;
    }

    IEnumerator IE_ChairStandTest()
    {
        ChairStandTestText.SetActive(true);
        yield return new WaitForSeconds(waitTime);
        ChairStandTestText.SetActive(false);

        ChairStandTestActionStart();
    }

    public void GaitSpeedTest()
    {
        SecondButtonPress += GaitSpeedTestSecondButtonPress;
        StartCoroutine(IE_GaitSpeedTest());
    }

    IEnumerator IE_GaitSpeedTest()
    {
        GaitSpeedTestText.SetActive(true);
        yield return new WaitForSeconds(waitTime);
        GaitSpeedTestText.SetActive(false);

        GaitSpeedTestActionStart();
    }

    //GaitSpeed测试中第二次按A为中断测试
    private void GaitSpeedTestSecondButtonPress()
    {
        SecondButtonPress -= GaitSpeedTestSecondButtonPress;
    }

    private bool passOnce = false;
    public void GaitSpeedTestEndDetect()
    {
        if (VRCamera.transform.position.z < EndPoint.transform.position.z && !passOnce)
        {
            passOnce = true;
            Debug.Log("GaitSpeedTest已完成");
            SPPBTestStartPanelBG.GetComponent<Image>().color = new Color(79f / 255f, 242f / 255f, 28f / 255f, 37f / 255f);
            SPPBTestStartPanel.GetComponentInChildren<TextMeshProUGUI>().text = "SPPB测试未进行";
            StartCoroutine(ShowCanvas());
            GaitSpeedTestActionEnd();
        }
    }

    #endregion

    public void ChangeType()
    {
        string gameObjectName = testType.ToString() + "Text";
        Debug.Log("Test Type Change to " + testType.ToString() + "Text");
        ShowTextType(gameObjectName);
    }

    public void ShowTextType(string gameObjectName)
    {
        StartCoroutine(IE_ShowTextType(gameObjectName));
    }

    IEnumerator IE_ShowTextType(string gameObjectName)
    {
        
        GameObject.Find(gameObjectName).SetActive(true);
        yield return new WaitForSeconds(waitTime);
        GameObject.Find(gameObjectName).SetActive(false);

    }

    IEnumerator ShowCanvas()
    {
        VRInfoCanvas.SetActive(true);
        VRInfoCanvas.GetComponentInChildren<TextMeshProUGUI>().text = "测试完成";
        yield return new WaitForSeconds(3);
        VRInfoCanvas.SetActive(false);
    }

/*    public void Wait(float wait)
    {
        StartCoroutine(IE_Wait(waitTime));
    }

    IEnumerator IE_Wait(float wait)
    {
        yield return new WaitForSeconds(wait);
    }*/

    public void CalPoint()
    {
        Debug.Log("CalPoint Start");
    }
}
