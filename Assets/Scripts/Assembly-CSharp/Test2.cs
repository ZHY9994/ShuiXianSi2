using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Test2 : MonoBehaviour
{
	[Serializable]
	public class SkinMoveData
	{
		public string Name;

		public SkinnedMeshRenderer SkinToMove;
	}

	public GameObject m_MainRoot;

	public SkinnedMeshRenderer m_MainSkin;

	public List<SkinMoveData> Data = new List<SkinMoveData>();

	public GameObject[] AllGOs;

	private void move()
	{
		foreach (var data in Data)
		{
			reassign(data.SkinToMove);
			moveSkin(data);
		}
	}

	private void moveSkin(SkinMoveData i_Data)
	{
		if (i_Data == null || m_MainRoot == null) return;
		var go = new GameObject(i_Data.Name);
		var tr = go.transform;
		tr.SetParent(m_MainRoot.transform, false);
		tr.localPosition = Vector3.zero;
		tr.localRotation = Quaternion.identity;
		tr.localScale = Vector3.one;
		if (i_Data.SkinToMove != null)
			i_Data.SkinToMove.transform.SetParent(tr, false);
	}

	private void setMesh(Material i_Mat)
	{
		foreach (var child in transform.Cast<Transform>().ToList())
		{
			var meshRenderer = child.GetComponentInChildren<MeshRenderer>();
			if (meshRenderer != null) meshRenderer.sharedMaterial = i_Mat;
		}
	}

	private void setSkin(Material i_Mat)
	{
		// IDA decompile and disasm by name both failed: Test2::setSkin.
	}

	private void reassign(SkinnedMeshRenderer i_SkinToMove)
	{
		// IDA decompile and disasm by name both failed: Test2::reassign.
	}

	private void populateData()
	{
		// IDA decompile and disasm by name both failed: Test2::populateData.
	}

	private void orderChilderen()
	{
		var children = transform.Cast<Transform>().ToList();
		children.Sort((a, b) => a.name.CompareTo(b.name));
		for (int i = 0; i < children.Count; i++) children[i].SetSiblingIndex(i);
	}

}
