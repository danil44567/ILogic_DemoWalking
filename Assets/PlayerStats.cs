using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    // Указание на изображение в котором отображаем голод
    [SerializeField] private Image hungryImage;

    // Указание на изображение в котором отображаем здоровье
    [SerializeField] private Image healthImage;

    // Указание на объект который будем отображать, что игрок очень голоден
    [SerializeField] private GameObject veryHungryText;

    // Указание на объект который будем отображать, когда у игрока закончилось здоровье
    [SerializeField] private GameObject deadScreen;

    // Текущий голод игрока
    private float hungry;

    // Текущее здоровье игрока
    private float health;

    // Начальный голод игрока (значение нужно для того, чтобы знать какой голод у нас был начальный)
    private float startHungry = 10;
    
    // Начальное здоровье игрока
    private float startHealth = 100;


    // Переменная нужная для таймера ударов по игроку с определённым интервалом
    private float healthDamageTimer;

    void Start()
    {
        // Во время запуса игры в текущий голод игрока устанавливаем начальное значение
        hungry = startHungry;

        health = startHealth;
    }

    void Update()
    {
        // На каждом кадре отнимаем значения (по сути таймер обратного отсчёта)
        hungry -= Time.deltaTime;

        // т.к свойство fillAmount принимает значение от 0 до 1, нам нужно привести это к сооветсвущим велечинам
        // Разделив текущие значение на максимальное мы по сути
        // получаем количество процентов текущего голода в формате от 0 до 1
        // где 1 = 100%, а 0 = 0%
        float hungryPercent = hungry / startHungry;

        // Устанавливаем процент голода в fillAmount для отображения
        hungryImage.fillAmount = hungryPercent;

        // Если голод ниже 10%, то отображаем что игрок очень голоден
        if (hungryPercent < 0.1f) {
            veryHungryText.SetActive(true);
        }


        // Если голод упал ниже 0
        if (hungry <= 0)
        {
            // Добавлем в таймер значение
            healthDamageTimer += Time.deltaTime;

            // Если значение таймера больше 5 секунд
            if (healthDamageTimer > 5)
            {
                // Снимаем игроку 10 здоровья
                health -= 10;
                // Сбрасываем таймер на 0 секунд
                healthDamageTimer = 0;
            }
        }

        // Если здоровье упало ниже 0
        if (health <= 0)
        {
            // Показываем игроку, что здоровья больше нет
            deadScreen.SetActive(true);
        }

        // Устананавлваем текущий процент здоровья (анлогично голоду)
        healthImage.fillAmount = health / startHealth;

    }


    // Метод, восстанавливающий голод игрока.
    // Принимает аргумент с количеством восстанавливаемого голода
    // Аргументом является переменная с типом float и именем satiety
    public void AddHugry(float satiety)
    {
        // Увеличиваем текущее значение голода на число, полученное при вызове метода
        hungry += satiety;

        // Если полученное значение голода, оказалось большем максимального, то ставим максимальное значение в голод.
        // Нам это нужно, чтобы не «перекормить» игрока, и в его значении голода не оказалось число больше максимального
        if (hungry > startHungry) 
        {
            // Усановка в текущий голод значение стартового голода (в нашем случае максимального)
            hungry = startHungry;
        }
    }
}
