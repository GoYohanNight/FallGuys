using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFSM : MonoBehaviour
{
    [SerializeField]
    private Transform characterBody;    // 캐릭터 이동에 사용할 컴포넌트
    [SerializeField]
    private Transform cameraArm;        // 카메라 회전에 사용할 컴포넌트
    [SerializeField]
    private Rigidbody rigidbody;        // 점프, 다이빙에 사용할 컴포넌트

    private Animator animator;          // 애니메이션 상태 전환에 사용할 컴포넌트

    public float moveSpeed = 0.0f;      // 캐릭터 이동 속도
    public float jumpPower = 0.0f;      // 캐릭터 점프 세기
    public float divePower = 0.0f;      // 캐릭터 다이빙 세기
    public float leviSpeed = 0.0f;      // 캐릭터 공중 이동 속도

    private Vector3 lookForward;        // 캐릭터가 바라보는 방향을 저장할 변수 (캐릭터의 로컬 X축)
    private Vector3 lookSide;           // 캐릭터의 로컬 Z축
    private Vector3 moveInput;          // 캐릭터가 이동할 방향을 저장할 변수
    private bool isMove = false;        // 방향키 입력 상태를 체크하는 bool 변수
    private bool isGrounded = true;     // 캐릭터 지면 접촉 상태를 체크하는 bool 변수

    private void Start()
    {
        // rigidbody = rigidbody.GetComponent<Rigidbody>();
        animator = characterBody.GetComponentInChildren<Animator>();
    }

    private enum State
    { // 캐릭터 머신의 상태 목록
        Idle,   // 대기
        Move,   // 달리기
        Jump,   // 점프
        Dive,   // 다이빙
        Dead    // 사망
    };

    // 캐릭터 머신의 기본 상태 = Idle
    State state = State.Idle;

    private void LookAround() // 마우스의 움직임에 따른 카메라 회전 함수
    {
        // 마우스의 이동값을 입력 받는 벡터값 MouseDelta 선언
        Vector2 mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        // 오브젝트 Camera Arm의 로테이션 값을 Euler 값으로 치환
        Vector3 camAngle = cameraArm.rotation.eulerAngles;

        // 카메라 Y축 회전 입력값 후처리
        float mediX = camAngle.x - mouseDelta.y; // 실제 마우스 Y축 이동을 mediX에 저장
        if (mediX < 180.0f) { // 마우스가 Y축 아래 방향으로 움직일 때
            mediX = Mathf.Clamp(mediX, -1.0f, 30.0f); // 가동범위는 30도까지
        } else { // 마우스가 Y축 위 방향으로 움직일 때
            mediX = Mathf.Clamp(mediX, 335.0f, 361.0f); // 가동범위는 -30도까지
        }

        // 입력 받은 마우스 이동값으로 캐릭터 카메라 회전
        cameraArm.rotation = Quaternion.Euler(mediX, camAngle.y + mouseDelta.x, camAngle.z);
    }

    private void Update()
    {
        Debug.Log(state);

        // 마우스의 움직임에 따른 카메라 회전 함수 실행
        LookAround();

        // 카메라가 바라보는 방향을 lookForward에 저장
        lookForward = new Vector3(cameraArm.forward.x, 0.0f, cameraArm.forward.z).normalized;
        lookSide = new Vector3(cameraArm.right.x, 0.0f, cameraArm.right.z).normalized;
        // 캐릭터가 바라보는 방향을 lookForward와 동기화
        characterBody.forward = lookForward;

        // 이동을 위한 키보드 입력값 moveInput에 저장
        moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0.0f, Input.GetAxisRaw("Vertical"));
        // 방향키 입력 상태를 체크
        isMove = moveInput.magnitude != 0;
        animator.SetBool("IsMove", isMove);
        // 캐릭터 지면 접촉 상태 체크
        animator.SetBool("IsGrounded", isGrounded);

        switch (state)
        {
            // 머신의 현재 상태 체크 후 지속적으로 상태별 함수 실행
            case State.Idle:
                Idle();
                break;
            case State.Move:
                Move();
                break;
            case State.Jump:
                Jump();
                break;
            case State.Dive:
                Dive();
                break;
            case State.Dead:
                Dead();
                break;
        }
    }


    // ------------------------------ Idle State ------------------------------
    private void Idle() // 머신이 Idle 상태일 때 실행될 함수
    {
        // 방향키 입력값이 있으면
        if (isMove)
        {
            // Idle -> Move
            this.state = State.Move;
        }

        // 스페이스바를 입력하면
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Idle -> Jump
            isGrounded = false;
            rigidbody.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            animator.SetTrigger("IsJump");
            this.state = State.Jump;
        }

        // 쉬프트키를 입력하거나 마우스를 우클릭하면
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.Mouse1))
        {
            // Idle -> Dive
            isGrounded = false;
            rigidbody.AddForce(lookForward * divePower, ForceMode.Impulse);
            rigidbody.AddForce(Vector3.up * divePower, ForceMode.Impulse);
            animator.SetTrigger("IsDive");
            this.state = State.Dive;
        }
    }


    // ------------------------------ Move State 정의 ------------------------------
    private void Move() // 머신이 Move 상태일 때 실행될 함수
    {
        // 캐릭터가 이동할 방향을 계산
        Vector3 moveDir = (lookForward * moveInput.z) + (lookSide * moveInput.x);
        transform.position += moveDir.normalized * moveSpeed * Time.deltaTime;


        // 방향키 입력값이 없으면
        if (!isMove)
        {
            // Move -> Idle
            this.state = State.Idle;
        }

        // 스페이스바를 입력하면
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Move -> Jump
            isGrounded = false;
            rigidbody.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            animator.SetTrigger("IsJump");
            this.state = State.Jump;
        }

        // 쉬프트키를 입력하거나 마우스를 우클릭하면
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.Mouse1))
        {
            // Move -> Dive
            isGrounded = false;
            rigidbody.AddForce(lookForward * divePower, ForceMode.Impulse);
            rigidbody.AddForce(Vector3.up * divePower, ForceMode.Impulse);
            animator.SetTrigger("IsDive");
            this.state = State.Dive;
        }
    }


    // ------------------------------ Jump State 정의 ------------------------------
    private void Jump() // 머신이 Jump 상태일 때 실행될 함수
    {
        // 캐릭터가 이동할 방향을 계산
        Vector3 moveDir = (lookForward * moveInput.z) + (lookSide * moveInput.x);
        transform.position += moveDir.normalized * leviSpeed * Time.deltaTime;

        // 쉬프트키를 입력하거나 마우스를 우클릭하면
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.Mouse1))
        {
            // Jump -> Dive
            isGrounded = false;
            rigidbody.AddForce(lookForward * divePower, ForceMode.Impulse);
            rigidbody.AddForce(Vector3.up * divePower, ForceMode.Impulse);
            animator.SetTrigger("IsDive");
            this.state = State.Dive;
        }

        // 지면에 닿을 때
        if (isGrounded)
        {
            if (!isMove)
            { // 방향키 입력값이 없으면
                // Jump -> Idle
                this.state = State.Idle;
            }
            else
            { // 방향키 입력값이 있으면
                // Jump -> Move
                this.state = State.Move;
            }
        }
    }


    // ------------------------------ Dive State 정의 ------------------------------
    private void Dive() // 머신이 Dive 상태일 때 실행될 함수
    {
        // 지면에 닿을 때
        if (isGrounded)
        {
            if (!isMove)
            { // 방향키 입력값이 없으면
                // Jump -> Idle
                this.state = State.Idle;
            }
            else
            { // 방향키 입력값이 있으면
                // Jump -> Move
                this.state = State.Move;
            }
        }
    }


    // ------------------------------ Dead State 정의 ------------------------------
    private void Dead() // 머신이 Dead 상태일 때 실행될 함수
    {

    }


    // 지면과의 접촉 상태 진입을 체크
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}