using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


public class CardGenerator : MonoBehaviour {

    // Use this for initialization
    public List<int> PokemonA = new List<int>();//A持有的宝可梦编号
    public List<int> PokemonB = new List<int>();//B持有的宝可梦编号
    public List<int> SkillA = new List<int>();  //A的技能编号
    public List<int> SkillB = new List<int>();  //B的技能编号
    public List<GameObject> HandCardA = new List<GameObject>();//A的手牌
    public List<GameObject> HandCardB = new List<GameObject>();//B的手牌
    int aChange,bChange; //A,B手牌的位移 
    float timer =0;
    int numA, numB;//A,B牌堆中卡牌数量

    void Awake()
    {
        //分别读取双方持有的宝可梦和技能卡牌
        PokemonA = GameObject.Find("HeroA").GetComponent<HeroShana>().Pokemon;
        PokemonB = GameObject.Find("HeroB").GetComponent<HeroShana>().Pokemon;
        SkillA = GameObject.Find("HeroA").GetComponent<HeroShana>().Skill;
        SkillB = GameObject.Find("HeroB").GetComponent<HeroShana>().Skill;
  

    }
    void Start ()
    {
        aChange= bChange = 0;//手牌位移初值
        numA = numB = 10;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    //产生新的技能牌
    public void GenerateSkillCard(Hero hero)
    {
        timer += Time.deltaTime;
        if(hero==Hero.HeroA)
        {            
            int random = (int)UnityEngine.Random.Range(0,SkillA.Count);
            GameObject newcard=(GameObject)Instantiate(Resources.Load("Prefab/SkillCardA"));          
            newcard.AddComponent(Type.GetType("Skill" + SkillA[random].ToString()));
            SkillA.RemoveAt(random);     
            iTween.MoveTo(newcard, new Vector3(-2+aChange, 1, -4), 10);            
            iTween.RotateTo(newcard, new Vector3(-90, 180, -180), 2);            
            aChange++;
            HandCardA.Add(newcard);           
            CleanUp(hero);
        }
        else if(hero==Hero.HeroB)
        {
            int random = (int)UnityEngine.Random.Range(0, SkillB.Count);
            GameObject newcard = (GameObject)Instantiate(Resources.Load("Prefab/SkillCardB"));
            newcard.AddComponent(Type.GetType("Skill" + SkillB[random].ToString()));
            SkillB.RemoveAt(random);
            iTween.MoveTo(newcard, new Vector3(-2 + bChange, 1, 4), 10);            
            iTween.RotateTo(newcard, new Vector3(-90, 0, -180), 2);
            bChange++;
            HandCardB.Add(newcard);
            CleanUp(hero);
        }
        
    }

    //采用字符串作为参数，方便在GameController中用掉手牌后进行整理
    public void CleanUp(Hero hero)
    {
        List<GameObject> list;
        if (hero ==Hero.HeroA)
            list = HandCardA;
        else
            list = HandCardB;
        float space = 8.0f/ list.Count;
        int k=0;
        foreach (GameObject g in list)
        {
            if(list==HandCardA)
                iTween.MoveTo(g, new Vector3(-3 + space * k, 1, -4), 0.5f);
           
            if(list==HandCardB)
                iTween.MoveTo(g, new Vector3(-3 + space * k, 1, 4), 0.5f);
            k++;
        }
    }

    //销毁技能牌
    public void DestoryCard(GameObject card)
    {
        foreach(GameObject a in HandCardA)
        {
            if (a == card)
            {
                HandCardA.Remove(card);
                Destroy(card);
                CleanUp(Hero.HeroA);
                break;
            }
                
        }
        foreach(GameObject b in HandCardB)
        {
            if(b==card)
            {
                HandCardB.Remove(card);
                Destroy(card);
                CleanUp(Hero.HeroB);
                break;
            }
        }
        
    }

    public void GenerateWeatherCard()
    {
        //随机产生天气卡牌，待补充
    }

}
