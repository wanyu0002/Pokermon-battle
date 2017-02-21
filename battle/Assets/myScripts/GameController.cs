using UnityEngine;
using System.Collections;
public enum GameState
{
    CardGenerating,//系统随机发放卡牌的阶段
    BeforePlay,//出牌前的程序准备阶段
    PlayCard,//出牌对战阶段
    End,//游戏回合清算阶段
}
public enum Hero
{
    HeroA,
    HeroB
}


public class GameController : MonoBehaviour {

    public CardGenerator cardGenerator;
    GameState gameState;
    Hero hero;//指示当前行动的英雄

    //时间相关
    float cycletime = 60f;
    float timer;

    //当前场上双方的宝可梦集合
    GameObject[] CardA;
    GameObject[] CardB;

    int CardReady;

    // Use this for initialization
    void Start()
    {
       
        timer = 0;//计时器初始化为0 
        cardGenerator = this.GetComponent<CardGenerator>();//初始化发牌器
        CardReady=0;
        hero = Hero.HeroA;   //从A开始行动
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(CardReady==0)
        {
            for (int i = 0; i < 3; i++)
            {
                cardGenerator.GenerateSkillCard(Hero.HeroA);               
                cardGenerator.GenerateSkillCard(Hero.HeroB);
            }
            CardReady = 1;
        }
        //抽牌阶段
        if (gameState == GameState.CardGenerating)
        {
            cardGenerator.GenerateSkillCard(hero);
            gameState = GameState.BeforePlay;
        }
        
        //行动阶段
        if(gameState == GameState.BeforePlay)
        {
            if (hero == Hero.HeroA)
            {
                print("玩家A行动\n");
                //找到所有英雄A在场上的牌，让它们都变成可以发动攻击的状态
                CardA = GameObject.FindGameObjectsWithTag("CardA");                
                foreach (GameObject i in CardA)
                {
                    i.GetComponent<BattleA>().flag = 0;
                }
               
            }
            if(hero==Hero.HeroB)
            {
                print("玩家B行动\n");
                //找到所有英雄B在场上的牌
                CardB = GameObject.FindGameObjectsWithTag("CardB");
                foreach (GameObject i in CardB)
                {
                    i.GetComponent<BattleB>().flag = 0;
                }
            }
            gameState = GameState.PlayCard;

        }
        if(gameState==GameState.PlayCard)
        {
           if(hero==Hero.HeroA)
            {
                timer += Time.deltaTime;
                int flag = 0;//检测是否所有都攻击过，若都攻击则将提前结束回合
                CardA = GameObject.FindGameObjectsWithTag("CardA");//重新获取当前存货宝可梦，避免对已被打败的宝可梦执行操作
                foreach (GameObject i in CardA)
                {
                    if (i.GetComponent<BattleA>().flag == 0)//若还有宝可梦没有攻击
                        flag = 1;
                }
                if(flag==0)//若都攻击了，则提前结束回合
                    gameState = GameState.End;
                if (timer>cycletime)
                {
                    print("玩家A时间用尽\n");
                    gameState = GameState.End;
                }
                cardGenerator.CleanUp(hero);
            }
            if (hero == Hero.HeroB)
            {
                timer += Time.deltaTime;
             
                CardB = GameObject.FindGameObjectsWithTag("CardB");
                //调用其攻击函数，选择目标发动攻击
                int i;
                for(i=0;i<CardB.Length;i++)
                {
                    if (timer > 3 * i+1)
                        CardB[i].GetComponent<BattleB>().fight();
                }
                if (timer >= 3*i+2)//所有B类牌都攻击过后，
                    gameState = GameState.End;

                cardGenerator.CleanUp(hero);
            }
               
        }
        if(gameState==GameState.End)
        {
            timer = 0;  //计时器清0
            CardA = GameObject.FindGameObjectsWithTag("CardA");
            CardB = GameObject.FindGameObjectsWithTag("CardB");

            if (CardA.Length == 0)
                print("玩家A战败\n");
            else if (CardB.Length == 0)
                print("玩家B战败\n");
            else
            {
                TransHero();
                gameState = GameState.CardGenerating;
            }                
         }
              
    }
    
    //转换当前行动英雄
    void TransHero()
    {
        if (hero == Hero.HeroA)
            hero = Hero.HeroB;
        else if (hero == Hero.HeroB)
            hero = Hero.HeroA;
    }

   
}
