using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChairStandTestManager : MonoBehaviour
{
    public static ChairStandTestManager instance;
    public float startTime;
    public float endTime;
    public float waitTime = 0.5f;
    public int score;

    private TextMeshProUGUI ChairStandTestScore;
    
    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        instance = this;
        ChairStandTestScore = GameObject.Find("ChairStandTestScore").GetComponent<TextMeshProUGUI>();
        SPPBLevelManager.ChairStandTestActionStart += StartTest;
        SPPBLevelManager.ChairStandTestActionEnd += EndTest;
    }

    public void StartTest()
    {
        startTime = Time.time;
        Timer.instance.TimerUpStart(60f);
    }

    public void EndTest()
    {
        ScoreCal();
        ChairStandTestScore.text = SPPBLevelManager.instance.scoreInChairStandTest.ToString();
        SPPBLevelManager.instance.chairStandTestIsPassed = true;
    }

    public void ScoreCal()
    {
        float gapTime = endTime - startTime;
        if (gapTime <= 11.19f)
        {
            score = 4;
        }
        else if (gapTime < 13.69f)
        {
            score = 3;
        }
        else if (gapTime < 16.69f)
        {
            score = 2;
        }
        else if (gapTime < 60)
        {
            score = 1;
        }
        SPPBLevelManager.instance.scoreInChairStandTest = score;
    }
}
