using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class AccountRegistrationScript : MonoBehaviour
{

    [SerializeField]
    InputField email_input, password_input, username_input;
    [SerializeField]
    GameObject main_panel, login_panel, success_panel;
    [SerializeField]
    Animator cube, player;

    // Start is called before the first frame update
    public void OnTryLogin()
    {
        string email = email_input.text;
        string password = password_input.text;

        

        LoginWithEmailAddressRequest req = new LoginWithEmailAddressRequest
        {
            Email = email,
            Password = password
        };

        PlayFabClientAPI.LoginWithEmailAddress(req,
        res =>
        {
            StartCoroutine("Load");
        },
        err =>
        {
            Debug.Log("Error: " + err.ErrorMessage);
            
        });
    }


    public void OnTryRegisterNewAccount()
    {

        string email = email_input.text;
        string password = password_input.text;
        string username = username_input.text;

        RegisterPlayFabUserRequest req = new RegisterPlayFabUserRequest
        {
            Email = email,
            Password = password,
            Username = username,
            RequireBothUsernameAndEmail = true
        };

        PlayFabClientAPI.RegisterPlayFabUser(req,
        res =>
        {
            StartCoroutine("Load");
        },
        err =>
        {
          
            Debug.Log("Error: " + err.ErrorMessage);
        });
    }

    IEnumerator Load() 
    {
        yield return new WaitForSeconds(1);
        player.SetFloat("z", 1);
        login_panel.SetActive(false);
        yield return new WaitForSeconds(1);
        success_panel.SetActive(true);
        yield return new WaitForSeconds(2);
        main_panel.SetActive(false);
        cube.SetTrigger("move");
        yield return new WaitForSeconds(2);
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
    }


}
