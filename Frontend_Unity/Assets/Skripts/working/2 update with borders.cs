using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControl : MonoBehaviour
{
    public float speed = 5f;
    public float minY = 1.5f;
    public float maxY = 13f;
    public float minX = -9f;
    public float maxX = 9f;


    void Update()
    {
        float verticalInput = Input.GetKey(KeyCode.UpArrow) ? 1f : Input.GetKey(KeyCode.DownArrow) ? -1f : 0f;
        float horizontalInput = Input.GetKey(KeyCode.LeftArrow) ? -1f : Input.GetKey(KeyCode.RightArrow) ? 1f : 0f;

        // Falls die Aufwärtstaste oder Abwärtstaste gedrückt ist, wird die horizontale Bewegung auf 0 gesetzt
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow))
        {
            horizontalInput = 0f;
        }

        // Falls die Links- oder Rechtstaste gedrückt ist, wird die vertikale Bewegung auf 0 gesetzt
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
        {
            verticalInput = 0f;
        }

        Vector3 movement = new Vector3(horizontalInput, verticalInput, 0).normalized;

        // Aktualisierung der Position des Balls mit den Grenzen
        Vector3 currentPosition = transform.position + movement * speed * Time.deltaTime;

        // Anwenden der Grenzen mithilfe von Mathf.Clamp
        float clampedX = Mathf.Clamp(currentPosition.x, minX, maxX);
        float clampedY = Mathf.Clamp(currentPosition.y, minY, maxY);

        // Aktualisieren der Position des Balls nach Anwendung der Grenzen
        transform.position = new Vector3(clampedX, clampedY, currentPosition.z);
    }
}

