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


    Throwable velocityGiver;
    Vector3 defaultScale;
    // Use this for initialization
    void Start()
    {
        velocityGiver = (Throwable)gameObject.GetComponent(typeof(Throwable));
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
                if(Mathf.Abs(velocityGiver.Velocity.x) > 0.0f)
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
