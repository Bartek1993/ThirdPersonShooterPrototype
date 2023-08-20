using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="weapon_data", menuName ="weapon")]
public class Weapons_SO: ScriptableObject
{
    
    public string weaponName;
    public string weaponType;
    public int weaponId;
    public int weaponIndex;
    public int weaponDamage;
    public float weaponSpeed;
    public Texture croshair;
    public AudioClip weapon_shot_sound;
    public GameObject weapon_muzzle;



}
