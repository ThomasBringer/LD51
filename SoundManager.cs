using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager sm;

    public AudioSource click;
    public AudioSource explode;
    public AudioSource tap;
    public AudioSource hit;
    public AudioSource jump;

    void Awake(){
        sm = this;
    }
}
