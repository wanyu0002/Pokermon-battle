using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Skill1 : SkillCard {

	// Use this for initialization
	//治疗卡片，给我方全体增加两点生命值
	
	// Update is called once per frame
	void Update () {
		
	}
    public override void Change()
    {
       GetComponentsInChildren<Renderer>()[0].material.mainTexture = (Texture)Resources.Load("SkillCardPictures/1" );
    }

    public override void Effect()
    {
        GameObject[] PokeCard = GameObject.FindGameObjectsWithTag("Card"+tag.ToString()[5]);
        if(tag.ToString()[5]=='A')
        foreach (GameObject a in PokeCard)
        {
                a.GetComponent<BattleA>().hp+=2;
        }
        else
            foreach (GameObject a in PokeCard)
            {
                a.GetComponent<BattleB>().hp+=2;
            }
        print("为" + tag.ToString()[5] + "方的宝可梦恢复2点生命值");
        FindObjectOfType<CardGenerator>().DestoryCard(this.gameObject);
    }
}
