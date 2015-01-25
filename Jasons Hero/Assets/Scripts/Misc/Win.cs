using UnityEngine;
using System.Collections;

public class Win : MonoBehaviour
{
	GUITexture m_Texture;
	public GUITexture m_RoundTexture;

	public Texture m_WinTexture;
	AudioSource m_Audio;

	Throwable[] m_AllThrowables;

	int m_Points = 0;

	public Texture[] m_Round1Textures;
	public Texture[] m_Round2Textures;
	public Texture[] m_Round3Textures;
	float m_Timer = 1.0f;
	int index = 0;

	void Start ()
	{
		m_Texture = GetComponentInParent<GUITexture>();
		m_Texture.enabled = false;
		m_AllThrowables = Object.FindObjectsOfType<Throwable> ();
		m_Audio = GetComponent<AudioSource>();
		m_RoundTexture.texture = m_Round1Textures[index];

		CharacterMovement[] movements = GameObject.FindObjectsOfType<CharacterMovement>();
		foreach(CharacterMovement movement in movements)
		{
			movement.stop = true;
		}
	}

	void Update ()
	{
		if (m_Timer > -1.0f)
		{
			if (m_Timer > 0.0f)
			{
				m_Timer -= Time.deltaTime;
			}
			else if (index < m_Round1Textures.Length - 1)
			{
				m_Timer = 1.0f;
				index++;

				if (m_Points == 0)
				{
					m_RoundTexture.texture = m_Round1Textures[index];
				}
				else if (m_Points == 1)
				{
					m_RoundTexture.texture = m_Round2Textures[index];
				}
				else
				{
					m_RoundTexture.texture = m_Round3Textures[index];
				}
			}
			else
			{
				index = 0;
				m_Timer = -1.0f;
				m_RoundTexture.enabled = false;

				CharacterMovement[] movements = GameObject.FindObjectsOfType<CharacterMovement>();
				foreach(CharacterMovement movement in movements)
				{
					movement.stop = false;
				}
			}
		}
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.name == "Princess")
		{
			CharacterMovement[] movements = GameObject.FindObjectsOfType<CharacterMovement>();
			foreach(CharacterMovement movement in movements)
			{
				movement.stop = true;
			}

			//Play sound
			m_Audio.PlayOneShot(m_Audio.clip);

			foreach (Throwable throwable in m_AllThrowables)
			{
				throwable.Reset();
			}

			index = 0;

			m_Points++;
			if (m_Points >= 3)
			{
				m_Texture.texture = m_WinTexture;
				m_Texture.enabled = true;
				m_RoundTexture.enabled = false;
			}
			else
			{
				if (m_Points == 1)
				{
					m_RoundTexture.texture = m_Round2Textures[index];
				}
				else
				{
					m_RoundTexture.texture = m_Round3Textures[index];
				}
				m_Timer = 1.0f;
				m_RoundTexture.enabled = true;
			}
		}
	}
}
