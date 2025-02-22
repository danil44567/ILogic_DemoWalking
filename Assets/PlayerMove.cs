using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMove : MonoBehaviour
{
    // Скорость вращения игрока
    [SerializeField] private float rotationSpeed = 1.0f;
    // Скорость движения игрока
    [SerializeField] private float moveSpeed = 1.0f;
    // Ссылка на Transform камеры внутри игрока
    [SerializeField] private Transform playerCamera;

    // Расстояние для проверки земли. Стандартное значение 1
    [SerializeField] private float groundDistance = 1.0f;

    // НЕОБХОДИМО УКАЗАТЬ В ИНСПЕКТОРЕ.
    // Переменная необходимая для указания какие физические слои являются землёй
    // Например, чтобы луч для проверки игнорировал игрока
    [SerializeField] private LayerMask groundMask;

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

        // Расчёт новой скороси движения игрока и сохранение его в перменную
        Vector3 moveVelocity = (forward + right) * moveSpeed;


        /* 
         * Для физического объекта присваиваем новые скорости
         * Обратите внимание, мы в своих расчётах не высчиываем Y, из-за чего он у нас 0
         * Следовательно мы можем перекрыть расчёты физики по оси Y и по ошибке присвоить туда 0
         * Из-за этого может сломаться возможность игрока падать/прыгать
         * Чтобы это исправить создадим новый вектор, в котором присвоим наши X и Z
         * Для Y мы передаём старое число (rb.velocity.y)
         */
        rb.velocity = new Vector3(moveVelocity.x, rb.velocity.y, moveVelocity.z);

        // Если в текущем кадре был нажат пробел
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Создаём новый луч от
            // - Из позиции игрока (transform.position)
            // - В направлении вертикально вниз по абсолютным значениям (Vector3.down)
            Ray ray = new Ray(transform.position, Vector3.down);

            /*
             * Бросаем созданный луч, чтобы проверить есть ли под игроком земля
             * Это нужно для того, чтобы игрок не мог прыгать находясь в воздухе
             * 
             * Аргументы:
             * ray - Созданный нами луч в направлении земли
             * out RaycastHit hit - Возвращаемая точка попадания луча с типом RaycastHit и названием перменной hit (в данном примере она не используется)
             * groundDistance - Переменная нашего скрипта, указывает на какое расстояние мы бросаем лучи для проверки земли
             * groundMask - Переменная нашего скрипта, указывает на то какие физические слои являются землёй
             * 
             * В groundMask важно указать слои таким образом, чтобы бросаемый луч игнорировал игрока
             * Из-за того, что мы бросаем луч из игрока, то он всегда будет в него попадать и условие всегда будет верным
             * Нам нужно чтобы условие было верным только в случае если луч действительно попал в землю
            */
            if (Physics.Raycast(ray, out RaycastHit hit, groundDistance, groundMask))
            {
                // Прменяемый силу в 200 единиц направленную вверх (Vector3.up)
                rb.AddForce(Vector3.up * 200);
            }
        }
    }
}
