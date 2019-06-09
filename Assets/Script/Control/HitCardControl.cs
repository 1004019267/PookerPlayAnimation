using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ePlayer
{
    p1 = 0,
    p2,
    p3,
    p4
}
public class HitCardControl : SingletonM<HitCardControl>
{
    ePlayer hitPlayer;//当前出牌角色
    TimeClock tc;
    Transform[] clockPos = new Transform[4];//闹钟出现的位置坐标
    float t;
    float maxT = 13;//倒计时最大时间
    BasePlayer nowPlayer;//当前玩家
    public bool isStartHit;//启动开始出牌控制
    GameManager gm;
    CardManager cm;
    // Use this for initialization
    void Start()
    {
        t = maxT;
        gm = GetComponent<GameManager>();
        cm = GetComponent<CardManager>();
        tc = transform.Find("Clock").GetComponent<TimeClock>();
        for (int i = 0; i < clockPos.Length; i++)
        {
            clockPos[i] = transform.Find("ClockPos/" + (i + 1).ToString());
        }
    }

    private void Update()
    {
        if (isStartHit)
        {
            WhoHitCard();
        }
    }
    /// <summary>
    /// 谁出牌状态机
    /// </summary>
    public void WhoHitCard()
    {
        //switch (hitPlayer)
        //{
        //    case ePlayer.p1:
        //        SetState("l", ePlayer.p2, 0, gm.playerLookCardPos[0].position, 2.5f);
        //        cm.SetCardPos(nowPlayer.GetAllBlackCard(), gm.leftPos, Vector3.left, 1, 1);
        //        break;
        //    case ePlayer.p2:
        //        SetState("d", ePlayer.p3, 1, gm.downPos, 5f);
        //        break;
        //    case ePlayer.p3:
        //        SetState("r", ePlayer.p4, 2, gm.playerLookCardPos[1].position, 2.5f);
        //        cm.SetCardPos(nowPlayer.GetAllBlackCard(), gm.rightPos, Vector3.right, 1, 1);
        //        break;
        //    case ePlayer.p4:
        //        SetState("t", ePlayer.p1, 3, gm.playerLookCardPos[2].position, 2.5f);
        //        cm.SetCardPos(nowPlayer.GetAllBlackCard(), gm.topPos, Vector3.right, 2.5f,0);
        //        break;
        //}
    }
    /// <summary>
    /// 设置自动出牌的状态切换
    /// </summary>
    /// <param name="btnInt"></param>
    /// <param name="count"></param>
    /// <param name="p1"></param>
    /// <param name="outCard"></param>
    /// <param name="offset"></param>
    void SetState(string btnInt, ePlayer p, int count, Vector3 outCard, float offset)
    {
        //UIManager.Instance.SetBtnInter(btnInt);
        //nowPlayer = PlayerManager.instance.GetPlayer(count);
        //if (CountDown(clockPos[count].position) == 1)
        //{
        //    cm.RandomOneHitCard(nowPlayer.GetAllCard(),nowPlayer.GetAllOutHitCard());
        //    cm.OutCard(nowPlayer.GetAllCard(), nowPlayer.GetAllOutHitCard(),gm.hitCardPos[count], outCard, offset);
        //    if (btnInt != "d")
        //    {
        //       cm.CancelBackCard((nowPlayer as OtherPlayer).GetAllBlackCard(), 1);
        //    }
        //    hitPlayer = p;
        //}
    }
    /// <summary>
    /// 强制切换玩家状态 
    /// </summary>
    /// <param name="p"></param>
    public void SetState(ePlayer p)
    {
        t = maxT;
        hitPlayer = p;
    }
    /// <summary>
    /// 倒计时 加显示闹钟
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>
    int CountDown(Vector3 pos)
    {
        tc.SetClockPos(pos);
        t -= Time.deltaTime;
        //Debug.Log(t);
        tc.ClockLook((int)t);
        if (t <= 0)
        {
            t = maxT;
            return 1;
        }
        return 0;
    }
}
