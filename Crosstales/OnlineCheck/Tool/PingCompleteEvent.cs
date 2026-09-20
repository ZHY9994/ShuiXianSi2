using System;
using UnityEngine.Events;

namespace Crosstales.OnlineCheck.Tool
{
	[Serializable]
	public class PingCompleteEvent : UnityEvent<float>
	{
	}
}
