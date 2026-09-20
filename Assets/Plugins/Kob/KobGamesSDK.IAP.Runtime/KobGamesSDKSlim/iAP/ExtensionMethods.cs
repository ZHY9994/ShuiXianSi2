using UnityEngine.Purchasing;

namespace KobGamesSDKSlim.iAP
{
	public static class ExtensionMethods
	{
		public static iAPProduct ToiAPProduct(this Product i_Product)
		{
			return new iAPProduct(i_Product);
		}

		public static iAPProductDefinition ToiAPProductDefinition(this ProductDefinition i_ProductDefinition)
		{
			return new iAPProductDefinition(i_ProductDefinition);
		}

		public static iAPProductMetadata ToiAPProductMetadata(this ProductMetadata i_ProductMetadata)
		{
			return new iAPProductMetadata(i_ProductMetadata);
		}
	}
}
