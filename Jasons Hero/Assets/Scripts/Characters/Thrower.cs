using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterMovement))]
public class Thrower : MonoBehaviour
{
	public Players i_Player;

    const string DEFAULT_LAYER = "Thrower";
    ThrowableDetector m_Detector;

    Throwable m_BeingCarried = null;

    const float THROW_POWER = 0.8f;
    const float IGNORE_COLLISION_PERIOD = 0.25f;

    CharacterMovement m_Movement;

    public GameObject i_MountPoint;

    // Use this for initialization
    void Start()
    {
        m_Detector = GetComponentInChildren<ThrowableDetector>();
        m_Movement = GetComponent<CharacterMovement>();
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
		if (m_BeingCarried == null)
			return;

        m_BeingCarried.changeState(Throwable.states.nope);
        m_BeingCarried.transform.parent = null;

        m_BeingCarried = null;

        m_Movement.IsCarryingOBJ = false;
    }

    void pickUp()
    {
        if (m_Detector.ThrowablesInRange.Count == 0)
            return;

        if (!InputManager.getThrowDown(i_Player))
            return;

        if (m_Detector.ThrowablesInRange.Count == 1)
        {
			if(m_Detector.ThrowablesInRange[0].isThrowable)
			{
            	onPickUp(m_Detector.ThrowablesInRange[0]);   
			}
            return;
        }

        for(int i =0; i < m_Detector.ThrowablesInRange.Count; i++)
        {
            Princess temp = m_Detector.ThrowablesInRange[i] as Princess;
            if(temp != null)
            {
				if(m_Detector.ThrowablesInRange[i].isThrowable)
				{
                	onPickUp(temp);
				}
                return;
            }
        }

		for(int i =0; i < m_Detector.ThrowablesInRange.Count; i++)
		{
			if(m_Detector.ThrowablesInRange[i].isThrowable)
			{
       	 		onPickUp(m_Detector.ThrowablesInRange[i]);
				return;
			}
		}
    }

    void onPickUp(Throwable pickedUp)
    {
        m_BeingCarried = pickedUp;
        m_BeingCarried.changeState(Throwable.states.carry);
        m_BeingCarried.transform.parent = transform;
        m_BeingCarried.BeingCarriedBy = this;

        m_BeingCarried.transform.position = i_MountPoint.transform.position;

        m_Movement.IsCarryingOBJ = true;
    }

    void onThrow()
    {
        m_BeingCarried.Velocity = InputManager.getLeftStick(i_Player).normalized * THROW_POWER;
        m_BeingCarried.changeState(Throwable.states.airborn);
        m_BeingCarried.transform.parent = null;

        IgnoreCollision(m_BeingCarried);
        StartCoroutine(tempIgnoreCollision(m_BeingCarried));

        m_Detector.removeThrowable(m_BeingCarried);

        m_BeingCarried.BeingCarriedBy = null;

        m_BeingCarried = null;

        m_Movement.IsCarryingOBJ = false;
    }

    void IgnoreCollision(Throwable thrown)
    {
        Collider[] collidersMine = GetComponents<Collider>();
        Collider[] collidersThiers = thrown.gameObject.GetComponents<Collider>();

        for(int i = 0; i < collidersMine.Length;i++)
        {
            for (int c = 0; c < collidersMine.Length; c++)
            {
				if(collidersMine[i].enabled == true && collidersThiers[c].enabled == true)
                	Physics.IgnoreCollision(collidersMine[i], collidersThiers[c], true);
            }
        }
    }

    IEnumerator tempIgnoreCollision(Throwable thrown)
    {
        Collider[] collidersMine = GetComponents<Collider>();
        Collider[] collidersThiers = thrown.gameObject.GetComponents<Collider>();

        yield return new WaitForSeconds(IGNORE_COLLISION_PERIOD);

        for (int i = 0; i < collidersMine.Length; i++)
        {
            for (int c = 0; c < collidersMine.Length; c++)
            {
				if(collidersMine[i].enabled == true && collidersThiers[c].enabled == true)
                	Physics.IgnoreCollision(collidersMine[i], collidersThiers[c], false);
            }
        }
    }
}
