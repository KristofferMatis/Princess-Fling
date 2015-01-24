using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ThrowableDetector : MonoBehaviour 
{
	List<Throwable> m_ThrowablesInRange = new List<Throwable>();
    public List<Throwable> ThrowablesInRange
    {
        get { return m_ThrowablesInRange; }
    }

    const string THROWABLE = "Throwable";
	
	void OnTriggerEnter(Collider other)
	{
        if (other.gameObject.tag == "Throwable" && other.gameObject != transform.parent.gameObject)
		{
            Throwable temp = other.GetComponent<Throwable>();
            if (!m_ThrowablesInRange.Contains(temp))
            {
                m_ThrowablesInRange.Add(temp);
            }
		}
	}

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Throwable")
        {
            Throwable temp = other.GetComponent<Throwable>();
            removeThrowable(temp);
        }
    }

    public void removeThrowable(Throwable thrown)
    {
        if (m_ThrowablesInRange.Contains(thrown))
        {
            m_ThrowablesInRange.Remove(thrown);
        }
    }

    public void addThrowable(Throwable thrown)
    {
        if (!m_ThrowablesInRange.Contains(thrown))
        {
            m_ThrowablesInRange.Add(thrown);
        }
    }
}
