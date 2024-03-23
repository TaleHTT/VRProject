using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadForestScene : MyUIElement
{
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void OnButtonClick()
    {
        base.OnButtonClick();
        SceneManager.LoadScene("ForestScene");
    }
}
