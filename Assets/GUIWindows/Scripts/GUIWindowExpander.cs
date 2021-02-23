using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class GUIWindowExpander : GUIPointerObject {

	private const float MaxTimeForDoubleClick = 0.5f;

	[SerializeField] private RectTransform parentWindow = null;
	[SerializeField] private bool isLocked = false;
	[Tooltip("Allows you to double click this target image to minimise/maximise")]
	[SerializeField] private bool doubleClick = true;
	[SerializeField] private UnityEvent onMinimised = null;
	[SerializeField] private UnityEvent onMaximised = null;
	private bool isMaximised = false;
	private bool doAction = false;

	private Vector2 initialPosition;
	private Vector2 initialMinAnchor;
	private Vector2 initialMaxAnchor;
	private Vector2 initialSize;
	private Vector2 initialPivot = Vector2.one * -1;

	private Vector2 targetPosition;
	private Vector2 targetMinAnchor;
	private Vector2 targetMaxAnchor;
	private Vector2 targetSize;

	private int numClicks;

	void Start() {
		onPointerDown.AddListener (parentWindow.SetAsLastSibling);
		onPointerDown.AddListener (TryDoubleClick);
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (doAction) {
			// Lerp to position & size
			parentWindow.sizeDelta = Vector2.MoveTowards (parentWindow.sizeDelta, targetSize, Time.deltaTime * 10000);
			parentWindow.anchoredPosition = Vector2.MoveTowards (parentWindow.anchoredPosition, targetPosition, Time.deltaTime * 5000);
			// reached target
			if (parentWindow.sizeDelta == targetSize && parentWindow.anchoredPosition == targetPosition) {
				doAction = false;
				if (isMaximised) { // set to a full stretched rect
					parentWindow.anchorMin = Vector2.zero;
					parentWindow.anchorMax = Vector2.one;
					parentWindow.sizeDelta = Vector2.zero;
					parentWindow.anchoredPosition = Vector2.zero;
					if (onMaximised != null) {
						onMaximised.Invoke ();
					}
				} else { // just invoke the event for a minimised window
					if (onMinimised != null) {
						onMinimised.Invoke ();
					}
				}
			}
		}
	}

	public void SetIsLocked(bool input) {
		isLocked = input;
	}

	public void MaximiseWindow () {
		if (isLocked) return;

		initialPosition = parentWindow.anchoredPosition;
		initialMinAnchor = parentWindow.anchorMin;
		initialMaxAnchor = parentWindow.anchorMax;
		initialSize = parentWindow.sizeDelta;
		initialPivot = parentWindow.pivot;

		parentWindow.SetPivot (Vector2.one * 0.5f);

		targetPosition = Vector2.zero;
		targetSize = new Vector2(Screen.width, Screen.height);

		isMaximised = true;
		doAction = true;
	}

	public void MinimiseWindow() {
		if (isLocked) return;
		parentWindow.anchorMin = initialMinAnchor;
		parentWindow.anchorMax = initialMaxAnchor;

		parentWindow.sizeDelta = new Vector2(Screen.width, Screen.height);

		if (initialPivot != Vector2.one * -1) {
			parentWindow.SetPivot(initialPivot);
		}

		targetPosition = initialPosition;
		targetSize = initialSize;

		isMaximised = false;
		doAction = true;
	}

	public void TryDoubleClick () {
		if (isLocked || !doubleClick) return;
		numClicks++;
		if (numClicks == 1) {
			Invoke ("ResetDoubleClick", MaxTimeForDoubleClick);
		} else if (numClicks >= 2) {
			Invoke ("Swap", 0.1f); // wait in case we're interrupting a mover
		}
	}

	public void Swap () {
		if (isMaximised) {
			MinimiseWindow ();
		} else {
			MaximiseWindow();
		}
	}

	private void ResetDoubleClick ()
	{
		numClicks = 0;
	}
}
