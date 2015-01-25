using UnityEngine;
using System.Collections;

public class Win : MonoBehaviour
{
	GUITexture m_Texture;
	public Texture m_WinTexture;

	void Start ()
	{
		m_Texture = GetComponentInParent<GUITexture>();
		m_Texture.enabled = false;
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.name == "Princess")
		{
			m_Texture.texture = m_WinTexture;
			m_Texture.enabled = true;

			//Play sound
		}
	}
}
