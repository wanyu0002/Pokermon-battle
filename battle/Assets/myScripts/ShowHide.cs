using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ShowHide : MonoBehaviour
{     //这段代码不要挂在你要隐藏的物体上，否则隐藏后代码就失效了，无法再次激活
    GameObject[] cardA,cardB;

    void Start()
    {
        cardA = GameObject.FindGameObjectsWithTag("CardA");
        cardB = GameObject.FindGameObjectsWithTag("CardB");     //用两个是因为标签分开了A，B..
    }


    private void Update()
    {
        cardA = GameObject.FindGameObjectsWithTag("CardA");
        cardB = GameObject.FindGameObjectsWithTag("CardB");     //用两个是因为标签分开了A，B..
        foreach (GameObject cardx in cardA)
        {
            if (cardx.GetComponent<BattleA>().hp <= 0 && cardx.GetComponent<BattleA>().showflag == true)//若宝可梦被打败
            {
                cardx.GetComponent<BattleA>().showflag = false;
                Destroy(cardx.GetComponentsInChildren<Transform>()[9].gameObject);//oo:删除卡牌附带的模型
                cardx.GetComponent<BattleA>().tag = "temp";           //(oo:更换标签)
                Destroy(cardx.GetComponent<BattleA>());               //oo:删除卡牌上的脚本


                cardx.SetActive(false);
            }
        }
        foreach (GameObject cardy in cardB)
            if (cardy.GetComponent<BattleB>().hp <= 0 && cardy.GetComponent<BattleB>().showflag == true)//若宝可梦被打败
            {
                CallB.cardnumberB--;                    //oo
                print(CallB.cardnumberB + "次数!!!");
                cardy.SetActive(false);
                cardy.GetComponent<BattleB>().showflag = false;
                cardy.GetComponent<BattleB>().tag = "temp";
            }
    }
}


