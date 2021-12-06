using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBehavior : MonoBehaviour
{
    [SerializeField]GameManage GameManage;
    [SerializeField]Slider UISlider;
    [SerializeField]Text UIText;
    public static PlayerBehavior PlayerBehaviorInstance;
    const int PlayerMaxHp = 10;
    int PlayerCurrentHp = 10;
    bool OnGround = false;
    bool IsJumping = false;
    bool IsBurning = false;
    float LastJumpingTime = 0;
    float MoveSpeed = 2;
    Vector3 JumpForce = new Vector3 (0,7,0);
    Rigidbody rb;
    float RayDistance;
    LayerMask cubeLayer;
    Animator animator;
    void Start()
    {
        PlayerBehaviorInstance = this;
        rb = GetComponent<Rigidbody>();
        cubeLayer = LayerMask.GetMask("Cube");
        animator = GetComponent<Animator>();
        RayDistance = Mathf.Sqrt(0.0052f);
    }
    void Update()
    {
        float Horizontal = Input.GetAxis("Horizontal");

        transform.position += new Vector3(Horizontal*Time.deltaTime*MoveSpeed,0,0);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x,-3.5f,4.3f),transform.position.y,0);
        if(Horizontal>0)
        {
            transform.rotation = Quaternion.Euler(0,90,0);
            animator.SetBool("Running",true);
        }
        else if(Horizontal<0)
        {
            transform.rotation = Quaternion.Euler(0,-90,0);
            animator.SetBool("Running",true);
        }
        else
        {
            animator.SetBool("Running",false);
        }
        
        OnGroundCheck();
        
        if(OnGround && Input.GetKeyDown(KeyCode.Space)&&(Time.time - LastJumpingTime>0.1f))
        {
            IsJumping =true;
        }
        if(IsJumping)
        {
            Jump(JumpForce);
            animator.SetTrigger("Jump");
        }


        UIText.text = "You are now at B" + ((int)(Time.time/5)).ToString() + " Floor";
    }
    // void OnDrawGizmosSelected()
    // {
    //     Gizmos.color = Color.blue;
    //     Gizmos.DrawLine(transform.position,transform.position-(transform.up*0.04f));
    //     Gizmos.DrawLine(transform.position,transform.position+new Vector3(0.06f,-0.04f,0));
    //     Gizmos.DrawLine(transform.position,transform.position+new Vector3(-0.06f,-0.04f,0));
    // }
    
    private void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject.CompareTag("GeneralFloor")&&
        (Physics.Raycast(transform.position,-transform.up,0.04f,cubeLayer)||
         Physics.Raycast(transform.position,new Vector3(0.06f,-0.04f,0),RayDistance,cubeLayer)||
         Physics.Raycast(transform.position,new Vector3(-0.06f,-0.04f,0),RayDistance,cubeLayer)))
        {
            GeneralFloorBehavior.CubeFunctionsArray
            [other.gameObject.GetComponent<GeneralFloorBehavior>().GetCurrnetCubeType()]
            (this,other.gameObject);
        }
    }
    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.CompareTag("DeadZone"))
        {
            GameManage.CallDeadMenu();
        }
    }
    public void Jump(Vector3 Jumpforce)
    {
        
        LastJumpingTime = Time.time;
        rb.velocity += Jumpforce;
        IsJumping = false;
    }
    public void ChangeHp(int HpChangeValue)
    {
        PlayerCurrentHp += HpChangeValue;
        if(PlayerCurrentHp>10)
        {
            PlayerCurrentHp = 10;
        }

        UISlider.value = PlayerCurrentHp;
        if(PlayerCurrentHp<1)
        {
            GameManage.CallDeadMenu();
        }
    }
    void OnGroundCheck()
    {
        if(Physics.Raycast(transform.position,-transform.up,0.04f,cubeLayer)||
        Physics.Raycast(transform.position,new Vector3(0.06f,-0.04f,0),RayDistance,cubeLayer)||
        Physics.Raycast(transform.position,new Vector3(-0.06f,-0.04f,0),RayDistance,cubeLayer))
        {
            OnGround = true;
            animator.SetBool("OnGround",true);
        }
        else
        {
            MoveSpeed = 2f;
            OnGround = false;
            animator.SetBool("OnGround",false);
        }        
    }
    public void Burning()
    {
        StartCoroutine(Burn());
    }
    IEnumerator Burn()
    {
        if(IsBurning){yield break;}
        IsBurning = true;
        ChangeHp(-1);
        yield return new WaitForSeconds(1);
        ChangeHp(-1);
        yield return new WaitForSeconds(1);
        ChangeHp(-1);
        yield return new WaitForSeconds(1);
        ChangeHp(-1);
        IsBurning = false;
    }
    public void Skidding()
    {
        MoveSpeed = 4f;
    }
}
