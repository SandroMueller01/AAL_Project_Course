using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trycoplempvemntpoleandball : MonoBehaviour
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

        ColorSelectedColumnWalls(selectedColumnIndex);

        if (IsAtTopOfSelectedColumn())
        {
            MoveTopBall();
        }
    }

    void MoveTopBall()
    {
        float verticalInput = Input.GetKey(KeyCode.UpArrow) ? 1f : Input.GetKey(KeyCode.DownArrow) ? -1f : 0f;

        if (verticalInput != 0)
        {
            // Wenn der Ball sich zwischen 9 und 13 auf der Y-Achse befindet
            if (transform.position.y >= 9f && transform.position.y <= 13f)
            {
                // Bewege sowohl die Säule als auch den Ball
                MoveColumnAndTopBall(verticalInput);
            }
            else
            {
                // Bewege nur den Ball
                MoveVertically(verticalInput);
            }
        }
    }

    void MoveColumnAndTopBall(float input)
    {
        // Bewege die Säule nach oben oder unten
        MoveVertically(input);

        // Bewege den Ball zusammen mit der Säule
        float newY = transform.position.y + input * speed * Time.deltaTime;
        float clampedY = Mathf.Clamp(newY, minY, maxY);
        transform.position = new Vector3(transform.position.x, clampedY, transform.position.z);
    }

    void MoveVertically(float input)
    {
        float newY = transform.position.y + input * speed * Time.deltaTime;
        float clampedY = Mathf.Clamp(newY, minY, maxY);
        transform.position = new Vector3(transform.position.x, clampedY, transform.position.z);
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
    bool IsAtTopOfSelectedColumn()
    {
        // Vor der Verwendung der Snapping-Punkte sortieren
        System.Array.Sort(ySnapPoints);

        // Den höchsten Y-Wert finden
        float highestY = Mathf.Max(ySnapPoints);

        // Überprüfe, ob der aktuelle Ball den höchsten Y-Wert hat
        return Mathf.Approximately(transform.position.y, highestY);
    }
}

