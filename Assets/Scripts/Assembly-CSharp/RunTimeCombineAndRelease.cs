using UnityEngine;

public class RunTimeCombineAndRelease : MonoBehaviour
{
	public SimpleMeshCombine simpleMeshCombine;

	public float combineTime = 0.5f;

	public float releaseTime = 2f;

	public void Awake()
	{
		simpleMeshCombine = GetComponent<SimpleMeshCombine>();
	}

	public void Start()
	{
		if (simpleMeshCombine != null)
		{
			Invoke(nameof(Combine), combineTime);
			Invoke(nameof(Release), releaseTime);
		}
	}

	public void Combine()
	{
		simpleMeshCombine.CombineMeshes();
	}

	public void Release()
	{
		if (simpleMeshCombine == null)
			return;
		simpleMeshCombine.EnableRenderers(true);
		if (simpleMeshCombine.combined != null)
		{
			Destroy(simpleMeshCombine.combined);
			simpleMeshCombine.combinedGameOjects = null;
		}
	}
}
