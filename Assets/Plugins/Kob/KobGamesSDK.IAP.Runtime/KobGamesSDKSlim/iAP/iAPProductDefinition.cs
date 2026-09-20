using System.Collections.Generic;
using UnityEngine.Purchasing;

namespace KobGamesSDKSlim.iAP
{
	public class iAPProductDefinition
	{
		private List<PayoutDefinition> m_Payouts;

		public string id { get; private set; }

		public string storeSpecificId { get; private set; }

		public ProductType type { get; private set; }

		public bool enabled { get; private set; }

		public IEnumerable<PayoutDefinition> payouts => m_Payouts;

		public PayoutDefinition payout => m_Payouts != null && m_Payouts.Count > 0 ? m_Payouts[0] : null;

		public iAPProductDefinition(ProductDefinition i_ProductDefinition)
		{
			if (i_ProductDefinition == null) throw new System.ArgumentNullException(nameof(i_ProductDefinition));
			this.id = i_ProductDefinition.id;
			this.storeSpecificId = i_ProductDefinition.storeSpecificId;
			this.type = i_ProductDefinition.type;
			this.enabled = i_ProductDefinition.enabled;
			m_Payouts = new List<PayoutDefinition>();
			SetPayouts(i_ProductDefinition.payouts);
		}

		private iAPProductDefinition(string id, string storeSpecificId, ProductType type, bool enabled, IEnumerable<PayoutDefinition> payouts)
		{
			this.id = id;
			this.storeSpecificId = storeSpecificId;
			this.type = type;
			this.enabled = enabled;
			m_Payouts = new List<PayoutDefinition>();
			SetPayouts(payouts);
		}

		public override bool Equals(object obj)
		{
			return obj is iAPProductDefinition other && string.Equals(id, other.id, System.StringComparison.Ordinal);
		}

		public override int GetHashCode()
		{
			return id == null ? 0 : id.GetHashCode();
		}

		internal void SetPayouts(IEnumerable<PayoutDefinition> newPayouts)
		{
			if (newPayouts == null) return;
			m_Payouts.Clear();
			m_Payouts.AddRange(newPayouts);
		}
	}
}
