using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{

    public Animator animator;
    public Rigidbody[] ragdoll_rigid;
    public CapsuleCollider[] rigid_colliders;
    public CharacterController EnemyMainController;
    public int enemy_health;
    float magnitude;
    float horizontal, vertical;
    [SerializeField]
    float distance;
    [SerializeField]
    Material[] material;
    [SerializeField]
    GameObject Player;
    [Header("Player Properties")]
    public int random_behaviour_generator;
    public bool player_is_shooting, player_is_aiming;
    


    // Start is called before the first frame update
    void Start()
    {
        animator.speed = UnityEngine.Random.Range(0.55f, 0.65f);
        ragdoll_rigid = GetComponentsInChildren<Rigidbody>();
        rigid_colliders = GetComponentsInChildren<CapsuleCollider>();
        OnDisableRagdoll();
        Player = GameObject.FindGameObjectWithTag("Player");
        foreach (Material mat in material)
        {
            mat.color = new Color(1, 1, 1, 1);
        }



    }

    // Update is called once per frame
    void Update()
    {
        RandomBehaviourGenerator();
        Distance();
        GetCurrentPlayerProperties();
        EnemyHealthStatus();
        EnemyAnimatorProperties();
        EnemyAI(random_behaviour_generator, player_is_aiming, player_is_shooting);

    }

    private void RandomBehaviourGenerator()
    {
        random_behaviour_generator = UnityEngine.Random.Range(0,100);
    }

    public void GetCurrentPlayerProperties()
    {
        
        player_is_aiming = Player.GetComponent<Fox_Controller>().aim;
        player_is_shooting = Player.GetComponentInChildren<Fox_Controller>().shoot;
 
    }

    private void EnemyAnimatorProperties()
    {
        animator.SetFloat("InputHorizontal", horizontal);
        animator.SetFloat("InputVertical", vertical);
        animator.SetFloat("InputMagnitude", magnitude);
    }

    private void EnemyHealthStatus()
    {
        if(enemy_health > 0) 
        {
            gameObject.transform.LookAt(new Vector3(Player.transform.position.x, gameObject.transform.position.y, Player.transform.position.z));
        }
        else
        {
            OnEnableRagdoll();
            Destroy(gameObject, 30);
            gameObject.transform.LookAt(null);
            
        }
    }

    private void Distance()
    {
        distance = Vector3.Distance(gameObject.transform.position, Player.transform.position);
        if (distance < 3f)
        {
            magnitude = .0f;
        }

        if (distance > 6f && distance < 8f)
        {
            magnitude -= .0003f;
        }

        if (distance > 8f && distance < 10f)
        {
            magnitude += .00006f;
        }

        if (distance > 10f)
        {
            magnitude = 1f;
        }
    }

    /// <summary>
    /// Call this method when player is aiming
    /// </summary>
    /// <param name="AI"></param>
    /// <param name="player_is_aiming"></param>
    /// <param name="player_is_shooting"></param>
    public void EnemyAI(int AI, bool player_is_aiming, bool player_is_shooting)
    {
       

        if (player_is_aiming )
        {
            ////
            if (AI < 50)
            {
                magnitude = 0f;
                Debug.Log("Enemy is shooting");
            }
            else 
            {
                Debug.Log("Enemy is hiding");
            }
        }
        else 
        {
            
        }
        
        
    }

    /// <summary>
    /// Call this method when enemy is shot
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(int damage) 
    {
        enemy_health -= damage;

    }
    private void OnDisableRagdoll()
    {

        foreach (Rigidbody rb in ragdoll_rigid)
        {
            rb.gameObject.transform.tag = "enemy";
            EnemyMainController.enabled = true;
            animator.enabled = true;
            rb.isKinematic = true;
        }

        foreach (CapsuleCollider col in rigid_colliders) 
        {
            col.gameObject.transform.tag = "enemy";
            col.enabled = false;
        }
    }
    private void OnEnableRagdoll()
    {
        foreach (Rigidbody rb in ragdoll_rigid)
        {
            EnemyMainController.enabled = true;
            animator.enabled = false;
            rb.isKinematic = false;
            rb.mass = 0.01f;
        }

        foreach (CapsuleCollider col in rigid_colliders)
        {

            StartCoroutine("disable_col");
        }

    }
    public void onExplosion() 
    {
        foreach (Rigidbody rb in ragdoll_rigid)
        {
            rb.AddExplosionForce(30, new Vector3(100, 1000, 100), 20);
            foreach (Material mat in material)
            {
                mat.color = Color.black;
               
            }
        }
        
        //
    }


    IEnumerator disable_col() 
    {
        yield return new WaitForSeconds(.1f);
        EnemyMainController.enabled = false;
        foreach (CapsuleCollider col in rigid_colliders)
        {
            col.enabled = true;

        }
        
        

    }
}
