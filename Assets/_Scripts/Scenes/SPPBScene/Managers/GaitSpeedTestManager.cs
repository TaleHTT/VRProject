using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GaidSpeedTestManager : MonoBehaviour
{
    public static GaidSpeedTestManager instance;
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
        SPPBLevelManager.GaitSpeedTestActionStart += StartTimeRecord;
        SPPBLevelManager.GaitSpeedTestActionEnd += EndTimeRecord;
        SPPBLevelManager.GaitSpeedTestActionEnd += GaitSpeedTestEndPrint;
    }

    public void GaitSpeedTestEndPrint()
    {
        Debug.Log($"<color=red>GaitSpeedTest startTime: {startTime}</color>");
        Debug.Log($"<color=red>GaitSpeedTest endTime: {endTime}</color>");
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
