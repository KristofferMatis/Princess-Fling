using UnityEngine;
using System.Collections;

public class KillZone : MonoBehaviour {

	void OnTriggerEnter (Collider other)
	{
		Respawner respawner = other.GetComponent<Respawner>();
		if (respawner != null)
		{
			//Princesses can go more to the left or right
			if (other.name == "Princess")
			{
				//Check who last had the princess
				Princess princess = other.gameObject.GetComponent<Princess>();
				Players thePlayer = princess.getLastThrower().i_Player;
				if (thePlayer == Players.PlayerOne)
				{
					respawner.Kill(-Respawner.AMOUNT_TO_MOVE_OVER);
				}
				else
				{
					respawner.Kill(Respawner.AMOUNT_TO_MOVE_OVER);
				}
			}
			else if (other.name == "PlayerOne")
			{
				respawner.Kill(Respawner.AMOUNT_TO_MOVE_OVER);
			}
			else
			{
				respawner.Kill(-Respawner.AMOUNT_TO_MOVE_OVER);
			}
		}
	}
}
