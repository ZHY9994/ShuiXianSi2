using System;
using System.Collections;
using UnityEngine;

public class Wayne_MaterialFlicker : MonoBehaviour
{
	public Renderer TargetRenderer;

	public PropertyType PropertyType;

	public TimeScaling timeScaling;

	public string PropertyName = "_Color";

	public float FlickerDuration = 0.2f;

	public float FlickerSpeed = 0.04f;

	public Color FlickerColor = new Color(0f, 0f, 0f, 1f);

	private Color m_InitialFlickerColor;

	private int m_PropertyID = -1;

	private bool m_PropertyFound;

	private MaterialPropertyBlock m_PropBlock;

	private MaterialPropertyBlock PropBlock => m_PropBlock ??= new MaterialPropertyBlock();

	private float TimeMode => timeScaling == TimeScaling.Unscaled ? Time.unscaledTime : Time.time;

	private void Start()
	{
		Initialization(gameObject);
	}

	private void Initialization(GameObject i_Object)
	{
		m_PropertyFound = false;
		if (TargetRenderer == null && i_Object != null)
		{
			TargetRenderer = WayneStaticFunctions.GetComponentNoAlloc<Renderer>(i_Object);
			if (TargetRenderer == null)
				TargetRenderer = i_Object.GetComponent<Renderer>();
			if (TargetRenderer == null)
				TargetRenderer = GetComponentInChildren<Renderer>();
		}
		if (TargetRenderer == null)
			return;
		Material material = TargetRenderer.material;
		if (material == null || !material.HasProperty(PropertyName))
			return;
		if (PropertyType == PropertyType.Color)
		{
			m_PropertyID = Shader.PropertyToID(PropertyName);
			m_PropertyFound = true;
			m_InitialFlickerColor = material.GetColor(m_PropertyID);
		}
		else
		{
			m_PropertyFound = true;
			m_InitialFlickerColor = material.color;
		}
	}

	public void StartFlickerWithDuration(float i_Duration)
	{
		if (TargetRenderer != null)
			StartCoroutine(Flicker(TargetRenderer, m_InitialFlickerColor, FlickerColor, FlickerSpeed, i_Duration));
	}

	private void StartFlicker()
	{
		if (TargetRenderer != null)
			StartCoroutine(Flicker(TargetRenderer, m_InitialFlickerColor, FlickerColor, FlickerSpeed, FlickerDuration));
	}

	private void ResetColor()
	{
		if (TargetRenderer != null)
			SetColor(m_InitialFlickerColor);
	}

	private void SetColor(Color color)
	{
		if (PropertyType == PropertyType.Color)
		{
			if (!m_PropertyFound)
				return;
			PropBlock.SetColor(PropertyName, color);
			TargetRenderer.SetPropertyBlock(PropBlock);
		}
		else if (TargetRenderer != null && TargetRenderer.material.HasProperty("_Color"))
			TargetRenderer.material.color = color;
	}

	public IEnumerator Flicker(Renderer i_Renderer, Color i_InitialColor, Color i_FlickerColor, float i_FlickerSpeed, float i_FlickerDuration)
	{
		if (i_Renderer == null || this == null || !m_PropertyFound)
			yield break;
		Color delta = i_InitialColor - i_FlickerColor;
		if (delta.r * delta.r + delta.g * delta.g + delta.b * delta.b + delta.a * delta.a < 1e-10f)
			yield break;
		float flickerStop = TimeMode + i_FlickerDuration;
		if (TimeMode < flickerStop)
		{
			SetColor(i_FlickerColor);
			yield return new WaitForSeconds(i_FlickerSpeed);
			if (this == null)
				yield break;
			SetColor(i_InitialColor);
			yield return new WaitForSeconds(i_FlickerSpeed);
			if (this == null)
				yield break;
		}
		else
		{
			SetColor(i_InitialColor);
		}
	}
}
