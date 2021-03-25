using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    public static AudioClip Attack, moving, pickUp, Win, BackGround,PowerUp;
    static AudioSource audioSrc;
    // Start is called before the first frame update
    void Start()
    {
        Attack = Resources.Load<AudioClip>("Attack");
        moving = Resources.Load<AudioClip>("moving");
        pickUp = Resources.Load<AudioClip>("pickUp");
        Win = Resources.Load<AudioClip>("Win");
        BackGround = Resources.Load<AudioClip>("BackGround");
        PowerUp = Resources.Load<AudioClip>("PowerUp");

        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "Attack":
                audioSrc.PlayOneShot(Attack);
                break;

            case "moving":
                audioSrc.PlayOneShot(moving);
                break;

            case "pickUp":
                audioSrc.PlayOneShot(pickUp);
                break;

            case "Win":
                audioSrc.PlayOneShot(Win);
                break;

            case "BackGround":
                audioSrc.PlayOneShot(BackGround);
                break;
            case "PowerUp":
                audioSrc.PlayOneShot(PowerUp);
                break;

        }
    }
}
