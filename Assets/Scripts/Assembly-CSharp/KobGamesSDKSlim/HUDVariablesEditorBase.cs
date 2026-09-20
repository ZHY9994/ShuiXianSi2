using System;
using System.Collections.Generic;
using KobGamesSDKSlim.Animation;
using KobGamesSDKSlim.Collectable;
using MoreMountains.FeedbacksForThirdParty;

namespace KobGamesSDKSlim
{
	[Serializable]
	public abstract class HUDVariablesEditorBase
	{
		[Serializable]
		[Obsolete]
		public class EarnObjectData
		{
			[Serializable]
			public class eCoinSenderTypeEarnObjectUIAnimDataDictionary : UnitySerializedDictionary<eCoinSenderType, EarnObjectUIAnimData>
			{
			}

			public eCoinSenderTypeEarnObjectUIAnimDataDictionary EarnObjectUIAnimData;

			public bool IsPlayHapticsOnReceive;

			public MMFeedbackHaptics.HapticTypes ReceivedHaptics;

			public MMFeedbackHaptics.HapticTypes EarningCompleteHaptics;

			public float HapticsCooldown;

			public EarnObjectUIAnimData DefaultEarnObjectUIAnimData
			{
				get
				{
					if (EarnObjectUIAnimData != null && EarnObjectUIAnimData.ContainsKey((eCoinSenderType)0))
						return EarnObjectUIAnimData[(eCoinSenderType)0];
					return null;
				}
			}

			public EarnObjectData()
			{
				IsPlayHapticsOnReceive = true;
				ReceivedHaptics = (MMFeedbackHaptics.HapticTypes)5;
				EarningCompleteHaptics = (MMFeedbackHaptics.HapticTypes)4;
				HapticsCooldown = 0.3f;
			}
		}

		[Serializable]
		public class TypeCollectableDataDictionary : UnitySerializedDictionary<eCollectableType, CollectableData>
		{
		}

		public bool IsUseCollectables;

		public EarnObjectData EarnObject;

		public TypeCollectableDataDictionary CollectableData;

		public AnimationData AnimationData;

		private void onValueChanged()
		{
			foreach (KeyValuePair<eCollectableType, CollectableData> item in CollectableData)
			{
				item.Value.Type = item.Key;
			}
		}
	}
}
