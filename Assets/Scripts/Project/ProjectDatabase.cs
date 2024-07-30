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
        Projects.Add(new Project(1 ,"Project A", 0, 0, 1, 0, 5, 7, 6, "เอา Project ไปเลย", "พนักงานโดนไล่ออก 1 คน")); // SELECT
        Projects.Add(new Project(2 , "Project B", 0, 0, 0, 0, 3, 9, 8, "เอา Project ไปเลย", "สั่งพักงาน 2 คนในทีม เป็นเวลา 1 วัน"));
        Projects.Add(new Project(3 ,"Project C", 0, 1, 0, 1, 7, 4, 4, "เอา Project ไปเลย", "ทิ้งการ์ดบนมือ 2 ใบ")); // SELECT
        Projects.Add(new Project(4 , "Project D", 0, 0, 0, 0, 4, 8, 6, "เอา Project ไปเลย", "ทิ้งกทิ้งการ์ดบตัวละครบนมือ 1 ใบ และพักงาน 1 คน"));
        Projects.Add(new Project(5 ,"Project E", 1, 0, 1, 1, 5, 6, 5, "เอา Project ไปเลย", "ทิ้งการ์ดตัวละครบนมือ 1 ใบถ้า ถ้าไม่มีไล่ออก 1 คน"));
        Projects.Add(new Project(6 ,"Project F", 1, 1, 0, 0, 4, 5, 6, "เอา Project ไปเลย", "ทิ้งการ์ด Action 1 ใบ และพักงาน 1 คน"));
        Projects.Add(new Project(7 ,"Project H", 0, 0, 0, 1, 4, 7, 6, "เอา Project ไปเลย", "นำการ์ดบใบนมือ 2 ใบ ให้คนอื่นคนละใบ"));
        Projects.Add(new Project(8 ,"Project I", 2, 0, 0, 0, 5, 8, 6, "เอา Project ไปเลย", "พักงานการ์ดฝ่าย IT ของเรา 2 คน"));
        Projects.Add(new Project(9 ,"Project J", 1, 0, 1, 0, 6, 9, 7, "เอา Project ไปเลย และพักงานคนอื่น 2 ใบ", "ไล่พนักงานออก 2"));
        Projects.Add(new Project(10 ,"Project K", 1, 0, 0, 1, 3, 6, 6, "เอา Project ไปเลย", "ทิ้งการ์ดในมือ 3 จั่ว 1")); // SELECT
        Projects.Add(new Project(11 ,"Project L", 0, 2, 0, 0, 2, 6, 7, "เอา Project ไปเลย", "คุณจะโดน Report เพิ่มทันที 1 ใบ")); // SELECT
        Projects.Add(new Project(12 ,"Project M", 1, 1, 1, 1, 5, 8, 6, "เอา Project ไปเลย จั่วการ์ด 2 ใบ", "ไล่ออก 2 คน และทิ้งการ์ด 1 ใบ"));
    }
}
