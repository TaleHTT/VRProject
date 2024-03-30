using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private bool isOnTimeRecord = false;

    public int pressCount;

    private void Awake()
    {
        instance = this;
        SPPBLevelManager.BalanceTestAction += BalanceTestStart;
    }

    private void Update()
    {
        Detail();
    }

    public void BalanceTestStart()
    {
        pressCount++;
        isOnTimeRecord = true;
        Part_1_StartTime = Time.time;
    }

    public void Detail()
    {
        if(isOnTimeRecord)
        {
            if (!inPart_1)
            {
                Part_1_StartTime = Time.time;
                Debug.Log("Part_1 Start");
                inPart_1 = true;
            }
            else if (inPart_1 && !inPart_2)
            {
                Part_1_EndTime = Time.time;
                if(Part_1_EndTime - Part_1_StartTime > 10f)
                {
                    Debug.Log("Part_1 End");
                    Part_1_Score = 1;
                    inPart_2 = true;
                    Part_2_StartTime = Time.time;
                    Debug.Log("Part_2 Start");
                }
            }
            else if (inPart_2 && !inPart_3)
            {
                Part_2_EndTime = Time.time;
                if(Part_2_EndTime - Part_2_StartTime > 10f)
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
                if(Part_3_EndTime - Part_3_StartTime > 10)
                {
                    Part_3_Score = 2;
                }
                else if(Part_3_EndTime - Part_3_StartTime > 3 && Part_3_EndTime - Part_3_StartTime < 10f)
                {
                    Part_3_Score = 1;
                }
            }
        }
    }
}
