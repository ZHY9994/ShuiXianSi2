using UnityEngine;

namespace KobGamesSDKSlim
{
	[ExecutionOrder(-85)]
	public class PoolManagerBase : Singleton<PoolManager>
	{
		private static readonly Color s_AuditBloodColor = new Color(0.15f, 0.85f, 0.25f, 1f);

		[SerializeField]
		public DictionaryPoolTypeListPoolConfig PoolItemsDefinition;

		[Header("Pool Objects")]
		public DictionaryPoolTypeListGameObject ActiveObjects;

		public DictionaryPoolTypeListGameObject AvailableObjects;

		[Header("Pool Holders")]
		public Transform PoolHolder;

		public DictionaryPoolTypeListTransform PoolHolderType;

		public override void OnAwakeEvent()
		{
			base.OnAwakeEvent();
			if (PoolItemsDefinition == null || PoolItemsDefinition.Count < 1)
			{
				ClearLists();
				RebuildLists();
				return;
			}
			if (PoolHolder != null && PoolItemsDefinition.Count == PoolHolder.childCount)
			{
				ClearLists();
				RebuildLists();
				return;
			}
			Prebake();
		}

		public override void Start()
		{
			base.Start();
		}

		protected new virtual void OnEnable()
		{
			GameManagerV1.GameManagerBase.OnGameReset += OnCoreLoop_Reset;
		}

		public override void OnDisable()
		{
			base.OnDisable();
			GameManagerV1.GameManagerBase.OnGameReset -= OnCoreLoop_Reset;
		}

		public virtual void OnCoreLoop_Reset()
		{
			foreach (var pair in ActiveObjects)
			{
				for (int i = pair.Value.List.Count - 1; i >= 0; i--)
					Queue(pair.Key, pair.Value.List[i]);
			}
		}

		private void Prebake()
		{
			Application.IsPlaying(this);
			RemoveTransforms();
			CreateTransforms();
		}

		private void RemoveTransforms()
		{
			Transform transform = GetComponent<Transform>();
			if (transform == null)
				return;
			for (int i = transform.childCount - 1; i >= 0; i--)
			{
				Transform child = transform.GetChild(i);
				if (child != null)
					DestroyImmediate(child.gameObject);
			}
			ClearLists();
		}

		private void CreateTransforms()
		{
			GameObject holderObject = new GameObject("Pool Holder");
			PoolHolder = holderObject.transform;
			PoolHolder.SetParent(transform);
			foreach (var pair in PoolItemsDefinition)
			{
				ePoolType type = pair.Key;
				PoolConfig config = pair.Value;
				GameObject typeObject = new GameObject(type.ToString());
				Transform typeHolder = typeObject.transform;
				typeHolder.SetParent(PoolHolder);
				PoolHolderType.Add(type, typeHolder);
				var list = new System.Collections.Generic.List<GameObject>();
				for (int i = 0; i < config.InitialQuantity; i++)
				{
					if (config.UseList)
					{
						foreach (GameObject prefab in config.PrefabList)
						{
							if (prefab == null) continue;
							GameObject item = Instantiate(prefab);
							item.SetActive(false);
							item.name = type + " Pool " + i;
							item.transform.SetParent(typeHolder);
							list.Add(item);
						}
					}
					else if (config.Prefab != null)
					{
						GameObject item = Instantiate(config.Prefab);
						item.SetActive(false);
						item.name = type + " Pool " + i;
						item.transform.SetParent(typeHolder);
						list.Add(item);
					}
				}
				AvailableObjects.Add(type, new GameObjectList(list));
				ActiveObjects.Add(type, new GameObjectList(new System.Collections.Generic.List<GameObject>()));
			}
		}

		private void CreateFromExistingTransforms()
		{
			ClearLists();
			RebuildLists();
		}

		private void RebuildLists()
		{
			foreach (var pair in PoolItemsDefinition)
			{
				ePoolType type = pair.Key;
				Transform holder = transform.FindDeepChild(type.ToString());
				PoolHolderType.Add(type, holder);
				var objects = new System.Collections.Generic.List<GameObject>();
				if (holder != null)
				{
					for (int i = 0; i < holder.childCount; i++)
					{
						Transform child = holder.GetChild(i);
						if (child != PoolHolder)
							objects.Add(child.gameObject);
					}
				}
				AvailableObjects.Add(type, new GameObjectList(objects));
				ActiveObjects.Add(type, new GameObjectList(new System.Collections.Generic.List<GameObject>()));
			}
		}

		private void ClearLists()
		{
			PoolHolderType?.Clear();
			ActiveObjects?.Clear();
			AvailableObjects?.Clear();
		}

		public GameObject Dequeue(ePoolType i_Type, Vector3? i_Position = null, Quaternion? i_Rotation = null, Vector3? i_Scale = null, Transform i_Parent = null, bool i_Enabled = true)
		{
			GameObject result = DequeueObject(i_Type);
			if (result == null) return null;
			if (i_Type == ePoolType.Player)
				GameTraceLog.LogError($"[PlayerPoolTrace] Dequeue before name={result.name} id={result.GetInstanceID()} active={result.activeSelf} pos={result.transform.position} parent={(result.transform.parent != null ? result.transform.parent.name : "<root>")}");
			if (i_Position.HasValue) result.transform.position = i_Position.Value;
			if (i_Rotation.HasValue) result.transform.rotation = i_Rotation.Value;
			if (i_Scale.HasValue) result.transform.localScale = i_Scale.Value;
			if (i_Parent != null) result.transform.SetParent(i_Parent);
			if (IsBloodEffect(i_Type)) ApplyBloodEffectColor(result);
			result.SetActive(i_Enabled);
			if (i_Type == ePoolType.Player)
				GameTraceLog.LogError($"[PlayerPoolTrace] Dequeue after name={result.name} id={result.GetInstanceID()} active={result.activeSelf} pos={result.transform.position} parent={(result.transform.parent != null ? result.transform.parent.name : "<root>")}");
			return result;
		}

		private static bool IsBloodEffect(ePoolType i_Type)
		{
			switch (i_Type)
			{
				case ePoolType.BloodSplat:
				case ePoolType.BloodExplosion:
				case ePoolType.BloodPop:
				case ePoolType.BloodDrip:
				case ePoolType.BloodExplosion_02:
				case ePoolType.BloodExplosion_03:
				case ePoolType.BloodDrip_02:
					return true;
				default:
					return false;
			}
		}

		private static void ApplyBloodEffectColor(GameObject i_Effect)
		{
			if (i_Effect == null) return;
			ParticleSystem[] particles = i_Effect.GetComponentsInChildren<ParticleSystem>(true);
			for (int i = 0; i < particles.Length; i++)
			{
				ParticleSystem.MainModule main = particles[i].main;
				main.startColor = s_AuditBloodColor;
			}
		}

		protected GameObject DequeueObject(ePoolType i_type)
		{
			GameObjectList available = AvailableObjects[i_type];
			PoolConfig config = PoolItemsDefinition[i_type];
			GameObject result;
			if (available.List.Count > 0)
			{
				int index = config.UseList ? Random.Range(0, available.List.Count) : available.List.Count - 1;
				result = available.List[index];
				available.List.Remove(result);
			}
			else
			{
				GameObject prefab = config.UseList
					? config.PrefabList[Random.Range(0, config.PrefabList.Count)]
					: config.Prefab;
				result = Instantiate(prefab);
				if (result == null)
					return null;
				result.name = i_type + " Pool";
				result.transform.SetParent(PoolHolderType[i_type]);
			}
			ActiveObjects[i_type].List.Add(result);
			return result;
		}

		public void Queue(ePoolType i_Type, GameObject i_Object)
		{
			if (i_Type == ePoolType.Player && i_Object != null)
				GameTraceLog.LogError($"[PlayerPoolTrace] Queue before name={i_Object.name} id={i_Object.GetInstanceID()} active={i_Object.activeSelf} pos={i_Object.transform.position} parent={(i_Object.transform.parent != null ? i_Object.transform.parent.name : "<root>")}");
			if (ActiveObjects[i_Type].List.Contains(i_Object))
				ActiveObjects[i_Type].List.Remove(i_Object);
			if (AvailableObjects[i_Type].List.Contains(i_Object))
				return;
			AvailableObjects[i_Type].List.Add(i_Object);
			Transform transform = i_Object.transform;
			transform.SetParent(PoolHolderType[i_Type]);
			transform.position = Vector3.zero;
			transform.rotation = Quaternion.identity;
			i_Object.name = i_Type + " Pool";
			i_Object.SetActive(false);
			if (i_Type == ePoolType.Player)
				GameTraceLog.LogError($"[PlayerPoolTrace] Queue after name={i_Object.name} id={i_Object.GetInstanceID()} active={i_Object.activeSelf} pos={i_Object.transform.position} parent={(i_Object.transform.parent != null ? i_Object.transform.parent.name : "<root>")}");
		}

		public PoolManagerBase()
		{
			PoolItemsDefinition = new DictionaryPoolTypeListPoolConfig();
			ActiveObjects = new DictionaryPoolTypeListGameObject();
			AvailableObjects = new DictionaryPoolTypeListGameObject();
			PoolHolderType = new DictionaryPoolTypeListTransform();
		}
	}
}
