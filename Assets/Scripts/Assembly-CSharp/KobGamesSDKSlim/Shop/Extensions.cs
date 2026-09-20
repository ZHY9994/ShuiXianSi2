using System;
using System.Collections.Generic;

namespace KobGamesSDKSlim.Shop
{
	public static class Extensions
	{
		private static Random rng;

		public static void Shuffle<T>(this IList<T> list)
		{
			if (list == null) throw new ArgumentNullException(nameof(list));
			if (rng == null) rng = new Random();
			for (int i = list.Count - 1; i > 0; i--)
			{
				int index = rng.Next(i + 1);
				T value = list[index];
				list[index] = list[i];
				list[i] = value;
			}
		}
	}
}
