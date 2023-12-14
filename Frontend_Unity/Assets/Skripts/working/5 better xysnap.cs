using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 public class neu : MonoBehaviour
{
public float speed = 5f;
public float minY = 1.5f;
public float maxY = 13f;
public float minX = -9f;
public float maxX = 9f;

// Koordinaten für die Snapping-Positionen auf der X-Achse
private float[] xSnapPoints = { -7.5f, -4.5f, -1.5f, 1.5f, 4.5f, 7.5f };

// Koordinaten für die Snapping-Positionen auf der Y-Achse
private float[] ySnapPoints = { 1.5f, 3.5f, 5.5f, 7.5f, 9.5f, 11.5f };

// Index für die aktuelle Position im xSnapPoints-Array
private int xSnapIndex = 0;

// Index für die aktuelle Position im ySnapPoints-Array
private int ySnapIndex = 0;

// Start is called before the first frame update
void Start()
    {   // Setze den Start-Index auf den Index der ursprünglichen Position des Balls
        xSnapIndex = System.Array.IndexOf(xSnapPoints, transform.position.x);
        ySnapIndex = System.Array.IndexOf(ySnapPoints, transform.position.y);

        // Falls die ursprüngliche Position nicht in den definierten Snapping-Punkten liegt, setze den Index auf 0
        if (xSnapIndex == -1) xSnapIndex = 0;
        if (ySnapIndex == -1) ySnapIndex = 0;
    }

    // Update is called once per frame
    void Update()
{// Bewegung auf der Y-Achse
    float verticalInput = Input.GetKey(KeyCode.UpArrow) ? 1f : Input.GetKey(KeyCode.DownArrow) ? -1f : 0f;

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

    // Wenn die obere Pfeiltaste gedrückt wurde
    if (Input.GetKeyDown(KeyCode.UpArrow))
    {
        // Zum nächsten Snapping-Punkt auf der Y-Achse springen lassen
        JumpToNextYSnapPoint();
    }
    // Wenn die untere Pfeiltaste gedrückt wurde
    else if (Input.GetKeyDown(KeyCode.DownArrow))
    {
        // Zum vorherigen Snapping-Punkt auf der Y-Achse springen lassen
        JumpToPreviousYSnapPoint();
    }

    // Ball auf den ausgewählten Snapping-Punkt setzen
    Vector3 newPosition = new Vector3(GetNextXSnapPoint(), GetNextYSnapPoint(), transform.position.z);

    // Anwenden der Grenzen mithilfe von Mathf.Clamp
    float clampedX = Mathf.Clamp(newPosition.x, minX, maxX);
    float clampedY = Mathf.Clamp(newPosition.y, minY, maxY);

    // Aktualisieren der Position des Balls nach Anwendung der Grenzen
    transform.position = new Vector3(clampedX, clampedY, newPosition.z);
}

// Funktion zum Abrufen des nächsten Werts aus dem xSnapPoints-Array
float GetNextXSnapPoint()
{
    return xSnapPoints[xSnapIndex];
}

// Funktion zum Abrufen des nächsten Werts aus dem ySnapPoints-Array
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

// Funktion zum Springen zum nächsten Snapping-Punkt auf der X-Achse
void JumpToNextXSnapPoint()
{
    // Zum nächsten Index gehen (und Schleife wiederholen, wenn Ende erreicht)
    xSnapIndex = (xSnapIndex + 1) % xSnapPoints.Length;
}

// Funktion zum Springen zum vorherigen Snapping-Punkt auf der Y-Achse
void JumpToPreviousYSnapPoint()
{
    // Zum vorherigen Index gehen (und Schleife wiederholen, wenn Anfang erreicht)
    ySnapIndex = (ySnapIndex - 1 + ySnapPoints.Length) % ySnapPoints.Length;
}
// Funktion zum Springen zum nächsten Snapping-Punkt auf der Y-Achse
void JumpToNextYSnapPoint()
{
    // Zum nächsten Index gehen (und Schleife wiederholen, wenn Ende erreicht)
    ySnapIndex = (ySnapIndex + 1) % ySnapPoints.Length;
}
        
}
