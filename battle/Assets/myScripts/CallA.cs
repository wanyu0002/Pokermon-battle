using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CallA : MonoBehaviour
{
    static public int callflagA;                       //当前召唤次数 
    GameObject[] cardA;
    static public List<string> picture = new List<string>();    //必须要用string类型，不然很多地方要改= =
    static public int num;                      //图片编号

    public bool initialflag;//第一次的初始化

    void Start()
    {
        cardA = GameObject.FindGameObjectsWithTag("CardA");
//        foreach (string item in GameObject.Find("HeroA").GetComponent<HeroShana>().Pokemon)
//        {
//            picture.Add(item);
//        }
        //       picture.Add("001");
          //      picture.Add("002");
            //    picture.Add("003");
              //  picture.Add("004");
        //        picture.Add("006");
        //        picture.Add("007");
        num = (int)UnityEngine.Random.Range(0, picture.Count);    //  Random.Range()包前不包后
        callflagA = 0;
        initialflag = false;
    }

    void Update()
    {
        foreach (string item in GameObject.Find("HeroA").GetComponent<HeroShana>().Pokemon)
            picture.Add(item);
//        picture = GameObject.Find("HeroA").GetComponent<HeroShana>().Pokemon;
        if (initialflag==false)             //第一次读取前三张卡牌
        {

            foreach(GameObject cardx in cardA)
            {
                cardx.AddComponent(Type.GetType("Poke" + picture[num].ToString()));
                cardx.GetComponentsInChildren<Renderer>()[0].material.mainTexture = (Texture)Resources.Load("PokeCardPictures/" + picture[num].ToString(), typeof(Texture2D));
                picture.RemoveAt(num);
                num = (int)UnityEngine.Random.Range(0, picture.Count);
            }
            initialflag = true;

        }
        foreach (GameObject cardx in cardA)
        {
            if (Input.GetMouseButtonDown(0))
            {    //首先判断是否点击了鼠标左键
                if (picture.Count > 0)
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);    //定义一条射线，这条射线从摄像机屏幕射向鼠标所在位置
                    RaycastHit hit;    //声明一个碰撞的点(暂且理解为碰撞的交点)
                    if (Physics.Raycast(ray, out hit))    //如果真的发生了碰撞，ray这条射线在hit点与别的物体碰撞了
                    {
                        if (GameController.hero == Hero.HeroA && hit.collider.gameObject.name == "PokeBallA" && cardx.tag=="temp" && callflagA == 0)    //如果碰撞的点所在的物体的名字是“StartButton”(collider就是检测碰撞所需的碰撞器)
                        {
                            
                            cardx.AddComponent(Type.GetType("Poke" + picture[num].ToString()));   //添加脚本
                            cardx.GetComponent<BattleA>().hp += 20;
                            //卡牌上数值要保持的更新
                            cardx.GetComponent<BattleA>().GetComponentsInChildren<UILabel>()[1].text = cardx.GetComponent<BattleA>().hp.ToString();
                            //贴图改变
                            cardx.GetComponent<BattleA>().GetComponentsInChildren<Renderer>()[0].material.mainTexture = (Texture)Resources.Load("PokeCardPictures/" + picture[num].ToString(), typeof(Texture2D));
                            if (picture != null)
                            {
                                picture.RemoveAt(num);
                                num = (int)UnityEngine.Random.Range(0, picture.Count);
                            }
                            cardx.SetActive(true);
                            cardx.GetComponent<BattleA>().showflag = true;
                            cardx.GetComponent<BattleA>().tag = "CardA";
                            cardx.GetComponent<BattleA>().flag = 1;                                 //(oo:new)
                            GameObject model = Instantiate(Resources.Load("PokemonModels/"+"model" + picture[num].ToString()) as GameObject);//添加模型
                            model.transform.position = cardx.transform.position;
                            model.transform.parent = cardx.transform;
                            print("召唤神奇宝贝！");
                            callflagA++;
                            Invoke("Update", 5f);
                        }
                    }
                }
                else
                    print("玩家A卡牌已全部召唤完，无法再继续召唤。");
            }    
        }
        callflagA = 0;
    }
}
