using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    protected static T _instance;

	public static T Instance
	{
		get
		{
			if (_instance == null)
			{
				var obj = new GameObject(typeof(T).Name);
				_instance = obj.AddComponent<T>();
			}

			return _instance;
		}
	}

	protected void Awake()
	{
		if(!_instance)
        {
			_instance = this as T;
        }
		DontDestroyOnLoad(this);
	}

	protected virtual void OnDestroy()
	{
		_instance = null;
	}
}
