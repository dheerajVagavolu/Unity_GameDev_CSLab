using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    //public Rigidbody rgbd;
    public float jumpForce;
    public CharacterController cc;
    public float gravityScale;
    // Start is called before the first frame update

    private Vector3 moveDirection;

    public Animator anim;
    public Transform pivot;
    public float rotateSpeed;
    public GameObject playerModel;

    public ParticleSystem muzzle;

    public float knockBackForce;
    public float knockBackTime;
    public float knockBackCounter;

    public ParticleSystem enemyDestroy;

    public float powerC = 3f;
    public float powerL = 3f;

    public Image Power;

    public AudioSource foot;

    public float timefoot;
    public float footlength;

    public AudioSource punch;



    void Start()
    {
        //rgbd = GetComponent<Rigidbody>();
        cc = GetComponent<CharacterController>();

    }

    // Update is called once per frame
    void Update()
    {
        Power.fillAmount = powerC / powerL;

        //rgbd.velocity = new Vector3(Input.GetAxis("Horizontal") * moveSpeed, rgbd.velocity.y, Input.GetAxis("Vertical") * moveSpeed);

        if (Input.GetButtonDown("Fire3"))
        {
            //rgbd.velocity = new Vector3(rgbd.velocity.x, jumpForce, rgbd.velocity.z);
            FindObjectOfType<GameManager>().Die();
        }

        //moveDirection = new Vector3(Input.GetAxis("Horizontal") * moveSpeed, moveDirection.y, Input.GetAxis("Vertical") * moveSpeed);

        if(powerC > 0)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                powerC = powerC - 1;

                GameObject[] enemies = GameObject.FindGameObjectsWithTag("Aibot");

                foreach (GameObject target in enemies)
                {
                    float distance = Vector3.Distance(target.transform.position, transform.position);
                    if (distance < 8)
                    {
                        enemyDestroy.transform.position = target.transform.position;
                        enemyDestroy.Play();
                        Destroy(target);
                    }
                }
                muzzle.Play();
                anim.SetTrigger("Fire");
                punch.Play();

            }
        }




        if (knockBackCounter <= 0)
        {

            float yStore = moveDirection.y;
            moveDirection = (transform.forward) * Input.GetAxis("Vertical") + (transform.right) * Input.GetAxis("Horizontal");
            moveDirection = moveDirection.normalized * moveSpeed;
            moveDirection.y = yStore;

            /*if (Input.GetButtonDown("Fire3"))
            {
                moveSpeed = 2 * moveSpeed;
            }
            moveSpeed = moveSpeed / 2;*/




            if (cc.isGrounded)
            {
                moveDirection.y = 0f;
                if (Input.GetButtonDown("Jump"))
                {
                    moveDirection.y = jumpForce;
                }

            }

        }
        else
        {

            knockBackCounter -= Time.deltaTime;
        }

        moveDirection.y = moveDirection.y + (Physics.gravity.y * gravityScale);
        cc.Move(moveDirection * Time.deltaTime);



        //Direction control
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {

            transform.rotation = Quaternion.Euler(0f, pivot.rotation.eulerAngles.y, 0f);
            Quaternion newRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0f, moveDirection.z));
            playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation, newRotation, rotateSpeed * Time.deltaTime);

            if (timefoot <= 0)
            {
                if (cc.isGrounded)
                {
                    foot.Play();
                    timefoot = footlength;
                }
            }
            else
            {
                timefoot -= Time.deltaTime;
            }
        }
        else
        {

            timefoot = 0f;
        }




        anim.SetBool("isGrounded", cc.isGrounded);
        anim.SetFloat("Speed", (Mathf.Abs(Input.GetAxis("Vertical"))) + (Mathf.Abs(Input.GetAxis("Horizontal"))));



    }

    public void knockBack(Vector3 direction)
    {
        knockBackCounter = knockBackTime;

        //direction = new Vector3(1f, 1f, 1f);

        moveDirection = direction * knockBackForce;
        moveDirection.y = knockBackForce;


    }

    public void powerUp()
    {
        powerC = powerL;
    }

    public void Launch()
    {

        moveDirection.y = jumpForce*3;
        moveDirection.y = moveDirection.y + (Physics.gravity.y * gravityScale);
        cc.Move(moveDirection * Time.deltaTime);
    }



}
