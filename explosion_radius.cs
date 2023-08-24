using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosion_radius : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void OnTriggerEnter(Collider other)
    {
        if(other.transform.GetComponent<Rigidbody>() != null) 
        {
            other.attachedRigidbody.AddExplosionForce(500, new Vector3(200, 200, 200), 20);
        }

        if (other.transform.tag == "barrel") 
        {
            env_object_script object_barrel = other.transform.GetComponent<env_object_script>();
            object_barrel.TakeDamage(2);
            object_barrel.onExplosion();

        }
        if (other.transform.tag == "enemy")
        {
            EnemyScript object_enemy = other.transform.GetComponent<EnemyScript>();
            object_enemy.TakeDamage(20);
            object_enemy.onExplosion();

        }


    }


    public void OnTriggerStay(Collider other)
    {
        if (other.transform.GetComponent<Rigidbody>() != null)
        {
            other.attachedRigidbody.AddExplosionForce(500, new Vector3(200, 1000, 200), 20);
        }

        if (other.transform.tag == "barrel")
        {
            env_object_script object_barrel = other.transform.GetComponent<env_object_script>();
            object_barrel.onExplosion();

        }
        if (other.transform.tag == "enemy")
        {
            EnemyScript object_enemy = other.transform.GetComponent<EnemyScript>();
            object_enemy.onExplosion();

        }
    }
}
