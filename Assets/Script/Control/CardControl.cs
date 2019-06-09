using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CardControl : MonoBehaviour
{
    BasePlayer p;//当前玩家控制角色

    Canvas canvas;
    RectTransform canvasRect;
    Vector2 mousePos2D;//点下鼠标位置
    Vector2 nowMousePos2D;//拖动鼠标位置
    float heigth;//牌的一半长度

    float t;//计时用
    int count = 0;//判断点击次数

    GameManager gm;
    public bool isStart;//主角是否可以控制
    Transform outCard;//主角出牌位置

    public bool isHit;//主角是否可以出牌

    Material ma;//选中的阴影的材质

    CardManager cm;
    void Start()
    {
        ma = Resources.Load("data/shadowM") as Material;
        gm = GetComponent<GameManager>();
        canvas = GetComponent<Canvas>();
        canvasRect = canvas.transform as RectTransform;
        heigth = transform.Find("Player/MyCard").GetComponent<RectTransform>().sizeDelta.y * 0.5f;
        outCard = transform.Find("HitCardPos/2");
        cm = GetComponent<CardManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isStart)
        {
            TakeBackALLCard();
            SetALLHitCard();
        }
        if (isHit)
        {
            HitCard();
        }
    }
    /// <summary>
    /// 设置当前玩家
    /// </summary>
    /// <param name="p"></param>
    public void SetNowPlayer(BasePlayer p)
    {
        this.p = p;
    }

    /// <summary>
    /// 为当前所有牌添加点击事件
    /// </summary>
    public void SetAllBtn()
    {
        //foreach (var item in p.GetAllCard())
        //{
        //    item.GetComponent<Button>().onClick.AddListener(() => cm.SetHitOutCard(p.GetAllOutHitCard(),item.gameObject));
        //}
    }

    /// <summary>
    /// 双击全部取消出牌状态
    /// </summary>
    void TakeBackALLCard()
    {
        if (Input.GetMouseButtonDown(0))
        {
            count++;
            //当第一次点击鼠标，启动计时器
            if (count == 1)
            {
                t = Time.time;
            }
            //当第二次点击鼠标，且时间间隔满足要求时双击鼠标
            if (2 == count && Time.time - t <= 0.2f)
            {
                //var hitCards = p.GetAllOutHitCard();
                //for (int i = 0; i < hitCards.Count; i++)
                //{
                //    GetRect.instance.SetRectPos(hitCards[i], new Vector2(GetRect.instance.GetRectPos(hitCards[i]).x, -150f));
                //}
                //hitCards.Clear();
                count = 0;
            }
            if (Time.time - t > 0.2f)
            {
                count = 0;
            }
        }
    }
    /// <summary>
    /// 是否被选中
    /// </summary>
    /// <param name="palyer"></param>
    /// <param name="card"></param>
    public void SetMaBlackOrNull(GameObject card)
    {
        Vector3 cardPos = GetRect.instance.GetRectPos(card);
        if (isSelect(cardPos))
        {
            SetMaterialBlack(card);
        }
        else
        {
            SetMaterialNull(card);
        }
    }
    /// <summary>
    /// 拖动选择卡牌
    /// </summary>
    void SetALLHitCard()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Vector2 mousePos2D = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            //RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);       
            //if (hit.collider != null)
            //{
            //    if (!setCards.Contains(hit.collider.gameObject))
            //    {
            //        setCards.Add(hit.collider.gameObject);
            //    }               
            //}
            GetRect.instance.GetMouseRecPos(canvas, canvasRect, out mousePos2D);
            //Debug.Log(mousePos2D);
        }

        //if (Input.GetMouseButton(0))
        //{
        //    GetRect.instance.GetMouseRecPos(canvas, canvasRect, out nowMousePos2D);  
        //    for (int i = 0; i < p.GetAllCard().Count; i++)
        //    {
        //        SetMaBlackOrNull(p.GetCard(i));
        //    }
        //}

        //if (Input.GetMouseButtonUp(0))
        //{
        //    for (int i = 0; i < p.GetAllCard().Count; i++)
        //    {
        //        MaSetHit(p.GetCard(i));
        //    }
        //}
    }

    /// <summary>
    /// 判断牌是否在滑动区域内
    /// </summary>
    /// <param name="cardPos"></param>
    /// <returns></returns>
    public bool isSelect(Vector3 cardPos)
    {
        if (mousePos2D.x <= cardPos.x && cardPos.x <= nowMousePos2D.x || nowMousePos2D.x <= cardPos.x && cardPos.x <= mousePos2D.x)
        {
            if (nowMousePos2D.y <= cardPos.y + heigth && cardPos.y - heigth <= nowMousePos2D.y)
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// 打出牌
    /// </summary>
    void HitCard()
    {
        if (Input.GetMouseButtonDown(1))
        {
            //cm.OutCard(p.GetAllCard(),p.GetAllOutHitCard(),outCard, gm.downPos);
            HitCardControl.Instance.SetState(ePlayer.p3);
        }
    }
   
    /// <summary>
    /// 判断是否是阴影材质 加入待出牌
    /// </summary>
    /// <param name="card"></param>
    /// <returns></returns>
    public void MaSetHit(GameObject card)
    {
        if (card.GetComponent<Image>().material == ma)
        {
            //cm.SetHitOutCard(p.GetAllOutHitCard(),card);
            SetMaterialNull(card);
        }
    }

    /// <summary>
    /// 选中改变阴影材质
    /// </summary>
    /// <param name="card"></param>
    public void SetMaterialBlack(GameObject card)
    {
        card.GetComponent<Image>().material = ma;
    }

    public void SetMaterialNull(GameObject card)
    {
        card.GetComponent<Image>().material = null;
    }
}
