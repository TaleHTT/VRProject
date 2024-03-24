using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonDebugLog : MonoBehaviour
{
    public void Test()
    {
        this.gameObject.SetActive(false);
        Debug.Log("hello");
    }
}
