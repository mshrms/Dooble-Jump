using UnityEngine;
using UnityEngine.EventSystems;

public class TextLinks : MonoBehaviour, IPointerClickHandler
{
	private const string Telegram = "https://t.me/ilyakolot";

	public void OnPointerClick(PointerEventData eventData)
	{
		Application.OpenURL(Telegram);
	}
}
