using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSounds : MonoBehaviour
{
    public AudioSource myFx;
    public AudioClip clickFx;
    // Start is called before the first frame update
    public void ClickSound()
    {
        myFx.PlayOneShot(clickFx);
    }
}
