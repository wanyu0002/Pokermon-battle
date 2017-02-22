using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallB : MonoBehaviour
{
    static public int callflagB;           //当前召唤次数 
    GameObject[] cardB;
    public List<string> picture = new List<string>();
    public int num;                        //图片编号
//    public int i;
    static public int cardnumberB;         //目前在场卡牌数
    public bool initialflag;               //第一次的初始化
    void Start()
    {

        cardB = GameObject.FindGameObjectsWithTag("CardB");
        //        List<int> picture =new List<int>();//图片编号
        picture.Add("017");
        picture.Add("071");
        picture.Add("100");
        picture.Add("105");
        picture.Add("129");
        picture.Add("007");            
        num = Random.Range(0, picture.Count);    //  Random.Range()包前不包后，这个地方必须用i，不能用picture.Count,为什么啊！
        callflagB = 0;                      //当前召唤次数
        cardnumberB = 3;
        initialflag = false;
    }

    void Update()
    {
        if (initialflag == false)             //第一次读取前三张卡牌
        {
            foreach (GameObject cardy in cardB)
            {
                cardy.GetComponent<BattleB>().GetComponentsInChildren<Renderer>()[0].material.mainTexture = (Texture)Resources.Load("PokeCardPictures/" + picture[num].ToString(), typeof(Texture2D));
                num = (int)UnityEngine.Random.Range(0, picture.Count);
            }
            initialflag = true;

        }


        foreach (GameObject cardy in cardB)
        {
            if (picture.Count > 0)
            {
                if (cardnumberB != 3 && GameController.timer > 1.5f && GameController.hero == Hero.HeroB)
                {
                    if (GameController.hero == Hero.HeroB && cardy.tag == "temp" && callflagB == 0)  
                    {
                        cardy.GetComponent<BattleB>().hp += 10;
                        //卡牌上数值要保持的更新
                        cardy.GetComponent<BattleB>().GetComponentsInChildren<UILabel>()[1].text = cardy.GetComponent<BattleB>().hp.ToString();
                        //贴图改变
                        cardy.GetComponent<BattleB>().GetComponentsInChildren<Renderer>()[0].material.mainTexture = (Texture)Resources.Load("PokeCardPictures/" + picture[num].ToString(), typeof(Texture2D));
                        //cardx.GetComponent<BattleA>().GetComponentsInChildren<Renderer>()[0].material.mainTextureScale = new Vector2(-1, 1);                    //贴图出现反向情况


                        if (picture != null)
                        {
                            picture.RemoveAt(num);
                            num = Random.Range(0, picture.Count);
                        }
                        //                    Invoke("Update", 3.0f);
                        cardy.SetActive(true);
                        cardy.GetComponent<BattleB>().showflag = true;
                        cardy.GetComponent<BattleB>().tag = "CardB";
                        cardy.GetComponent<BattleB>().flag = 1;
//                        cardy.AddComponent<BattleB>();   //暂时隐藏：添加脚本
                        print("召唤神奇宝贝！");
                        callflagB++;
                        cardnumberB++;
                    }
                }
            }
            else
                print("敌方B卡牌已全部召唤完，无法再继续召唤。");
        }
        callflagB = 0;
    }
}
