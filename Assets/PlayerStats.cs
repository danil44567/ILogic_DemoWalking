using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    // Указание на изображение в котором отображаем голод
    [SerializeField] private Image hungryImage;

    // Текущий голод игрока
    private float hungry;

    // Начальный голод игрока (значение нужно для того, чтобы знать какой голод у нас был начальный)
    private float startHungry = 100;

    void Start()
    {
        // Во время запуса игры в текущий голод игрока устанавливаем начальное значение
        hungry = startHungry;
    }

    void Update()
    {
        // На каждом кадре отнимаем значения (по сути таймер обратного отсчёта)
        hungry -= Time.deltaTime;

        // т.к свойство fillAmount принимает значение от 0 до 1, нам нужно привести это к сооветсвущим велечинам
        // Разделив текущие значение на максимальное мы по сути
        // получаем количество процентов текущего голода в формате от 0 до 1
        // где 1 = 100%, а 0 = 0%
        hungryImage.fillAmount = hungry / startHungry;
    }
}
