using System;
using System.Collections.Generic;
using UnityEngine;

public class HandAreaLayout
{
    public Vector3 OriginPosition;
    public Vector3 CardOffset;
    public float rotationAngles;

    public HandAreaLayout() { }

    public HandAreaLayout(Vector3 originalPosition, Vector3 cardOffset, float RotationAngles)
    {
        this.OriginPosition = originalPosition;
        this.CardOffset = cardOffset;
        this.rotationAngles = RotationAngles;
    }

    public void UpdateCardPosition(List<Card> cardsofhand, GameObject hand)
    {
        double ToRand = Math.PI * rotationAngles / 180f;
        float SinAngles = (float)Math.Sin(ToRand);
        float CosAngles = (float)Math.Cos(ToRand);

        for (int i = 0; i < cardsofhand.Count; i++)
        {
            Transform card = hand.transform.Find(cardsofhand[i].CardName);

            CardPlay cardPlay = card.GetComponent<CardPlay>();

            if (cardsofhand.Count % 2 == 1)
            {
                int Offset = cardsofhand.Count / 2 - i;
                float Offset_X;
                float Offset_Y;
                Offset_Y = i - cardsofhand.Count / 2;
                Offset_X = CardOffset.x * CosAngles * Offset;
                Offset_Y = CardOffset.y * SinAngles * Offset_Y * Offset;
                card.position = new Vector3(
                    OriginPosition.x - Offset_X,
                    OriginPosition.y + Offset_Y,
                    OriginPosition.z - i * CardOffset.z
                );
                card.rotation = Quaternion.Euler(0f, 180f, 0f - (Offset * rotationAngles));
            }
            else
            {
                float Offset = cardsofhand.Count / 2 - i - 0.5f;
                float Offset_X;
                float Offset_Y;
                if (i < cardsofhand.Count / 2)
                {
                    Offset_Y = i - cardsofhand.Count / 2;
                }
                else
                {
                    Offset_Y = i - cardsofhand.Count / 2 + 0.5f;
                }
                Offset_X = CardOffset.x * CosAngles * Offset;
                Offset_Y = CardOffset.y * SinAngles * Offset_Y * Offset;
                card.position = new Vector3(
                    OriginPosition.x - Offset_X,
                    OriginPosition.y + Offset_Y,
                    OriginPosition.z - i * CardOffset.z
                );
                card.rotation = Quaternion.Euler(0f, 180f, 0f - (Offset * rotationAngles));
            }

            cardPlay.OriginalPosition = new Vector3(
                card.transform.position.x,
                card.transform.position.y,
                card.transform.position.z
            );
            cardPlay.OriginalRotation = new Quaternion(
                card.transform.rotation.x,
                card.transform.rotation.y,
                card.transform.rotation.z,
                card.transform.rotation.w
            );
        }
    }

    public void UpdateCardPosition_Enemy(List<Card> cardsofhand, GameObject hand)
    {
        double ToRand = Math.PI * rotationAngles / 180f;
        float SinAngles = (float)Math.Sin(ToRand);
        float CosAngles = (float)Math.Cos(ToRand);

        for (int i = 0; i < cardsofhand.Count; i++)
        {
            Transform card = hand.transform.Find(cardsofhand[i].CardName);

            CardPlay_Enemy cardPlay = card.GetComponent<CardPlay_Enemy>();

            if (cardsofhand.Count % 2 == 1)
            {
                int Offset = cardsofhand.Count / 2 - i;
                float Offset_X;
                float Offset_Y;
                Offset_Y = i - cardsofhand.Count / 2;
                Offset_X = CardOffset.x * CosAngles * Offset;
                Offset_Y = CardOffset.y * SinAngles * Offset_Y * Offset;
                card.position = new Vector3(
                    OriginPosition.x + Offset_X,
                    OriginPosition.y - Offset_Y,
                    OriginPosition.z - i * CardOffset.z
                );
                card.rotation = Quaternion.Euler(0f, 0f, 0f + (Offset * rotationAngles));
            }
            else
            {
                float Offset = cardsofhand.Count / 2 - i - 0.5f;
                float Offset_X;
                float Offset_Y;
                if (i < cardsofhand.Count / 2)
                {
                    Offset_Y = i - cardsofhand.Count / 2;
                }
                else
                {
                    Offset_Y = i - cardsofhand.Count / 2 + 0.5f;
                }
                Offset_X = CardOffset.x * CosAngles * Offset;
                Offset_Y = CardOffset.y * SinAngles * Offset_Y * Offset;
                card.position = new Vector3(
                    OriginPosition.x + Offset_X,
                    OriginPosition.y - Offset_Y,
                    OriginPosition.z - i * CardOffset.z
                );
                card.rotation = Quaternion.Euler(0f, 0f, 0f + (Offset * rotationAngles));
            }

            cardPlay.OriginalPosition = new Vector3(
                card.transform.position.x,
                card.transform.position.y,
                card.transform.position.z
            );
            cardPlay.OriginalRotation = new Quaternion(
                card.transform.rotation.x,
                card.transform.rotation.y,
                card.transform.rotation.z,
                card.transform.rotation.w
            );
        }
    }
}
