using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Events;

[DisallowMultipleComponent]
[ExecuteAlways]
public class LocalizedStringEventProxy : MonoBehaviour
{
	[SerializeField]
	private LocalizedString m_StringReference = new LocalizedString();

	[SerializeField]
	private List<UnityEngine.Object> m_FormatArguments = new List<UnityEngine.Object>();

	[SerializeField]
	private UnityEventString m_UpdateString = new UnityEventString();

	private bool m_IsRegistered;

	public LocalizedString StringReference
	{
		get => m_StringReference;
		set
		{
			m_StringReference = value ?? new LocalizedString();
			RefreshString();
		}
	}

	public UnityEventString OnUpdateString
	{
		get => m_UpdateString;
		set => m_UpdateString = value;
	}

	private void OnEnable()
	{
		if (!m_IsRegistered)
		{
			LocalizationEventCaller.Register(RefreshString);
			m_IsRegistered = true;
		}

		RefreshString();
	}

	private void OnDisable()
	{
		if (!m_IsRegistered)
			return;

		LocalizationEventCaller.UnRegister(RefreshString);
		m_IsRegistered = false;
	}

	private void OnValidate()
	{
		RefreshString();
	}

	public void RefreshString()
	{
		string localized = LocalizationUtility.GetLocalizedString(m_StringReference, m_FormatArguments);
		m_UpdateString.Invoke(localized);
	}
}
