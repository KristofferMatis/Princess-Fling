using UnityEngine;
using System.Collections;

public class Win : MonoBehaviour
{
	GUITexture m_Texture;
	public Texture m_WinTexture;
	AudioSource m_Audio;

	Throwable[] m_AllThrowables;

	int m_Points = 0;

	void Start ()
	{
		m_Texture = GetComponentInParent<GUITexture>();
		m_Texture.enabled = false;
		m_AllThrowables = Object.FindObjectsOfType<Throwable> ();
		m_Audio = GetComponent<AudioSource>();
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.name == "Princess")
		{
			//Play sound
			m_Audio.PlayOneShot(m_Audio.clip);

			m_Points++;

			foreach (Throwable throwable in m_AllThrowables)
			{
				throwable.Reset();
			}

			m_Points++;
			if (m_Points >= 3)
			{
				m_Texture.texture = m_WinTexture;
				m_Texture.enabled = true;
				return;
			}
		}
	}
}
