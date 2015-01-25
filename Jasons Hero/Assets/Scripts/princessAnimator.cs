using UnityEngine;
using System.Collections;

public class princessAnimator : MonoBehaviour
{
    enum animations
    {
        running,
        carried,
        idle
    };

    public AnimationClip[] clips;

    animations m_state = animations.idle;


    Animation m_animationthingy;


	Princess velocityGiver;
    Vector3 defaultScale;
    // Use this for initialization
    void Start()
    {
		velocityGiver = (Princess)gameObject.GetComponent(typeof(Princess));
        defaultScale = transform.localScale;

        m_animationthingy = GetComponentInChildren<Animation>();
    }


    // Update is called once per frame
    void Update()
    {
        flipper();
        
        switch(velocityGiver.State)
        {
            case Throwable.states.carry:
                m_state = animations.carried;
                break;
            case Throwable.states.airborn:
                m_state = animations.carried;
                break;
            case Throwable.states.nope:
				if(Mathf.Abs(transform.position.x) > 0.5f)
                {
                    m_state = animations.running;
                }
                else
                {
                    m_state = animations.idle;
                }
                break;
            default:
                break;
        }
        m_animationthingy.CrossFade(clips[(int)m_state].name, 0.1f);
    }

    void flipper()
    {
		if(m_state != animations.running)
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
		else
		{
			if (transform.position.x < -0.5f)
			{
				transform.localScale = new Vector3(Mathf.Abs(defaultScale.x), defaultScale.y, defaultScale.z);
			}
			else if (transform.position.x > 0.5f)
			{
				transform.localScale = new Vector3(-Mathf.Abs(defaultScale.x), defaultScale.y, defaultScale.z);
			}
		}
	}
}
