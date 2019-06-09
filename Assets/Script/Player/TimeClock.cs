using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TimeClock : MonoBehaviour
{

    UnityEngine.Object[] timer;//时间材质
    UnityEngine.Object[] lastFive;//倒计时5
    UnityEngine.Object[] lastFour;//倒计时4
    UnityEngine.Object[] lastThree;//倒计时3
    UnityEngine.Object[] lastTwo;//倒计时2
    UnityEngine.Object[] lastOne;//倒计时1
    UnityEngine.Object timer_back;//本体

    Image decade;//十位
    Image digit;//个位
    Image clock;//本体

    float t;
    float lastT;
    int count = 1;//记录一轮时间换图片次数
    void Start()
    {
        timer = Resources.LoadAll("data/timer");
        lastFive = Resources.LoadAll("ani/clock5");
        lastFour = Resources.LoadAll("ani/clock4");
        lastThree = Resources.LoadAll("ani/clock3");
        lastTwo = Resources.LoadAll("ani/clock2");
        lastOne = Resources.LoadAll("ani/clock1");
        timer_back = Resources.Load("data/timer_back");

        decade = transform.Find("Decade").GetComponent<Image>();
        digit = transform.Find("Digit").GetComponent<Image>();
        clock = transform.GetComponent<Image>();

    }
    /// <summary>
    /// 设置闹钟方位
    /// </summary>
    /// <param name="pos"></param>
    public void SetClockPos(Vector3 pos)
    {
        transform.position = pos;
        gameObject.SetActive(true);
    }
    /// <summary>
    /// 显示秒表
    /// </summary>
    /// <param name="t"></param>
    public void ClockLook(int t)
    {
        if (t > 5)
        {
            decade.gameObject.SetActive(true);
            digit.gameObject.SetActive(true);
            clock.overrideSprite = timer_back as Sprite;
            //将整形转成字符数组
            char[] chars = t.ToString().PadLeft(2, '0').ToCharArray();
            SetNumSprite(int.Parse(chars[0].ToString()), decade);
            SetNumSprite(int.Parse(chars[1].ToString()), digit);
        }
        else
        {
            decade.gameObject.SetActive(false);
            digit.gameObject.SetActive(false);
            switch (t)
            {
                case 5:
                    SetFiveBelowNum(clock, lastFive);
                    break;
                case 4:
                    SetFiveBelowNum(clock, lastFour);
                    break;
                case 3:
                    SetFiveBelowNum(clock, lastThree);
                    break;
                case 2:
                    SetFiveBelowNum(clock, lastTwo);
                    break;
                case 1:
                    SetFiveBelowNum(clock, lastOne);
                    break;
                case 0:
                    gameObject.SetActive(false);
                    break;
            }
        }
    }
    /// <summary>
    /// 设置数字图片
    /// </summary>
    /// <param name="num"></param>
    /// <param name="im"></param>
    void SetNumSprite(int num, Image im)
    {
        im.overrideSprite = timer[num + 1] as Sprite;
    }
    /// <summary>
    /// 倒数后5秒
    /// </summary>
    /// <param name="im"></param>
    /// <param name="obj"></param>
    void SetFiveBelowNum(Image im, UnityEngine.Object[] obj)
    {
        t += Time.deltaTime;
        if (t - lastT >= 0.25f)
        {
            if (count < 4)
            {
                count++;
            }
            else
            {
                count = 1;
            }
            im.overrideSprite = obj[count] as Sprite;
            lastT = t;
        }
    }
}
