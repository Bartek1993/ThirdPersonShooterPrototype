using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine.Editor;
using Cinemachine;
using UnityEditor.Animations.Rigging;
using UnityEngine.Animations.Rigging;
using Unity.VisualScripting;
using UnityEngine.UI;
using Unity.Mathematics;

public class Fox_Controller : MonoBehaviour
{
    [SerializeField]
    AudioSource m_AudioSource;
    [SerializeField]
    Rigidbody rigidBody;
    [SerializeField]
    float x, y, z, speed;
    Vector3 movement;
    [SerializeField]
    Animator animator;
    [SerializeField]
    bool aim, run, slash, shoot, aim_weapon, shoot_weapon;
    [SerializeField]
    CinemachineFreeLook ThirdPersonCamera;
    [SerializeField]
    [Header("Male weapons rigs")]
    Rig[] male_weapons;
    [SerializeField]
    [Header("Pistol weapons rigs")]
    Rig[] gun_weapons;
    [SerializeField]
    [Header("Automatic weapons rigs")]
    Rig[] automatic_weapons;
    [SerializeField]
    [Header("Body rigs")]
    Rig[] body_rigs;
    [SerializeField]
    int weapon_id;
    [SerializeField]
    GameObject[] weapon_models;
    [SerializeField]
    GameObject croshair_object;
    [SerializeField]
    Texture croshair;
    [SerializeField]
    RawImage croshair_image;
    [SerializeField]
    GameObject target;
    [Header("Current Weapon Properties")]
    [SerializeField]
    GameObject current_weapon;
    [SerializeField]
    string weapon_name,weapon_type;
    [SerializeField]
    int weapon_damage;
    [SerializeField]
    float weapon_speed;
    [SerializeField] 
    AudioClip weapon_shoot_sound;
    [SerializeField]
    Animator weapon_animator;
    private float nextFire;
    [SerializeField]
    GameObject muzzle, muzzle_position, smoke, impact, impact_barell;
    [SerializeField]
    float camera_x_shoot_offset, camera_y_shoot_offset, camera_z_shoot_offset;

    // Start is called before the first frame update
    void Start()
    {
        weapon_id = 1;
        current_weapon = weapon_models[weapon_id];
        rigidBody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        weapon_animator = current_weapon.GetComponentInChildren<Weapon_Class>().weapon_animator;
       




    }

  

    // Update is called once per frame
    void Update()
    {
        /*
         * set cursror properties  
         * */
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        //

        Movement();
        setAnimator();
        CinemachineCameraOptions();
        setRigsForWeapons();
        setWeaponID();
        setCH();
        setWeaponModel();
        GetCurrentWeaponProperties();
        Shooting();
    }

    private void Shooting()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f));
        if (aim && shoot && Time.time > nextFire) 
        {
            StartCoroutine("Shooting_Weapon");
            nextFire = Time.time + weapon_speed;
            if (Physics.Raycast(ray, out hit)) 
            {
                Debug.Log("Shot from: " + weapon_name);
                Debug.Log("You shot: " + hit.transform.name);
               

                if (hit.transform.tag == "barrel") 
                {
                    
                    env_object_script object_script = hit.transform.GetComponent<env_object_script>();
                    object_script.TakeDamage(weapon_damage);
                  
                }

                if (hit.transform.tag == "enemy")
                {

                    EnemyScript object_script = hit.transform.GetComponent<EnemyScript>();
                    object_script.TakeDamage(weapon_damage);
                  
                }


                if (hit.transform.tag == "barrel")
                {
                    Instantiate(impact_barell, hit.point, Quaternion.identity);
                }
                else 
                {
                    Instantiate(impact, hit.point, Quaternion.identity);
                }
               
                
                
                if (hit.rigidbody != null || hit.transform.GetComponentsInChildren<Rigidbody>() != null)
                {

                    Debug.Log("RIGID HITTT");
                    hit.rigidbody.AddForce(-hit.normal * 350f);
                }
            }
        }
    }

    public void GetCurrentWeaponProperties()
    {
        weapon_name = current_weapon.GetComponentInChildren<Weapon_Class>().name;
        weapon_type = current_weapon.GetComponentInChildren<Weapon_Class>().weaponType;
        weapon_damage = current_weapon.GetComponentInChildren<Weapon_Class>().weaponDamage;
        weapon_speed = current_weapon.GetComponentInChildren<Weapon_Class>().weaponSpeed;
        croshair = current_weapon.GetComponentInChildren<Weapon_Class>().croshair;
        weapon_shoot_sound = current_weapon.GetComponentInChildren<Weapon_Class>().weapon_shoot_sound;
        weapon_animator = current_weapon.GetComponentInChildren<Weapon_Class>().weapon_animator;
        muzzle = current_weapon.GetComponentInChildren<Weapon_Class>().weapon_muzzle;
        muzzle_position = current_weapon.GetComponentInChildren<Weapon_Class>().muzzle_position;
        
        
    
    }

    private void setWeaponModel()
    {
        switch(weapon_id) 
        {
            case 0:
                weapon_models[0].SetActive(true);
                weapon_models[1].SetActive(false);
                weapon_models[2].SetActive(false);
                break;
            case 1:
                weapon_models[0].SetActive(false);
                weapon_models[1].SetActive(true);
                weapon_models[2].SetActive(false);
                break;
            case 2:
                weapon_models[0].SetActive(false);
                weapon_models[1].SetActive(false);
                weapon_models[2].SetActive(true);
                
                break;
        }

        current_weapon = weapon_models[weapon_id];
    }

    private void setCH()
    {

        croshair_image.texture = croshair;

        if (aim)
        {
            croshair_object.SetActive(true);

        }
        else 
        {
            croshair_object.SetActive(false);
        }
    }

    private void setWeaponID()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) 
        {
            weapon_id += 1;

        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            weapon_id -= 1;

        }

        if (weapon_id > 2 || weapon_id <= 1) { weapon_id = 1; }
    }

    private void setRigsForWeapons()
    {
        if(weapon_id == 0) 
        {
          

            for (var w = 0; w < gun_weapons.Length; w++) 
            {
                gun_weapons[w].weight -= 0.05f;
            }
            for (var w = 0; w < automatic_weapons.Length; w++)
            {
                automatic_weapons[w].weight -= 0.05f;
            }
            ThirdPersonCamera.m_XAxis.m_MaxSpeed = 300f;

            if (aim || shoot)
            {
                male_weapons[0].weight += .05f;

            }
            else
            {
                male_weapons[0].weight -= .05f;
            }
 
        }

        if (weapon_id == 1)
        {
       

            for (var w = 0; w < male_weapons.Length; w++)
            {
                male_weapons[w].weight -= 0.05f;
            }
            for (var w = 0; w < automatic_weapons.Length; w++)
            {
                automatic_weapons[w].weight -= 0.05f;
            }
            ThirdPersonCamera.m_XAxis.m_MaxSpeed = 300f;

            if (aim || shoot)
            {
                gun_weapons[0].weight += .05f;

            }
            else
            {
                gun_weapons[0].weight -= .05f;
            }

        }

        if (weapon_id == 2)
        {


            for (var w = 0; w < male_weapons.Length; w++)
            {
                male_weapons[w].weight -= 0.05f;
            }
            for (var w = 0; w < gun_weapons.Length; w++)
            {
                gun_weapons[w].weight -= 0.05f;
            }
            ThirdPersonCamera.m_XAxis.m_MaxSpeed = 270f;
            if (aim || shoot)
            {
                automatic_weapons[0].weight += .05f;

            }
            else
            {
                automatic_weapons[0].weight -= .05f;
            }

            
            
        }



        }

    private void CinemachineCameraOptions()
    {
        if (aim )
        {
            ThirdPersonCamera.m_BindingMode = CinemachineTransposer.BindingMode.WorldSpace;
            ThirdPersonCamera.m_RecenterToTargetHeading.m_enabled = false;
            ThirdPersonCamera.m_RecenterToTargetHeading.m_RecenteringTime = 0f;
            ThirdPersonCamera.m_RecenterToTargetHeading.m_WaitTime = 0f;
            ThirdPersonCamera.GetComponent<CinemachineCameraOffset>().m_Offset.z = 2.1f + camera_x_shoot_offset;
            ThirdPersonCamera.GetComponent<CinemachineCameraOffset>().m_Offset.x = .45f + camera_y_shoot_offset;
            ThirdPersonCamera.GetComponent<CinemachineCameraOffset>().m_Offset.y = 0f + camera_z_shoot_offset;
            ThirdPersonCamera.m_XAxis.m_MinValue = -180f;
            ThirdPersonCamera.m_XAxis.m_MaxValue = 180f;
            ThirdPersonCamera.m_XAxis.m_Wrap = true;
            for (var b = 0; b < body_rigs.Length; b++)
            {
                body_rigs[b].weight = 1f;
            }
           

        }
        else 
        {
            ThirdPersonCamera.m_BindingMode = CinemachineTransposer.BindingMode.WorldSpace;
            ThirdPersonCamera.m_RecenterToTargetHeading.m_enabled = true;
            ThirdPersonCamera.m_RecenterToTargetHeading.m_RecenteringTime = 10;
            ThirdPersonCamera.m_RecenterToTargetHeading.m_WaitTime = 10;
            ThirdPersonCamera.GetComponent<CinemachineCameraOffset>().m_Offset.z = 0f;
            ThirdPersonCamera.GetComponent<CinemachineCameraOffset>().m_Offset.x = 0f;
            ThirdPersonCamera.GetComponent<CinemachineCameraOffset>().m_Offset.y = 0f;
            ThirdPersonCamera.m_XAxis.m_MinValue = -180f;
            ThirdPersonCamera.m_XAxis.m_MaxValue = 180f;
            ThirdPersonCamera.m_XAxis.m_Wrap = true;
            for (var b = 0; b < body_rigs.Length; b++)
            {
                body_rigs[b].weight = 0.25f;
            }
          
        }
    }

    private void setAnimator()
    {
        animator.speed = 0.72f;
        animator.SetFloat("x", x);
        animator.SetBool("run", run);
        animator.SetFloat("z", z);
        animator.SetBool("aim", aim);
        weapon_animator.SetBool("aim", aim_weapon);
        weapon_animator.SetBool("shoot", shoot_weapon);
        animator.SetBool("shoot", shoot);



        //aim = true;

        animator.SetBool("slash", slash);
        animator.SetInteger("weapon_id", weapon_id);

    }

    void Movement()
    {
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");
        movement = new Vector3 (x, y, z);
        aim = Input.GetKey(KeyCode.Mouse1);
        shoot = Input.GetKey(KeyCode.Mouse0);
        aim_weapon = Input.GetKey(KeyCode.Mouse1);
        shoot_weapon = Input.GetKey(KeyCode.Mouse0);


        run = Input.GetKey(KeyCode.LeftShift);
        if (run && movement.magnitude >0.1f)
        {
            aim = false;
            shoot = false;
            speed = 4f;
           
        }
        else 
        {
            
            speed = 1.5f;
        }
    }

    private void FixedUpdate()
    {
        RigidbodyMovement();
    }

    private void RigidbodyMovement()
    {

        // SETTING UP THE CAMERA, I AM USING CINEMACHINE PACKAGE
        // WILL WORK WITH ANY CAMERA TAGGED AS MAIN
        Vector3 camForward = Camera.main.transform.forward;
        Vector3 camRight = Camera.main.transform.right;
        // RESTRICTION ON CAMERA AXIS, PLAYER WONT ROTATING ON THE Y AXIS
        camForward.y = 0;
        camRight.y = 0;
        // SETTING UP THE MOVEMENT
        // MOVEMENT IS VECTOR3
        movement = camForward * z + camRight * x;
        // IF HORIZONTAL OR VERTICAL INPUT IS GREATER THAN 0.01F THEN PLAYER WILL MOVE AND ROTATE
        if (movement.magnitude > 0.01)
        {
            //MOVE RIGIDBODY
            
            rigidBody.velocity = new Vector3(movement.x * speed, rigidBody.velocity.y, movement.z * speed);
            gameObject.transform.LookAt(null);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), .6f);
        }

        if(aim || shoot)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), .3f);
            //transform.Rotate(new Vector3(0,Input.GetAxis("Mouse X"),0) * 200 * Time.fixedDeltaTime);
            gameObject.transform.LookAt(new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z));
        }


    }




    /*
     * Corutines
     * 
     */

    IEnumerator Shooting_Weapon() 
    {
        NoiseSettings settings;
        
        GameObject muzzle_inst = Instantiate(muzzle,muzzle_position.transform.position,Quaternion.identity);
        muzzle_inst.gameObject.transform.parent = null;
        camera_x_shoot_offset = 0.00f;
        camera_y_shoot_offset = 0.00f;
        camera_z_shoot_offset = 0.01f;
        yield return new WaitForSeconds(.12f);
        Destroy(muzzle_inst);
        camera_x_shoot_offset = 0.0f;
        camera_y_shoot_offset = 0.0f;
        camera_z_shoot_offset = 0.0f;
        m_AudioSource.PlayOneShot(weapon_shoot_sound);
        Debug.Log("You shot " + weapon_name);
        yield return null;

    }
    
}
