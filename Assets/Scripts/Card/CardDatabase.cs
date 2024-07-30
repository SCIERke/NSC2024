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
        Cards.Add(new Card(3, "Web Developer ", 1, 1, "IT", "จั่วการ์ด 2 ใบ (7+)", Resources.Load<Sprite>("Picture/Character/" + "Employee"))); //SELECT
        Cards.Add(new Card(4, "Cybersecurity Engineer", 1, 1, "IT", "หากการ์ดใบนี้สามารถอยู่ในสนามได้จนถึง", Resources.Load<Sprite>("Picture/Character/" + "Employee")));
        Cards.Add(new Card(5, "Hacker", 1, 1, "IT", "ขโมยการ์ดของผู้เล่นทุกคน 1 ใบ ที่มีตัวละครสาย IT อยู่ในสนาม (7+)", Resources.Load<Sprite>("Picture/Character/" + "Hacker"))); // clear
        Cards.Add(new Card(6, "Software Engineer", 1, 1, "IT", "ทําลายการ์ด Report ที่ติดอยู่ที่เรา 1 ใบ (7+)", Resources.Load<Sprite>("Picture/Character/" + "Software_Engineer"))); // clear
        Cards.Add(new Card(7, "Developer", 1, 1, "IT", "แลกมือกับคนอื่น (8+)", Resources.Load<Sprite>("Picture/Character/" + "Employee")));
        
        //Marketing
        Cards.Add(new Card(8, "Marketing Employee", 1, 1, "Marketing", "None", Resources.Load<Sprite>("Picture/Character/" + "Employee"))); // clear
        Cards.Add(new Card(9, "Marketing Workaholic E.", 2, 1, "Marketing", "None", Resources.Load<Sprite>("Picture/Character/" + "Workaholic"))); // clear
        Cards.Add(new Card(10, "Publicist", 1, 1, "Marketing", "จั่วการ์ด 1 ใบ หากการ์ดนั้นเป็นการ์ด Action จะสามารถเล่นการ์ดนั้นได้ทันที (6+, IR)" ,Resources.Load<Sprite>("Picture/Character/" + "Publicist"))); // clear //SELECT
        Cards.Add(new Card(11, "Market researcher", 1, 1, "MArketing", "ป้องกันการคัดค้านจากผู้เล่นคนอื่น จนกว่าจะจบเทิร์นเรา(7+)", Resources.Load<Sprite>("Picture/Character/" + "Employee")));
        Cards.Add(new Card(12, "Marketing consultant", 1, 1, "Marketing", "ให้เลือกทิ้งการ์ด 1 ใบจากมือเรา และให้ผู้เล่นที่เหลือเลือกทิ้งการ์ดในมือคนละ 2 ใบ (8+)", Resources.Load<Sprite>("Picture/Character/" + "Marketing_consultant")));
        Cards.Add(new Card(13, "Strategic Marketing Planner", 1, 1, "Marketing", "เลือก Project ที่มีให้เลือกทําออกหนึ่งอัน และแทนด้วย Project ตัวใหม่ (+8)", Resources.Load<Sprite>("Picture/Character/" + "Employee")));
        Cards.Add(new Card(14, "Advertiser", 1, 1, "Marketing", "เลือกทําโปรเจกต์ที่ต้องการได้สําเร็จทันที และจบเทิร์นเราทันทีเมื่อความใช้สามารถนี้สําเร็จ (12+)", Resources.Load<Sprite>("Picture/Character/" + "Employee")));
        
        //Human Resource
        Cards.Add(new Card(15, "Human Resource Employee", 1, 1, "Human_Resource", "None", Resources.Load<Sprite>("Picture/Character/" + "Employee"))); // clear
        Cards.Add(new Card(16, "Workaholic Employee", 2, 1, "Human_Resource", "None", Resources.Load<Sprite>("Picture/Character/" + "Workaholic"))); // clear
        Cards.Add(new Card(17, "Recruiter", 1, 1, "Human_Resource", "เลือกขโมยการ์ดของผู้เล่นคนอื่น 1 ใบ หากการ์ดนั้นเป็นการ์ดตัวละคร จะสามารถเล่นการ์ดนั้นได้ทันที (7+,IR)", Resources.Load<Sprite>("Picture/Character/" + "Employee")));
        Cards.Add(new Card(18, "Human Resources Department", 1, 1, "Human_Resource", "นําพนักงานที่อยู่ในกองทิ้งมาเล่นได้ทันที 1 ตัว (9+)", Resources.Load<Sprite>("Picture/Character/" + "Employee")));
        Cards.Add(new Card(19, "Employee Relations Department", 1, 1, "Human_Resource", "เปลี่ยนฝ่ายของพนักงานในทีมเรา 1 คน (5+)", Resources.Load<Sprite>("Picture/Character/" + "Employee")));
        Cards.Add(new Card(20, "Human resources executive", 1, 1, "Human_Resource", "นําการ์ดตัวละครที่ถูกพักงานอยู่ในทีม กลับมาทํางานได้ 1 คน (5+, IR)", Resources.Load<Sprite>("Picture/Character/" + "Human_resources_executive")));
        Cards.Add(new Card(21, "Department manager", 1, 1, "Human_Resource", "เลือกไล่การ์ดตัวละครของใครก็ได้ออก 1 คน (8+)", Resources.Load<Sprite>("Picture/Character/" + "Employee")));

        //Accountant
        Cards.Add(new Card(22, "Employee", 1, 1, "Accountant", "None", Resources.Load<Sprite>("Picture/Character/" + "Employee"))); // clear
        Cards.Add(new Card(23, "Workaholic Employee", 2, 1, "Accountant", "None", Resources.Load<Sprite>("Picture/Character/" + "Workaholic"))); // clear
        Cards.Add(new Card(24, "Finance Manager", 1, 1, "Accountant", "เพิ่มแต้มลูกเต๋าของเรา 3 แต้มจนกว่าจะจบเทิร์นเรา (7+, IR)", Resources.Load<Sprite>("Picture/Character/" + "Employee"))); //SELECT
        Cards.Add(new Card(25, "Accounts Receivable Manager", 1, 1, "Accountant", "เลือกผู้เล่น 1 คน ขโมยการ์ดจากมือของผู้เล่นคนนั้น 2 ใบ และเลือก 1 ใบจากในนั้นขึ้นมือ ส่วนอีกใบให้นำลงกองทิ้ง (6+)", Resources.Load<Sprite>("Picture/Character/" + "Employee")));
        Cards.Add(new Card(26, "Auditor", 1, 1, "Accountant", "เลือกผู้เล่น 1 คน เพื่อดูการ์ดบนมือคนนั้น (5+, IR)", Resources.Load<Sprite>("Picture/Character/" + "Employee")));
        Cards.Add(new Card(27, "Accounts Payable Manager", 1, 1, "Accountant", "เลือกไล่การ์ดตัวละครของตนเองในสนามได้ 1-3 ใบ แลกกับได้จำนวน AP เพิ่มเท่ากับจำนวนพนักงาน", Resources.Load<Sprite>("Picture/Character/" + "Employee"))); //SELECT
        Cards.Add(new Card(28, "Financial analyst", 1, 1, "Accountant", "เพิ่มแต้มลูกเต๋า ตามจำนวนการ์ดตัวละครที่มีในทีม โดยไม่นับตัวนี้ (9+)", Resources.Load<Sprite>("Picture/Character/" + "Employee")));
        
        //Action
        Cards.Add(new Card(29, "Recruit", 0, 1, "Action", "สามารถลง minion ได้ 2 ตัว", Resources.Load<Sprite>("Picture/Character/" + "Hacker")));
        Cards.Add(new Card(30, "Energy Boost", 0, 1, "Action", "ทําให้ Working Point (WP) ของผู้เล่น เพิ่มทีละ 1 ตามจํานวนของ Minion ในสนาม", Resources.Load<Sprite>("Picture/Character/" + "Employee")));
        Cards.Add(new Card(31, "Closed Office", 0, 1, "Action", "เลือกผู้เล่นคนใดคนหนึ่ง ผู้เล่นคนนั้นต้องนําการ์ดในแต่ละสายไปลง Drop zone สายละ 1 ตัว", Resources.Load<Sprite>("Picture/Character/" + "Employee")));
    }
}
