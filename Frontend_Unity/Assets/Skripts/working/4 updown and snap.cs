using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class updownandsnap : MonoBehaviour
{
    public float speed = 5f;
    public float minY = 1.5f;
    public float maxY = 13f;
    public float minX = -9f;
    public float maxX = 9f;

    // Koordinaten für die Snapping-Positionen auf der X-Achse
    private float[] xSnapPoints = { -7.5f, -4.5f, -1.5f, 1.5f, 4.5f, 7.5f };

    // Index für die aktuelle Position im xSnapPoints-Array
    private int xSnapIndex = 0;

    void Update()
    {
        // Bewegung auf der Y-Achse
        float verticalInput = Input.GetKey(KeyCode.UpArrow) ? 1f : Input.GetKey(KeyCode.DownArrow) ? -1f : 0f;

        // Wenn der Y-Wert zwischen 9 und 13 liegt, Snapping-Mechanismus aktivieren
        if (transform.position.y >= 9f && transform.position.y <= 13f)
        {
            // Wenn die linke Pfeiltaste gedrückt wurde
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                // Zum vorherigen Snapping-Punkt springen lassen
                JumpToPreviousSnapPoint();
            }
            // Wenn die rechte Pfeiltaste gedrückt wurde
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                // Zum nächsten Snapping-Punkt springen lassen
                JumpToNextSnapPoint();
            }
        }

        // Ball auf den ausgewählten Snapping-Punkt setzen
        Vector3 newPosition = new Vector3(GetNextXSnapPoint(), transform.position.y + verticalInput * speed * Time.deltaTime, transform.position.z);

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

    // Funktion zum Springen zu vorherigem Snapping-Punkt auf der X-Achse
    void JumpToPreviousSnapPoint()
    {
        // Zum vorherigen Index gehen (und Schleife wiederholen, wenn Anfang erreicht)
        xSnapIndex = (xSnapIndex - 1 + xSnapPoints.Length) % xSnapPoints.Length;
    }

    // Funktion zum Springen zu nächstem Snapping-Punkt auf der X-Achse
    void JumpToNextSnapPoint()
    {
        // Zum nächsten Index gehen (und Schleife wiederholen, wenn Ende erreicht)
        xSnapIndex = (xSnapIndex + 1) % xSnapPoints.Length;
    }
}
