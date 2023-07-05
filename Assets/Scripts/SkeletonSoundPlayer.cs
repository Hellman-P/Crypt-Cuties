using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonSoundPlayer : MonoBehaviour
{
    public AudioSource attackSound;
    public AudioSource damageSound;
    public void PlayAttackSound()
    {
        attackSound.Play();
    }

    public void PlayDamageSound()
    {
        damageSound.Play();
    }
}
