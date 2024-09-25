using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //플레이어의 움직임 속도를 설정하는 변수
    [Header("Player Movement")]
    public float moveSpeed = 5.0f;          //이동 속도
    public float jumpForce = 5.0f;          //점프 힘

    //카메라 설정 변수
    [Header("Camera Settings")]
    public Camera firstPersonCamera;        //1인칭 카메라
    public Camera thirdPersonCamera;        //3인칭 카메라

    public float radius = 5.0f;             //3인칭 카메라와 플레이어 간의 거리
    public float minRadius = 1.0f;          //카메라 최소 거리
    public float maxRadius = 10.0f;         //카메라 최대거리

    public float yMinLimit = 30;            //카메라 수직 회전 최소각
    public float yMaxLimit = 90;            //카메라 수직 회전 최대각

    private float theta = 0.0f;                 //카메라의 수평 회전 각도
    private float phi = 0.0f;                   //카메라의 수직 회전 각도
    private float targetVerticalRoataion = 0;   //목표 수직 회전 각도
    private float verticalRoataionSpeed = 240f; //수직 회전 속도

    public float mouseSenesitivity = 2f;        //마우스 감도

    //내부 변수들
    private bool isFirstPerson = true;      //1인칭 모드 인지 여부
    private bool isGrounded;                //플레이어가 땅에 있는지 여부
    private Rigidbody rb;                   //플레이어의 Rigidbody

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();             //RigidBody 컴포넌트를 가져온다.

        Cursor.lockState = CursorLockMode.Locked;   //마우스 커서를 잠그고 숨긴다.
        SetupCameras();
        SetActiveCamera();
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        HandleRotation();
        HanbleJump();
    }

    //활성화할 카메라를 설정하는 함수
    void SetActiveCamera()
    {
        firstPersonCamera.gameObject.SetActive(isFirstPerson);  //1인칭 카메라 활성화 여부
        thirdPersonCamera.gameObject.SetActive(isFirstPerson);  //3인칭 카메라 활성화 여부
    }

    //카메라 초기 위치 및 회전을 설정하는 함수
    void SetupCameras()
    {
        firstPersonCamera.transform.localPosition = new Vector3(0f, 0.6f, 0f);          //1인칭 카메라 위치
        firstPersonCamera.transform.localRotation = Quaternion.identity;                //1인칭 카메라 회전 초기화
    }

    //플레이어 점프를 처리하는 함수
    void HanbleJump()
    {
        //점프 버튼을 누르고 땅에 있을 때
        if(Input.GetKeyDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);         //위쪽으로 힘을 가해 점프
            isGrounded = false;                                             //공중에 있는 상태로 전환
        }
    }

    //플레이어의 이동을 처리하는 함수
    void HandleMovement()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");         //좌우 입력 (-1,1)
        float moveVerical = Input.GetAxis("Verical");               //앞뒤 입력 (1,-1)

        if(!isFirstPerson)  //3인칭 모드 일 때, 카메라 방향으로 이동 처리
        {
            Vector3 cameraForward = thirdPersonCamera.transform.forward;        //카메라 앞 방향
            cameraForward.y = 0f;       //수직 방향 제거
            cameraForward.Normalize();      //방향 백터 정규화 (0~1) 사이의 값으로 만들어준다.

            Vector3 cameraRight = thirdPersonCamera.transform.right;        //카메라 오른쪽 방향
            cameraRight.y = 0f;       
            cameraRight.Normalize();

            //이동 백터 계산
            Vector3 movement = cameraForward * moveVerical + cameraRight * moveHorizontal;
            rb.MovePosition(rb.position + movement * moveSpeed * Time.deltaTime);   //물리 기반 이동
        }
        else
        {
            //캐릭터 기준으로 이동 (1인칭)
            Vector3 movement = transform.right * moveHorizontal + transform.forward * moveVerical;
            rb.MovePosition(rb.position + movement * moveSpeed * Time.deltaTime);   //물리 기반 이동
        }
        
        //캐릭터 기준으로 이동
        Vector3 movement = transform.right * moveHorizontal + transform.forward * moveVerical;
        rb.MovePosition(rb.position +  movement * moveSpeed * Time.deltaTime);      //물리 기반 이동
    }
    void HandleRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSenesitivity;        //마우스 좌우 입력
        float mouseY = Input.GetAxis("Mouse Y") * mouseSenesitivity;        //마우스 상하 입력

        //수평 회전 (theta 값)
        theta += mouseX;            //마우스 입력값 추가
        theta = Mathf.Repeat(theta, 360f);          //각도 값이 360을 엄지 않도록 조정

        //수직 회전 처리
        targetVerticalRoataion -= mouseY;
        targetVerticalRoataion = Mathf.Clamp(targetVerticalRoataion,yMaxLimit,yMinLimit);   //수직 회전 제한
        phi = Mathf.MoveTowards(phi, targetVerticalRoataion, verticalRoataionSpeed * Time.deltaTime);

        //플레이어 회전(캐릭터가 수평으로만 회전)
        transform.rotation = Quaternion.Euler(0.0f, theta, 0.0f);

        if(isFirstPerson)
        {
            firstPersonCamera.transform.localRotation = Quaternion.Euler(phi, object.0.0f, 0.0f);   //1인칭 카메라 수직 회전
        }
        else
        {
            //3인칭 카메라 구면 좌표계에서 위치 및 회전 계산
            float x = radius * Mathf.Sin(Mathf.Deg2Rad * phi) * Mathf.Cos(Mathf.Deg2Rad * theta);
            float y = radius * Mathf.Cos(Mathf.Deg2Rad * phi);
            float z = radius * Mathf.Sin(Mathf.Deg2Rad * phi) * Mathf.Sin(Mathf.Deg2Rad * theta);

            thirdPersonCamera.transform.position = transform.position + new Vector3(x, y, z);

        }

        firstPersonCamera.transform.localRotation = Quaternion.Euler(0.0f, phi, 0.0f);//1인칭 카메라 수직 회전

    }

    //플레이어가 땅에 닿아 있는지 감지
    private void OnCollisionStay(Collision collision)
    {
        isGrounded = true;                      //충돌 중이면 플레이어는 땅에 있다.   
    }
}
