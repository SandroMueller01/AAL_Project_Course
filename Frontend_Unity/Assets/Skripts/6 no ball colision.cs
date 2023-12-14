using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class noballcolision : MonoBehaviour
{
    public float speed = 5f;
    public float minY = 1.5f;
    public float maxY = 13f;
    public float minX = -9f;
    public float maxX = 9f;

    private float[] xSnapPoints = { -7.5f, -4.5f, -1.5f, 1.5f, 4.5f, 7.5f };
    private float[] ySnapPoints = { 1.5f, 3.5f, 5.5f, 7.5f, 9.5f, 11.5f };

    private int xSnapIndex = 0;
    private int ySnapIndex = 0;

    void Update()
    {
        // Der Ball bewegt sich nicht mehr durch Eingaben
        // Kommentiere die folgende Zeile aus, wenn du die Bewegung wieder aktivieren möchtest
        // float verticalInput = 0f;

        Vector3 newPosition = new Vector3(GetNextXSnapPoint(), GetNextYSnapPoint(), transform.position.z);

        float clampedX = Mathf.Clamp(newPosition.x, minX, maxX);
        float clampedY = Mathf.Clamp(newPosition.y, minY, maxY);

        if (!IsCollidingWithOtherBalls(clampedX, clampedY))
        {
            transform.position = new Vector3(clampedX, clampedY, newPosition.z);
        }
    }

    float GetNextXSnapPoint()
    {
        return xSnapPoints[xSnapIndex];
    }

    float GetNextYSnapPoint()
    {
        return ySnapPoints[ySnapIndex];
    }

    bool IsCollidingWithOtherBalls(float x, float y)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(new Vector2(x, y), 0.5f);

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Ball") && collider.gameObject != gameObject)
            {
                // Hier könnte eine Meldung ausgegeben werden, dass eine Kollision stattgefunden hat
                // Debug.Log("Kollision mit einem anderen Ball!");
                return true;
            }
        }

        return false;
    }
}

