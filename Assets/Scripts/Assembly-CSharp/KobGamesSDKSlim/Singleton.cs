using UnityEngine;

namespace KobGamesSDKSlim
{
	public class Singleton<T> : SingletonBase<Singleton<T>>, ISingelton where T : MonoBehaviour, ISingelton
	{
		private static T s_Instance;

		public bool DontDestroyLoad;

		[SerializeField]
		[HideInInspector]
		private bool m_IsSingletonLogsEnabled;

		private string emptyTitle;

		protected bool m_IsDestroyed;

		private static object _lock;

		protected static bool applicationIsQuittingFlag;

		protected static bool applicationIsQuitting;

		static Singleton()
		{
			_lock = new object();
			applicationIsQuittingFlag = false;
			applicationIsQuitting = false;
		}

		public bool IsSingletonLogsEnabled
		{
			get
			{
				return m_IsSingletonLogsEnabled;
			}
			set
			{
				m_IsSingletonLogsEnabled = value;
			}
		}

		public static bool IsInstanceNull => s_Instance == null;

		public static T Instance
		{
			get
			{
				if (applicationIsQuitting)
					return null;
				if (s_Instance == null)
				{
					lock (_lock)
					{
						s_Instance = FindObjectOfType<T>();
					}
				}
				return s_Instance;
			}
		}

		protected sealed override void Awake()
		{
			if (s_Instance == null)
			{
				s_Instance = GetComponent<T>();
				if (DontDestroyLoad)
					setDontDestroyOnLoad();
				OnAwakeEvent();
			}
			else if (s_Instance != this)
			{
				m_IsDestroyed = true;
				Destroy(gameObject);
			}
		}

		public virtual void OnAwakeEvent()
		{
			return;
		}

		public virtual void Start()
		{
			return;
		}

		protected virtual void OnEnable()
		{
			return;
		}

		public virtual void OnDisable()
		{
			return;
		}

		protected virtual void OnApplicationFocus(bool i_FocusStatus)
		{
			return;
		}

		protected virtual void OnApplicationPause(bool i_PauseStatus)
		{
			return;
		}

		public virtual void OnDestroy()
		{
			if (s_Instance == this)
				s_Instance = null;
			m_IsDestroyed = true;
		}

		protected void setDontDestroyOnLoad()
		{
			DontDestroyLoad = true;
			if (transform.parent != null)
				transform.SetParent(null);
			UnityEngine.Object.DontDestroyOnLoad(gameObject);
		}

		public virtual void OnApplicationQuit()
		{
			applicationIsQuitting = true;
			applicationIsQuittingFlag = true;
		}
	}
}
