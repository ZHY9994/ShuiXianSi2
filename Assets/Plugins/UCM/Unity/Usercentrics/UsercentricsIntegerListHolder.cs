using System;

namespace Unity.Usercentrics
{
	[Serializable]
	internal class UsercentricsIntegerListHolder
	{
		public int[] list;

		internal UsercentricsIntegerListHolder(int[] list)
		{
			this.list = list;
		}
	}
}
