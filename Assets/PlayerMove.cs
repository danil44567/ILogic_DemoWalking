using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 1.0f;
    [SerializeField] private float moveSpeed = 1.0f;

    [SerializeField] private Transform playerCamera;

    private Rigidbody rb;

    void Start()
    {

        // ��������� ������ � ������
        Cursor.lockState = CursorLockMode.Locked;

        // �������� Rigidbody � ������ �������
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // ������� ������������� �������� ����
        float mouseX = Input.GetAxis("Mouse X");

        // �������� ������� ������� � ��������� �����������
        Vector3 rotation = transform.rotation.eulerAngles;

        // ����������� ��� Y 
        rotation.y += mouseX * rotationSpeed;

        // �������� ��������� ������ ���������� ��� � ������������
        transform.rotation = Quaternion.Euler(rotation);


        // ������� ������������ �������� ����
        float mouseY = Input.GetAxis("Mouse Y");
        Vector3 cameraRotation = playerCamera.rotation.eulerAngles;
        cameraRotation.x += mouseY * -rotationSpeed;
        playerCamera.rotation = Quaternion.Euler(cameraRotation);




        // �������� �������������� � ������������ ���� ������������
        float vert = Input.GetAxis("Vertical"); // W ��� S
        float hor = Input.GetAxis("Horizontal"); // A ��� D

        // �������� ����������� ����� �������
        Vector3 forward = transform.forward * vert;
        // ����������� ������ �� ������
        Vector3 right = transform.right * hor;

        // ��� ����������� ������� ����������� ����� ��������
        rb.velocity = (forward + right) * moveSpeed;
    }
}
