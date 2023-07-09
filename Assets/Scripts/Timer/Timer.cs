using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timeText;
    static Timer instance;
    float clearTime;

    public static bool stop = false;
    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        instance = this;
    }
    private void FixedUpdate()
    {
        if (stop)
            return;

        clearTime += Time.fixedDeltaTime;
        timeText.text = System.TimeSpan.FromSeconds(value: clearTime).ToString(format: @"mm\:ss\:ff");
    }

    public static string GetTime()
    {
        return instance.timeText.text;
    }
}
