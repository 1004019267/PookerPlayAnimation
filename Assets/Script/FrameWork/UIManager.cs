using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : SingletonM<UIManager>
{
    Button btnSitDown;
    Button btnDealing;
    Button btnLeftPlay;
    Button btnRightPlay;
    Button btnTopPlay;
    Button btnOverTurn;
    GameManager gm;
    CardControl cc;
    CardManager cm;

    Text[] cardNum = new Text[4];//记录卡牌数量

    bool isLook = false;    
    // Use this for initialization
    void Start()
    {
        gm = GetComponent<GameManager>();
        cc = GetComponent<CardControl>();
        cm = GetComponent<CardManager>();
        btnSitDown = transform.Find("Btn/SitDown").GetComponent<Button>();
        btnDealing = transform.Find("Btn/Dealing").GetComponent<Button>();
        btnLeftPlay = transform.Find("Btn/LeftPlay").GetComponent<Button>();
        btnRightPlay = transform.Find("Btn/RightPlay").GetComponent<Button>();
        btnTopPlay = transform.Find("Btn/UpPlay").GetComponent<Button>();
        btnOverTurn = transform.Find("Btn/OverTurn").GetComponent<Button>();

        for (int i = 0; i < cardNum.Length; i++)
        {
            cardNum[i] = transform.Find("CardNum/" + (i + 1).ToString() + "/Text").GetComponent<Text>();
        }

        btnSitDown.onClick.AddListener(OnBtnSitDown);
        btnDealing.onClick.AddListener(OnBtnDealing);
        btnLeftPlay.onClick.AddListener(OnBtnLeftPlay);
        btnRightPlay.onClick.AddListener(OnBtnRightPlay);
        btnTopPlay.onClick.AddListener(OnBtnTopPlay);
        btnOverTurn.onClick.AddListener(OnBtnOverTurn);
    }

    private void OnDestroy()
    {
        for (int i = 0; i < PlayerManager.instance.GetAllPlayer().Count; i++)
        {
            //PlayerManager.instance.GetPlayer(i).CardCount -= SetCardNumText;
        }
    }

    void OnBtnSitDown()
    {
        btnSitDown.interactable = false;
        btnDealing.interactable = true;
        gm.CreatPlayer();
        for (int i = 0; i < PlayerManager.instance.GetAllPlayer().Count; i++)
        {
            //PlayerManager.instance.GetPlayer(i).CardCount += SetCardNumText;
        }
    }

    void OnBtnDealing()
    {
        btnDealing.interactable = false;
        StartCoroutine(LicensedAnimation.Instance.GiveCardAct());
        PlayerManager.instance.GetPlayer(1).bStart = true;
        PlayerManager.instance.GetPlayer(1).handCard.bStart=true;
        PlayerManager.instance.GetPlayer(0).bStart = true;
        PlayerManager.instance.GetPlayer(0).handCard.bStart = true;
        //cc.SetNowPlayer(PlayerManager.instance.GetPlayer(1));
        for (int i = 0; i < cardNum.Length; i++)
        {
            cardNum[i].transform.parent.gameObject.SetActive(true);
        }
        btnOverTurn.interactable = true;
    }

    void OnBtnLeftPlay()
    {
        var p = PlayerManager.instance.GetPlayer(0);
        //if (cm.HitFirstFewCard(p.GetAllCard(), p.GetAllOutHitCard(),5) == 0) return;
        //cm.OutCard(p.GetAllCard(), p.GetAllOutHitCard(), gm.hitCardPos[0], gm.playerLookCardPos[0].position, 2.5f);
        //cm.CancelBackCard(p.GetAllBlackCard(),5);
        //cm.SetCardPos(p.GetAllBlackCard(), gm.leftPos, Vector3.left, 1, 1);
        HitCardControl.Instance.SetState(ePlayer.p2);
    }

    void OnBtnRightPlay()
    {
        var p = PlayerManager.instance.GetPlayer(2) ;
        //if (cm.HitFirstFewCard(p.GetAllCard(), p.GetAllOutHitCard(), 6) == 0) return;
        //cm.OutCard(p.GetAllCard(), p.GetAllOutHitCard(), gm.hitCardPos[2], gm.playerLookCardPos[1].position, 2.5f);
        //cm.CancelBackCard(p.GetAllBlackCard(),6);
        //cm.SetCardPos(p.GetAllBlackCard(), gm.rightPos, Vector3.right, 1, 1);
        HitCardControl.Instance.SetState(ePlayer.p4);
    }

    void OnBtnTopPlay()
    {
        var p = PlayerManager.instance.GetPlayer(3) ;
        //if (cm.HitFirstFewCard(p.GetAllCard(), p.GetAllOutHitCard(), 4) == 0) return;
        //cm.OutCard(p.GetAllCard(), p.GetAllOutHitCard(), gm.hitCardPos[3], gm.playerLookCardPos[2].position, 2.5f);
        //cm.CancelBackCard(p.GetAllBlackCard(), 4);
        //cm.SetCardPos(p.GetAllBlackCard(), gm.topPos, Vector3.right, 2.5f, 0);
        HitCardControl.Instance.SetState(ePlayer.p1);
    }
    /// <summary>
    /// 根据不用的string 切换不同人物的出牌权限
    /// </summary>
    /// <param name="which"></param>
    public void SetBtnInter(string which)
    {
        btnLeftPlay.interactable = false;
        btnRightPlay.interactable = false;
        btnTopPlay.interactable = false;
        cc.isHit = false;
        switch (which)
        {
            case "l":
                btnLeftPlay.interactable = true;
                break;
            case "r":
                btnRightPlay.interactable = true;
                break;
            case "t":
                btnTopPlay.interactable = true;
                break;
            case "d":
                cc.isHit = true;
                break;
        }
    }
    /// <summary>
    /// 查看别的牌组 或者 隐藏别人的牌组
    /// </summary>
    void OnBtnOverTurn()
    {
        //cm.LookOtherCard(PlayerManager.instance.GetPlayer(0).GetAllCard(), isLook);
        //cm.LookOtherCard(PlayerManager.instance.GetPlayer(2).GetAllCard(), isLook);
        //cm.LookOtherCard(PlayerManager.instance.GetPlayer(3).GetAllCard(), isLook);
        isLook = !isLook;
    }
    /// <summary>
    /// 设置玩家牌组数量显示
    /// </summary>
    /// <param name="p"></param>
    /// <param name="count"></param>
    public void SetCardNumText(int count)
    {
        //cardNum[id].text = count.ToString();
    }
}
