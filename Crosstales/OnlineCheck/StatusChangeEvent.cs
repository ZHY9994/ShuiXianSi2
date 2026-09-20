using System;
using UnityEngine.Events;

namespace Crosstales.OnlineCheck
{
	[Serializable]
	public class StatusChangeEvent : UnityEvent<bool>
	{
	}
}
