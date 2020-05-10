using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doomost : MonoBehaviour
{
    // Start is called before the first frame update

    public static AudioSource DoomOST;
    public AudioClip DoomClip;

    void Awake()
    {
        AudioSource[] audios = GetComponents<AudioSource>();
        DoomOST = audios[0];
        DoomOST.clip = DoomClip;
        DoomOST.loop = true;
        DoomOST.Play();
        DoomOST.volume = 0.5f;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
