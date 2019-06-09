using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardManager :MonoBehaviour
{    
    System.Random r = new System.Random();
    // /<summary>
    /// 设置牌的位置
    /// </summary>
    /// <param name="card"></param>
    /// <param name="midPos"></param>
    /// <param name="face"></param>
    /// <param name="i"></param>
    /// <param name="xOffset"></param>
    /// <param name="yOffset"></param>
    public void SetCardPos(List<GameObject> card, Vector3 midPos, Vector3 face, int i, float xOffset, float yOffset)
    {
        card[i].transform.position = midPos - face * i * xOffset - Vector3.down * i * yOffset;
        for (int j = 0; j < card.Count; j++)
        {
            card[j].transform.position += face * xOffset + Vector3.down * yOffset;
            card[j].transform.SetAsFirstSibling();
        }
    }

    public void SetCardPos(List<GameObject> card, Vector3 midPos, Vector3 face, float xOffset, float yOffset)
    {
        for (int i = 0; i < card.Count; i++)
        {
            card[i].transform.position = midPos - face * i * xOffset - Vector3.down * i * yOffset;
            for (int j = 0; j < card.Count; j++)
            {
                card[j].transform.position += face * xOffset + Vector3.down * yOffset;
                card[j].transform.SetAsFirstSibling();
            }
        }
    }

    /// <summary>
    /// 设置牌组大小
    /// </summary>
    /// <param name="card"></param>
    /// <param name="size"></param>
    public void SetCardScale(List<GameObject>card,float size)
    {
        for (int i = 0; i < card.Count; i++)
        {
            card[i].transform.localScale = Vector3.one * size;
        }
    }

    /// <summary>
    /// 看其他玩家的牌
    /// </summary>
    /// <param name="p"></param>
    /// <param name="tOrF"></param>
    public void LookOtherCard(List<GameObject>card,bool tOrF)
    {
        for (int i = 0; i < card.Count; i++)
        {
            card[i].gameObject.SetActive(tOrF);
        }
    }

    /// <summary>
    /// 设置卡的位置
    /// </summary>
    /// <param name="go"></param>
    public void SetHitOutCard(List<GameObject> hitOutCard, GameObject go)
    {
        var pos = GetRect.instance.GetRectPos(go);
        if (pos.y == -150f)
        {
            GetRect.instance.SetRectPos(go, new Vector2(pos.x, -130f));
            hitOutCard.Add(go);
        }
        else
        {
            GetRect.instance.SetRectPos(go, new Vector2(pos.x, -150f));
            hitOutCard.Remove(go);
        }
    }
    /// <summary>
    /// 出牌
    /// </summary>
    /// <param name="p"></param>
    /// <param name="outCard"></param>
    public void OutCard(List<GameObject> card, List<GameObject> hitOutCard, Transform outCard, Vector3 midPos, float offset = 5)
    {
        if (hitOutCard.Count == 0) return;
        for (int i = 0; i < outCard.GetChildCount(); i++)
        {
            UnityEngine.GameObject.Destroy(outCard.transform.GetChild(i).gameObject);
        }
        SetCardScale(hitOutCard, 0.5f);

        for (int i = 0; i < hitOutCard.Count; i++)
        {
            hitOutCard[i].transform.SetParent(outCard);
            hitOutCard[i].GetComponent<Button>().interactable = false;
        }
        SetCardPos(hitOutCard, outCard.position, Vector3.right, 2.5f,0);

        for (int i = 0; i < hitOutCard.Count; i++)
        {
            card.Remove(hitOutCard[i]);
        }

        if (card.Count != 0)
        {
            SetCardPos(card, midPos, Vector3.right, offset,0);
        }
        hitOutCard.Clear();
    }
 
    /// <summary>
    /// 随机选一张牌加入待出牌
    /// </summary>
    /// <param name="p"></param>
    public void RandomOneHitCard(List<GameObject> card, List<GameObject> hitOutCard)
    {
        if (card.Count <= 0) return;
        var go = card[r.Next(0, card.Count)];
        hitOutCard.Add(go);
        go.SetActive(true);
    }

    /// <summary>
    /// 选择前几张牌加入待出牌
    /// </summary>
    /// <param name="p"></param>
    /// <param name="count"></param>
    public int HitFirstFewCard(List<GameObject>card,List<GameObject>hitOutCard,int count)
    {
        if (card.Count < count) return 0;
        for (int i = 0; i < count; i++)
        {
            var go = card[i];
            go.SetActive(true);
            hitOutCard.Add(card[i]);
        }
        return 1;
    }
    /// <summary>
    /// 移除背景牌
    /// </summary>
    /// <param name="p"></param>
    /// <param name="num"></param>
    public void CancelBackCard(List<GameObject>card,int num)
    {
        if (card.Count< num) return;
        for (int i = 0; i < num; i++)
        {
            var go = card[0];
            card.Remove(go);
            UnityEngine.GameObject.Destroy(go);
        }
    }
}