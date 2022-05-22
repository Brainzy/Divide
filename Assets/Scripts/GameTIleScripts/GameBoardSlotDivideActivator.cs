using System.Collections;
using System.Collections.Generic;
using IncommingTileScripts;
using UnityEngine;

namespace GameTIleScripts
{
	public class GameBoardSlotDivideActivator : MonoBehaviour
	{
		[SerializeField] private GameBoardSlotDivideActivator upNeighbourTile;
		[SerializeField] private GameBoardSlotDivideActivator downNeighbourTile;
		[SerializeField] private GameBoardSlotDivideActivator rightNeighbourTile;
		[SerializeField] private GameBoardSlotDivideActivator leftNeighbourTile;
		[SerializeField] private float comboWaitTime = 0.3f;
		[SerializeField] private ScoreManager scoreManager;
		
		public GameTile numberOnMySlot;
		
		
		private readonly List<GameBoardSlotDivideActivator> myActiveNeighbours = new List<GameBoardSlotDivideActivator>();

		private void Awake()
		{
			if (upNeighbourTile!=null) myActiveNeighbours.Add(upNeighbourTile);
			if (downNeighbourTile!=null) myActiveNeighbours.Add(downNeighbourTile);
			if (rightNeighbourTile!=null) myActiveNeighbours.Add(rightNeighbourTile);
			if (leftNeighbourTile!=null) myActiveNeighbours.Add(leftNeighbourTile);
		}

		private void ExecuteDivision(int divider)
		{
			if (numberOnMySlot == null) return;
			scoreManager.AddScore(numberOnMySlot.myValue > divider ? divider : numberOnMySlot.myValue);
			var calculatedNumberAfterDivision = numberOnMySlot.myValue / divider;
			numberOnMySlot.myValue = calculatedNumberAfterDivision;
			if (calculatedNumberAfterDivision < 2) numberOnMySlot = null;
		}
		public void FindAndDivideNeighbours()
		{
			if (numberOnMySlot == null) return;
			var listToDivide = new List<GameBoardSlotDivideActivator>();
			MakeAListOfPossibleDivisions(listToDivide);
			DivideValidNumbers(listToDivide);
			if (listToDivide.Count > 0) // if something was divided check for combo
			{
				listToDivide.Add(this);
				StartCoroutine(RecursivelyRepeatInCaseOfCombo(listToDivide));
			}
		}

		private IEnumerator  RecursivelyRepeatInCaseOfCombo(List<GameBoardSlotDivideActivator> listToDivide)
		{
			yield return new WaitForSeconds(comboWaitTime);
			for (int i = 0; i < listToDivide.Count; i++)
			{
				FindAndDivideNeighbours();
			}
		}

		private void DivideValidNumbers(List<GameBoardSlotDivideActivator> listToDivide)
		{
			var myRememberedNumber = numberOnMySlot.myValue;
			for (int i = 0; i < listToDivide.Count; i++)
			{
				ExecuteDivision(listToDivide[i].numberOnMySlot.myValue);
				listToDivide[i].ExecuteDivision(myRememberedNumber);
			}
		}

		private void MakeAListOfPossibleDivisions(List<GameBoardSlotDivideActivator> listToDivide)
		{
			for (int i = 0; i < myActiveNeighbours.Count; i++)
			{
				var neighbourNumberScript = myActiveNeighbours[i].numberOnMySlot;
				if (neighbourNumberScript == null) continue; // if tile is empty 
				if (neighbourNumberScript.myValue % numberOnMySlot.myValue == 0 ||
				    numberOnMySlot.myValue % neighbourNumberScript.myValue == 0)
				{
					listToDivide.Add(myActiveNeighbours[i]);
				}
			}
		}
	}
}
