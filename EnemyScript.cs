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
    public int enemy_AI;
    [SerializeField]
    GameObject Player;



    // Start is called before the first frame update
    void Start()
    {
        ragdoll_rigid = GetComponentsInChildren<Rigidbody>();
        rigid_colliders = GetComponentsInChildren<CapsuleCollider>();
        OnDisableRagdoll();
        Player = GameObject.FindGameObjectWithTag("Player");
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (enemy_health <= 0) 
        {
            OnEnableRagdoll();
            Destroy(gameObject, 10);
        }

        

    }

    public void EnemyAI(int AI, bool player_is_aiming, bool player_is_shooting)
    {


        float distance = Vector3.Distance(gameObject.transform.position, Player.transform.position);
        gameObject.transform.LookAt(new Vector3(Player.transform.position.x,gameObject.transform.position.y,Player.transform.position.z));
        enemy_AI = AI;
        if (player_is_aiming )
        {
            
            float horizontal = UnityEngine.Random.Range(-0.9f, 1f);
            switch (AI)
            { 
                case 1:
                    animator.SetFloat("InputHorizontal", horizontal );
                    gameObject.transform.Translate(new Vector3(horizontal,0,0) * 20f * Time.deltaTime);

                    break;     
            }
        }
        else 
        {
            AI = 0;
        }
        
        
    }

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
        }
        
        //
    }


    IEnumerator disable_col() 
    {
        yield return new WaitForSeconds(.2f);
        EnemyMainController.enabled = false;
        foreach (CapsuleCollider col in rigid_colliders)
        {
            col.enabled = true;

        }
        
    }
}
