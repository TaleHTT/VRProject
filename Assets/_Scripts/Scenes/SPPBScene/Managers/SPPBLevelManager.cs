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
    public static bool isOnBalanceTest = false;
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
    /*public static Action BalanceTestActionStart;
    public static Action BalanceTestActionEnd;*/
    public static Action BalanceTestAction;
    public bool BalanceTimerIsOn;

    public static Action SecondButtonPress = null;

    public bool isGaitSpeedTest;
    public bool isChairStandTest;
    public bool isBalanceTest;

    public E_SPPBTestType testType = 0;

    private GameObject VRInfoCanvas;

    private GameObject BalanceTestText;
    private GameObject ChairStandTestText;
    private GameObject GaitSpeedTestText;
    private GameObject NoneText;
    private GameObject EndTestText;
    private TextMeshProUGUI TotalPesultScore;

    public bool changeOrSetButton = false;
    public Action ChangeOrSetTestType;

    private float waitTime = 0.5f;

    public int scoreInGaitSpeedTest;
    public int scoreInChairStandTest;
    public int scoreInBalanceTest;
    public int totalScore;

    public bool gaitSpeedTestIsPassed = false;
    public bool chairStandTestIsPassed = false;
    public bool balanceTestIsPassed = false;
    

    private void Awake()
    {
        Init();
        ChangeOrSetTestType += ChangeType;
    }

    private void Init()
    {
        instance = this;
        TotalPesultScore = GameObject.Find("TotalResultScore").GetComponent<TextMeshProUGUI>();
        BalanceTestText = GameObject.Find("BalanceTestText");
        BalanceTestText.GetComponent<TextMeshProUGUI>().enabled = false;
        ChairStandTestText = GameObject.Find("ChairStandTestText");
        ChairStandTestText.GetComponent<TextMeshProUGUI>().enabled = false;
        GaitSpeedTestText = GameObject.Find("GaitSpeedTestText");
        GaitSpeedTestText.GetComponent<TextMeshProUGUI>().enabled = false;
        NoneText = GameObject.Find("NoneText");
        NoneText.GetComponent<TextMeshProUGUI>().enabled = false;
        VRInfoCanvas = GameObject.Find("VRInfoCanvas");
        VRInfoCanvas.SetActive(false);
        EndTestText = GameObject.Find("EndTestText");
        EndTestText.GetComponent<TextMeshProUGUI>().enabled = false;
        

        //VRCanvas = GameObject.Find("VRCanvas");
        VRCamera = GameObject.Find("VRCamera");
        EndPoint = GameObject.Find("EndPoint");

        SPPBTestStartPanel = GameObject.Find("SPPBTestStartPanel");
        SPPBTestStartPanelBG = GameObject.Find("SPPBTestStartPanelBG");
    }

    private void Update()
    {
        if (testType.ToString() == "BalanceTest")
        {
            //Debug.Log("Change tyep to BalanceTest");
            isOnBalanceTest = true;
        }
        else
        {
            isOnBalanceTest = false;
        }
        //Debug.Log(isOnBalanceTest);
        GaitSpeedTestEndDetectInUpdate();
        TotalScoreCalAfterThreeTestPass();
    }

    public void TotalScoreCalAfterThreeTestPass()
    {
        Debug.Log($"{balanceTestIsPassed}  {chairStandTestIsPassed}  {gaitSpeedTestIsPassed}");
        if (balanceTestIsPassed && chairStandTestIsPassed && gaitSpeedTestIsPassed)
        {
            totalScore = scoreInBalanceTest + scoreInChairStandTest + scoreInGaitSpeedTest;
            TotalPesultScore.text = totalScore.ToString();
        }
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
        ChairStandTestActionEnd();
    }

    IEnumerator IE_ChairStandTest()
    {
        ChairStandTestText.GetComponent<TextMeshProUGUI>().enabled = true;
        yield return new WaitForSeconds(waitTime);
        ChairStandTestText.GetComponent<TextMeshProUGUI>().enabled = false;

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
        string gameObjectName = "EndTestText";
        ShowTextType(gameObjectName);
        testType = E_SPPBTestType.None;
        SecondButtonPress -= GaitSpeedTestSecondButtonPress;
    }

    private bool passOnce = false;
    public void GaitSpeedTestEndDetectInUpdate()
    {
        if (VRCamera.transform.position.x <= EndPoint.transform.position.x && !passOnce)
        {
            passOnce = true;
           //EndTest();
            /*SPPBTestStartPanelBG.GetComponent<Image>().color = new Color(79f / 255f, 242f / 255f, 28f / 255f, 37f / 255f);
            SPPBTestStartPanel.GetComponentInChildren<TextMeshProUGUI>().text = "SPPB测试未进行";*/
            //StartCoroutine(ShowCanvas());
            GaitSpeedTestActionEnd();
            SPPBLevelManager.instance.gaitSpeedTestIsPassed = true;
        }
    }

    #endregion

    public void ChangeType()
    {
        isOnBalanceTest = false;
        string gameObjectName = testType.ToString() + "Text";
        Debug.Log("Test Type Change to " + testType.ToString() + "Text");
        ShowTextType(gameObjectName);
    }

    public void EndTest()
    {
        string gameObjectName = "EndTestText";
        ShowTextType(gameObjectName);
        testType = E_SPPBTestType.None;
    }

    public void ShowTextType(string gameObjectName)
    {
        StartCoroutine(IE_ShowTextType(gameObjectName));
    }

    IEnumerator IE_ShowTextType(string gameObjectName)
    {

        GameObject.Find(gameObjectName).GetComponent<TextMeshProUGUI>().enabled = true;
        yield return new WaitForSeconds(waitTime);
        GameObject.Find(gameObjectName).GetComponent<TextMeshProUGUI>().enabled = false;

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
