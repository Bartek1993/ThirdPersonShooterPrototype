using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Class: MonoBehaviour 
{

    public Weapons_SO Weapons_SO;

    public string weaponName;
    public string weaponType;
    public int weaponId;
    public int weaponDamage;
    public float weaponSpeed;
    public AudioClip weapon_shoot_sound;
    public AudioSource weapon_source;
    public Texture croshair;
    public Animator weapon_animator;
    public GameObject weapon_muzzle;
    public GameObject muzzle_position;

    public void Start()
    {
       
    }

    public void Update()
    {
        weaponName = Weapons_SO.weaponName;
        weaponType = Weapons_SO.weaponType;
        weaponId = Weapons_SO.weaponId;
        weaponDamage = Weapons_SO.weaponDamage;
        weaponSpeed = Weapons_SO.weaponSpeed;
        croshair = Weapons_SO.croshair;
        weapon_shoot_sound = Weapons_SO.weapon_shot_sound;
        weapon_muzzle = Weapons_SO.weapon_muzzle;

    }


   
}
