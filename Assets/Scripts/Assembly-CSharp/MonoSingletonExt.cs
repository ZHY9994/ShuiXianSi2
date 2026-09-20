using UnityEngine;

public abstract class MonoSingletonExt<T> : MonoBehaviour where T : MonoSingletonExt<T>
{
	private const string STR_NoInstanceOfATemporaryOneIsCreated = "No instance of {0}, a temporary one is created.";

	private const string STR_TempInstanceOf = "Temp Instance of ";

	private const string STR_ProblemDuringTheCreationOf = "Problem during the creation of {0}";

	private static T m_Instance;

	private static bool IsShuttingDown;

	public static T instance
	{
		get
		{
			if (IsShuttingDown)
			{
				return null;
			}
			if (m_Instance == null)
			{
				m_Instance = Object.FindObjectOfType<T>();
				if (m_Instance == null)
				{
					GameObject gameObject = new GameObject(STR_TempInstanceOf + typeof(T));
					m_Instance = gameObject.AddComponent<T>();
				}
			}
			return m_Instance;
		}
	}

	private void Awake()
	{
		if (m_Instance == null)
		{
			m_Instance = this as T;
			Object.DontDestroyOnLoad(gameObject);
		}
	}

	public virtual void Init()
	{
	}

	internal virtual void OnApplicationQuit()
	{
		IsShuttingDown = true;
		m_Instance = null;
	}
}
