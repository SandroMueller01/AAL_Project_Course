using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newonlytopball : MonoBehaviour
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
    private int selectedColumnIndex = 0;

    void Start()
    {
        xSnapIndex = System.Array.IndexOf(xSnapPoints, transform.position.x);
        ySnapIndex = System.Array.IndexOf(ySnapPoints, transform.position.y);

        if (xSnapIndex == -1) xSnapIndex = 0;
        if (ySnapIndex == -1) ySnapIndex = 0;
    }

    void Update()
    { //auswahl der säule
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
        // färbung der ausgewählten säule
        ColorSelectedColumnWalls(selectedColumnIndex);

       
       // get top ball index and move top ball
            int topBallIndex = GetTopBallIndexInSelectedColumn();
            if (topBallIndex != -1)
            {
                MoveTopBall(topBallIndex);
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
    // get top ball index and move top ball

    int GetTopBallIndexInSelectedColumn()
    {
        for (int topBallIndex = 0; topBallIndex < ySnapPoints.Length; topBallIndex++)
        {
            if (Mathf.Approximately(transform.position.y, ySnapPoints[topBallIndex]))
            {
                return topBallIndex;
            }
        }

        return -1;
    }

    bool IsAtCurrentXSnapPoint()
    {
        return Mathf.Approximately(transform.position.x, GetNextXSnapPoint());
    }

    bool IsAtCurrentYSnapPoint()
    {
        return Mathf.Approximately(transform.position.y, GetNextYSnapPoint());
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

    void MoveHorizontally(float input)
    {
        float newX = transform.position.x + input * speed * Time.deltaTime;
        float clampedX = Mathf.Clamp(newX, minX, maxX);
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
    }

    void ColorSelectedColumnWalls(int columnIndex)
    {
        ResetWallColors();

        GameObject leftWall = GameObject.Find("Sidewall " + (columnIndex + 1));
        GameObject rightWall = GameObject.Find("Sidewall " + (columnIndex + 2));

        if (leftWall != null)
        {
            leftWall.GetComponent<Renderer>().material.color = Color.red;
        }

        if (rightWall != null)
        {
            rightWall.GetComponent<Renderer>().material.color = Color.red;
        }
    }

    void ResetWallColors()
    {
        for (int i = 1; i <= 7; i++)
        {
            GameObject wall = GameObject.Find("Sidewall " + i);
            if (wall != null)
            {
                wall.GetComponent<Renderer>().material.color = Color.white;
            }
        }
    }

    void MoveTopBall(int topBallIndex)
    {
        // Hol dir die Y-Koordinate des Snapping-Punkts für den obersten Ball
        float newY = ySnapPoints[topBallIndex];

        // Setze die Position des Balls auf den Snapping-Punkt
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);

        float verticalInput = Input.GetKey(KeyCode.UpArrow) ? 1f : Input.GetKey(KeyCode.DownArrow) ? -1f : 0f;
        float horizontalInput = Input.GetKey(KeyCode.LeftArrow) ? -1f : Input.GetKey(KeyCode.RightArrow) ? 1f : 0f;


        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (!IsAtCurrentYSnapPoint())
            {
                JumpToNextYSnapPoint();
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (!IsAtCurrentYSnapPoint())
            {
                JumpToPreviousYSnapPoint();
            }
        }

        Vector3 newPosition = new Vector3(GetNextXSnapPoint(), GetNextYSnapPoint(), transform.position.z);
        float clampedX = Mathf.Clamp(newPosition.x, minX, maxX);
        float clampedY = Mathf.Clamp(newPosition.y, minY, maxY);
        transform.position = new Vector3(clampedX, clampedY, newPosition.z);
    }
}
