using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class GUIPointerObject : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler {
	[SerializeField] private UnityEvent onPointerUp = null;
	[SerializeField] private UnityEvent onPointerDown = null;
	[SerializeField] private UnityEvent onPointerEnter = null;
	[SerializeField] private UnityEvent onPointerExit = null;
	public void OnPointerUp (PointerEventData eventData) {
		if (onPointerUp != null) {
			onPointerUp.Invoke();
		}
	}
	public void OnPointerDown (PointerEventData eventData) {
		if (onPointerDown != null) {
			onPointerDown.Invoke();
		}
	}
	public void OnPointerEnter (PointerEventData eventData) {
		if (onPointerEnter != null) {
			onPointerEnter.Invoke();
		}
	}
	public void OnPointerExit (PointerEventData eventData) {
		if (onPointerExit != null) {
			onPointerExit.Invoke();
		}
	}
}
