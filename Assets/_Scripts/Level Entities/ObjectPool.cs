using UnityEngine;
using UnityEngine.Pool;

public class ObjectPool : MonoBehaviour
{
	[SerializeField] private PlatformFactory platformFactory;
	[SerializeField] private MovingPlatformFactory movingPlatformFactory;
	[SerializeField] private TrapPlatformFactory trapPlatformFactory;

	public ObjectPool<Platform> platforms;
	public ObjectPool<MovingPlatform> movingPlatforms;
	public ObjectPool<TrapPlatform> trapPlatforms;

	private void Start()
	{
		//PLATFORMS
		platforms = new ObjectPool<Platform>(() =>
		{
			return platformFactory.GetNewInstance();
		}, platform =>
		{
			platform.gameObject.SetActive(true);
		}, platform =>
		{
			platform.gameObject.SetActive(false);
		}, platform => 
		{
			Destroy(platform.gameObject);
		}, true, 15, 30);

		//MOVING PLATFORMS
		movingPlatforms = new ObjectPool<MovingPlatform>(() =>
		{
			return movingPlatformFactory.GetNewInstance();
		}, platform =>
		{
			platform.gameObject.SetActive(true);
		}, platform =>
		{
			platform.gameObject.SetActive(false);
		}, platform =>
		{
			Destroy(platform.gameObject);
		}, true, 15, 30);

		//TRAP PLATFORMS
		trapPlatforms = new ObjectPool<TrapPlatform>(() =>
		{
			return trapPlatformFactory.GetNewInstance();
		}, platform =>
		{
			platform.gameObject.SetActive(true);
		}, platform =>
		{
			platform.gameObject.SetActive(false);
		}, platform =>
		{
			Destroy(platform.gameObject);
		}, true, 15, 30);

	}
}
