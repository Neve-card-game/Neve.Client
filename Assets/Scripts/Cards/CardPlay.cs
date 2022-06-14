using System.Drawing;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPlay : MonoBehaviour
{
    public List<Transform> waypoints = new List<Transform>();
    new Transform transform;

    public int pointCounts = 50;
    public float moveDistance;
    public float animationSpeed;
    public float ScaleRate;

    public Vector3 OriginalPosition;
    public Vector3 OriginalScale;
    public Quaternion OriginalRotation;

    public Vector3[] LinePointList;
    Vector3 currentPosition;
    Vector3 distance;
    Vector3 newPosition;
    public Vector3 HandPosition;
    public Quaternion HandRotation;

    public CardState cardState = CardState.Stay;

    StateMachine machine;

    GameObject BattleField;
    GameObject AssistPoint_BF;
    GameObject AssistPoint_Hand;
    GameObject OutLine;
    Transform[] FindPoint;

    public new Rigidbody rigidbody;
    public delegate void CardStateBroadcast(string CardName,CardState cardState);
    public static event CardStateBroadcast CardLeaveHand;
    public static event CardStateBroadcast CardOnClick_BF;
    public static event CardStateBroadcast CardMoveToBF;

    void Start()
    {
        transform = GetComponent<Transform>();
        rigidbody = GetComponent<Rigidbody>();

        OutLine = this.transform.Find("OutLine").gameObject;
        AssistPoint_BF = GameObject.Find("AssistPoint_BF");
        AssistPoint_Hand = GameObject.Find("AssistPoint_Hand");
        BattleField = GameObject.Find("BattleField");

        StayState stay = new StayState(0, this);
        EnlargeState enlarge = new EnlargeState(1, this);
        DragState drag = new DragState(2, this);
        MoveToBFState moveToBF = new MoveToBFState(3, this);
        MoveToHandState moveToHand = new MoveToHandState(4, this);

        OriginalPosition = transform.position;
        OriginalScale = transform.localScale;
        OriginalRotation = transform.rotation;

        machine = new StateMachine(stay);
        machine.AddState(enlarge);
        machine.AddState(drag);
        machine.AddState(moveToBF);
        machine.AddState(moveToHand);
    }

    void Update()
    {
        UpdateCard();
    }

    private void UpdateCard()
    {
        switch (cardState)
        {
            case CardState.Stay:
                machine.TranslateState(0);
                break;
            case CardState.Enlarge:
                machine.TranslateState(1);
                break;
            case CardState.Drag:
                machine.TranslateState(2);
                break;
            case CardState.MoveToBF:
                machine.TranslateState(3);
                break;
            case CardState.MoveToHand:
                machine.TranslateState(4);
                break;
        }
    }

    void LateUpdate()
    {
        machine.Update();
    }

    //
    private void OnMouseDown()
    {
        if (cardState == CardState.MoveToBF)
        {
            CardOnClick_BF(this.name,this.cardState);
        }
    }

    private void OnMouseUp()
    {
        if (cardState == CardState.Drag){
            cardState = CardState.MoveToBF;
            CardMoveToBF(this.name,this.cardState);
        }
    }

    private void OnMouseDrag()
    {
        if (cardState == CardState.Enlarge)
            cardState = CardState.Drag;
    }

    private void OnMouseEnter()
    {
        OutLine.SetActive(true);
        if (cardState == CardState.Stay)
        {
            cardState = CardState.Enlarge;
        }
    }

    private void OnMouseExit()
    {
        OutLine.SetActive(false);
        if (cardState == CardState.Enlarge)
        {
            cardState = CardState.Stay;
        }
    }

    //
    public void StartEnlarge()
    {
        StartCoroutine(Enlarge());
    }

    public void EndEnlarge()
    {
        StopCoroutine(Enlarge());
    }

    public void StartShrink()
    {
        StartCoroutine(Shrink());
    }

    public void StartDrag()
    {
        rigidbody.position = new Vector3(
            rigidbody.position.x,
            rigidbody.position.y,
            rigidbody.position.z - 3.2f
        );

        rigidbody.rotation = Quaternion.Euler(0f, 180f, 0f);
        //1：把物体的世界坐标转为屏幕坐标 (依然会保留z坐标)
        currentPosition = Camera.main.WorldToScreenPoint(this.transform.position);

        distance = currentPosition - Input.mousePosition;
    }

    public void StayDrag()
    {
        rigidbody.constraints = RigidbodyConstraints.None;

        //2：更新物体屏幕坐标系的x,y
        currentPosition = new Vector3(
            Input.mousePosition.x + distance.x,
            Input.mousePosition.y + distance.y,
            currentPosition.z
        );

        //3：把屏幕坐标转为世界坐标
        newPosition = Camera.main.ScreenToWorldPoint(currentPosition);

        //4：更新物体的世界坐标，y轴不变（高度不变）
        this.transform.position = new Vector3(newPosition.x, newPosition.y, newPosition.z);
    }

    public void StartMoveToBF()
    {
        waypoints.Clear();
        if (FindPoint != null)
        {
            Array.Clear(FindPoint, 0, FindPoint.Length);
        }
        FindPoint = AssistPoint_BF.GetComponentsInChildren<Transform>();
        Transform current = this.transform;
        waypoints.Add(current);
        for (int i = 0; i < FindPoint.Length; i++)
        {
            waypoints.Add(FindPoint[i]);
        }
        StartCoroutine(MoveToBF());
    }

    public void StartMoveToHand()
    {
        waypoints.Clear();
        if (FindPoint != null)
        {
            Array.Clear(FindPoint, 0, FindPoint.Length);
        }
        FindPoint = AssistPoint_Hand.GetComponentsInChildren<Transform>();
        Transform current = this.transform;
        waypoints.Add(current);
        for (int i = 0; i < FindPoint.Length; i++)
        {
            waypoints.Add(FindPoint[i]);
        }
        StartCoroutine(MoveToHand());
    }

    //
    IEnumerator Enlarge()
    {
        float lerpTime = 0f;
        while (lerpTime <= 1)
        {
            transform.localScale = Vector3.Lerp(OriginalScale, OriginalScale * ScaleRate, lerpTime);
            transform.position = Vector3.Lerp(
                OriginalPosition,
                OriginalPosition + new Vector3(0f, 1f, -1f) * moveDistance,
                lerpTime
            );
            transform.rotation = Quaternion.Lerp(
                OriginalRotation,
                Quaternion.Euler(0f, 180f, 0f),
                lerpTime
            );
            lerpTime += Time.deltaTime * animationSpeed;
            yield return null;
        }
    }

    IEnumerator Shrink()
    {
        float lerpTime = 0f;
        while (lerpTime <= 1)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, OriginalScale, lerpTime);
            transform.position = Vector3.Lerp(transform.position, OriginalPosition, lerpTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, OriginalRotation, lerpTime);

            lerpTime += Time.deltaTime * animationSpeed;
            yield return null;
        }
    }

    IEnumerator MoveToBF()
    {
        this.transform.SetParent(BattleField.transform);
        CardLeaveHand(this.name,this.cardState);
        Init();
        float lerpTime = 0f;
        for (int i = 0; i < LinePointList.Length - 1; i++)
        {
            transform.localScale = Vector3.Lerp(
                transform.localScale,
                OriginalScale * 0.8f,
                lerpTime
            );
            transform.rotation = Quaternion.Lerp(
                transform.rotation,
                Quaternion.Euler(-15f, 180f, 0f),
                lerpTime
            );

            transform.position = Vector3.Lerp(LinePointList[i], LinePointList[i + 1], lerpTime);

            lerpTime += Time.deltaTime * animationSpeed;

            yield return new WaitForFixedUpdate();
        }
        rigidbody.useGravity = true;
    }

    IEnumerator MoveToHand()
    {
        List<Vector3> newP = new List<Vector3>();
        for (int i = 0; i < waypoints.Count; i++)
        {
            if (!newP.Contains(waypoints[i].position))
            {
                newP.Add(waypoints[i].position);
            }
        }

        newP.Add(HandPosition);
        Init(newP);
        float lerpTime = 0f;
        rigidbody.constraints = RigidbodyConstraints.None;
        for (int i = 0; i < LinePointList.Length - 1; i++)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, HandRotation, lerpTime);

            transform.position = Vector3.Lerp(LinePointList[i], LinePointList[i + 1], lerpTime);

            lerpTime += Time.deltaTime * animationSpeed;

            yield return new WaitForFixedUpdate();
        }
        rigidbody.constraints = RigidbodyConstraints.FreezePosition;
        cardState = CardState.Stay;
    }

    //
    public void Init()
    {
        if (LinePointList != null)
        {
            Array.Clear(LinePointList, 0, LinePointList.Length);
        }
        List<Vector3> newP = new List<Vector3>();
        for (int i = 0; i < waypoints.Count; i++)
        {
            if (!newP.Contains(waypoints[i].position))
            {
                newP.Add(waypoints[i].position);
            }
        }

        LinePointList = BezierUtils.GetBeizerPointList(pointCounts, newP);
    }

    public void Init(List<Vector3> pointPosition)
    {
        if (LinePointList != null)
        {
            Array.Clear(LinePointList, 0, LinePointList.Length);
        }
        LinePointList = BezierUtils.GetBeizerPointList(pointCounts, pointPosition);
    }
}
