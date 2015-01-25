using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class Throwable : MonoBehaviour 
{
	public enum states
	{
		airborn,
		carry,
		nope
	};
    public states State
    {
        get { return m_State; }
    }


	protected CharacterController m_Controller;

	protected states m_State = states.nope;


    protected Vector2 m_Velocity = Vector2.zero;
    public Vector2 Velocity
    {
        get { return m_Velocity; }
        set { m_Velocity = value; }
    }

    public float i_FallSpeed = 1.0f;


    const string DEFAULT_LAYER = "Throwable";
    const string AIRBORNE_LAYER = "Airborne";

	public float m_Weight = 1.0f;

	protected Vector3 m_OriginalPosition;

    Thrower m_Thrower = null;

    const float VELOCITY_LOSS = 0.75f;

	protected AudioSource m_Audio;

    public bool isThrowable
    {
        get 
        {
            if (m_State == states.airborn || m_State == states.nope)
                return true;

            return false;
        }
    }

    protected Thrower m_BeingCarriedBy = null;
    public virtual Thrower BeingCarriedBy
    {
        get { return BeingCarriedBy; }
        set { m_BeingCarriedBy = value; }
    }

	// Use this for initialization
	protected virtual void Start () 
	{
		m_Controller = gameObject.GetComponent<CharacterController> ();

        gameObject.layer = LayerMask.NameToLayer(DEFAULT_LAYER);

        m_Thrower = gameObject.GetComponent<Thrower>();

		m_OriginalPosition = transform.position;

		m_Audio = GetComponent<AudioSource>();
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
        m_Controller.Move(m_Velocity);
    }

    protected virtual void carry()
    {

    }

    protected virtual void nope()
    {
        updateVelocity();
        m_Controller.Move(m_Velocity);
    }

    protected virtual void updateVelocity()
    {
        m_Velocity.y -= i_FallSpeed * Time.deltaTime;
    }

	public virtual void changeState(states state)
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
        gameObject.layer = LayerMask.NameToLayer(DEFAULT_LAYER);
    }

    protected virtual void onExitCarry()
    {
        m_Controller.enabled = true;
    }

    protected virtual void onExitNope()
    {
		if (m_Thrower != null)
        {
            m_Thrower.drop();
        }
    }

    protected virtual void onAirborn()
    {
        gameObject.layer = LayerMask.NameToLayer(AIRBORNE_LAYER);
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

        Throwable temp = hit.gameObject.GetComponent<Throwable>();

        if (temp != null)
        {
            CharacterMovement tempy = temp as CharacterMovement;

            if (tempy == null)
            {
                temp.changeState(states.airborn);
                temp.Velocity = m_Velocity * VELOCITY_LOSS;
            }
            else
            {
                tempy.changeState(states.airborn);


                tempy.Velocity = new Vector2(m_Velocity.x, Mathf.Abs(m_Velocity.y) + 0.5f) * VELOCITY_LOSS;
            }
        }
            

        changeState(states.nope);
    }

	public virtual void Reset ()
	{
		transform.position = m_OriginalPosition;
		m_Velocity = Vector2.zero;
	}
}
