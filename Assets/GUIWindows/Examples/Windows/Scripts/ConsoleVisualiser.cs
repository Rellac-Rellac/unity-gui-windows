using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class ConsoleVisualiser : MonoBehaviour
{
	[SerializeField] private Transform textParent = null;
	[SerializeField] private int textSize = 20;
	[SerializeField] private int maxMessages = 10;

	public UnityEvent onMessageRecieved;
	
	private Font messageFont;
	private int currentNumMessages;
	
	static readonly Dictionary<LogType, Color> logTypeColors = new Dictionary<LogType, Color>() {
		{ LogType.Assert, Color.white },
		{ LogType.Error, Color.red },
		{ LogType.Exception, Color.red },
		{ LogType.Log, Color.white },
		{ LogType.Warning, Color.yellow },
	};
	
	void Awake() {
		messageFont = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
	}
	
	void OnEnable () {
		#if UNITY_5_3_OR_NEWER 
		Application.logMessageReceived += HandleLog;
		#else
		Application.RegisterLogCallback(HandleLog);
		#endif
	}
	
	void OnDisable () {
		#if UNITY_5_3_OR_NEWER 
		Application.logMessageReceived -= HandleLog;
		#else
		Application.RegisterLogCallback(null);
		#endif
	}
	
	void HandleLog (string message, string stackTrace, LogType type) {
		StartCoroutine(SendMessageToText(message, type));
	}
	
	private IEnumerator SendMessageToText(string message, LogType type) {
		yield return new WaitForEndOfFrame();
		
		GameObject go = new GameObject(message);
		go.transform.SetParent(textParent);
		go.transform.SetSiblingIndex(0);
		go.transform.localScale = Vector3.one;
		
		Text txt = go.AddComponent<Text>();
		txt.color = logTypeColors[type];
		txt.text = message;
		txt.font = messageFont;
		txt.fontSize = textSize;
		
		RectTransform rt = (RectTransform)go.transform;
		Vector2 size = rt.sizeDelta;
		size.y = textSize;
		rt.sizeDelta = size;
		
		currentNumMessages++;
		if (currentNumMessages > maxMessages) {
			Destroy(textParent.GetChild(textParent.childCount-1).gameObject);
			currentNumMessages--;
		}

		onMessageRecieved.Invoke ();
		
		yield return null;
	}
}