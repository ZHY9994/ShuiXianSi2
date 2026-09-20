using UnityEngine.Purchasing;

namespace KobGamesSDKSlim.iAP
{
	public class iAPProduct
	{
		public iAPProductDefinition definition { get; private set; }

		public iAPProductMetadata metadata { get; internal set; }

		public string InappType { get; private set; }

		public bool availableToPurchase { get; internal set; }

		public string transactionID { get; internal set; }

		public bool hasReceipt => !string.IsNullOrEmpty(receipt);

		public string receipt { get; internal set; }

		public iAPProduct(Product i_Product)
		{
			transactionID = string.Empty;
			receipt = string.Empty;
			if (i_Product == null) throw new System.ArgumentNullException(nameof(i_Product));
			definition = i_Product.definition.ToiAPProductDefinition();
			metadata = i_Product.metadata.ToiAPProductMetadata();
			receipt = i_Product.receipt;
			transactionID = i_Product.transactionID;
			availableToPurchase = i_Product.availableToPurchase;
		}

		public iAPProduct SetInappType(string i_InappType)
		{
			InappType = i_InappType;
			return this;
		}

		public override bool Equals(object obj)
		{
			return obj is iAPProduct other && definition != null && definition.Equals(other.definition);
		}

		public override int GetHashCode()
		{
			return definition == null ? 0 : definition.GetHashCode();
		}
	}
}
