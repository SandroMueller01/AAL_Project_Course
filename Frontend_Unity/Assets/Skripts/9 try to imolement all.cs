using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trytoimolementall : MonoBehaviour
{
    public float speed = 5f;
    public float minY = 1.5f;
    public float maxY = 13f;
    public float minX = -9f;
    public float maxX = 9f;

    // Koordinaten f�r die Snapping-Positionen auf der X-Achse
    private float[] xSnapPoints = { -7.5f, -4.5f, -1.5f, 1.5f, 4.5f, 7.5f };

    // Koordinaten f�r die Snapping-Positionen auf der Y-Achse
    private float[] ySnapPoints = { 1.5f, 3.5f, 5.5f, 7.5f, 9.5f, 11.5f };

    // Index f�r die aktuelle Position im xSnapPoints-Array
    private int xSnapIndex = 0;

    // Index f�r die aktuelle Position im ySnapPoints-Array
    private int ySnapIndex = 0;

    // Index f�r die ausgew�hlte S�ule
    public int selectedColumnIndex = 0;

    private GameObject selectedColumnObject;

    public string selectedColumnObjectName = "SelectedColumn";

    public string ballTag = "Ball";

    private GameObject currentTopBall;

    // Start is called before the first frame update
    void Start()
    {   // Setze den Start-Index auf den Index der urspr�nglichen Position des Balls
        xSnapIndex = System.Array.IndexOf(xSnapPoints, transform.position.x);
        ySnapIndex = System.Array.IndexOf(ySnapPoints, transform.position.y);

        // Falls die urspr�ngliche Position nicht in den definierten Snapping-Punkten liegt, setze den Index auf 0
        if (xSnapIndex == -1) xSnapIndex = 0;
        if (ySnapIndex == -1) ySnapIndex = 0;

        // Finde das GameObject der ausgew�hlten S�ule
        selectedColumnObject = GameObject.Find("Sidewall " + (selectedColumnIndex + 1));


    }

    // Update is called once per frame
    void Update()
    {
        ColorWall();

        Debug.Log("Before GetTopBallInSelectedColumn");

        GameObject topBall = GetTopBallInSelectedColumn();

        Debug.Log("After GetTopBallInSelectedColumn");

        currentTopBall = topBall; // Aktualisiere den aktuellen "Top Ball"

        Debug.Log("CurrentTop ball:" + currentTopBall);

        if (currentTopBall != null)
        {
            Debug.Log("Top Ball found!" + "Top Ball: " + topBall);
            MoveBall();
        }
    }


    void MoveBall()
    {
        Debug.Log("Entering MoveBall");

        if (currentTopBall != null)
        { // Bewegung auf der Y-Achse
            float verticalInput = Input.GetKey(KeyCode.UpArrow) ? 1f : Input.GetKey(KeyCode.DownArrow) ? -1f : 0f;

            // Wenn die linke Pfeiltaste gedr�ckt wurde und Y-Wert zwischen 9 und 13 liegt
            if (Input.GetKeyDown(KeyCode.LeftArrow) && transform.position.y >= 9f && transform.position.y <= 13f)
            {
                // Zum vorherigen Snapping-Punkt auf der X-Achse springen lassen
                JumpToPreviousXSnapPoint();
            }
            // Wenn die rechte Pfeiltaste gedr�ckt wurde und Y-Wert zwischen 9 und 13 liegt
            else if (Input.GetKeyDown(KeyCode.RightArrow) && transform.position.y >= 9f && transform.position.y <= 13f)
            {
                // Zum n�chsten Snapping-Punkt auf der X-Achse springen lassen
                JumpToNextXSnapPoint();
            }

            // Wenn die obere Pfeiltaste gedr�ckt wurde
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                // Zum n�chsten Snapping-Punkt auf der Y-Achse springen lassen
                JumpToNextYSnapPoint();
            }
            // Wenn die untere Pfeiltaste gedr�ckt wurde
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                // Zum vorherigen Snapping-Punkt auf der Y-Achse springen lassen
                JumpToPreviousYSnapPoint();
            }

            // Ball auf den ausgew�hlten Snapping-Punkt setzen
            Vector3 newPosition = new Vector3(GetNextXSnapPoint(), GetNextYSnapPoint(), transform.position.z);

            // Anwenden der Grenzen mithilfe von Mathf.Clamp
            float clampedX = Mathf.Clamp(newPosition.x, minX, maxX);
            float clampedY = Mathf.Clamp(newPosition.y, minY, maxY);

            // Aktualisieren der Position des Balls nach Anwendung der Grenzen
            transform.position = new Vector3(clampedX, clampedY, newPosition.z);
        }
        else
        { 
            Debug.Log("No top ball found."); 
        }
     Debug.Log("Exiting MoveBall");
    }

    void ColorWall()
    {   // �berpr�fe, ob Pfeiltaste nach links gedr�ckt wurde
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            // Ausgew�hlte S�ule nach links verschieben (falls nicht bereits am Anfang)
            if (selectedColumnIndex > 0)
            {
                selectedColumnIndex--;
            }
        }

        // �berpr�fe, ob Pfeiltaste nach rechts gedr�ckt wurde
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            // Ausgew�hlte S�ule nach rechts verschieben (falls nicht bereits am Ende)
            if (selectedColumnIndex < xSnapPoints.Length - 1)
            {
                selectedColumnIndex++;
            }
        }
        // F�rbe die Seitenw�nde der ausgew�hlten S�ule
        ColorAdjacentColumnWalls(selectedColumnIndex);
    }

    GameObject GetTopBallInSelectedColumn()
    {
        Debug.Log("Entering GetTopBallInSelectedColumn");

        // Finde die X-Positionen der beiden Seitenw�nde
        float xPosition1 = GetSidewallXPosition(selectedColumnIndex + 1);
        float xPosition2 = GetSidewallXPosition(selectedColumnIndex + 2);

        // Berechne den Mittelwert
        float averageXPosition = (xPosition1 + xPosition2) / 2f;

        Debug.Log("Selected Column: " + selectedColumnObject);

        if (selectedColumnObject != null)
        {
            // Finde den Ball mit dem Tag "Ball" in der ausgew�hlten S�ule
            GameObject[] ballsInColumn = GameObject.FindGameObjectsWithTag(ballTag);

            Debug.Log("Number of Balls: " + ballsInColumn.Length);

            float highestY = float.MinValue;
            GameObject topBall = null;

            float columnXPosition = selectedColumnObject.transform.position.x;

            foreach (GameObject ball in ballsInColumn)
            {
                // �berpr�fe, ob der Ball im Bereich der ausgew�hlten S�ule ist
                float ballXPosition = ball.transform.position.x;

                // �berpr�fe, ob der Ball in der ausgew�hlten S�ule ist
                if (Mathf.Approximately(ball.transform.position.x, averageXPosition))
                {
                    Debug.Log("Ball in Selected Column: " + ball);

                    // �berpr�fe, ob der Ball die h�chste Y-Position hat
                    if (ball.transform.position.y > highestY)
                    {
                        highestY = ball.transform.position.y;
                        topBall = ball;

                        Debug.Log("New Top Ball: " + topBall);
                    }
                }
            }

            // Debug-Ausgabe hinzuf�gen
            if (topBall != null)
            {
                Debug.Log("Top ball found at Y: " + highestY);
            }
            else
            {
                Debug.Log("No top ball found");
            }

            return topBall;
        }

        else
        {
            Debug.Log("Exiting GetTopBallInSelectedColumn");
            return null; // Wenn die ausgew�hlte S�ule nicht gefunden wurde
            
        }
    }
    // Funktion zum Abrufen des n�chsten Werts aus dem xSnapPoints-Array
    float GetNextXSnapPoint()
    {
        return xSnapPoints[xSnapIndex];
    }

    // Funktion zum Abrufen des n�chsten Werts aus dem ySnapPoints-Array
    float GetNextYSnapPoint()
    {
        return ySnapPoints[ySnapIndex];
    }

    // Funktion zum Springen zum vorherigen Snapping-Punkt auf der X-Achse
    void JumpToPreviousXSnapPoint()
    {
        // Zum vorherigen Index gehen (und Schleife wiederholen, wenn Anfang erreicht)
        xSnapIndex = (xSnapIndex - 1 + xSnapPoints.Length) % xSnapPoints.Length;
    }

    // Funktion zum Springen zum n�chsten Snapping-Punkt auf der X-Achse
    void JumpToNextXSnapPoint()
    {
        // Zum n�chsten Index gehen (und Schleife wiederholen, wenn Ende erreicht)
        xSnapIndex = (xSnapIndex + 1) % xSnapPoints.Length;
    }

    // Funktion zum Springen zum vorherigen Snapping-Punkt auf der Y-Achse
    void JumpToPreviousYSnapPoint()
    {
        // Zum vorherigen Index gehen (und Schleife wiederholen, wenn Anfang erreicht)
        ySnapIndex = (ySnapIndex - 1 + ySnapPoints.Length) % ySnapPoints.Length;
    }
    // Funktion zum Springen zum n�chsten Snapping-Punkt auf der Y-Achse
    void JumpToNextYSnapPoint()
    {
        // Zum n�chsten Index gehen (und Schleife wiederholen, wenn Ende erreicht)
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
                    // Farbe f�r benachbarte S�ulen
                    sideWall.GetComponent<Renderer>().material.color = Color.blue;
                }
                else
                {

                    // Farbe f�r nicht ausgew�hlte S�ulen
                    sideWall.GetComponent<Renderer>().material.color = Color.white;
                }
            }
        }
        float xPosition1 = GetSidewallXPosition(columnIndex + 1);
        float xPosition2 = GetSidewallXPosition(columnIndex + 2);

        // Berechne den Mittelwert
        float averageXPosition = (xPosition1 + xPosition2) / 2f;

        // Debug-Ausgabe des Mittelwerts
        Debug.Log("Average X Position: " + averageXPosition);
    }
    float GetSidewallXPosition(int sidewallIndex)
    {
        GameObject sideWall = GameObject.Find("Sidewall " + sidewallIndex);
        if (sideWall != null)
        {
            return sideWall.transform.position.x;
        }

        return 0f; // R�ckgabewert im Fehlerfall
    }

}
