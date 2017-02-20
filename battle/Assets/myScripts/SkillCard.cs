using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCard : MonoBehaviour {

    // Use this for initialization
   
    string CardName;
    bool isUsed;//标记单体类卡片是否已经成功选择目标；
    public int choice;
	void Start ()
    {
        //初始化，未被选择，未成功选择目标
        choice = 0;
        isUsed = false;
        //改变贴图
        Change();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
  public virtual void OnMouseDown()
    {
        GameObject[] SkillCard = GameObject.FindGameObjectsWithTag(tag);
        int flag = 0;
        foreach (GameObject a in SkillCard)
        {
            if (a.GetComponent<SkillCard>().choice == 1)
            {
                flag = 1;
                break;
            }
        }
        if(flag==0)
        {
            GetComponentsInChildren<Renderer>()[0].material.color = Color.green;
            choice = 1;
            Effect();
            
        }
    }
    //改变贴图
    public virtual void Change()
    {


    }
    //卡牌的具体效果
    public virtual void Effect()
    { }
    //鼠标移上去时显示卡牌的具体信息
    void OnMouseEnter()
    {
        print("检测到移入");
    }
    void OnMouseExit()
    {
        print("检测到移出");
    }
}
