using System;
using MoreMountains.FeedbacksForThirdParty;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

namespace KobGamesSDKSlim
{
	[RequireComponent(typeof(CanvasGroup))]
	public class ExtendedButton : Button, IDragHandler, IEventSystemHandler, IBeginDragHandler, IEndDragHandler
	{
		public delegate void InteractivityChangedCallback(bool i_Value, eButtonInteractivityChangeReason i_Reason);

		private const string k_MaterialID_Desaturated = "_Desaturate";

		protected bool m_OneTimeSetup;

		private bool _IsSetup;

		private eButtonState m_CurrState;

		public bool CanDoScaleAnim = true;

		public eButtonScaleSizeType ScaleSizeType;

		public Vector3 CustomScale = Vector3.one;

		public bool CanDoHaptics = true;

		public MMFeedbackHaptics.HapticTypes HapticType;

		public bool ResetOnDisable = true;

		public Material MaterialOverride;

		private Vector3 m_OriginalScale;

		[Obsolete]
		public Action onDown = delegate { };

		[Obsolete]
		public Action onUp = delegate { };

		public Action<PointerEventData> OnDownEvent = delegate { };

		public Action<PointerEventData> OnUpEvent = delegate { };

		public Action<PointerEventData> OnBeginDragEvent = delegate { };

		public Action<PointerEventData> OnDragEvent = delegate { };

		public Action<PointerEventData> OnEndDragEvent = delegate { };

		[SerializeField]
		protected ExtendedButtonModuleBehaviour[] m_Modules = new ExtendedButtonModuleBehaviour[0];

		[SerializeField]
		protected Transform m_ScalerTransform;

		[SerializeField]
		protected CanvasGroup m_CanvasGroup;

		[SerializeField]
		protected Image m_OriginalImage;

		[SerializeField]
		protected Image[] m_Images;

		[SerializeField]
		protected Color[] m_ImagesOriginalColors;

		[SerializeField]
		protected Text[] m_Texts;

		[SerializeField]
		protected Color[] m_TextsOriginalColors;

		[SerializeField]
		protected TextMeshProUGUI[] m_TextsTMP;

		[SerializeField]
		protected Color[] m_TextsTMPOriginalColors;

		[SerializeField]
		protected Color[] m_TextsTMPOutlineOriginalColors;

		private Transform m_DummyTransform;

		protected bool m_IsSetup
		{
			get
			{
				return _IsSetup;
			}
			set
			{
				_IsSetup = value;
			}
		}

		private MenuVariablesEditor MenuVars => GameConfig.Instance.Menus;

		public event InteractivityChangedCallback OnInteractivityChanged = delegate { };

		public bool UpdateModuleData(ExtendedButtonModuleBehaviour i_Module)
		{
			if (i_Module == null) throw new ArgumentNullException(nameof(i_Module));
			return i_Module.SetData(MaterialOverride, colors);
		}

		protected sealed override void Awake()
		{
			base.Awake();
			firstTimeSetup();
		}

		protected sealed override void OnEnable()
		{
			base.OnEnable();
			transform.localScale = m_OriginalScale;
			OnEnableImpl();
		}

		protected virtual void OnEnableImpl()
		{
			return;
		}

		protected sealed override void OnDisable()
		{
			base.OnDisable();
			if (Application.isPlaying && m_IsSetup && ResetOnDisable)
				ResetState();
			OnDisableImpl();
		}

		protected virtual void OnDisableImpl()
		{
			return;
		}

		public override void OnPointerDown(PointerEventData i_PointerEventData)
		{
			base.OnPointerDown(i_PointerEventData);
			DoInputFeedback(true);
			OnDownEvent?.InvokeSafe(i_PointerEventData);
			onDown?.Invoke();
		}

		public override void OnPointerUp(PointerEventData i_PointerEventData)
		{
			base.OnPointerUp(i_PointerEventData);
			DoInputFeedback(false);
			OnUpEvent?.InvokeSafe(i_PointerEventData);
			onUp?.Invoke();
		}

		private void DoInputFeedback(bool i_Down = true)
		{
			if (!interactable) return;
			m_DummyTransform = m_ScalerTransform != null ? m_ScalerTransform : transform;
			if (m_DummyTransform == null) return;
			if (!i_Down) m_DummyTransform.localScale = Vector3.one;
			if (i_Down && CanDoScaleAnim)
				m_DummyTransform.localScale = ScaleSizeType == eButtonScaleSizeType.CustomScale ? CustomScale : Vector3.one;
			if (i_Down && CanDoHaptics && Managers.Instance != null && Managers.Instance.HapticManager != null)
				Managers.Instance.HapticManager.Haptic(HapticType);
		}

		public virtual void OnBeginDrag(PointerEventData i_PointerEventData)
		{
			OnBeginDragEvent?.InvokeSafe(i_PointerEventData);
		}

		public virtual void OnDrag(PointerEventData i_PointerEventData)
		{
			OnDragEvent?.InvokeSafe(i_PointerEventData);
		}

		public virtual void OnEndDrag(PointerEventData i_PointerEventData)
		{
			OnEndDragEvent?.InvokeSafe(i_PointerEventData);
		}

		protected virtual void firstTimeSetup()
		{
			if (m_OneTimeSetup) return;
			m_OriginalScale = transform.localScale;
			if (MaterialOverride != null) MaterialOverride = new Material(MaterialOverride);
			if (m_OriginalImage != null && MaterialOverride != null) m_OriginalImage.material = MaterialOverride;
			if (m_Modules == null) throw new NullReferenceException(nameof(m_Modules));
			foreach (var module in m_Modules) UpdateModuleData(module);
			m_OneTimeSetup = true;
		}

		public void Setup(UnityAction i_OnClick = null, Action<PointerEventData> i_OnDownEvent = null, Action<PointerEventData> i_OnUpEvent = null, Action<PointerEventData> i_OnBeginDragEvent = null, Action<PointerEventData> i_OnDragEvent = null, Action<PointerEventData> i_OnEndDragEvent = null, InteractivityChangedCallback i_OnInteractivityChanged = null)
		{
			if (m_IsSetup)
			{
				return;
			}
			m_IsSetup = true;
			firstTimeSetup();
			SetEvents(i_OnClick, i_OnDownEvent, i_OnUpEvent, i_OnBeginDragEvent, i_OnDragEvent, i_OnEndDragEvent, i_OnInteractivityChanged);
			SetInteractable(true);
		}

		protected void SetEvents(UnityAction i_OnClick = null, Action<PointerEventData> i_OnDownEvent = null, Action<PointerEventData> i_OnUpEvent = null, Action<PointerEventData> i_OnBeginDragEvent = null, Action<PointerEventData> i_OnDragEvent = null, Action<PointerEventData> i_OnEndDragEvent = null, InteractivityChangedCallback i_OnInteractivityChanged = null)
		{
			if (i_OnClick != null) onClick.AddListener(i_OnClick);
			OnDownEvent = i_OnDownEvent;
			OnUpEvent = i_OnUpEvent;
			OnBeginDragEvent = i_OnBeginDragEvent;
			OnDragEvent = i_OnDragEvent;
			OnEndDragEvent = i_OnEndDragEvent;
			OnInteractivityChanged = i_OnInteractivityChanged;
		}

		public virtual void ResetState()
		{
			m_IsSetup = false;
			onClick.RemoveAllListeners();
			Action<PointerEventData> reset = _ => { };
			OnDownEvent = reset;
			OnUpEvent = reset;
			OnBeginDragEvent = reset;
			OnDragEvent = reset;
			OnEndDragEvent = reset;
			OnInteractivityChanged = (value, reason) => { };
			SetInteractable(false, eButtonInteractivityChangeReason.Reset);
		}

		public virtual void SetInteractable(bool i_Value)
		{
			SetInteractable(i_Value, eButtonInteractivityChangeReason.Manual);
		}

		public virtual void SetInteractable(bool i_Value, eButtonInteractivityChangeReason i_Reason)
		{
			bool oldValue = interactable;
			base.interactable = i_Value;
			if (i_Reason != eButtonInteractivityChangeReason.Reset) SetAssetsState(!i_Value ? eButtonState.Disabled : eButtonState.Normal);
			if (oldValue != i_Value) OnInteractivityChanged?.Invoke(i_Value, i_Reason);
		}

		public virtual void SetGraphicsVisibility(bool i_Value)
		{
			if (m_CanvasGroup == null) throw new NullReferenceException(nameof(m_CanvasGroup));
			m_CanvasGroup.alpha = i_Value ? 1f : 0f;
		}

		protected virtual void SetAssetsState(eButtonState i_ButtonState)
		{
			m_CurrState = i_ButtonState;
			if (m_Modules == null) return;
			foreach (var module in m_Modules)
				if (module != null) module.SetState(i_ButtonState);
		}
	}
}
