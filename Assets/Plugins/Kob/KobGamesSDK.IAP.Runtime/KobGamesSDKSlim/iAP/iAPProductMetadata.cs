using UnityEngine.Purchasing;

namespace KobGamesSDKSlim.iAP
{
	public class iAPProductMetadata
	{
		public string localizedPriceString { get; internal set; }

		public string localizedTitle { get; internal set; }

		public string localizedDescription { get; internal set; }

		public string isoCurrencyCode { get; internal set; }

		public decimal localizedPrice { get; internal set; }

		public iAPProductMetadata(ProductMetadata i_ProductMetadata)
		{
			if (i_ProductMetadata == null) throw new System.ArgumentNullException(nameof(i_ProductMetadata));
			localizedPriceString = i_ProductMetadata.localizedPriceString;
			localizedTitle = i_ProductMetadata.localizedTitle;
			localizedDescription = i_ProductMetadata.localizedDescription;
			isoCurrencyCode = i_ProductMetadata.isoCurrencyCode;
			localizedPrice = i_ProductMetadata.localizedPrice;
		}

		private iAPProductMetadata(string priceString, string title, string description, string currencyCode, decimal localizedPrice)
		{
			localizedPriceString = priceString;
			localizedTitle = title;
			localizedDescription = description;
			isoCurrencyCode = currencyCode;
			this.localizedPrice = localizedPrice;
		}
	}
}
