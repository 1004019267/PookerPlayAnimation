using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
class LicensedAnimation : SingletonM<LicensedAnimation>
{
    UnityEngine.Object[] cardStack;//牌堆材质
    Image cardStackImage;//当前某个排堆

    float interval = 0.1f;//生成牌间隔
    float giveCardfOffect = 20;//出牌动画差值

    //出牌动画开始位置
    Transform left;
    Transform top;
    Transform right;
    Transform down;

    GameObject panle;//出牌处的遮罩

    CardControl cc;

    private void Start()
    {
        cardStack = Resources.LoadAll("ani/deal");
        cardStackImage = transform.Find("CardStack").GetComponent<Image>();
        panle = transform.Find("Panle").gameObject;
        cc = GetComponent<CardControl>();

        left = transform.Find("Left");
        top = transform.Find("Up");
        right = transform.Find("Right");
        down = transform.Find("Down");
    }

    /// <summary>
    /// 发牌动画
    /// </summary>
    /// <returns></returns>
    public IEnumerator GiveCardAct()
    {
        left.gameObject.SetActive(true);
        top.gameObject.SetActive(true);
        right.gameObject.SetActive(true);
        down.gameObject.SetActive(true);
        for (int i = 0; i < cardStack.Length; i++)
        {
            if (i % 2 == 0)
            {
                left.DOMove(left.position + Vector3.left * giveCardfOffect, interval * 0.5f);
                right.DOMove(right.position + Vector3.right * giveCardfOffect, interval * 0.5f);
                top.DOMove(top.position + Vector3.up * giveCardfOffect, interval * 0.5f);
                down.DOMove(down.position + Vector3.down * giveCardfOffect, interval * 0.5f);
            }
            else
            {
                left.DOMove(left.position - Vector3.left * giveCardfOffect, interval * 0.5f);
                right.DOMove(right.position - Vector3.right * giveCardfOffect, interval * 0.5f);
                top.DOMove(top.position - Vector3.up * giveCardfOffect, interval * 0.5f);
                down.DOMove(down.position - Vector3.down * giveCardfOffect, interval * 0.5f);
            }

            cardStackImage.overrideSprite = cardStack[i] as Sprite;
            yield return new WaitForSeconds(interval);
        }
        cardStackImage.gameObject.SetActive(false);
        left.gameObject.SetActive(false);
        top.gameObject.SetActive(false);
        right.gameObject.SetActive(false);
        down.gameObject.SetActive(false);
        panle.SetActive(false);
        cc.isStart = true;
        cc.SetAllBtn();
        HitCardControl.Instance.isStartHit = true;
    }
}

