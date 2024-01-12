using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BallMovement : MonoBehaviour
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
    public int selectedColumnIndex = 0;

    private GameObject selectedColumnObject;
    public string selectedColumnObjectName = "SelectedColumn";
    public string ballTag = "Ball";

    private GameObject currentTopBall;

    void Start()
    {
        xSnapIndex = System.Array.IndexOf(xSnapPoints, transform.position.x);
        ySnapIndex = System.Array.IndexOf(ySnapPoints, transform.position.y);

        if (xSnapIndex == -1) xSnapIndex = 0;
        if (ySnapIndex == -1) ySnapIndex = 0;

        selectedColumnObject = GameObject.Find("Sidewall " + (selectedColumnIndex + 1));
    }

    void Update()
    {
        ColorWall();
        Debug.Log("Before GetTopBallInSelectedColumn");
        GameObject topBall = GetTopBallInSelectedColumn();
        Debug.Log("After GetTopBallInSelectedColumn");
        if (topBall != null)
        {
            currentTopBall = topBall;
            Debug.Log("Current Top ball:" + currentTopBall);

            if (topBall == gameObject)
            {
                Debug.Log("start move ball with" + currentTopBall);
                MoveBall(currentTopBall);
                Debug.Log("finished move ball with" + currentTopBall);
            }
        }
    }

    void MoveBall(GameObject currentTopBall)
    {
       
        if (currentTopBall != null)
        {
            if (transform.position.y < maxY) // Ball is within the columns
            {
                // Wenn die linke Pfeiltaste gedrückt wurde und Y-Wert zwischen 9 und 13 liegt
                if (Input.GetKeyDown(KeyCode.LeftArrow) && transform.position.y >= 9f && transform.position.y <= 13f)
                {
                    // Zum vorherigen Snapping-Punkt auf der X-Achse springen lassen
                    JumpToPreviousXSnapPoint();
                }
                // Wenn die rechte Pfeiltaste gedrückt wurde und Y-Wert zwischen 9 und 13 liegt
                else if (Input.GetKeyDown(KeyCode.RightArrow) && transform.position.y >= 9f && transform.position.y <= 13f)
                {
                    // Zum nächsten Snapping-Punkt auf der X-Achse springen lassen
                    JumpToNextXSnapPoint();
                }

                else if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    JumpToNextYSnapPoint();
                }
                else if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    JumpToPreviousYSnapPoint();
                }

                Vector3 newPosition = new Vector3(currentTopBall.transform.position.x, GetNextYSnapPoint());
                float clampedY = Mathf.Clamp(newPosition.y, minY, maxY);

                currentTopBall.transform.position = new Vector3(currentTopBall.transform.position.x, clampedY);

                Debug.Log("current top ball:" + currentTopBall);
            }
        }
    }

    void ColorWall()
    {
        if (transform.position.y >= maxY) // Ball is above the columns
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (selectedColumnIndex > 0)
                {
                    selectedColumnIndex--;
                }
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (selectedColumnIndex < xSnapPoints.Length - 1)
                {
                    selectedColumnIndex++;
                }
            }
        }

        ColorAdjacentColumnWalls(selectedColumnIndex);
    }

    GameObject GetTopBallInSelectedColumn()
    {
        float xPosition1 = GetSidewallXPosition(selectedColumnIndex + 1);
        float xPosition2 = GetSidewallXPosition(selectedColumnIndex + 2);
        float averageXPosition = (xPosition1 + xPosition2) / 2f;

        if (selectedColumnObject != null)
        {
            GameObject[] allBalls = GameObject.FindGameObjectsWithTag(ballTag);
            GameObject[] ballsInColumn = allBalls
                .Where(ball => Mathf.Approximately(ball.transform.position.x, averageXPosition))
                .ToArray();

            GameObject topBall = null;
            float highestY = float.MinValue;

            foreach (GameObject ball in ballsInColumn)
            {
                if (ball.transform.position.y > highestY)
                {
                    highestY = ball.transform.position.y;
                    topBall = ball;
                }
            }

            return topBall;
        }
        else
        {
            return null;
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

    void JumpToPreviousXSnapPoint()
    {
        xSnapIndex = (xSnapIndex - 1 + xSnapPoints.Length) % xSnapPoints.Length;
    }

    void JumpToNextXSnapPoint()
    {
        xSnapIndex = (xSnapIndex + 1) % xSnapPoints.Length;
    }

    void JumpToPreviousYSnapPoint()
    {
        ySnapIndex = (ySnapIndex - 1 + ySnapPoints.Length) % ySnapPoints.Length;
    }

    void JumpToNextYSnapPoint()
    {
        ySnapIndex = (ySnapIndex + 1) % ySnapPoints.Length;
    }

    void ColorAdjacentColumnWalls(int columnIndex)
    {
        for (int i = 1; i <= 7; i++)
        {
            GameObject sideWall = GameObject.Find("Sidewall " + i);
            if (sideWall != null)
            {
                if (i == columnIndex + 1 || i == columnIndex + 2)
                {
                    sideWall.GetComponent<Renderer>().material.color = Color.blue;
                }
                else
                {
                    sideWall.GetComponent<Renderer>().material.color = Color.white;
                }
            }
        }
    }
    float GetSidewallXPosition(int sidewallIndex)
    {
        GameObject sideWall = GameObject.Find("Sidewall " + sidewallIndex);
        if (sideWall != null)
        {
            return sideWall.transform.position.x;
        }
        return 0f;
    }
}

