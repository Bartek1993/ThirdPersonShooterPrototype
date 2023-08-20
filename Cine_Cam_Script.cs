using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Cine_Cam_Script : MonoBehaviour
{
    [SerializeField]
    CinemachineFreeLook this_camera;
    [SerializeField]
    GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        this_camera = GetComponent<CinemachineFreeLook>();
    }

    // Update is called once per frame
    void Update()
    {
        this_camera.Follow = Player.transform;
        this_camera.LookAt = Player.transform;
    }
}
