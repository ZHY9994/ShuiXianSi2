using System.Collections;
using UnityEngine;

[AddComponentMenu("Simple Mesh Combine")]
public class SimpleMeshCombine : MonoBehaviour
{
	public GameObject[] combinedGameOjects;

	public GameObject combined;

	public string meshName = "Combined_Meshes";

	public bool _canGenerateLightmapUV;

	public int vCount;

	public bool generateLightmapUV;

	public float lightmapScale = 1f;

	public GameObject copyTarget;

	public bool destroyOldColliders;

	public bool keepStructure = true;

	public Mesh autoOverwrite;

	public bool setStatic = true;

	public bool increaseVertexCount;

	public void EnableRenderers(bool e)
	{
		for (int i = 0; i < combinedGameOjects.Length; i++)
		{
			if (combinedGameOjects[i] == null)
			{
				break;
			}
			Renderer component = combinedGameOjects[i].GetComponent<Renderer>();
			if (component != null)
			{
				component.enabled = e;
			}
		}
	}

	public MeshFilter[] FindEnabledMeshes()
	{
		MeshFilter[] meshFilters = transform.GetComponentsInChildren<MeshFilter>();
		int enabledCount = 0;
		for (int i = 0; i < meshFilters.Length; i++)
		{
			MeshRenderer renderer = meshFilters[i].GetComponent<MeshRenderer>();
			if (renderer != null && renderer.enabled)
			{
				enabledCount++;
			}
		}
		MeshFilter[] enabledMeshes = new MeshFilter[enabledCount];
		int index = 0;
		for (int i = 0; i < meshFilters.Length; i++)
		{
			MeshRenderer renderer = meshFilters[i].GetComponent<MeshRenderer>();
			if (renderer != null && renderer.enabled)
			{
				enabledMeshes[index++] = meshFilters[i];
			}
		}
		return enabledMeshes;
	}

	public void ReleaseMeshes()
	{
		EnableRenderers(true);
		if (combined != null)
		{
			Destroy(combined);
		}
		combinedGameOjects = null;
		vCount = 0;
	}

	public void CombineMeshes()
	{
		GameObject combinedObject = new GameObject("_Combined Mesh [" + name + "]");
		combinedObject.AddComponent<MeshFilter>();
		combinedObject.AddComponent<MeshRenderer>();
		MeshFilter[] enabledMeshes = FindEnabledMeshes();
		ArrayList materials = new ArrayList();
		ArrayList combineGroups = new ArrayList();
		combinedGameOjects = new GameObject[enabledMeshes.Length];
		for (int i = 0; i < enabledMeshes.Length; i++)
		{
			MeshFilter meshFilter = enabledMeshes[i];
			combinedGameOjects[i] = meshFilter.gameObject;
			MeshRenderer renderer = meshFilter.GetComponent<MeshRenderer>();
			renderer.enabled = false;
			Mesh mesh = meshFilter.sharedMesh;
			if (mesh == null)
			{
				continue;
			}
			Material[] sharedMaterials = renderer.sharedMaterials;
			for (int subMeshIndex = 0; subMeshIndex < mesh.subMeshCount; subMeshIndex++)
			{
				Material material = sharedMaterials[subMeshIndex];
				int materialIndex = Contains(materials, material);
				if (materialIndex < 0)
				{
					materials.Add(material);
					combineGroups.Add(new ArrayList());
					materialIndex = materials.Count - 1;
				}
				ArrayList group = (ArrayList)combineGroups[materialIndex];
				group.Add(new CombineInstance
				{
					transform = meshFilter.transform.localToWorldMatrix,
					mesh = mesh,
					subMeshIndex = subMeshIndex
				});
			}
		}
		CombineInstance[] finalInstances = new CombineInstance[materials.Count];
		Mesh[] temporaryMeshes = new Mesh[materials.Count];
		for (int i = 0; i < materials.Count; i++)
		{
			ArrayList group = (ArrayList)combineGroups[i];
			CombineInstance[] groupInstances = (CombineInstance[])group.ToArray(typeof(CombineInstance));
			Mesh mesh = new Mesh();
			mesh.CombineMeshes(groupInstances, true, true);
			temporaryMeshes[i] = mesh;
			finalInstances[i] = new CombineInstance
			{
				mesh = mesh,
				subMeshIndex = 0
			};
		}
		MeshFilter combinedFilter = combinedObject.GetComponent<MeshFilter>();
		Mesh combinedMesh = new Mesh();
		combinedFilter.sharedMesh = combinedMesh;
		combinedMesh.Clear();
		combinedMesh.CombineMeshes(finalInstances, false, false);
		for (int i = 0; i < temporaryMeshes.Length; i++)
		{
			temporaryMeshes[i].Clear();
			DestroyImmediate(temporaryMeshes[i]);
		}
		MeshRenderer combinedRenderer = combinedObject.GetComponent<MeshRenderer>();
		combinedRenderer.materials = (Material[])materials.ToArray(typeof(Material));
		combined = combinedObject;
		EnableRenderers(false);
		combinedObject.transform.SetParent(transform);
		combinedMesh.RecalculateBounds();
		vCount = combinedMesh.vertexCount;
		if (setStatic)
		{
			combinedObject.isStatic = true;
		}
	}

	public int Contains(ArrayList l, Material n)
	{
		for (int i = 0; i < l.Count; i++)
		{
			if (l[i] == n)
			{
				return i;
			}
		}
		return -1;
	}

}
