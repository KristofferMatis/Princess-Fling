using UnityEngine;
using System.Collections;

public class Thrower : MonoBehaviour
{
	public Players i_Player;

    const string DEFAULT_LAYER = "Thrower";
    ThrowableDetector m_Detector;

    Throwable m_BeingCarried = null;

    const float THROW_POWER = 0.5f;
    const float IGNORE_COLLISION_PERIOD = 0.25f;

    // Use this for initialization
    void Start()
    {
        gameObject.layer = LayerMask.NameToLayer(DEFAULT_LAYER);
        m_Detector = GetComponentInChildren<ThrowableDetector>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_BeingCarried != null)
        {
            throwing();
        }
        else
        {
            pickUp();
        }
    }

    void throwing()
    {
        if (InputManager.getThrowUp(i_Player))
            onThrow();
    }

    public void drop()
    {
        m_BeingCarried.changeState(Throwable.states.nope);
        m_BeingCarried.transform.parent = null;

        m_BeingCarried = null;
    }

    void pickUp()
    {
        if (m_Detector.ThrowablesInRange.Count == 0)
            return;

        if (!InputManager.getThrowDown(i_Player))
            return;

        if (m_Detector.ThrowablesInRange.Count == 1)
        {
            onPickUp(m_Detector.ThrowablesInRange[0]);            
            return;
        }

        for(int i =0; i < m_Detector.ThrowablesInRange.Count; i++)
        {
            Princess temp = m_Detector.ThrowablesInRange[i] as Princess;
            if(temp != null)
            {
                onPickUp(temp);
                return;
            }
        }

        onPickUp(m_Detector.ThrowablesInRange[0]);
    }

    void onPickUp(Throwable pickedUp)
    {
        m_BeingCarried = pickedUp;
        m_BeingCarried.changeState(Throwable.states.carry);
        m_BeingCarried.transform.parent = transform;
    }

    void onThrow()
    {
        m_BeingCarried.Velocity = InputManager.getLeftStick(i_Player).normalized * THROW_POWER;
        m_BeingCarried.changeState(Throwable.states.airborn);
        m_BeingCarried.transform.parent = null;

        StartCoroutine(tempIgnoreCollision(m_BeingCarried));

        m_Detector.removeThrowable(m_BeingCarried);
        m_BeingCarried.BeingCarriedBy = null;

        m_BeingCarried = null;
    }

    IEnumerator tempIgnoreCollision(Throwable thrown)
    {
        Collider[] collidersMine = GetComponents<Collider>();
        Collider[] collidersThiers = thrown.gameObject.GetComponents<Collider>();

        for(int i = 0; i < collidersMine.Length;i++)
        {
            for (int c = 0; c < collidersMine.Length; c++)
            {
                Physics.IgnoreCollision(collidersMine[i], collidersThiers[c], true);
            }
        }

        yield return new WaitForSeconds(IGNORE_COLLISION_PERIOD);

        for (int i = 0; i < collidersMine.Length; i++)
        {
            for (int c = 0; c < collidersMine.Length; c++)
            {
                Physics.IgnoreCollision(collidersMine[i], collidersThiers[c], false);
            }
        }
    }
}
