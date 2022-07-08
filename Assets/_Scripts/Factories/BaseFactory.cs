using UnityEngine;

public class BaseFactory<T> : MonoBehaviour where T : MonoBehaviour
{
	public T prefab;

	public T GetNewInstance()
	{
		var instance = Instantiate(prefab);
		return instance;
	}
}
