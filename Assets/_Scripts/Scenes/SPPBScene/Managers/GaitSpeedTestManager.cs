using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GaidSpeedTestManager : MonoBehaviour
{
    public static GaidSpeedTestManager instance;
    public float startTime;
    public float endTime;
    public float waitTime = 0.5f;

    public int score = 0;

    private TextMeshProUGUI GaitSpeedTestScore;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        instance = this;
        GaitSpeedTestScore = GameObject.Find("GaitSpeedTestScore").GetComponent<TextMeshProUGUI>();
        
        SPPBLevelManager.GaitSpeedTestActionStart += StartTest;
        SPPBLevelManager.GaitSpeedTestActionEnd += EndTest;
        //SPPBLevelManager.GaitSpeedTestActionEnd += GaitSpeedTestEndPrint;
        //SPPBLevelManager.GaitSpeedTestActionEnd += EndScoreSet;
    }

    public void GaitSpeedTestEndPrint()
    {
        Debug.Log($"<color=red>GaitSpeedTest startTime: {startTime}</color>");
        Debug.Log($"<color=red>GaitSpeedTest endTime: {endTime}</color>");
    }
    
    /*public void EndTimeRecord()
    {
        endTime = Time.time - 0.5f;
    }*/

    public void EndTest()
    {
        Debug.Log("GaitSpeedTest End");
        Timer.instance.TimerStopShowing();
        SPPBLevelManager.instance.testIsActive = false;
        endTime = Time.time - 0.5f;
        ScoreCal();
        GaitSpeedTestScore.text = SPPBLevelManager.instance.scoreInGaitSpeedTest.ToString();
        SPPBLevelManager.instance.gaitSpeedTestIsPassed = true;
    }

    public void StartTest()
    {
        StartCoroutine(Wait(waitTime));
        Debug.Log("GaitSpeedTest Start");
        SPPBLevelManager.instance.testIsActive = true;
        startTime = Time.time;
        Timer.instance.TimerUpStart(60f);
    }

    public void ScoreCal()
    {
        float gapTime = endTime - startTime;
        if (gapTime < 4.82)
        {
            score = 4;
        }
        else if(gapTime < 6.20f)
        {
            score = 3;
        }
        else if(gapTime < 8.70f)
        {
            score = 2;
        }
        else
        {
            score = 1;
        }
        SPPBLevelManager.instance.scoreInGaitSpeedTest = score;
    }

    IEnumerator Wait(float waitForSeconds)
    {
        yield return new WaitForSeconds(waitForSeconds);
    }
    
    
}
