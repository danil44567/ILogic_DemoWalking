using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    // �������� �� ����������� � ������� ���������� �����
    [SerializeField] private Image hungryImage;

    // �������� �� ����������� � ������� ���������� ��������
    [SerializeField] private Image healthImage;

    // �������� �� ������ ������� ����� ����������, ��� ����� ����� �������
    [SerializeField] private GameObject veryHungryText;

    // �������� �� ������ ������� ����� ����������, ����� � ������ ����������� ��������
    [SerializeField] private GameObject deadScreen;

    // ������� ����� ������
    private float hungry;

    // ������� �������� ������
    private float health;

    // ��������� ����� ������ (�������� ����� ��� ����, ����� ����� ����� ����� � ��� ��� ���������)
    private float startHungry = 10;
    
    // ��������� �������� ������
    private float startHealth = 100;


    // ���������� ������ ��� ������� ������ �� ������ � ����������� ����������
    private float healthDamageTimer;

    void Start()
    {
        // �� ����� ������ ���� � ������� ����� ������ ������������� ��������� ��������
        hungry = startHungry;

        health = startHealth;
    }

    void Update()
    {
        // �� ������ ����� �������� �������� (�� ���� ������ ��������� �������)
        hungry -= Time.deltaTime;

        // �.� �������� fillAmount ��������� �������� �� 0 �� 1, ��� ����� �������� ��� � ������������ ���������
        // �������� ������� �������� �� ������������ �� �� ����
        // �������� ���������� ��������� �������� ������ � ������� �� 0 �� 1
        // ��� 1 = 100%, � 0 = 0%
        float hungryPercent = hungry / startHungry;

        // ������������� ������� ������ � fillAmount ��� �����������
        hungryImage.fillAmount = hungryPercent;

        // ���� ����� ���� 10%, �� ���������� ��� ����� ����� �������
        if (hungryPercent < 0.1f) {
            veryHungryText.SetActive(true);
        }


        // ���� ����� ���� ���� 0
        if (hungry <= 0)
        {
            // �������� � ������ ��������
            healthDamageTimer += Time.deltaTime;

            // ���� �������� ������� ������ 5 ������
            if (healthDamageTimer > 5)
            {
                // ������� ������ 10 ��������
                health -= 10;
                // ���������� ������ �� 0 ������
                healthDamageTimer = 0;
            }
        }

        // ���� �������� ����� ���� 0
        if (health <= 0)
        {
            // ���������� ������, ��� �������� ������ ���
            deadScreen.SetActive(true);
        }

        // �������������� ������� ������� �������� (��������� ������)
        healthImage.fillAmount = health / startHealth;

    }


    // �����, ����������������� ����� ������.
    // ��������� �������� � ����������� ������������������ ������
    // ���������� �������� ���������� � ����� float � ������ satiety
    public void AddHugry(float satiety)
    {
        // ����������� ������� �������� ������ �� �����, ���������� ��� ������ ������
        hungry += satiety;

        // ���� ���������� �������� ������, ��������� ������� �������������, �� ������ ������������ �������� � �����.
        // ��� ��� �����, ����� �� ������������� ������, � � ��� �������� ������ �� ��������� ����� ������ �������������
        if (hungry > startHungry) 
        {
            // �������� � ������� ����� �������� ���������� ������ (� ����� ������ �������������)
            hungry = startHungry;
        }
    }
}
