using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectDatabase : MonoBehaviour
{
    public static List<Project> Projects = new List<Project>();

    //id ,name ,reqIT ,reqMarketing ,reqHR ,reqAC , reqWorkingPoint ,UpperInt ,LowerInt ,UpperCondition ,LowerCondition
    void Awake()
    {
        //Temp
        Projects.Add(new Project(0 ,"None", 0, 0, 0, 0, 0, 0, 0, "None", "None"));

        //Real
        Projects.Add(new Project(1 ,"Project A", 0, 0, 1, 0, 5, 7, 6, "��� Project ����", "��ѡ�ҹⴹ����͡ 1 ��")); // SELECT
        Projects.Add(new Project(2 , "Project B", 0, 0, 0, 0, 3, 9, 8, "��� Project ����", "��觾ѡ�ҹ 2 ��㹷�� ������ 1 �ѹ"));
        Projects.Add(new Project(3 ,"Project C", 0, 1, 0, 1, 7, 4, 4, "��� Project ����", "��駡��촺���� 2 �")); // SELECT
        Projects.Add(new Project(4 , "Project D", 0, 0, 0, 0, 4, 8, 6, "��� Project ����", "��駡��駡��촺����Фú���� 1 � ��оѡ�ҹ 1 ��"));
        Projects.Add(new Project(5 ,"Project E", 1, 0, 1, 1, 5, 6, 5, "��� Project ����", "��駡��촵���Фú���� 1 㺶�� ������������͡ 1 ��"));
        Projects.Add(new Project(6 ,"Project F", 1, 1, 0, 0, 4, 5, 6, "��� Project ����", "��駡��� Action 1 � ��оѡ�ҹ 1 ��"));
        Projects.Add(new Project(7 ,"Project H", 0, 0, 0, 1, 4, 7, 6, "��� Project ����", "�ӡ��촺㺹��� 2 � ��餹��蹤����"));
        Projects.Add(new Project(8 ,"Project I", 2, 0, 0, 0, 5, 8, 6, "��� Project ����", "�ѡ�ҹ���촽��� IT �ͧ��� 2 ��"));
        Projects.Add(new Project(9 ,"Project J", 1, 0, 1, 0, 6, 9, 7, "��� Project ���� ��оѡ�ҹ����� 2 �", "��边ѡ�ҹ�͡ 2"));
        Projects.Add(new Project(10 ,"Project K", 1, 0, 0, 1, 3, 6, 6, "��� Project ����", "��駡������� 3 ���� 1")); // SELECT
        Projects.Add(new Project(11 ,"Project L", 0, 2, 0, 0, 2, 6, 7, "��� Project ����", "�س��ⴹ Report �����ѹ�� 1 �")); // SELECT
        Projects.Add(new Project(12 ,"Project M", 1, 1, 1, 1, 5, 8, 6, "��� Project ���� ���ǡ��� 2 �", "����͡ 2 �� ��з�駡��� 1 �"));
    }
}
