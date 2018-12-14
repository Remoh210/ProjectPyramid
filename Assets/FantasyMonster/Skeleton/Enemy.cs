using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{

    public Transform player;
    [Range(1.0f, 100.0f)]
    public float Health = 200.0f;
    static Animator animator;
    private Vector3 StartPosition;
    private Quaternion StartRotation;
    bool IsEnemyDead = false;
    bool updatedScore = false;

    void Awake()
    {
        animator = this.gameObject.GetComponent<Animator>();
        //
        StartPosition = this.transform.position;
        StartRotation = this.transform.rotation;
    }

    // Use this for initialization
    void Start()
    {

    }
    //RESPAWN
    void RespawnEnemy()
    {
        IsEnemyDead = false;
        this.transform.position = StartPosition;
        animator.SetBool("isEnemyDead", IsEnemyDead);
        Health = 100.0f;
        updatedScore = false;
    }

    //ENEMY DAMAGE COLLIDER
    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "enemyHit" && Health > 0)
        {
            Health -= 25.0f;
            animator.Play("Damage");
            AudioSource[] fireAudio = col.gameObject.GetComponents<AudioSource>();
            fireAudio[1].Play();
            this.gameObject.GetComponent<AudioSource>().Play();
        }
    }


    public void ResetValues()
    {
        IsEnemyDead = false;
        updatedScore = false;
        animator = this.gameObject.GetComponent<Animator>();
        animator.SetBool("isEnemyDead", IsEnemyDead);
        Health = 100.0f;

        //
        StartPosition = this.transform.position;
        StartRotation = this.transform.rotation;
    }

    //ENEMY DEATH 

    // Update is called once per frame
    void Update()
    {
        //Check Health and die if = 0
        if(Health <= 0)
        {
            if(!updatedScore)
            {
                GameObject.Find("Score").GetComponent<ScoreManager>().IncreaseScore(30);
                updatedScore = true;
                IsEnemyDead = true;
                animator.Play("Death");
                Health = -10.0f;
                Destroy(gameObject, 1.0f);
            }

            //Temporary Respawn enemy if R was pressed
            //if (Input.GetKeyDown(KeyCode.R))
            //{
            //    RespawnEnemy();
            //}
        }



        //Moving and Attaking
        if (!IsEnemyDead)
        {
            Vector3 direction = player.position - this.transform.position;
            float angle = Vector3.Angle(direction, this.transform.forward);
            if (Vector3.Distance(player.position, this.transform.position) < 10 && angle < 40)
            {

                direction.y = 0;

                this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
                                            Quaternion.LookRotation(direction), 0.1f);

                animator.SetBool("isIdle", false);
                if (direction.magnitude > 5 && !animator.GetCurrentAnimatorStateInfo(0).IsName("Damage"))
                {
                    this.transform.Translate(0, 0, 0.03f);
                    animator.SetBool("isWalking", true);
                    animator.SetBool("isAttacking", false);
                }
                else if (player.GetComponent<Health_Damage_collisions>().IsPlayerDead)
                {
                    //this.transform.position = StartPosition;
                    Invoke("RespawnEnemy", 2f);
                    animator.SetBool("isIdle", true);
                    animator.SetBool("isWalking", false);
                    animator.SetBool("isAttacking", false);
                }
                else
                {
                    animator.SetBool("isAttacking", true);
                    animator.SetBool("isWalking", false);
                }

            }
            else
            {
                animator.SetBool("isIdle", true);
                animator.SetBool("isWalking", false);
                animator.SetBool("isAttacking", false);
            }
        }
    }
}
