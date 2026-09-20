using System;

namespace Unity.Usercentrics
{
	[Serializable]
	public class DataRetention
	{
		public RetentionPeriod purposes;

		public RetentionPeriod specialPurposes;

		public string _stdRetention;

		public int? stdRetention => StringParser.ToInt(_stdRetention);
	}
}
