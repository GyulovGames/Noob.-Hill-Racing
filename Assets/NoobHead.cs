using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NoobHead : MonoBehaviour
{
    [SerializeField] private HingeJoint2D hingeJoint2D;

    public UnityEvent headHit;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            headHit.Invoke();
            JointAngleLimits2D limith = hingeJoint2D.limits;
            limith.max = 80;
            limith.min = -80;
            hingeJoint2D.limits = limith;
            
        }
    }
}
