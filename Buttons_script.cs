using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttons_script : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    AudioSource source;
    [SerializeField]
    AudioClip [] button_clips;
    [SerializeField]
    GameObject user_signin_registration_panel;

    public void onButtonClick() 
    {
        source.PlayOneShot(button_clips[1]);
        user_signin_registration_panel.SetActive(true);
    }
    public void onButtonHover() 
    {
        source.PlayOneShot(button_clips[0]);
    }

    public void DisableWindow() 
    {
        user_signin_registration_panel.SetActive(false);
    }
    


}
