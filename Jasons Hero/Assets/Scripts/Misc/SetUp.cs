using UnityEngine;
using System.Collections;

public class SetUp : MonoBehaviour
{
	const string THROWABLE = "Throwable";
	const string THROWER = "Thrower";

	void Awake()
	{
		Physics2D.IgnoreLayerCollision (LayerMask.NameToLayer (THROWABLE), LayerMask.NameToLayer (THROWER), true);
		Physics.IgnoreLayerCollision (LayerMask.NameToLayer (THROWABLE), LayerMask.NameToLayer (THROWER), true);
	}

	// Use this for initialization
	void Start () 
	{
	
	}
}
