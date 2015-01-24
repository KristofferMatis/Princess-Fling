using UnityEngine;
using System.Collections;

public class KillZone : MonoBehaviour {

	const float AMOUNT_TO_MOVE_OVER = 5.0f;

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

				if (princess.getLastThrower().i_Player == Players.PlayerOne)
				{
					respawner.Kill(-AMOUNT_TO_MOVE_OVER);
				}
				else
				{
					respawner.Kill(AMOUNT_TO_MOVE_OVER);
				}
				return;
			}

			//Players just respawn nearby
			respawner.Kill(0);
		}
	}
}
