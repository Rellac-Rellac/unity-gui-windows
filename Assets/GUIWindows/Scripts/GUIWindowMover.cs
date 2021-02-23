using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class GUIWindowMover : GUIPointerObject {

	[SerializeField] private RectTransform parentWindow = null;
	[SerializeField] private bool isLocked = false;
	[SerializeField] private UnityEvent onWindowMoved = null;
	private Vector2 mouseOffset;
	private bool isGrabbed = false;

	void Start() {
		onPointerDown.AddListener (SetIsGrabbed);
	}

	void Update ()
	{
		if (!isGrabbed || isLocked) return;

		parentWindow.position = (Vector2)Input.mousePosition + mouseOffset;
		if (Input.GetMouseButtonUp (0)) {
			isGrabbed = false;
			if (onWindowMoved != null) {
				onWindowMoved.Invoke();
			}
		}
	}

	public void SetIsLocked(bool input) {
		isLocked = input;
		isGrabbed = false;
	}

	public void SetIsGrabbed () {
		mouseOffset = parentWindow.position - Input.mousePosition;
		isGrabbed = true;
	}
}
