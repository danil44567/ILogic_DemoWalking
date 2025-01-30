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

        // Блокируем курсор в центре
        Cursor.lockState = CursorLockMode.Locked;

        // Получаем Rigidbody с нашего объекта
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Получем горионтальное движение мыши
        float mouseX = Input.GetAxis("Mouse X");

        // Получаем текущий поворот в трёхмерных координатах
        Vector3 rotation = transform.rotation.eulerAngles;

        // Поворачивам ось Y 
        rotation.y += mouseX * rotationSpeed;

        // Присваем повёрнутый вектор преобразуя его в четырёхмерный
        transform.rotation = Quaternion.Euler(rotation);


        // Получем вертикальное движение мыши
        float mouseY = Input.GetAxis("Mouse Y");
        Vector3 cameraRotation = playerCamera.rotation.eulerAngles;
        cameraRotation.x += mouseY * -rotationSpeed;
        playerCamera.rotation = Quaternion.Euler(cameraRotation);




        // Получаем горизонтальный и вертикальный ввод пользователя
        float vert = Input.GetAxis("Vertical"); // W или S
        float hor = Input.GetAxis("Horizontal"); // A или D

        // Получаем направление перед игроком
        Vector3 forward = transform.forward * vert;
        // Направление справа от игрока
        Vector3 right = transform.right * hor;

        // Для физического объекта присваиваем новые скорости
        rb.velocity = (forward + right) * moveSpeed;
    }
}
