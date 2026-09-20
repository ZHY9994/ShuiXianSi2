using UnityEngine;

namespace KobGamesSDKSlim.UnityStandardAssets.ImageEffects
{
	[ExecuteInEditMode]
	[RequireComponent(typeof(Camera))]
	[AddComponentMenu("Image Effects/Other/Screen Overlay")]
	public class ScreenOverlay : PostEffectsBase
	{
		public enum OverlayBlendMode
		{
			Additive = 0,
			ScreenBlend = 1,
			Multiply = 2,
			Overlay = 3,
			AlphaBlend = 4
		}

		public OverlayBlendMode blendMode = OverlayBlendMode.Overlay;

		public float intensity = 1f;

		public Texture2D texture;

		public Shader overlayShader;

		private Material overlayMaterial;

		public override bool CheckResources()
		{
			CheckSupport();
			overlayMaterial = CheckShaderAndCreateMaterial(overlayShader, overlayMaterial);
			return isSupported;
		}

		private void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			if (CheckResources() && overlayMaterial != null)
			{
				overlayMaterial.SetVector("_UV_Transform", new Vector4(1f, 0f, 0f, 1f));
				overlayMaterial.SetFloat("_Intensity", intensity);
				overlayMaterial.SetTexture("_Overlay", texture);
				Graphics.Blit(source, destination, overlayMaterial, (int)blendMode);
			}
			else
			{
				Graphics.Blit(source, destination);
			}
		}
	}
}
