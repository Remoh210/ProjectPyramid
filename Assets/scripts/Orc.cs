using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orc : MonoBehaviour {

    public Transform player;
    [Range(1.0f, 100.0f)]
    public float Health = 100.0f;
    static Animator animator;
    private Vector3 StartPosition;
    private Quaternion StartRotation;
    bool IsEnemyDead = false;
    bool canAttack = true;
    bool updatedScore = false;

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        //
        StartPosition = this.transform.position;
        StartRotation = this.transform.rotation;
    }
    //RESPAWN
    void RespawnEnemy()
    {
        IsEnemyDead = false;
        this.transform.position = StartPosition;
        animator.SetBool("isEnemyDead", IsEnemyDead);
        Health = 100.0f;
    }

    //ENEMY DAMAGE COLLIDER
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "enemyHit" && Health > 0)
        {
            TakeDamage(25.0f);
        }
    }

    void TakeDamage(float amount)
    {
        Health -= amount;
    }

    public void TakeRecoilDamage()
    {
        canAttack = false;
        TakeDamage(15.0f);
        animator.Play("Damage");
        StartCoroutine("MoveBack");
    }
    
    IEnumerator MoveBack()
    {
        float timer = 0.0f;
        while (timer < 0.5f)
        {
            this.transform.Translate(0, 0, -0.25f);
            timer += Time.deltaTime;
            canAttack = true;
            yield return null;
        }
    }

    //ENEMY DEATH 

    // Update is called once per frame
    void Update()
    {
        //Check Health and die if = 0
        if (Health <= 0)
        {
            IsEnemyDead = true;
            BoxCollider collider = GameObject.Find("Collider").GetComponent<BoxCollider>();
            collider.enabled = false;
            animator.Play("Death", -1, 0.8f);

            if (!updatedScore)
            {
                GameObject.Find("Score").GetComponent<ScoreManager>().IncreaseScore(15);
                updatedScore = true;
            }

            Health = -10.0f;
            Destroy(gameObject, 1.0f);

            // //Temporary Respawn enemy if R was pressed
            // if (Input.GetKeyDown(KeyCode.R))
            // {
            //     RespawnEnemy();
            // }

        }
        //Moving and Attaking
        else if (!IsEnemyDead)
        {
            Vector3 direction = player.position - this.transform.position;
            float angle = Vector3.Angle(direction, this.transform.forward);

            BoxCollider collider = GameObject.Find("Collider").GetComponent<BoxCollider>();
            if (Vector3.Distance(player.position, this.transform.position) < 50 && angle < 40)
            {

                direction.y = 0;

                this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
                                            Quaternion.LookRotation(direction), 0.1f);

                animator.SetBool("isIdle", false);
                if (direction.magnitude > 15 && !animator.GetCurrentAnimatorStateInfo(0).IsName("Damage"))
                {
                    if(canAttack)
                        this.transform.Translate(0, 0, 0.03f);
                    animator.SetBool("isWalking", true);
                    animator.SetBool("isAttacking", false);

                    if (collider.enabled == true)
                    {
                        collider.enabled = false;
                    }
                }
                else if (player.GetComponent<Health_Damage_collisions>().IsPlayerDead)
                {
                    //this.transform.position = StartPosition;
                    Invoke("RespawnEnemy", 2f);
                    animator.SetBool("isIdle", true);
                    animator.SetBool("isWalking", false);
                    animator.SetBool("isAttacking", false);

                    if (collider.enabled == true)
                    {
                        collider.enabled = false;
                    }
                }
                else
                {
                    if(canAttack)
                    {
                        collider.enabled = true;
                        animator.SetBool("isAttacking", true);
                        this.transform.Translate(0, 0, 0.09f);
                        animator.SetBool("isWalking", false);
                    }
                }

            }
            else
            {
                animator.SetBool("isIdle", true);
                animator.SetBool("isWalking", false);
                animator.SetBool("isAttacking", false);
                collider.enabled = false;
            }
        }
    }
}
