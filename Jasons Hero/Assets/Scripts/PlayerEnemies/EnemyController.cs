using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{
	//list of enemies
	//enemies controlled by each player


	public static Transform getEnemyTransformControlledByPlayer(Players playerIndex)
	{
		return GameObject.CreatePrimitive (PrimitiveType.Cube).transform;
	}
}
