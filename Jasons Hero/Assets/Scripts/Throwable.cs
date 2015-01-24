using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class Throwable : MonoBehaviour 
{
	protected enum states
	{
		airborn,
		carry,
		nope
	};

	CharacterController m_Controller;

	states m_State = states.nope;


    Vector2 m_Velocity = Vector2.zero;
    public Vector2 Velocity
    {
        get { return m_Velocity; }
        set { m_Velocity = value; }
    }

    public float i_FallSpeed = 1.0f;


	// Use this for initialization
	protected virtual void Start () 
	{
		m_Controller = gameObject.GetComponent<CharacterController> ();
	}

	// Update is called once per frame
    protected virtual void Update() 
	{
		switch(m_State)
        {
            case states.airborn:
                airBorne();
                break;
            case states.carry:
                carry();
                break;
            case states.nope:
                nope();
                break;
        }
	}

    protected virtual void airBorne()
    {
        updateVelocity();
    }

    protected virtual void carry()
    {

    }

    protected virtual void nope()
    {
        updateVelocity();
    }

    protected virtual void updateVelocity()
    {
        m_Velocity.y -= i_FallSpeed * Time.deltaTime;
    }

	protected virtual void changeState(states state)
	{
		onStateChange ();

        switch (m_State)
        {
            case states.airborn:
                if(m_State != state)
                    onExitAirborn();
                break;
            case states.carry:
                if (m_State != state)
                    onExitCarry();
                break;
            case states.nope:
                if (m_State != state)
                    onExitNope();
                break;
        }

        switch (state)
        {
            case states.airborn:
                if (m_State != state)
                    onAirborn();
                break;
            case states.carry:
                if (m_State != state)
                    onCarry();
                break;
            case states.nope:
                if (m_State != state)
                    onNope();
                break;
        }

        m_State = state;
	}

	protected virtual void onStateChange()
	{
	}

    protected virtual void onExitAirborn()
    {
        m_Velocity = Vector2.zero;
    }

    protected virtual void onExitCarry()
    {
        m_Controller.enabled = true;
    }

    protected virtual void onExitNope()
    {
    }

    protected virtual void onAirborn()
    {        
    }

    protected virtual void onCarry()
    {
        m_Controller.enabled = false;
        m_Velocity = Vector2.zero;
    }

    protected virtual void onNope()
    {
    }

    protected virtual void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (m_State != states.airborn)
            return;

        changeState(states.nope);
    }
}
