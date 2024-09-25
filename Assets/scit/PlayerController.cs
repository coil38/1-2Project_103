using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //�÷��̾��� ������ �ӵ��� �����ϴ� ����
    [Header("Player Movement")]
    public float moveSpeed = 5.0f;          //�̵� �ӵ�
    public float jumpForce = 5.0f;          //���� ��

    //���� ������
    private bool isGrounded;                //�÷��̾ ���� �ִ��� ����
    private Rigidbody rb;                   //�÷��̾��� Rigidbody

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();             //RigidBody ������Ʈ�� �����´�.
    }

    // Update is called once per frame
    void Update()
    {
        HanbleJump();
        HandleMovement();
    }

    //�÷��̾� ������ ó���ϴ� �Լ�
    void HanbleJump()
    {
        //���� ��ư�� ������ ���� ���� ��
        if(Input.GetKeyDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);         //�������� ���� ���� ����
            isGrounded = false;                                             //���߿� �ִ� ���·� ��ȯ
        }
    }

    //�÷��̾��� �̵��� ó���ϴ� �Լ�
    void HandleMovement()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");         //�¿� �Է� (-1,1)
        float moveVerical = Input.GetAxis("Verical");               //�յ� �Է� (1,-1)

        //ĳ���� �������� �̵�
        Vector3 movement = transform.right * moveHorizontal + transform.forward * moveVerical;
        rb.MovePosition(rb.position +  movement * moveSpeed * Time.deltaTime);      //���� ��� �̵�
    }

    //�÷��̾ ���� ��� �ִ��� ����
    private void OnCollisionStay(Collision collision)
    {
        isGrounded = true;                      //�浹 ���̸� �÷��̾�� ���� �ִ�.   
    }
}
