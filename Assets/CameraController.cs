using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform target;//ī�޶� ���� ���
    [SerializeField]
    private float minDistance = 3f;//ī�޶�� target�� �ּ� �Ÿ�
    [SerializeField]
    private float maxDistance = 30f;//ī�޶�� target�� �ִ� �Ÿ�
    [SerializeField]
    private float wheelSpeed = 500f;//���콺 �� ��ũ�� �ӵ�
    [SerializeField]
    private float xMoveSpeed = 500f;//ī�޶� x�� ȸ�� �ӵ�
    [SerializeField]
    private float yMoveSpeed = 250f;//ī�޶� y�� ȸ�� �ӵ�

    private float yMinLimit = 5;//ī�޶� x�� ȸ�� ���� �ּ� ��
    private float yMaxLimit = 80;//ī�޶� x�� ȸ�� ���� �ִ� ��
    private float x, y;//���콺 �̵� ���� ��
    private float distance;//ī�޶�� target�� �Ÿ�

    private void Awake()
    {
        distance = Vector3.Distance(transform.position, target.position);
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;
    }

    // Update is called once per frame
    void Update()
    {
        //target�� �������� ������, ����X
        if (target == null) return;

        //���콺 x, y�� ������ ���� ����
        x += Input.GetAxis("Mouse X") * xMoveSpeed * Time.deltaTime;
        y -= Input.GetAxis("Mouse Y") * yMoveSpeed * Time.deltaTime;

        //������Ʈ�� ��/�Ʒ�(x��) �Ѱ� ���� ����
        y = ClampAngle(y, yMinLimit, yMaxLimit);
        //ī�޶� ȸ�� ���� ����
        transform.rotation = Quaternion.Euler(y, x, 0);

        //���콺 �� ��ũ���� �̿��� target�� ī�޶��� �Ÿ� �� ����
        distance = Input.GetAxis("Mouse ScrollWheel") * wheelSpeed * Time.deltaTime;
        //�Ÿ��� �ּ�, �ִ�Ÿ��� ������ �� ���� ����� �ʵ����Ѵ�.
        distance = Mathf.Clamp(distance, minDistance, maxDistance);
    }
    private void LateUpdate()
    {
        //target�� �������� ������ ����X
        if (target == null) return;
        //ī�޶��� ��ġ ���� ����
        //target ��ġ �������� distance��ŭ ������ ä�� �i�ư���.
        transform.position = transform.rotation * new Vector3(0, 0, -distance) + target.position;
    }
    private float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360) angle += 360;
        if (angle > 360) angle -= 360;

        return Mathf.Clamp(angle, min, max);
    }
}
