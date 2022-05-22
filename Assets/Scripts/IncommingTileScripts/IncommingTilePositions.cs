using System.Collections.Generic;
using UnityEngine;

public class IncommingTilePositions : MonoBehaviour
{    
	[SerializeField] private Transform[] objectsMarkingIncommingTilePositions;
	
	public List<Vector3> incommingPositions = new List<Vector3>();
	

	private void Awake()
	{
		for (int i = 0; i < objectsMarkingIncommingTilePositions.Length; i++)
		{
			incommingPositions.Add(objectsMarkingIncommingTilePositions[i].localPosition);
		}
		
	}
	
}
