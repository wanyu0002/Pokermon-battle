using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill2 : SkillCard {

	// Use this for initialization
    //AOE卡片，对敌方全体造成伤害
	// Update is called once per frame
	void Update () {
		
	}
    public override void Change()
    {
        GetComponentsInChildren<Renderer>()[0].material.mainTexture = (Texture)Resources.Load("SkillCardPictures/2");
    }
    public override void Effect()
    {
        char deny;
        if (tag.ToString()[5] == 'A')
           deny = 'B';
        else
            deny = 'A';
        GameObject[] PokeCard = GameObject.FindGameObjectsWithTag("Card" + deny);
        if (tag.ToString()[5] == 'A')
            foreach (GameObject a in PokeCard)
            {
                a.GetComponent<BattleB>().hp-=2;
            }
        else
            foreach (GameObject a in PokeCard)
            {
                a.GetComponent<BattleA>().hp-=2;
            }
        print("对" + deny + "方的宝可梦造成两点伤害");
        FindObjectOfType<CardGenerator>().DestoryCard(this.gameObject);
    }

}
