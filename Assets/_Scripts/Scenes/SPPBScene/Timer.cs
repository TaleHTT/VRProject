using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public static Timer instance;
    public float startTime;
    public float endTime;
    public float waitTime = 0.5f;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        instance = this;
        PlayerPressButtonA.SPPBTestStart += StartTimeRecord;
        SPPBLevelManager.SPPBTestEnd += EndTimeRecord;
    }

    public void EndTimeRecord()
    {
        endTime = Time.time - 0.5f;
    }

    public void StartTimeRecord()
    {
        StartCoroutine(Wait(waitTime));
        startTime = Time.time;  
    }

    IEnumerator Wait(float waitForSeconds)
    {
        yield return new WaitForSeconds(waitForSeconds);
    }
}
