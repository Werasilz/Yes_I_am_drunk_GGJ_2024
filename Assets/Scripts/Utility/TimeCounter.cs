using System.Collections;
using UnityEngine;

public enum CounterType
{
    TimeDecrease,
    TimeIncrease
}

[System.Serializable]
public class TimeCounter
{
    public CounterType counterType;
    public float currentTime;
    public float targetTime;
    public bool isCounting;
    public System.Action<bool> OnCountComplete;
    private Coroutine _countingCoroutine;

    public void StartCounting(MonoBehaviour monoBehaviour, System.Action completeCallback = null, System.Action updateCallback = null)
    {
        if (!isCounting)
        {
            isCounting = true;
            currentTime = 0f;

            if (_countingCoroutine != null)
            {
                monoBehaviour.StopCoroutine(_countingCoroutine);
                _countingCoroutine = null;
            }

            _countingCoroutine = monoBehaviour.StartCoroutine(UpdateTimeCount(completeCallback, updateCallback));
        }
    }

    public void StopCounting(MonoBehaviour monoBehaviour)
    {
        if (isCounting)
        {
            isCounting = false;
            monoBehaviour.StopCoroutine(_countingCoroutine);
            _countingCoroutine = null;
        }
    }

    private IEnumerator UpdateTimeCount(System.Action completeCallback = null, System.Action updateCallback = null)
    {
        if (counterType == CounterType.TimeDecrease)
        {
            currentTime = targetTime;
        }
        else if (counterType == CounterType.TimeIncrease)
        {
            currentTime = 0f;
        }

        while (isCounting)
        {
            if (counterType == CounterType.TimeDecrease)
            {
                currentTime -= Time.deltaTime;
                if (currentTime <= 0)
                {
                    currentTime = 0;
                    isCounting = false;
                    OnCountComplete?.Invoke(true);
                    completeCallback?.Invoke();
                }
            }
            else if (counterType == CounterType.TimeIncrease)
            {
                currentTime += Time.deltaTime;
                if (currentTime >= targetTime)
                {
                    currentTime = targetTime;
                    isCounting = false;
                    OnCountComplete?.Invoke(true);
                    completeCallback?.Invoke();
                }
            }

            updateCallback?.Invoke();
            yield return new WaitForFixedUpdate();
        }

        OnCountComplete?.Invoke(false);
    }
}
