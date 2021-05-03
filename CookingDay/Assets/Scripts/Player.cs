using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float TotalHealth = 100f;
    public float CurrentHealth;
    public float Speed;
    public float RotSpeed;
    private float Rotation; 
    public float Gravity;

    Vector3 MoveDirection;

    CharacterController controller;
    Animator anim;

    bool IsReady;
   

    List<Transform> EnemiesList = new List<Transform>();
    public float ColliderRadius;

    public float EnemyDamage = 25f;

    public bool IsAlive;

    public Image healthBar;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();

        CurrentHealth = TotalHealth;
        IsAlive = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        GetMouseInput();
        
    }

    void Move()
    {
        if(controller.isGrounded)
        {
            if(Input.GetKey(KeyCode.W))
            {
                if(!anim.GetBool("attacking"))
                {
                    anim.SetBool("walking", true);
                    anim.SetInteger("transition", 1);
                    MoveDirection = Vector3.forward * Speed;
                    MoveDirection = transform.TransformDirection(MoveDirection);
                }else
                {
                    anim.SetBool("walking", false);
                    MoveDirection = Vector3.zero;
                   // StartCoroutine(Attack(1));
                }
            }
            if(Input.GetKeyUp(KeyCode.W))
            {
                anim.SetBool("walking", false);
                anim.SetInteger("transition", 0);
                MoveDirection = Vector3.zero;
            }
        }

        Rotation += Input.GetAxis("Horizontal") * RotSpeed * Time.deltaTime;
        transform.eulerAngles = new Vector3(0, Rotation, 0);

        MoveDirection.y -= Gravity * Time.deltaTime;
        controller.Move(MoveDirection * Time.deltaTime);

    }

    void GetMouseInput()
    {
        if(controller.isGrounded)
        {
            if(Input.GetMouseButtonDown(0))
            {
                if(anim.GetBool("walking"))
                {
                    anim.SetBool("walking", false);
                    anim.SetInteger("transition", 0);
                }

                if(!anim.GetBool("walking"))
                {
                    //make the attack
                    StartCoroutine("Attack");

                }
            }
        }
    }

    IEnumerator Attack()
    {
        if(!IsReady && !anim.GetBool("hitting"))
        {
            IsReady = true;
            anim.SetBool("attacking", true);
            anim.SetInteger("transition", 2);
            
            

            yield return new WaitForSeconds(0.5f);

            GetEnemiesRange();

            foreach(Transform enemies in EnemiesList)
            {
                    //execute damage on the enemy
                    Enemy enemy = enemies.GetComponent<Enemy>();

                    if(enemy !=null)
                    {
                        enemy.GetHit(EnemyDamage);
                    }
            }

            yield return new WaitForSeconds(0.8f);


            anim.SetInteger("transition", 0);
            anim.SetBool("attacking", false);
            IsReady = false;
        }
    }

    void GetEnemiesRange()
    {
        EnemiesList.Clear();
        foreach(Collider c in Physics.OverlapSphere((transform.position + transform.forward * ColliderRadius), ColliderRadius))
        {
            if(c.gameObject.CompareTag("Enemy"))
            {
                EnemiesList.Add(c.transform);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + transform.forward,  ColliderRadius);
    }

    public void GetHit(float Damage)
  {

     CurrentHealth -= Damage;

     healthBar.fillAmount = CurrentHealth / TotalHealth;

     if(CurrentHealth > 0)
     {
        
        //being hit
        StopCoroutine("Attack");
        anim.SetInteger("transition", 3);
        anim.SetBool("hitting", true);
        StartCoroutine(RecoveryFromHit());
        FindObjectOfType<AudioManager>().Play("swordEnemy");
       
     }
     else
     {
         //dieng
         anim.SetInteger("transition", 4);
         IsAlive = false;
         
       
     }
     if(IsAlive == false)
     {
         GameController.instance.ShowGameOver();
         Debug.Log("Im dead");
         
     }


  }

  IEnumerator RecoveryFromHit()
  {
    yield return new WaitForSeconds(1.1f);
    anim.SetInteger("transition", 0);
    anim.SetBool("hitting", false);
    IsReady = false;
    anim.SetBool("attacking", false);
  }


}
