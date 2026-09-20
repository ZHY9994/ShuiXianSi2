using System;
using UnityEngine;

namespace KobGamesSDKSlim.UnityStandardAssets.ImageEffects
{
	[ExecuteInEditMode]
	[RequireComponent(typeof(Camera))]
	public class PostEffectsBase : MonoBehaviour
	{
		public PostEffectsBase()
		{
			supportHDRTextures = true;
			isSupported = true;
		}
		protected bool supportHDRTextures;

		protected bool supportDX11;

		protected bool isSupported;

		protected Material CheckShaderAndCreateMaterial(Shader s, Material m2Create)
		{
			if (!s)
			{
				enabled = false;
				return null;
			}
			if (!s.isSupported || !m2Create || m2Create.shader != s)
			{
				if (!s.isSupported)
				{
					enabled = false;
					isSupported = false;
					return null;
				}
				m2Create = new Material(s) { hideFlags = HideFlags.DontSave };
				return m2Create;
			}
			return m2Create;
		}

		protected Material CreateMaterial(Shader s, Material m2Create)
		{
			if (!s)
				return null;
			if (m2Create && m2Create.shader == s && s.isSupported)
				return m2Create;
			if (!s.isSupported)
				return null;
			return new Material(s) { hideFlags = HideFlags.DontSave };
		}

		private void OnEnable()
		{
			isSupported = true;
		}

		protected bool CheckSupport()
		{
		return CheckSupport(false);
		}

		public virtual bool CheckResources()
		{
		return isSupported;
		}

		protected void Start()
		{
			CheckResources();
		}

		protected bool CheckSupport(bool needDepth)
		{
		isSupported = true;
		supportHDRTextures = SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.ARGBHalf);
		supportDX11 = SystemInfo.graphicsShaderLevel >= 50 && SystemInfo.supportsComputeShaders;
		if (!needDepth)
			return true;
		if (SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.Depth))
		{
			var camera = GetComponent<Camera>();
			camera.depthTextureMode |= DepthTextureMode.Depth;
			return true;
		}
		enabled = false;
		isSupported = false;
		return false;
		}

		protected bool CheckSupport(bool needDepth, bool needHdr)
		{
		var supported = CheckSupport(needDepth);
		if (supported && needHdr && !supportHDRTextures)
		{
			enabled = false;
			isSupported = false;
			return false;
		}
		return supported;
		}

		public bool Dx11Support()
		{
		return supportDX11;
		}

		protected void ReportAutoDisable()
		{
			return;
		}

		private bool CheckShader(Shader s)
		{
		if (!s)
			throw new NullReferenceException();
		if (!s.isSupported)
		{
			enabled = false;
			isSupported = false;
		}
		return false;
		}

		protected void NotSupported()
		{
			enabled = false;
			isSupported = false;
		}

		protected void DrawBorder(RenderTexture dest, Material material)
		{
			RenderTexture.active = dest;
			GL.PushMatrix();
			GL.LoadOrtho();
			for (var pass = 0; material != null && pass < material.passCount; pass++)
			{
				material.SetPass(pass);
				var width = 1f / dest.width;
				var height = 1f / dest.height;
				GL.Begin(GL.QUADS);
				GL.TexCoord2(0, 1); GL.Vertex3(0, 0, .1f);
				GL.TexCoord2(1, 1); GL.Vertex3(width, 0, .1f);
				GL.TexCoord2(1, 0); GL.Vertex3(width, 1, .1f);
				GL.TexCoord2(0, 0); GL.Vertex3(0, 1, .1f);
				GL.TexCoord2(0, 1); GL.Vertex3(1 - width, 0, .1f);
				GL.TexCoord2(1, 1); GL.Vertex3(1, 0, .1f);
				GL.TexCoord2(1, 0); GL.Vertex3(1, 1, .1f);
				GL.TexCoord2(0, 0); GL.Vertex3(1 - width, 1, .1f);
				GL.TexCoord2(0, 1); GL.Vertex3(0, 0, .1f);
				GL.TexCoord2(1, 1); GL.Vertex3(1, 0, .1f);
				GL.TexCoord2(1, 0); GL.Vertex3(1, height, .1f);
				GL.TexCoord2(0, 0); GL.Vertex3(0, height, .1f);
				GL.TexCoord2(0, 1); GL.Vertex3(0, 1 - height, .1f);
				GL.TexCoord2(1, 1); GL.Vertex3(1, 1 - height, .1f);
				GL.TexCoord2(1, 0); GL.Vertex3(1, 1, .1f);
				GL.TexCoord2(0, 0); GL.Vertex3(0, 1, .1f);
				GL.End();
			}
			GL.PopMatrix();
		}
	}
}
