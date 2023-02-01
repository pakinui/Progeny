using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausePanelController : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnEnable()
    {
        Time.timeScale = 0;
    }

    public void ClosePanel()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        Time.timeScale = 1;
    }
}
