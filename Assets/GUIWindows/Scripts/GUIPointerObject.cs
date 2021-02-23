using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class GUIPointerObject : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler {
	[HideInInspector]
	public UnityEvent onPointerUp = null;
	[HideInInspector]
	public UnityEvent onPointerDown = null;
	[HideInInspector]
	public UnityEvent onPointerEnter = null;
	[HideInInspector]
	public UnityEvent onPointerExit = null;
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
