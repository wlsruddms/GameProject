using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class Player : MonoBehaviour
{
    // 초기값 10, 1000, 10, 0.1
    public float moveSpeed;
    public float rotateSpeed;
    public float jumpPower;
    public float jetPower;

    private Rigidbody rb;
    private int jumpcount = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
    //
        if(Input.GetKey(KeyCode.LeftShift)){
            print("FlightMode ON");
            fly();
        }
        else{
            print("FlightMode OFF");
            walk();
        }

        // 좌우 움직임
        float mouseMoveX = Input.GetAxis("Mouse X");
        transform.Rotate(0, mouseMoveX * rotateSpeed * Time.deltaTime, 0);
    }


    void fly(){
        // 부스터 기능
        if(Input.GetKey(KeyCode.W)){
            rb.AddRelativeForce(Vector3.forward * jetPower, ForceMode.Impulse);
        }
        if(Input.GetKey(KeyCode.S)){
            rb.AddRelativeForce(Vector3.back * jetPower, ForceMode.Impulse);
        }
        if(Input.GetKey(KeyCode.A)){
            rb.AddRelativeForce(Vector3.left * jetPower, ForceMode.Impulse);
        }
        if(Input.GetKey(KeyCode.D)){
            rb.AddRelativeForce(Vector3.right * jetPower, ForceMode.Impulse);
        }
        if(Input.GetKey(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jetPower, ForceMode.Impulse);
        }
    }

    void walk(){
        // 걷기
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 dir = new Vector3(h, 0, v);
        dir.Normalize(); // 대각선 이동 속도 : x : z,  1 : 1 : 1 로 정규화 
        dir = transform.TransformDirection(dir); // 월드 좌표가 아닌 로컬 좌표로 움직이게 함, 바라보는 방향으로
        rb.MovePosition(rb.position + (dir * moveSpeed * Time.deltaTime)); 
        // Time.deltaTime 프레임 정규화 
        
        // 점프 두번 가능
        if(Input.GetKeyDown(KeyCode.Space) && jumpcount < 2)
        {
            rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            jumpcount++;
        }
    }
    private void OnCollisionEnter(Collision collision){
        // 점프 초기화
        if(collision.gameObject.tag == "Ground"){
            jumpcount = 0;
        }
    }
}
