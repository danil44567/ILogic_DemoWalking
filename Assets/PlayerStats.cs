using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    // �������� �� ����������� � ������� ���������� �����
    [SerializeField] private Image hungryImage;

    // ������� ����� ������
    private float hungry;

    // ��������� ����� ������ (�������� ����� ��� ����, ����� ����� ����� ����� � ��� ��� ���������)
    private float startHungry = 100;

    void Start()
    {
        // �� ����� ������ ���� � ������� ����� ������ ������������� ��������� ��������
        hungry = startHungry;
    }

    void Update()
    {
        // �� ������ ����� �������� �������� (�� ���� ������ ��������� �������)
        hungry -= Time.deltaTime;

        // �.� �������� fillAmount ��������� �������� �� 0 �� 1, ��� ����� �������� ��� � ������������ ���������
        // �������� ������� �������� �� ������������ �� �� ����
        // �������� ���������� ��������� �������� ������ � ������� �� 0 �� 1
        // ��� 1 = 100%, � 0 = 0%
        hungryImage.fillAmount = hungry / startHungry;
    }
}
