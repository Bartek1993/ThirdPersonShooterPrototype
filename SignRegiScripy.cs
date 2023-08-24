using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignRegiScripy : MonoBehaviour
{

    [SerializeField]
    AudioSource source;
    [SerializeField]
    AudioClip[] clip;
    // Start is called before the first frame update
    void Start()
    {
        source.PlayOneShot(clip[0]);
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
