using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using KobGamesSDKSlim.GameManagerV1;

namespace KobGamesSDKSlim
{
	public class AnimatorBase : MonoBehaviour
	{
		public bool m_IsPlaying;

		public Tween Tween;

		[Space]
		public bool PlayOnEnable;

		public bool RandomStartTime;

		[Range(0f, 1f)]
		public float StartTime;

		[Space]
		public bool UseDotweenID;

		public int Id;

		protected const int k_NoId = -9999;

		public int DoTweenID;

		public float Duration;

		public Ease Ease;

		public bool IsRelative;

		[Space]
		public UpdateType UpdateType;

		public bool UpdateFrameIndependant;

		[Space]
		public int LoopCount;

		public LoopType LoopType;

		protected int FinalID => UseDotweenID ? DoTweenID : Id;

		private bool m_UsingLoops => LoopCount > 0 || LoopCount == -1;

		private ValueDropdownList<int> DotweenIDs()
		{
			return DOTWeenIDsBase.ValuesDropdownData;
		}

		public AnimatorBase()
		{
			Id = k_NoId;
			DoTweenID = DOTWeenIDsBase.Default;
			Duration = 1f;
			Ease = (Ease)19;
		}

		protected virtual void Awake()
		{
			if (RandomStartTime)
				StartTime = Random.Range(0f, 1f);
		}

		protected virtual void Start()
		{
			SetRestartAction();
		}

		protected virtual void SetRestartAction()
		{
			return;
		}

		protected virtual void OnEnable()
		{
			GameManagerBase.OnGameReset += OnGameReset;
			ResetValues();
			if (PlayOnEnable)
				StartAnimation();
		}

		protected virtual void OnDisable()
		{
			GameManagerBase.OnGameReset -= OnGameReset;
			StopAnimation();
		}

		protected virtual void OnGameReset()
		{
			if (!DOTWeenIDsBase.ValuesDropdownData.Exists(x => x.Value == FinalID))
				ResetValues();
		}

		public virtual void ResetValues()
		{
			StopAnimation();
		}

		public virtual void StartAnimation()
		{
			if (Tween != null)
				Tween.Kill(true);
			m_IsPlaying = true;
		}

		public virtual void StopAnimation()
		{
			m_IsPlaying = false;
			if (Tween != null)
				Tween.Kill(true);
		}

		public virtual void RestartAnimation()
		{
			ResetValues();
			StartAnimation();
		}

		public virtual void PauseAnimation()
		{
			if (Tween != null)
				Tween.Pause();
		}

		public void ContinueAnimation()
		{
			if (Tween != null)
				Tween.Play();
		}

		public virtual void SetId(Tween i_Tween)
		{
			if (i_Tween != null && FinalID != k_NoId)
				i_Tween.SetId(FinalID);
		}
	}
}
