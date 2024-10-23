using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Levels : MonoBehaviour
{
    public int cashOnComplete = 500;
    public UnityEvent OnInitEvent, OnCompleteEvent, OnFailedEvent;

    public void InitializeLevel()
    {
        OnInitEvent?.Invoke();
    }

    public void CurrentLevelComplete()
    {
        OnCompleteEvent?.Invoke();
    }

    public void LevelFailed()
    {
        OnFailedEvent?.Invoke();
    }
}
