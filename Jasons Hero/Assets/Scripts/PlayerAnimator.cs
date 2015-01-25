using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Throwable))]
public class PlayerAnimator : MonoBehaviour 
{
    enum animations
    {
        idle,
        running,
        throwing,
        carrying,
        caryingwithMove,
        carried
    };

    public AnimationClip[] clips;

    animations m_state = animations.idle;


    Animation m_animationthingy;


    Throwable velocityGiver;
    Thrower throwthing;
    Vector3 defaultScale;
	// Use this for initialization
    void Start()
    {
        velocityGiver = (Throwable)gameObject.GetComponent(typeof(Throwable));
        defaultScale = transform.localScale;
        throwthing = (Thrower)gameObject.GetComponent(typeof(Thrower));


        m_animationthingy = GetComponentInChildren<Animation>();
    }
	

	// Update is called once per frame
	void Update () 
    {
        flipper();

        
        if(m_state == animations.throwing)
        {
            if(m_animationthingy.isPlaying == false)
            {
                m_state = animations.idle;
            }
        }
        else if(velocityGiver.State == Throwable.states.nope)
        {
            if(throwthing.m_BeingCarried!= null)
            {
                if (Mathf.Abs(velocityGiver.Velocity.x) > 0.0f)
                {
                    m_state = animations.caryingwithMove;
                }
                else
                {
                    m_state = animations.carrying;
                }
            }
            else if(Mathf.Abs(velocityGiver.Velocity.x) > 0.0f)
            {
                m_state = animations.running;
            }
            else
            {
                m_state = animations.idle;
            }
        }
        else if(velocityGiver.State == Throwable.states.carry)
        {
            m_state = animations.carried;
        }

        m_animationthingy.CrossFade(clips[(int)m_state].name, 0.1f);
        Debug.Log(clips[(int)m_state].name);
	}

    public void throwthething()
    {
        m_state = animations.throwing;

        m_animationthingy.CrossFade(clips[(int)m_state].name, 0.1f);
        Debug.Log(clips[(int)m_state].name);
    }

    void flipper()
    {
        if (velocityGiver.Velocity.x > 0.0f)
        {
            transform.localScale = new Vector3(Mathf.Abs(defaultScale.x), defaultScale.y, defaultScale.z);
        }
        else if (velocityGiver.Velocity.x < 0.0f)
        {
            transform.localScale = new Vector3(-Mathf.Abs(defaultScale.x), defaultScale.y, defaultScale.z);
        }
    }
}
