using System.Collections.Generic;
using GameTIleScripts;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace IncommingTileScripts
{
	public class GameTile : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
	{
		public TextMeshProUGUI myText;
		public RectTransform myRectTransform;
		public bool dragable;
		public bool isOnKeep;
	
		public int myValue
		{
			get => MyValue;
			set
			{
				MyValue = value;
				if (MyValue < 2) Destroy(gameObject);
				myText.SetText(MyValue.ToString());
			}
		}
		private int MyValue;
    
		private Vector3 myLastPosition;
	
		public void OnBeginDrag(PointerEventData eventData)
		{
			if (dragable == false) return;
			myLastPosition = transform.localPosition;
		}

		public void OnDrag(PointerEventData eventData)
		{
			if (dragable == false) return;
			myRectTransform.anchoredPosition += eventData.delta;
		}
		public void OnEndDrag (PointerEventData eventData)
		{
			if (dragable == false) return;
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
}
