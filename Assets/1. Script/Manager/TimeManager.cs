using UnityEngine;

public class TimeManager : MonoSingleton<TimeManager>
{
    [SerializeField]
    private float timeScale = 1.0f;

    [SerializeField]
    private float minTimeScale = 0.1f;

    [SerializeField]
    private float maxTimeScale = 5.0f;

    public float TimeScale => timeScale;
    public float MinTimeScale => minTimeScale;
    public float MaxTimeScale => maxTimeScale;

    private void Start()
    {
        SetTimeScale(timeScale);
    }

    public void SetTimeScale(float scale)
    {
        timeScale = Mathf.Clamp(scale, minTimeScale, maxTimeScale);
        Time.timeScale = timeScale;
    }

    public void IncreaseTimeScale(float amount)
    {
        SetTimeScale(timeScale + amount);
    }

    public void DecreaseTimeScale(float amount)
    {
        SetTimeScale(timeScale - amount);
    }

    public void ResetTimeScale()
    {
        SetTimeScale(1.0f);
    }

    public void PauseTime()
    {
        Time.timeScale = 0f;
    }

    public void ResumeTime()
    {
        Time.timeScale = timeScale;
    }
}
