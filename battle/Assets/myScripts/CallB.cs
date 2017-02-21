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
//        i = picture.Count;             
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
                picture.RemoveAt(num);
                print(picture.Count);
                num = (int)UnityEngine.Random.Range(0, picture.Count);
            }
            initialflag = true;

        }
        foreach (GameObject cardy in cardB)
        {
            if (cardnumberB!=3 && GameController.timer>1.5f)
           {
                if ((cardy.GetComponent<BattleB>().hp <= 0) && cardy.GetComponent<BattleB>().showflag == false && callflagB == 0)    //如果碰撞的点所在的物体的名字是“StartButton”(collider就是检测碰撞所需的碰撞器)
                {
                    cardy.GetComponent<BattleB>().hp += 20;
                    //卡牌上数值要保持的更新
                    cardy.GetComponent<BattleB>().GetComponentsInChildren<UILabel>()[1].text = cardy.GetComponent<BattleB>().hp.ToString();
                    //贴图改变
                    cardy.GetComponent<BattleB>().GetComponentsInChildren<Renderer>()[0].material.mainTexture = (Texture)Resources.Load("PokeCardPictures/" + picture[num].ToString(), typeof(Texture2D));
                    //                        cardx.GetComponent<BattleA>().GetComponentsInChildren<Renderer>()[0].material.mainTextureScale = new Vector2(-1, 1);                    //贴图出现反向情况


                    if (picture != null)
                    {
                        picture.RemoveAt(num);
                        //i = picture.Count;   // 每次remove后的size都会发生变化，但是迭代基数没有根据remove后的size动态调整，导致越界及集合遍历不完全。 
                        //i--;
                        num = Random.Range(0, picture.Count);
                    }
                    //                    Invoke("Update", 3.0f);
                    cardy.SetActive(true);
                    cardy.GetComponent<BattleB>().showflag = true;
                    cardy.GetComponent<BattleB>().tag = "CardB";
                    print("召唤神奇宝贝！");
                    callflagB++;
                    cardnumberB++;
                }
            }
        }
        callflagB = 0;
    }
}
