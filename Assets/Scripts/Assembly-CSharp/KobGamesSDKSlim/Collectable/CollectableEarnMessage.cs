using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace KobGamesSDKSlim.Collectable
{
	public class CollectableEarnMessage : MonoBehaviour
	{
		[SerializeField]
		private CanvasGroup m_CanvasGroup;

		[SerializeField]
		private Image m_Panel;

		[SerializeField]
		private TextMeshProUGUI m_Text;

		[SerializeField]
		private Image m_Icon;

		[SerializeField]
		private ParticleSystem m_EarnParticles;

		[SerializeField]
		[HideInInspector]
		private ParticleSystemRenderer m_EarnParticlesRenderer;

		[SerializeField]
		private bool m_IsUseTextFormat = true;

		[SerializeField]
		private bool m_IsHideBigNumbers = true;

		private float m_EarnAmount;

		private Vector3 m_SpawnPosition;

		private eCollectableEarnMessageAnimType m_AnimType;

		private eCollectableType m_CollectableType;

		private string m_TextFormat;

		private Sequence m_AnimSequence;

		private AnimationData m_CollectableData => GameConfig.Instance.HUD.AnimationData;

		private AnimationData.EarnMessageAnimDataDictionary m_AnimDataDictionary => m_CollectableData.EarnMessageAnim;

		private AnimationData.EarnMessageAnimData m_AnimData => m_AnimDataDictionary.ContainsKey(m_AnimType) ? m_AnimDataDictionary[m_AnimType] : m_AnimDataDictionary[eCollectableEarnMessageAnimType.Default];

		private CollectableData m_CollectableUniqueData => GameConfig.Instance.HUD.CollectableData[CollectableType];

		protected virtual eCollectableType CollectableType => eCollectableType.Coin;

		private void OnValidate()
		{
			SetRefs();
		}

		protected virtual void SetRefs()
		{
			m_CanvasGroup = GetComponentInChildren<CanvasGroup>();
			Transform root = transform;
			m_Panel = root.FindDeepChild<Image>("Background");
			m_Text = root.FindDeepChild<TextMeshProUGUI>("Text");
			m_Icon = root.FindDeepChild<Image>("Icon");
			m_EarnParticles = root.FindDeepChild<ParticleSystem>("Earn Particles");
			if (m_EarnParticles != null) m_EarnParticlesRenderer = m_EarnParticles.GetComponent<ParticleSystemRenderer>();
		}

		public virtual void ShowMessage(eCollectableType i_Type, int i_Amount, eCollectableEarnMessageAnimType i_AnimType, Vector3 i_Position)
		{
			m_AnimSequence?.Kill();
			m_AnimType = i_AnimType;
			m_CollectableType = i_Type;
			m_EarnAmount = i_Amount;
			m_SpawnPosition = i_Position;
			if (m_CanvasGroup == null) SetRefs();
			m_CanvasGroup.transform.localScale = Vector3.one * (m_AnimData?.SizeMultiplier ?? 1f);
			transform.localScale = Vector3.zero;
			transform.position = i_Position;
			if (m_Icon != null) m_Icon.sprite = m_CollectableUniqueData.CollectableSprite;
			if (m_Text != null)
			{
				string value = m_IsHideBigNumbers ? hideBigNumber(i_Amount) : i_Amount.ToString();
				if (m_IsUseTextFormat) { m_TextFormat ??= m_Text.text; value = string.Format(m_TextFormat, value); }
				m_Text.SetText(value);
			}
			if (m_AnimData?.AnimMode == AnimationData.EarnMessageAnimData.eAnimMode.SimpleText) showSimpleText(m_AnimData.SimpleTextAnimMode);
			if (m_AnimData?.IsSpawnParticles == true) spawnParticles();
		}

		private string hideBigNumber(int i_Num)
		{
			if (i_Num < 10000) return i_Num.ToString();
			if (i_Num >= 100000000) return (i_Num / 1000000d).ToString() + "M";
			if (i_Num >= 1000000) return (i_Num / 1000000d).ToString() + "M";
			return (i_Num / 1000d).ToString() + "K";
		}

		private void showSimpleText(AnimationData.EarnMessageAnimData.SimpleTextAnimData i_AnimData)
		{
			if (i_AnimData == null) { onShowMessageComplete(); return; }
			m_AnimSequence = DOTween.Sequence();
			m_AnimSequence.Append(transform.DOScale(i_AnimData.AppearScale, i_AnimData.AppearDuration).SetEase(i_AnimData.AppearEase));
			m_AnimSequence.Append(transform.DOMoveY(transform.position.y + i_AnimData.MoveOffset, i_AnimData.MoveDuration).SetEase(i_AnimData.MoveEase));
			m_AnimSequence.Append(transform.DOScale(i_AnimData.DisappearScale, i_AnimData.DisappearDuration).SetEase(i_AnimData.DisappearEase));
			m_AnimSequence.OnComplete(onShowMessageComplete);
		}

		private void spawnParticles()
		{
			if (m_EarnParticlesRenderer != null && m_CollectableUniqueData != null) m_EarnParticlesRenderer.material = m_CollectableUniqueData.ParticlesMaterial;
			m_EarnParticles?.Play();
		}

		protected virtual void onShowMessageComplete()
		{
			PoolManager.Instance?.Queue(ePoolType.CollectableEarnMessage, gameObject);
		}
	}
}
