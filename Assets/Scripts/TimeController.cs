using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    public static TimeController instance;


    private TimeSpan timePlaying;
    private bool timerGoing;
    private float elapsedTime;
    string timePlayingStr;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        timerGoing = false;
    }

    public void StartTimer()
    {
        timerGoing = true;
        elapsedTime = 0f;

        StartCoroutine(UpdateTimer());
    }

    public void StopTimer()
    {
        timerGoing = false;
    }

    public string TimeWas()
    {
        return timePlayingStr;
    }
    private IEnumerator UpdateTimer()
    {
        while(timerGoing)
        {
            elapsedTime += Time.deltaTime;
            timePlaying = TimeSpan.FromSeconds(elapsedTime);
            timePlayingStr = timePlaying.ToString("mm'.'ss'.'ff");

            yield return null;
        }
    }

}
