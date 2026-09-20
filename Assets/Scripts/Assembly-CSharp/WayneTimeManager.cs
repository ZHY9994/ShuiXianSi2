using System.Collections.Generic;
using KobGamesSDKSlim;
using UnityEngine;

public class WayneTimeManager : Singleton<WayneTimeManager>
{
	public WayneTimeManager()
	{
		TargetTimeScale = 1f;
		LerpTimeScale = true;
		NormalTimeScale = 1f;
		CurrentTimeScale = 1f;
		m_FrozenTimeLeft = -1f;
	}

	[Tooltip("The reference timescale, to which the system will go back to after all the time changed")]
	public float NormalTimeScale;

	[Header("Dont Change")]
	[Tooltip("The current, real time, time scale")]
	public float CurrentTimeScale;

	[Tooltip("The time scale the system is lerping towards")]
	public float TargetTimeScale;

	[Tooltip("Wheather or not the timescale should be lerping")]
	public bool LerpTimeScale;

	[Tooltip("The speed at which the timesclae should lero towards its target")]
	public float LerpSpeed;

	protected Stack<TimeScaleParamters> m_TimeScaleProperties;

	protected float m_FrozenTimeLeft;

	protected TimeScaleParamters m_CurrentProperty;

	protected float m_InitialFixedDeltaTime;

	protected float m_InitialMaximumDeltaTime;

	private TimeManagerData m_Parameters => GameConfig.Instance.GamePlay.TimeManagerData;

	private new void OnEnable()
	{
		FreezeFrameEvent.Register(OnFreezeFrameEvent);
		TimeScaleEvent.Register(OnTimeScaleEvent);
	}

	private new void OnDisable()
	{
		FreezeFrameEvent.UnRegister(OnFreezeFrameEvent);
		TimeScaleEvent.UnRegister(OnTimeScaleEvent);
	}

	public override void Start()
	{
		Initialization();
	}

	protected void Update()
	{
		if (m_TimeScaleProperties.Count == 0)
		{
			TargetTimeScale = NormalTimeScale;
		}
		else
		{
			m_CurrentProperty = m_TimeScaleProperties.Peek();
			TargetTimeScale = m_CurrentProperty.TimeScale;
			LerpSpeed = m_CurrentProperty.LerpSpeed;
			LerpTimeScale = m_CurrentProperty.Lerp;
			m_CurrentProperty.Duration -= Time.unscaledDeltaTime;
			m_TimeScaleProperties.Pop();
			m_TimeScaleProperties.Push(m_CurrentProperty);
			if (m_CurrentProperty.Duration <= 0f && !m_CurrentProperty.Infinite)
				Unfreeze();
		}

		float value = TargetTimeScale;
		if (LerpTimeScale)
			value = Mathf.Lerp(Time.timeScale, TargetTimeScale, Mathf.Clamp01(Time.unscaledDeltaTime * LerpSpeed));
		ApplyTimeScale(value);
	}

	public virtual void Initialization()
	{
		TargetTimeScale = NormalTimeScale;
		m_TimeScaleProperties = new Stack<TimeScaleParamters>();
		m_InitialFixedDeltaTime = Time.fixedDeltaTime;
		m_InitialMaximumDeltaTime = Time.maximumDeltaTime;
		ApplyTimeScale(NormalTimeScale);
	}

	protected virtual void ApplyTimeScale(float i_NewValue)
	{
		Time.timeScale = i_NewValue;
		if (i_NewValue != 0f)
			Time.fixedDeltaTime = m_InitialFixedDeltaTime * i_NewValue;
		Time.maximumDeltaTime = m_InitialMaximumDeltaTime * i_NewValue;
		CurrentTimeScale = Time.timeScale;
	}

	protected virtual void SetTimeScale(float i_NewTimeScale)
	{
		m_TimeScaleProperties.Clear();
		ApplyTimeScale(i_NewTimeScale);
	}

	protected virtual void SetTimeScale(TimeScaleParamters i_TimeScaleProperties)
	{
		m_TimeScaleProperties.Push(i_TimeScaleProperties);
	}

	protected virtual void ResetTimeScale()
	{
		SetTimeScale(NormalTimeScale);
	}

	protected virtual void Unfreeze()
	{
		if (m_TimeScaleProperties.Count < 1)
			ResetTimeScale();
		else
			m_TimeScaleProperties.Pop();
	}

	public virtual void SetNormalTimeScale(float i_NewNormalTimeScale)
	{
		NormalTimeScale = i_NewNormalTimeScale;
	}

	public virtual void OnTimeScaleEvent(TimeScaleMethods i_TimeScaleMethods, float i_TimeScale, float i_Duration, bool i_Lerp, float i_LerpSpeed, bool i_Infinite)
	{
		switch (i_TimeScaleMethods)
		{
			case TimeScaleMethods.Set:
				SetNormalTimeScale(i_TimeScale);
				break;
			case TimeScaleMethods.For:
				SetTimeScale(new TimeScaleParamters
				{
					TimeScale = i_TimeScale,
					Duration = i_Duration,
					Lerp = i_Lerp,
					LerpSpeed = i_LerpSpeed,
					Infinite = i_Infinite
				});
				break;
			case TimeScaleMethods.Reset:
				ResetTimeScale();
				break;
			case TimeScaleMethods.Unfreeze:
				Unfreeze();
				break;
		}
	}

	public void OnFreezeFrameEvent(float i_Duration)
	{
		m_FrozenTimeLeft = i_Duration;
		SetTimeScale(new TimeScaleParamters { TimeScale = 0f, Duration = i_Duration });
	}

	public void SlowWithDuration()
	{
		if (!m_Parameters.UseSlowMo)
			return;
		TimeScaleEvent.Trigger(TimeScaleMethods.For, m_Parameters.TargetScale, m_Parameters.TimeScaleDuration,
			m_Parameters.ShouldLerp, m_Parameters.TimeScaleLerpSpeed, false);
	}

	public void SlowMoWithoutDuration()
	{
		TimeScaleEvent.Trigger(TimeScaleMethods.For, TargetTimeScale, 0f, false, 0f, true);
	}

	public void ResetTimeScaleValue()
	{
		TimeScaleEvent.Trigger(TimeScaleMethods.Reset, TargetTimeScale, 0f, false, 0f, false);
	}
}
