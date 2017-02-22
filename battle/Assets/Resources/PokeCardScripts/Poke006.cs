using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poke006 : BattleA {

    // Use this for initialization
    public override void setSingle()
    {

        attack = 20;
        hp = 40;
        GetComponentsInChildren<Renderer>()[0].material.mainTexture = (Texture)Resources.Load("PokeCardPictures/001", typeof(Texture2D));

    }

    // Update is called once per frame
    public override void reset()
    {
        GetComponentsInChildren<Renderer>()[0].material.color = Color.white;
        choice = 0;
        flag = 1;
        if (hp > 4)
        {
            hp += 1;
            this.GetComponentsInChildren<UILabel>()[1].text = hp.ToString();
            print("妙蛙草的叶绿素特性发动，上场就回复生命!!!");
        }
        other = null;
    }
}
