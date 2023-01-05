using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private KeyCode jumpKeyCode = KeyCode.Space;

    [SerializeField]
    private Transform cameraTransform;
    private Movement3D movement3D;
    private PlayerAnimator playerAnimator;
    private void Awake()
    {
        Cursor.visible = false;//마우스 커서를 보이지 않게 설정
        Cursor.lockState = CursorLockMode.Locked;//마우스 커서의 위치를 고정

        movement3D = GetComponent<Movement3D>();
        playerAnimator = GetComponent<PlayerAnimator>();
    }
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        //애니메이션 파라미터 설정
        playerAnimator.OnMovement(x, z);

        //이동속도 설정(앞으로 이동할 때에만 5, 나머지는 2)
        movement3D.MoveSpeed = z > 0 ? 5.0f : 2.0f;
        movement3D.MoveTo(cameraTransform.rotation * new Vector3(x, 0, z));

        //회전 설정(항상 앞만 보도록 캐릭터의 회전은 카메라와 같은 회전 값으로 설정)
        transform.rotation = Quaternion.Euler(0, cameraTransform.eulerAngles.y, 0);

        //점프 설정
        if (Input.GetKeyDown(jumpKeyCode))
        {
            playerAnimator.OnJump();//애니메이션 파라미터 설정
            movement3D.JumpTo();//점프 함수 호출
        }

        if (Input.GetMouseButton(0))
        {
            playerAnimator.OnKickAttack();
        }

        if (Input.GetMouseButton(1))
        {
            playerAnimator.OnWeaponAttack();
        }
    }
}
