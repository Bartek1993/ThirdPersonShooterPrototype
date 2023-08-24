using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class env_object_script : MonoBehaviour
{
    [SerializeField]
    int hp;
    [SerializeField]
    GameObject broken_model, explosion, explosionRadius;
    // Start is called before the first frame update
    void Start()
    {
        hp = 10;
    }

    // Update is called once per frame
    void Update()
    {
        
    }




    public void TakeDamage(int damage) 
    {
        hp -= damage;
        if (hp < 0) 
        {

            GameObject brokenModel = Instantiate(broken_model,gameObject.transform.position,Quaternion.identity);
            GameObject explosion_object = Instantiate(explosion,gameObject.transform.position,Quaternion.identity);
            explosion.transform.parent = null;
            GameObject explosionrad = Instantiate(explosionRadius, gameObject.transform.position, Quaternion.identity);
            Destroy(explosionrad, 0.05f);
            Destroy(explosion_object, 20);
            Destroy(gameObject,.1f);
            
        };
    }
}
