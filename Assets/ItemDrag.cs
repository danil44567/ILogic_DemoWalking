using UnityEngine;

public class ItemDrag : MonoBehaviour
{
    // Физические слои, для указания какие объекты мы можем переносить (настраиваются в редакторе Unity)
    [SerializeField] private LayerMask layerMask;

    // Расстояние с которого мы можем брать объекты
    [SerializeField] private float pickDistance = 3;

    private float currentPick;

    // Флаг нужны для указания есть ли в данный момент перетаскиваемый объект
    private bool isDrag;

    // Переменная для сохранения объекта который мы перетаскиваем
    private Transform dragTransform;

    // Ссылка на PlayerStats у нашего игрока (нужно указать ссылку в инспекторе)
    [SerializeField] private PlayerStats playerStats;

    [SerializeField] private Inventory inventory;

    void Update()
    {
        // Если был клик пользователя в текущем кадре
        if (Input.GetMouseButtonDown(0))
        {
            // Если уже что-то переносим
            if (isDrag)
            {
                // Перестаём нести придмет
                isDrag = false;
            }
            else
            {
                /*
                 * Создаётся новый Луч(Ray)
                 * из позиции игрока (transform.position)
                 * в том направлении куда игрок смотрит (transform.forward)
                 */
                Ray ray = new Ray(transform.position, transform.forward);

                /*
                 * Используя Физичесекую проверку лучем проверям попали ли мы в объект
                 * ray - луч, который мы выше создали
                 * out RaycastHit hit - получаем результат попадания, если мы попали
                 * pickDistance - расстояние на которое мы бросили луч
                 * layerMask - физические слои в которые мы можем попасть (настраиваются в редакторе Unity)
                 */
                if (Physics.Raycast(ray, out RaycastHit hit, pickDistance, layerMask))
                {
                    Debug.Log(hit.transform.position);

                    // Устанавливаем флаг, что мы начали переность объект
                    isDrag = true;
                    // Сохраняем объект в который мы попали лучом, чтобы в дальнейшем расчитать его перетаскивание
                    dragTransform = hit.transform;

                    currentPick = hit.distance;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = new Ray(transform.position, transform.forward);

            if (Physics.Raycast(ray, out RaycastHit hit, pickDistance))
            {
                if (hit.transform.TryGetComponent(out DoorButton btn))
                {
                    btn.Activate();
                }
            }

        }

        // Если мы перетаскиваем предмет
        if (isDrag)
        {
            float wheel = Input.GetAxis("Mouse ScrollWheel");
            currentPick += wheel;
            currentPick = Mathf.Clamp(currentPick, 1, pickDistance);

            /*
             * Создание новой позиции перетаскиваемого объекта
             * transform.position - Текущая позиция игрока
             * (transform.forward * pickDistance) - Направление перед игроком на дистанции pickDistance
             * newPos - Общий результат, точка прямо перед игроком на указанной дистанции
             */
            Vector3 newPos = transform.position + (transform.forward * currentPick);
            // Присваиваем новой позицию объекту, который мы перетаскивали
            dragTransform.position = newPos;

            // Если во время перетаскивания нажата ПКМ (см. код выше и условие в котором мы находимся)
            if (Input.GetMouseButtonDown(1))
            {
                // Если перетаскиваемый объект имеет компонент Food (является съедобным)
                // Получаем в перемнной food компонент "еды" текущего объека
                if (dragTransform.TryGetComponent(out Food food))
                {
                    // Перестаём нести придмет
                    isDrag = false;

                    // Обращаемся в скрипт PlayerStats и восстанавливаем игроку голод,
                    // Вызвав метод AddHungry и передём в скобках аргумент с значением сытости нашей еды
                    playerStats.AddHugry(food.satiety);

                    // Уничтожаем перетаскиваемый объект (Ну мы же его съели)
                    Destroy(dragTransform.gameObject);
                }
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                inventory.AddItem(dragTransform.gameObject);

                // Перестаём нести придмет
                isDrag = false;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                GameObject item = inventory.GetItem();
                if (item != null)
                {
                    dragTransform = item.transform;
                    isDrag = true;
                }
            }
        }
    }
}
