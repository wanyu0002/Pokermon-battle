﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HeroShana : MonoBehaviour {

    string HeroName;
    public GameObject HeroModel;
    public  List<string> Pokemon=new List<string>();//持有的宝可梦编号
    public  List<int> Skill = new List<int>();  //技能编号,公共技能和专属技能写在一起    
	// Use this for initialization
	void Start ()
    {
        HeroName = "莎娜";
        //补充莎娜模型并显示在指定位置
        //给公共两种技能卡数组赋值
        Pokemon.Add("001");
        Pokemon.Add("002");
        Pokemon.Add("003");
        Pokemon.Add("001");
        Pokemon.Add("002");
        Pokemon.Add("003");
        string[] a = new string[]{ "001","004","007","017","071","100" };
        int[] b = new int[] {1, 2, 3, 1, 2, 3, 1, 2,3,1 };
        AddRange(a, b); 
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void AddRange(string[] a,int[] b)
    {
        //Pokemon.AddRange(a);
        Skill.AddRange(b);
    }
}
