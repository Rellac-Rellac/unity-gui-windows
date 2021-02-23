using UnityEngine;
using System.Collections;

public class GUIBorderParent : MonoBehaviour {
	private GUIWindowHandle[] handles;
	// Use this for initialization
	void Start () {
		handles = GetComponentsInChildren<GUIWindowHandle> ();
	}
	
	public void SetIsLocked(bool input) {
		for (int i = 0; i < handles.Length; i++) {
			handles[i].SetIsLocked(input);
		}
	}
}
