namespace KobGamesSDKSlim
{
	public struct Money
	{
		public decimal Value { get; private set; }

		public Money(decimal value)
		{
			Value = value;
		}

		public Money(Money value)
		{
			Value = value.Value;
		}

		public static implicit operator Money(decimal value)
		{
			return new Money(value);
		}

		public static implicit operator decimal(Money value)
		{
			return value.Value;
		}
	}
}
