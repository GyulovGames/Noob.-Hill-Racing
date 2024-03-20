using UnityEngine;
using UnityEngine.Events;
using YG;

public class NoobHead : MonoBehaviour
{
    [SerializeField] private AudioSource crackPlayer;
    [SerializeField] private HingeJoint2D hingeJoint2D;
    [Space(10)]
    public UnityEvent headHit;

    private bool isBroken = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            if (!isBroken)
            {
                isBroken = true;
                headHit.Invoke();
                if (YandexGame.savesData.Sounds_sdk)
                {
                    crackPlayer.Play();
                }
                JointAngleLimits2D limith = hingeJoint2D.limits;
                limith.max = 80;
                limith.min = -80;
                hingeJoint2D.limits = limith;
            }         
        }
    }
}