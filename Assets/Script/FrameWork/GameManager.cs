using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    int playerLength = 4;//玩家个数
    System.Random r = new System.Random();
    int[] roleNum;//角色个数

    //不同牌的模型
    Transform leftCard;
    Transform rightCard;
    Transform topCard;
    Transform myCard;

    Transform[] playerCardPos = new Transform[4];//其他人是背景牌 自己是面朝牌
    public Transform[] playerLookCardPos = new Transform[3];//看其他玩家牌出现的位置
    public Transform[] hitCardPos = new Transform[4];//出牌位置

    UnityEngine.Object[] sprites;//牌资源

    float interval = 0.1f;//生成牌间隔

    //手牌位置
    public Vector3 downPos;
    public Vector3 leftPos;
    public Vector3 rightPos;
    public Vector3 topPos;

    CardManager cm;

    List<PokerData> cardData;

    Material ma;//选中的阴影的材质

    // Use this for initialization
    void Start()
    {
        sprites = Resources.LoadAll("data/cardv");
        ma = Resources.Load("data/shadowM") as Material;

        roleNum = new int[playerLength];
        InitRole();

        leftCard = transform.Find("Player/LeftCard");
        rightCard = transform.Find("Player/RightCard");
        topCard = transform.Find("Player/TopCard");
        myCard = transform.Find("Player/MyCard");

        for (int i = 0; i < 4; i++)
        {
            playerCardPos[i] = transform.Find("Player/" + "Card" + (i + 1).ToString());
        }

        for (int i = 0; i < 3; i++)
        {
            playerLookCardPos[i] = transform.Find("Player/" + "LookCard" + (i + 1).ToString());
        }

        for (int i = 0; i < hitCardPos.Length; i++)
        {
            hitCardPos[i] = transform.Find("HitCardPos/" + (i + 1).ToString());
        }

        downPos = new Vector3(Screen.width / 2 + Screen.width % 2, myCard.position.y);
        topPos = new Vector3(Screen.width / 2 + Screen.width % 2, topCard.position.y);
        leftPos = new Vector3(leftCard.position.x, Screen.height / 2 + Screen.height % 2 - 10);
        rightPos = new Vector3(rightCard.position.x, Screen.height / 2 + Screen.height % 2 - 20);

        cm = GetComponent<CardManager>();
    }

    public void GiveCard()
    {
    }

    //IEnumerator CreatCard(BasePlayer p)
    //{
    //    for (int i = 0; i < 17; i++)
    //    {
    //        CreatLookCard(p, playerCardPos[1], 0.8f, i, true);
    //        cm.SetCardPos(p.GetAllCard(), downPos, Vector3.right, i, 5f,0);
    //        yield return new WaitForSeconds(interval * 1.6f);
    //    }
    //}

    //IEnumerator CreatCardTop(BasePlayer p)
    //{
    //    for (int i = 0; i < 17; i++)
    //    {
    //        CreatBlackCard(p as OtherPlayer, topCard, playerCardPos[3], i);
    //        cm.SetCardPos((p as OtherPlayer).GetAllBlackCard(), topPos, Vector3.right, i, 2.5f,0);
    //        CreatLookCard(p, playerLookCardPos[2], 0.5f, i, false);
    //        cm.SetCardPos(p.GetAllCard(), playerLookCardPos[2].position, Vector3.right, i, 2.5f,0);
    //        yield return new WaitForSeconds(interval * 1.6f);
    //    }
    //}

    //IEnumerator CreatCardLeft(BasePlayer p)
    //{
    //    for (int i = 0; i < 17; i++)
    //    {
    //        CreatBlackCard(p as OtherPlayer, leftCard, playerCardPos[0], i);
    //        cm.SetCardPos((p as OtherPlayer).GetAllBlackCard(), leftPos, Vector3.left, i, 1, 1);
    //        CreatLookCard(p, playerLookCardPos[0], 0.5f, i, false);
    //        cm.SetCardPos(p.GetAllCard(), playerLookCardPos[0].position, Vector3.right, i, 2.5f);
    //        yield return new WaitForSeconds(interval * 1.6f);
    //    }
    //}

    //IEnumerator CreatCardRight(BasePlayer p)
    //{
    //    for (int i = 0; i < 17; i++)
    //    {
    //        CreatBlackCard(p as OtherPlayer, rightCard, playerCardPos[2], i);
    //        cm.SetCardPos((p as OtherPlayer).GetAllBlackCard(), rightPos, Vector3.right, i, 1, 1);
    //        CreatLookCard(p, playerLookCardPos[1], 0.5f, i, false);
    //        cm.SetCardPos(p.GetAllCard(), playerLookCardPos[1].position, Vector3.right, i, 2.5f);
    //        yield return new WaitForSeconds(interval * 1.6f);
    //    }
    //}

    ///// <summary>
    ///// 为某个玩家创建一张正面的卡
    ///// </summary>
    ///// <param name="p"></param>
    ///// <param name="playerLookCardPos"></param>
    ///// <param name="scale"></param>
    ///// <param name="i"></param>
    ///// <param name="isLook"></param>
    //void CreatLookCard(BasePlayer p, Transform playerLookCardPos, float scale, int i, bool isLook)
    //{
    //    var go = Instantiate(myCard).gameObject;
    //    go.transform.SetParent(playerLookCardPos);
    //    go.transform.localScale = Vector3.one * scale;
    //    go.name = i.ToString();
    //    go.GetComponent<Image>().overrideSprite = sprites[i + 1] as Sprite;
    //    go.SetActive(isLook);
    //    p.AddCard(go);
    //}

    ///// <summary>
    ///// 为某个玩家创建一张背朝向的卡
    ///// </summary>
    //void CreatBlackCard(OtherPlayer p, Transform midPos, Transform playerCardPos, int i)
    //{
    //    var go = Instantiate(midPos).gameObject;
    //    go.transform.SetParent(playerCardPos);
    //    go.transform.localScale = Vector3.one * 0.5f;
    //    p.AddBlackCard(go);
    //}

    /// <summary>
    /// 设置是哪个角色
    /// </summary>  
    void InitRole()
    {
        int num;
        for (int i = 0; i < roleNum.Length; i++)
        {
            num = r.Next(0, 24);
            if (!roleNum.Contains(num))
            {
                roleNum[i] = num;
            }
        }
    }
    /// <summary>
    /// 初始化玩家
    /// </summary>
    public void CreatPlayer()
    {
        for (int i = 0; i < playerLength; i++)
        {
            var im = transform.Find("Player/" + (i + 1).ToString()).GetComponent<Image>();
            im.gameObject.SetActive(true);
            BasePlayer p = new BasePlayer();
            p.id = i;
            switch (i)
            {
                case 0:
                    p.act.Init(im, roleNum[i], "l");
                    p.handCard = new HandCard(sprites,ma, playerLookCardPos[0], myCard,playerLookCardPos[0].position, Vector3.right, 2.5f, 0);
                    break;
                case 1:
                    p.act.Init(im, roleNum[i], "l");
                    p.handCard = new HandCard(sprites, ma, playerCardPos[i], myCard, downPos, Vector3.right, 5f, 0);
                    p.sendCard = new HandCard(sprites, ma, hitCardPos[i], myCard, hitCardPos[i].position, Vector3.right, 5f, 0);
                    break;
                case 2:
                    break;
                case 3:
                    break;
                default:
                    break;
            }
            for (int j = 0; j < 17; j++)
            {
                p.handCard.Add(j);
            }
            PlayerManager.instance.AddPlayer(p);
            //if (i == 1)
            //{

            //    p.act.Init(im, roleNum[i], "l");
            //    p.handCard = new HandCard(sprites, ma, playerCardPos[i], myCard, downPos, Vector3.right, 5f, 0);
            //    p.sendCard = new HandCard(sprites, ma, hitCardPos[i], myCard, hitCardPos[i].position, Vector3.right, 5f, 0);

            //}
            //else
            //{
            //    p.id = i;
            //    if (i <= 1)
            //    {
            //        p.act.Init(im, roleNum[i], "l");
            //    }
            //    else
            //    {
            //        p.act.Init(im, roleNum[i], "r");
            //    }
            //    PlayerManager.instance.AddPlayer(p);
            //}
        }
    }

    // Update is called once per frame
    void Update()
    {
        PlayerManager.instance.Update();
    }
}
