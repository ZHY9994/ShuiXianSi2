using System;
using System.Collections.Generic;
using UnityEngine;

namespace KobGamesSDKSlim
{
	[Serializable]
	public class ResolutionsData
	{
		[Serializable]
		public class ResolutionData
		{
			public string Name;

			public int Width;

			public int Height;

			public float DPI;

			public string NameLabel => Width + "x" + Height;

			public override string ToString()
			{
				return Name + " " + NameLabel;
			}
		}

		[SerializeField]
		private List<ResolutionData> m_ResolutionDatas;

		public bool IsDebugEnabled = true;

		public ResolutionData GetResolutionData()
		{
			var camera = Camera.main;
			if (camera == null)
				throw new NullReferenceException();
			return GetResolutionData(camera.pixelWidth, camera.pixelHeight);
		}

		public ResolutionData GetResolutionData(int i_Width, int i_Height)
		{
			ResolutionData result = null;
			if (m_ResolutionDatas != null)
			{
				foreach (var data in m_ResolutionDatas)
				{
					if ((data.Width == i_Width && data.Height == i_Height) ||
						(data.Width == i_Height && data.Height == i_Width))
					{
						result = data;
						break;
					}
				}
			}
			return result;
		}
	}
}
