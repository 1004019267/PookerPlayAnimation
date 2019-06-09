using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class HandCard
{
    List<PokerData> cardDatas = new List<PokerData>();
    List<GameObject> cards = new List<GameObject>();
    UnityEngine.Object[] sprites;
    Material ma;
    Transform cardPos;//牌的父节点
    Transform card;//牌的样板
    Vector3 midPos;//牌的中心点
    Vector3 face;//产生朝向
    float xOffset;//x偏移量
    float yOffset;//y偏移量

    float t;//计时
    float lastT;
    float interval = 0.16f;//生成间隔
    int i = 0;//循环计数
    public bool bStart;//是否开始

    public event System.Action<int> CardCount;

    public HandCard(UnityEngine.Object[] sprites, Material ma, Transform cardPos, Transform card, Vector3 midPos, Vector3 face, float xOffset, float yOffset)
    {
        this.sprites = sprites;
        this.ma = ma;
        this.cardPos = cardPos;
        this.card = card;
        this.midPos = midPos;
        this.face = face;
        this.xOffset = xOffset;
        this.yOffset = yOffset;
    }

    public void Add(int iCardIndex)
    {
        PokerData pd = new PokerData();
        pd.iCardIndex = iCardIndex;
        pd.sprite = sprites[iCardIndex + 1] as Sprite;
        pd.bShadow = false;
        cardDatas.Add(pd);
        //CardCount(cardDatas.Count);
    }

    public void Remove(int iCardIndex)
    {
        for (int i = 0; i < cardDatas.Count; i++)
        {
            if (cardDatas[i].iCardIndex == iCardIndex)
            {
                cardDatas.Remove(cardDatas[i]);
                CardCount(cardDatas.Count);
            }
        }
    }
    public void CreateCard(bool bActive, float scale)
    {
        if (bStart)
        {
            t += Time.deltaTime;
            if (t - lastT >= interval)
            {
                var go = GameObject.Instantiate(card).gameObject;
                go.transform.SetParent(cardPos);
                go.transform.localScale = Vector3.one * scale;
                go.name = cardDatas[i].iCardIndex.ToString();
                go.GetComponent<Image>().overrideSprite = cardDatas[i].sprite;
                go.SetActive(bActive);
                cards.Add(go);

                cards[i].transform.position = midPos - face * i * xOffset - Vector3.down * i * yOffset;
                for (int j = 0; j < cards.Count; j++)
                {
                    cards[j].transform.position += face * xOffset + Vector3.down * yOffset;
                    cards[j].transform.SetAsFirstSibling();
                }
                lastT = t;
                i++;
                if (i == cardDatas.Count)
                {
                    bStart = false;
                    i = 0;
                }
            }
        }
    }

    public void SendPoker(int iCardIndex)
    {
        for (int i = 0; i < cardDatas.Count; i++)
        {
            if (cards[i].name == iCardIndex.ToString())
            {
                cards.Remove(cards[i]);
            }
        }
        Remove(iCardIndex);
        Refresh();
    }

    public void Refresh()
    {
        for (int i = 0; i < cards.Count; i++)
        {
            cards[i].transform.position = midPos - face * i * xOffset - Vector3.down * i * yOffset;
            for (int j = 0; j < cards.Count; j++)
            {
                cards[j].transform.position += face * xOffset + Vector3.down * yOffset;
                cards[j].transform.SetAsFirstSibling();
            }
        }
    }

    public void Clear()
    {
        cards.Clear();
    }
}

