using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class IncommingTileScript : MonoBehaviour, IDragHandler, IEndDragHandler
{
	public int myValue;
	public TextMeshProUGUI myText;
	public RectTransform myRectTransform;
	public bool dragable;

	private Vector3 myLastPosition;

	public void OnDrag(PointerEventData eventData)
	{
		if (dragable == false) return;
		myRectTransform.anchoredPosition += eventData.delta;
	}
	
	public void OnEndDrag (PointerEventData eventData)
	{
		List<RaycastResult> results = new List<RaycastResult>();
		EventSystem.current.RaycastAll(eventData, results);
		var hitObjectList = new List<Transform>();
		for (var index = 0; index < results.Count; index++)
		{
			var hitObject = results[index].gameObject.transform.parent;
			hitObjectList.Add(hitObject);
		}
	}

}
