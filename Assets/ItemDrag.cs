﻿using UnityEngine;

public class ItemDrag : MonoBehaviour
{
    // Физические слои, для указания какие объекты мы можем переносить (настраиваются в редакторе Unity)
    [SerializeField] private LayerMask layerMask;

    // Расстояние с которого мы можем брать объекты
    [SerializeField] private float pickDistance = 3;

    // Флаг нужны для указания есть ли в данный момент перетаскиваемый объект
    private bool isDrag;

    // Переменная для сохранения объекта который мы перетаскиваем
    private Transform dragTransform;

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
                }
            }
        }

        // Если мы перетаскиваем предмет
        if (isDrag)
        {
            /*
             * Создание новой позиции перетаскиваемого объекта
             * transform.position - Текущая позиция игрока
             * (transform.forward * pickDistance) - Направление перед игроком на дистанции pickDistance
             * newPos - Общий результат, точка прямо перед игроком на указанной дистанции
             */
            Vector3 newPos = transform.position + (transform.forward * pickDistance);
            // Присваиваем новой позицию объекту, который мы перетаскивали
            dragTransform.position = newPos;
        }
    }
}
