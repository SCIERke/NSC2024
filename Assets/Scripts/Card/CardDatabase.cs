using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDatabase : MonoBehaviour
{
    public static List<Card> Cards = new List<Card>();


    //id ,Name ,WP ,AP ,Type ,Description ,I
    void Awake()
    {
        //Temp
        Cards.Add(new Card(0, "None", 0, 0, "Normal", "None", Resources.Load<Sprite>("Picture/Character/" + "Employee"))); // clear

        //IT
        Cards.Add(new Card(1, "IT Employee", 1, 1, "IT", "None" , Resources.Load<Sprite>("Picture/Character/" + "Employee"))); // clear //SELECT
        Cards.Add(new Card(2, "IT Workaholic Employee", 1, 1, "IT", "None", Resources.Load<Sprite>("Picture/Character/" + "Workaholic"))); // clear //SELECT
        Cards.Add(new Card(3, "Web Developer ", 1, 1, "IT", "���ǡ��� 2 � (7+)", Resources.Load<Sprite>("Picture/Character/" + "Employee"))); //SELECT
        Cards.Add(new Card(4, "Cybersecurity Engineer", 1, 1, "IT", "�ҡ����㺹������ö�����ʹ���騹�֧", Resources.Load<Sprite>("Picture/Character/" + "Employee")));
        Cards.Add(new Card(5, "Hacker", 1, 1, "IT", "���¡��촢ͧ�����蹷ء�� 1 � ����յ���Ф���� IT �����ʹ�� (7+)", Resources.Load<Sprite>("Picture/Character/" + "Hacker"))); // clear
        Cards.Add(new Card(6, "Software Engineer", 1, 1, "IT", "�����¡��� Report ���Դ��������� 1 � (7+)", Resources.Load<Sprite>("Picture/Character/" + "Software_Engineer"))); // clear
        Cards.Add(new Card(7, "Developer", 1, 1, "IT", "�š��͡Ѻ����� (8+)", Resources.Load<Sprite>("Picture/Character/" + "Employee")));
        
        //Marketing
        Cards.Add(new Card(8, "Marketing Employee", 1, 1, "Marketing", "None", Resources.Load<Sprite>("Picture/Character/" + "Employee"))); // clear
        Cards.Add(new Card(9, "Marketing Workaholic E.", 2, 1, "Marketing", "None", Resources.Load<Sprite>("Picture/Character/" + "Workaholic"))); // clear
        Cards.Add(new Card(10, "Publicist", 1, 1, "Marketing", "���ǡ��� 1 � �ҡ���촹���繡��� Action ������ö��蹡��촹����ѹ�� (6+, IR)" ,Resources.Load<Sprite>("Picture/Character/" + "Publicist"))); // clear //SELECT
        Cards.Add(new Card(11, "Market researcher", 1, 1, "MArketing", "��ͧ�ѹ��äѴ��ҹ�ҡ�����蹤���� �����ҨШ��������(7+)", Resources.Load<Sprite>("Picture/Character/" + "Employee")));
        Cards.Add(new Card(12, "Marketing consultant", 1, 1, "Marketing", "������͡��駡��� 1 㺨ҡ������ ����������蹷����������͡��駡������ͤ��� 2 � (8+)", Resources.Load<Sprite>("Picture/Character/" + "Marketing_consultant")));
        Cards.Add(new Card(13, "Strategic Marketing Planner", 1, 1, "Marketing", "���͡ Project �����������͡����͡˹���ѹ ���᷹���� Project ������� (+8)", Resources.Load<Sprite>("Picture/Character/" + "Employee")));
        Cards.Add(new Card(14, "Advertiser", 1, 1, "Marketing", "���͡�����ਡ�����ͧ����������稷ѹ�� ��Ш�������ҷѹ������ͤ���������ö��������� (12+)", Resources.Load<Sprite>("Picture/Character/" + "Employee")));
        
        //Human Resource
        Cards.Add(new Card(15, "Human Resource Employee", 1, 1, "Human_Resource", "None", Resources.Load<Sprite>("Picture/Character/" + "Employee"))); // clear
        Cards.Add(new Card(16, "Workaholic Employee", 2, 1, "Human_Resource", "None", Resources.Load<Sprite>("Picture/Character/" + "Workaholic"))); // clear
        Cards.Add(new Card(17, "Recruiter", 1, 1, "Human_Resource", "���͡���¡��촢ͧ�����蹤���� 1 � �ҡ���촹���繡��촵���Ф� ������ö��蹡��촹����ѹ�� (7+,IR)", Resources.Load<Sprite>("Picture/Character/" + "Employee")));
        Cards.Add(new Card(18, "Human Resources Department", 1, 1, "Human_Resource", "��Ҿ�ѡ�ҹ�������㹡ͧ����������ѹ�� 1 ��� (9+)", Resources.Load<Sprite>("Picture/Character/" + "Employee")));
        Cards.Add(new Card(19, "Employee Relations Department", 1, 1, "Human_Resource", "����¹���¢ͧ��ѡ�ҹ㹷����� 1 �� (5+)", Resources.Load<Sprite>("Picture/Character/" + "Employee")));
        Cards.Add(new Card(20, "Human resources executive", 1, 1, "Human_Resource", "��ҡ��촵���Ф÷��١�ѡ�ҹ����㹷�� ��Ѻ�ҷ�ҧҹ�� 1 �� (5+, IR)", Resources.Load<Sprite>("Picture/Character/" + "Human_resources_executive")));
        Cards.Add(new Card(21, "Department manager", 1, 1, "Human_Resource", "���͡�����촵���Фâͧ�á����͡ 1 �� (8+)", Resources.Load<Sprite>("Picture/Character/" + "Employee")));

        //Accountant
        Cards.Add(new Card(22, "Employee", 1, 1, "Accountant", "None", Resources.Load<Sprite>("Picture/Character/" + "Employee"))); // clear
        Cards.Add(new Card(23, "Workaholic Employee", 2, 1, "Accountant", "None", Resources.Load<Sprite>("Picture/Character/" + "Workaholic"))); // clear
        Cards.Add(new Card(24, "Finance Manager", 1, 1, "Accountant", "��������١��Ңͧ��� 3 ��������ҨШ�������� (7+, IR)", Resources.Load<Sprite>("Picture/Character/" + "Employee"))); //SELECT
        Cards.Add(new Card(25, "Accounts Receivable Manager", 1, 1, "Accountant", "���͡������ 1 �� ���¡��촨ҡ��ͧ͢�����蹤���� 2 � ������͡ 1 㺨ҡ㹹�鹢����� ��ǹ�ա�����ŧ�ͧ��� (6+)", Resources.Load<Sprite>("Picture/Character/" + "Employee")));
        Cards.Add(new Card(26, "Auditor", 1, 1, "Accountant", "���͡������ 1 �� ���ʹ١��촺���ͤ���� (5+, IR)", Resources.Load<Sprite>("Picture/Character/" + "Employee")));
        Cards.Add(new Card(27, "Accounts Payable Manager", 1, 1, "Accountant", "���͡�����촵���Фâͧ���ͧ�ʹ���� 1-3 � �š�Ѻ��ӹǹ AP ������ҡѺ�ӹǹ��ѡ�ҹ", Resources.Load<Sprite>("Picture/Character/" + "Employee"))); //SELECT
        Cards.Add(new Card(28, "Financial analyst", 1, 1, "Accountant", "��������١��� ����ӹǹ���촵���Ф÷����㹷�� �����Ѻ��ǹ�� (9+)", Resources.Load<Sprite>("Picture/Character/" + "Employee")));
        
        //Action
        Cards.Add(new Card(29, "Recruit", 0, 1, "Action", "����öŧ minion �� 2 ���", Resources.Load<Sprite>("Picture/Character/" + "Hacker")));
        Cards.Add(new Card(30, "Energy Boost", 0, 1, "Action", "������ Working Point (WP) �ͧ������ �������� 1 �����ҹǹ�ͧ Minion �ʹ��", Resources.Load<Sprite>("Picture/Character/" + "Employee")));
        Cards.Add(new Card(31, "Closed Office", 0, 1, "Action", "���͡�����蹤�㴤�˹�� �����蹤���鹵�ͧ��ҡ������������ŧ Drop zone ����� 1 ���", Resources.Load<Sprite>("Picture/Character/" + "Employee")));
    }
}
