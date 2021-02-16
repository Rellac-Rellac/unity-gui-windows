using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class GUIWindow : MonoBehaviour {
	public void CloseWindow () {
		Destroy(gameObject);
	}
}