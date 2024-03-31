using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;
using Valve.VR;

public enum E_BalanceTestPart
{
    None,
    Part_1,
    Part_2,
    Part_3,
}

/// <summary>
/// ��Ϊ�����ⷽʽ�����������಻ͬ��ʵ�ֱȽ�����
/// </summary>
public class BalanceTestManager : MonoBehaviour
{
    public static BalanceTestManager instance;
    public float Part_1_StartTime;
    public float Part_1_EndTime;
    public float Part_2_StartTime;
    public float Part_2_EndTime;
    public float Part_3_StartTime;
    public float Part_3_EndTime;

    public int Part_1_Score;
    public int Part_2_Score;
    public int Part_3_Score;
    public int score = 0;

    public bool inPart_1 = false;
    public bool inPart_2 = false;
    public bool inPart_3 = false;

    public E_BalanceTestPart balanceTestPart;
    
    private TextMeshProUGUI BalanceTestScore;

    //private bool isOnTimeRecord = false;

    public int pressCount = 0;

    private void Awake()
    {
        instance = this;
        SPPBLevelManager.BalanceTestAction += BalanceTestStart;
        BalanceTestScore = GameObject.Find("BalanceTestScore").GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        BalanceTestDetect();
    }

    public void BalanceTestStart()
    {
        
    }

    public void BalanceTestDetect()
    {
        if(SPPBLevelManager.isOnBalanceTest)
        {
            /*if (pressCount == 0)
            {
                Part_1_StartTime = Time.time;
                Debug.Log("Part_1 Start");
                inPart_1 = true;
            }
            else if (inPart_1 && !inPart_2)
            {
                Part_1_EndTime = Time.time;
                if (Part_1_EndTime - Part_1_StartTime > 10f || PlayerPressButtonA.instance.input.GetStateDown(SteamVR_Input_Sources.Any))
                {
                    Debug.Log("Part_1 End");
                    Part_1_Score = 1;
                    pressCount++;
                    inPart_2 = true;
                    Part_2_StartTime = Time.time;
                    Debug.Log("Part_2 Start");
                }
            }
            else if (inPart_2 && !inPart_3)
            {
                Part_2_EndTime = Time.time;
                if (Part_2_EndTime - Part_2_StartTime > 10f)
                {
                    Debug.Log("Part_2 End");
                    Part_2_Score = 1;
                    inPart_3 = true;
                    Part_3_StartTime = Time.time;
                    Debug.Log("Part_3 Start");
                }
            }
            else
            {
                Part_3_EndTime = Time.time;
                if (Part_3_EndTime - Part_3_StartTime > 10)
                {
                    Part_3_Score = 2;
                }
                else if (Part_3_EndTime - Part_3_StartTime > 3 && Part_3_EndTime - Part_3_StartTime < 10f)
                {
                    Part_3_Score = 1;
                }
            }*/

            switch (pressCount)
            {
                case 1:
                    if (!inPart_1)
                    {
                        Debug.Log("Part_1 Start");
                        Timer.instance.TimerDownStart(10f);
                        Part_1_StartTime = Time.time;
                        inPart_1 = true;
                    }
                    Part_1_EndTime = Time.time;
                    break;
                case 2:
                    if (inPart_1)
                    {
                        Debug.Log("Part_1 End");
                        inPart_1 = false;
                        if (Part_1_EndTime - Part_1_StartTime > 10f)
                        {
                            Part_1_Score = 1;
                        }
                        score += Part_1_Score;
                    }
                    break;
                case 3:
                    if (!inPart_2)
                    {
                        Debug.Log("Part_2 Start");
                        Timer.instance.TimerDownStart(10f);
                        Part_2_StartTime = Time.time;
                        inPart_2 = true;
                    }
                    Part_2_EndTime = Time.time;
                    break;
                case 4:
                    if (inPart_2)
                    {
                        Debug.Log("Part_2 End");
                        inPart_2= false;
                        if (Part_2_EndTime - Part_2_StartTime > 10f)
                        {
                            Part_2_Score = 1;
                        }
                        score += Part_2_Score;
                    }
                    break;
                case 5:
                    if (!inPart_3)
                    {
                        Debug.Log("Part_3 Start");
                        Timer.instance.TimerDownStart(10f);
                        Part_3_StartTime = Time.time;
                        inPart_3 = true;
                    }
                    Part_3_EndTime = Time.time;
                    break;
                case 6:
                    if (inPart_3)
                    {
                        Debug.Log("Part_3 End");
                        inPart_3 = false;
                        if (Part_3_EndTime - Part_3_StartTime > 10f)
                        {
                            Part_3_Score = 2;
                        }
                        else if (Part_3_EndTime - Part_3_StartTime > 3f)
                        {
                            Part_3_Score = 1;
                        }
                        else
                        {
                            Part_3_Score = 0;
                        }

                        score += Part_3_Score;
                        SPPBLevelManager.instance.scoreInBalanceTest = score;
                        //Debug.Log(score);
                        pressCount = 0;
                        SPPBLevelManager.instance.balanceTestIsPassed = true;
                        SPPBLevelManager.isOnBalanceTest = false;
                        BalanceTestScore.text = SPPBLevelManager.instance.scoreInBalanceTest.ToString();
                        //SPPBLevelManager.instance.EndTest();
                    }
                    break;
            }
        }
    }


    /*public float totalTime = 10f; // �ܼ�ʱʱ��
    private float currentTime; // ��ǰ��ʱʱ��
    private TextMeshProUGUI timerText; // ��ʱ�����ı����

    public void TimerStart()
    {
        timerText.gameObject.SetActive(false);
        timerText.gameObject.SetActive(true);
        StartCoroutine(UpdateTimer());
    }

    private IEnumerator UpdateTimer()
    {
        currentTime = totalTime;
        while (currentTime > 0)
        {
            if(PlayerPressButtonA.instance.input.GetStateDown(SteamVR_Input_Sources.Any))
            {
                break;
            }
            currentTime -= Time.deltaTime;
            timerText.text = currentTime.ToString("F2"); // ����ʱ��ʱ����ʾΪ��λС��
            yield return null;
        }
        timerText.text = "Time's up!";
    }*/
}
