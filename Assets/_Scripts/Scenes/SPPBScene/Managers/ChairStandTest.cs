using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChairStandTestManager : MonoBehaviour
{
    public static ChairStandTestManager instance;
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
        SPPBLevelManager.ChairStandTestActionStart += StartTimeRecord;
    }

    public void StartTimeRecord()
    {
        startTime = Time.time;
    }

}
