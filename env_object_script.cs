using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class env_object_script : MonoBehaviour
{
    [SerializeField]
    int hp;
    [SerializeField]
    GameObject broken_model, explosion, explosionRadius;
    [SerializeField]
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
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
            Destroy(explosionrad, .4f);
            Destroy(explosion_object, 5);
            Destroy(gameObject,.1f);
            
        };
    }



    public void onExplosion() 
    {
        rb.AddForce(new Vector3(100,100,100));
    }
}
