using System;
using System.Collections.Generic;
using KobGamesSDKSlim;
using MrWayne;
using UnityEngine;
using Wayne.Game.Shops;

public class TrajectoryManager : Singleton<TrajectoryManager>
{
	[SerializeField]
	private Transform m_PreviewRoot;

	[SerializeField]
	private Transform m_CorrectRoot;

	[SerializeField]
	private Vector3 m_CenterOffset;

	[SerializeField]
	private List<TrajectoryDot> m_PreviewList;

	[SerializeField]
	private List<TrajectoryDot> m_CorrectDotList;

	private int m_PlayerThrownCount;

	private TrajectoryDataEditor m_DataEditor => GameConfig.Instance.GamePlay.Trajectory;

	private TrajectoryDataEditor.TutorialData m_TutParameters => m_DataEditor.Tutorial;

	private TrajectoryDataEditor.TutorialData.Data m_ActiveTrajectoryData => m_TutParameters.GetData(m_PlayerThrownCount);

	private new void OnEnable()
	{
		PlayerAimingState.OnPlayerThrown += onPlayerThrown;
		KobGamesSDKSlim.GameManagerV1.GameManagerBase.OnLevelStarted += onLevelStarted;
		WeaponSelectionManager.OnSetWeapon += onWeaponSelectedCallback;
		HideTutorial();
	}

	public override void OnDisable()
	{
		base.OnDisable();
		PlayerAimingState.OnPlayerThrown -= onPlayerThrown;
		KobGamesSDKSlim.GameManagerV1.GameManagerBase.OnLevelStarted -= onLevelStarted;
		WeaponSelectionManager.OnSetWeapon -= onWeaponSelectedCallback;
	}

	private void onLevelStarted()
	{
		m_PlayerThrownCount = 0;
		if (KobGamesSDKSlim.StorageManager.Instance.CurrentLevel != 1)
			return;
		initializeList(m_PreviewList, m_PreviewRoot, m_DataEditor.General.ScaleMinMax, ePoolType.TrajectoryDotsEmptyCirce);
		initializeList(m_CorrectDotList, m_CorrectRoot, m_DataEditor.Tutorial.ScaleMinMax, ePoolType.TrajectoryDots);
	}

	private void onPlayerThrown(eWeaponType i_Type)
	{
		++m_PlayerThrownCount;
	}

	private void onWeaponSelected()
	{
		if (KobGamesSDKSlim.StorageManager.Instance.CurrentLevel == 1 && KobGamesSDKSlim.StorageManager.Instance.WeaponSelectionTutorial && KobGamesSDKSlim.StorageManager.Instance.IsFirstLaunch)
			startPreview();
		else
			HideTutorial();
	}

	private void onWeaponSelectedCallback(WeaponItemData i_ItemData)
	{
		onWeaponSelected();
	}

	private void initializeList(List<TrajectoryDot> i_DotList, Transform i_Root, Vector2 i_Scale, ePoolType i_Type)
	{
		i_DotList.Clear();
		int dotCount = m_DataEditor.General.DotCount;
		float scaleStep = i_Scale.y / dotCount;
		for (int i = 0; i < dotCount; i++)
		{
			GameObject pooled = PoolManager.Instance.Dequeue(i_Type, null, null, null, i_Root, true);
			TrajectoryDot dot = pooled.GetComponent<TrajectoryDot>();
			dot.Initialize(i_Scale.y, m_DataEditor.General.DotColor.Evaluate(0f));
			i_DotList.Add(dot);
			i_Scale.y -= i_Scale.y > i_Scale.x ? scaleStep : 0f;
		}
		i_DotList[0].gameObject.SetActive(false);
		i_DotList[1].gameObject.SetActive(false);
		i_DotList[2].gameObject.SetActive(false);
	}

	public void ShowPreview(Vector3 i_Center)
	{
		if (m_ActiveTrajectoryData != null)
		{
			m_PreviewRoot.gameObject.SetActive(true);
			updateDots(m_ActiveTrajectoryData.PushDirection, i_Center, m_PreviewList);
		}
	}

	public bool UpdateCorrectness(Vector3 i_Center, Vector3 i_PushDirection)
	{
		float correctness = GetPushDirectionCorrectness(i_PushDirection);
		updateDefaultDotColor(m_DataEditor.Tutorial.CorrectGradient.Evaluate(correctness));
		if (correctness >= m_DataEditor.Tutorial.CorrectThreshold)
			ShowCorrect(i_Center);
		else
			HideCorrect();
		return correctness >= m_DataEditor.Tutorial.CorrectThreshold;
	}

	public void ShowCorrect(Vector3 i_Center)
	{
		if (m_ActiveTrajectoryData == null) throw new NullReferenceException();
		m_CorrectRoot.gameObject.SetActive(true);
		updateDots(m_ActiveTrajectoryData.PushDirection, i_Center, m_CorrectDotList);
	}

	public void HideCorrect()
	{
		m_CorrectRoot.gameObject.SetActive(false);
	}

	public void HideTutorial()
	{
		m_PreviewRoot.gameObject.SetActive(false);
		m_CorrectRoot.gameObject.SetActive(false);
	}

	private void updateDots(Vector3 i_ShotVelocity, Vector3 i_ProjectilePosition, List<TrajectoryDot> i_DotList)
	{
		int dotCount = m_DataEditor.General.DotCount;
		float spacing = m_DataEditor.General.DotSpacing;
		if (i_DotList == null)
			return;
		for (int i = 2; i < dotCount && i + 1 < i_DotList.Count; i++)
		{
			Vector3 position = WayneStaticFunctions.GetParabolaPosition(i_ProjectilePosition, i_ShotVelocity, i, spacing);
			if (i + 1 >= dotCount)
				return;
			i_DotList[i + 1].transform.position = position;
		}
	}

	public bool IsPushDirectionCorrect(Vector3 i_Direction)
	{
		var data = m_ActiveTrajectoryData;
		if (data == null) throw new NullReferenceException();
		return i_Direction.y >= data.PushDirection.y - data.Offset &&
			i_Direction.y <= data.PushDirection.y + data.Offset &&
			i_Direction.z >= data.PushDirection.z - data.Offset &&
			i_Direction.z <= data.PushDirection.z + data.Offset;
	}

	public float GetPushDirectionCorrectness(Vector3 i_Direction)
	{
		var data = m_ActiveTrajectoryData;
		if (data == null) throw new NullReferenceException();
		return (Mathf.Clamp01(1f - Mathf.Abs(i_Direction.y - data.PushDirection.y) / data.Offset) +
			Mathf.Clamp01(1f - Mathf.Abs(i_Direction.z - data.PushDirection.z) / data.Offset)) * 0.5f;
	}

	private void updateDefaultDotColor(Color i_Color)
	{
		foreach (TrajectoryDot dot in m_PreviewList) dot.UpdateColor(i_Color);
	}

	private void startPreview()
	{
		UnitBase unit = UnitManager.Instance.AlivePlayerUnit;
		if (unit == null) return;
		updateDefaultDotColor(m_DataEditor.General.DotColor.Evaluate(0f));
		ShowPreview(unit.transform.position + m_CenterOffset);
		HideCorrect();
	}

	public TrajectoryManager()
	{
		m_PreviewList = new List<TrajectoryDot>();
		m_CorrectDotList = new List<TrajectoryDot>();
	}
}
