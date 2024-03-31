using System.Collections;
using System.Collections.Generic;
using TMPro;
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
/// 因为该类检测方式和另外两个类不同，实现比较特殊
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

    public bool inPart_1 = false;
    public bool inPart_2 = false;
    public bool inPart_3 = false;

    public E_BalanceTestPart balanceTestPart;

    //private bool isOnTimeRecord = false;

    public int pressCount;

    private void Awake()
    {
        instance = this;
        SPPBLevelManager.BalanceTestAction += BalanceTestStart;
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
        if(PlayerPressButtonA.instance.isOnBalanceTest)
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
                        TimerStart();
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
                    }
                    break;
                case 3:
                    if (!inPart_2)
                    {
                        Debug.Log("Part_2 Start");
                        TimerStart();
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
                    }
                    break;
                case 5:
                    if (!inPart_3)
                    {
                        Debug.Log("Part_3 Start");
                        TimerStart();
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
                            Part_3_Score = 0;
                        }
                        else
                        {
                            Part_3_Score = 1;
                        }
                        pressCount = 0;
                        PlayerPressButtonA.instance.isOnBalanceTest = false;
                        SPPBLevelManager.instance.EndTest();
                    }
                    break;
            }
        }
    }


    public float totalTime = 10f; // 总计时时间
    private float currentTime; // 当前计时时间
    private TextMeshProUGUI timerText; // 计时器的文本组件

    public void TimerStart()
    {
        timerText = GameObject.Find("TimerText").GetComponent<TextMeshProUGUI>();
        currentTime = totalTime;
        StartCoroutine(UpdateTimer());
    }

    private IEnumerator UpdateTimer()
    {
        while (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            timerText.text = currentTime.ToString("F2"); // 将计时器时间显示为两位小数
            yield return null;
        }

        timerText.text = "Time's up!";
    }
}
