public struct Percentage
{
	public decimal Value { get; private set; }

	public decimal ValueAsPercentage => Value / 100m;

	public Percentage(int value)
	{
		Value = value;
	}

	public Percentage(decimal value)
	{
		Value = value;
	}

	public static decimal operator -(decimal value, Percentage percentage)
	{
		return value - value * percentage.ValueAsPercentage;
	}

	public static decimal operator -(int value, Percentage percentage)
	{
		return (decimal)value - (decimal)value * percentage.ValueAsPercentage;
	}

	public static decimal operator -(Percentage percentage, decimal value)
	{
		return value * percentage.ValueAsPercentage - value;
	}

	public static decimal operator -(Percentage percentage, int value)
	{
		return (decimal)value * percentage.ValueAsPercentage - value;
	}

	public static Percentage operator -(Percentage value, Percentage percentage)
	{
		return new Percentage(value.Value - percentage.Value);
	}

	public static decimal operator +(decimal value, Percentage percentage)
	{
		return value + value * percentage.ValueAsPercentage;
	}

	public static decimal operator +(int value, Percentage percentage)
	{
		return (decimal)value + (decimal)value * percentage.ValueAsPercentage;
	}

	public static decimal operator +(Percentage percentage, decimal value)
	{
		return value + value * percentage.ValueAsPercentage;
	}

	public static decimal operator +(Percentage percentage, int value)
	{
		return (decimal)value + (decimal)value * percentage.ValueAsPercentage;
	}

	public static Percentage operator +(Percentage value, Percentage percentage)
	{
		return new Percentage(value.Value + percentage.Value);
	}

	public static decimal operator *(decimal value, Percentage percentage)
	{
		return value * percentage.ValueAsPercentage;
	}

	public static decimal operator *(int value, Percentage percentage)
	{
		return (decimal)value * percentage.ValueAsPercentage;
	}

	public static decimal operator *(Percentage percentage, decimal value)
	{
		return value * percentage.ValueAsPercentage;
	}

	public static decimal operator *(Percentage percentage, int value)
	{
		return (decimal)value * percentage.ValueAsPercentage;
	}

	public static Percentage operator *(Percentage value, Percentage percentage)
	{
		return new Percentage(value.ValueAsPercentage * percentage.ValueAsPercentage);
	}

	public static decimal operator /(decimal value, Percentage percentage)
	{
		return value / percentage.ValueAsPercentage;
	}

	public static decimal operator /(int value, Percentage percentage)
	{
		return (decimal)value / percentage.ValueAsPercentage;
	}

	public static decimal operator /(Percentage percentage, decimal value)
	{
		return percentage.ValueAsPercentage / value;
	}

	public static decimal operator /(Percentage percentage, int value)
	{
		return percentage.ValueAsPercentage / value;
	}

	public static Percentage operator /(Percentage value, Percentage percentage)
	{
		return new Percentage(value.ValueAsPercentage / percentage.ValueAsPercentage);
	}
}
