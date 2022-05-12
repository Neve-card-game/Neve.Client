<<<<<<< HEAD
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardListEditor : MonoBehaviour, IPointerClickHandler
{
    public delegate void OnClickThisCard(PointerEventData eventData);
    public static event OnClickThisCard CardOnClick;
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("移除卡牌：" + this.name);
        CardOnClick(eventData);
    }
}
=======
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardListEditor : MonoBehaviour, IPointerClickHandler
{
    public delegate void OnClickThisCard(PointerEventData eventData);
    public static event OnClickThisCard CardOnClick;
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("移除卡牌：" + this.name);
        CardOnClick(eventData);
    }
}
>>>>>>> 0803b0379f9681b21ef2903850e2288c306242e9
