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



    // Start is called before the first frame update
    void Start()
    {
        ragdoll_rigid = GetComponentsInChildren<Rigidbody>();
        rigid_colliders = GetComponentsInChildren<CapsuleCollider>();
        OnDisableRagdoll();
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (enemy_health <= 0) 
        {
            OnEnableRagdoll();
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
            EnemyMainController.enabled = false;
            animator.enabled = false;
            rb.isKinematic = false;
            rb.mass = 0.01f;
        }

        foreach (CapsuleCollider col in rigid_colliders)
        {
            col.enabled = true;
        }

    }
}
