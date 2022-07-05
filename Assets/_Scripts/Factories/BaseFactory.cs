using UnityEngine;

public class BaseFactory<T> : MonoBehaviour where T : MonoBehaviour
{
	public T prefab;

	public T GetNewInstance(Vector3 position)
	{
		var instance = Instantiate(prefab, position, Quaternion.identity);
		return instance;
	}
}
