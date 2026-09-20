using UnityEngine;

namespace KobGamesSDKSlim.UI
{
	public class CoinUpdater : CoinContainer
	{
		[SerializeField]
		protected bool AutoPopAnimation = true;

		protected override void OnEnable()
		{
			StorageManager.OnCoinsAmountChanged += CoinsChanged;
			SetCoins(StorageManager.Instance.CoinsAmount, false);
		}

		protected override void OnDisable()
		{
			StorageManager.OnCoinsAmountChanged -= CoinsChanged;
		}

		protected virtual void CoinsChanged(int i_Value)
		{
			SetCoins(i_Value, AutoPopAnimation);
		}
	}
}
