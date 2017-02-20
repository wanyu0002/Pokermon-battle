using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill3 : SkillCard {

	// Use this for initialization
	
	// Update is called once per frame
	void Update ()
    {
        GameObject other;
       if(choice == 1 && Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                other = hit.collider.gameObject;

                if (other.tag == "Card" + this.tag[5])
                {
                    if (this.tag[5] == 'A')
                        other.GetComponent<BattleA>().attack += 4;
                    else
                        other.GetComponent<BattleB>().attack += 4;

                    print("为" + this.tag[5] + "方的单只宝可梦增加了4点攻击力");
                    FindObjectOfType<CardGenerator>().DestoryCard(this.gameObject);
                }
            }
            
        }
       
    }
    public override void Change()
    {
        GetComponentsInChildren<Renderer>()[0].material.mainTexture = (Texture)Resources.Load("SkillCardPictures/3");
    }

}
