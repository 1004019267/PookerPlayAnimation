using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePlayer
{

    public int id;
    public PlayerAct act = new PlayerAct();
    public HandCard handCard;//手牌
    public HandCard sendCard;//准备出的牌 
    public HandCard backCard;//背面牌
    public bool bStart;
    // Use this for initialization
    public virtual void Start()
    {

    }

    // Update is called once per frame
    public virtual void Update()
    {
        act.WhichState();
        if (bStart == true)
        {
            handCard.CreateCard(true, 1);
            sendCard.CreateCard(true, 0.5f);
        }
    }
}
