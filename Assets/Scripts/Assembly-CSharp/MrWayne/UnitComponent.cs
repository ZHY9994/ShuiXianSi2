using KobGamesSDKSlim;
using KobGamesSDKSlim.GameManagerV1;
using UnityEngine;

namespace MrWayne
{
	public class UnitComponent : MonoBehaviour
	{
		[SerializeField]
		protected UnitBase m_UnitBase;

		protected GeneralUnitParameters m_GeneralParameter => GameConfig.Instance.GamePlay.UnitData.GeneralUnitParameters;

		public eUnityType UnitType => m_UnitBase.UnitType;

		public UnitBase UnitBase => m_UnitBase;

		protected virtual void OnEnable()
		{
			GameManagerBase.OnLevelCompleted += onLevelCompleted;
			UnitHealth.OnUnitDeath += onUnitDeath;
		}

		protected virtual void OnDisable()
		{
			GameManagerBase.OnLevelCompleted -= onLevelCompleted;
			UnitHealth.OnUnitDeath -= onUnitDeath;
		}

		protected virtual void onLevelCompleted()
		{
			return;
		}

		protected virtual void onUnitDeath(UnitBase i_Unit)
		{
			return;
		}

		protected virtual void SetRefs()
		{
			m_UnitBase = GetComponent<UnitBase>();
		}
	}
}
