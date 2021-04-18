using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;
using UnityEngine.UI;

public class InteractiveCanvasUI : MonoBehaviour
{
	public Text text_Action;
	public RectTransform rectTransform;

	public void ShowUI(Vector2 position, Vector2 offset, string text)
	{
		SetActive(true);
		rectTransform.position = position + offset;
		text_Action.text = text;
	}

	public void SetActive(bool active)
	{
		gameObject.SetActive(active);
	}
}