using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class IncommingTileScript : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
	public int myValue;
	public TextMeshProUGUI myText;
	public RectTransform myRectTransform;
	public bool dragable;

	private Vector3 myLastPosition;
	
	public void OnBeginDrag(PointerEventData eventData)
	{
		myLastPosition = transform.localPosition;
	}

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
			if (hitObject!=transform && !hitObjectList.Contains(hitObject)) hitObjectList.Add(hitObject);
		}
		if (PlayerDroppedTileHandler.inst.IncommingTileDroppedSuccessCheck(this,hitObjectList) == false)
		{
			ResetPositionDropFailed();
		}
	}

	private void ResetPositionDropFailed()
	{
		transform.localPosition = myLastPosition;
	}



}
