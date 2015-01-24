using UnityEngine;
using System.Collections;

public class Thrower : MonoBehaviour 
{

    const string DEFAULT_LAYER = "Thrower";

	// Use this for initialization
	void Start () 
    {

        gameObject.layer = LayerMask.NameToLayer(DEFAULT_LAYER);
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}
}
