using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*// implement serial read functionality in functioning ball behavior script to connect to arduino and Edu-Exo
//enable the serial communication: In the menu bar, select: ’File’ → ’Build Settings’ → ’Player Settings ...’.
//This opens the inspector in the main window scroll down to ’Configuration’ andselect ’.NET Standard 2.0’ for API compatibility level.
//ake sure that you closed the serial connection between the Aduino IDE and the Arduino microcontroller. Otherwise, you may encounter
// problems with the serial communication between Unity and the Arduino. Closing the serial monitor in the Arduino IDE should do that.
// If the game starts before you can control the bar with the exoskeleton, add a delay at the beginning ofthe game.
// If you restart the game and have a strange behavior, it can help to also restart the Arduino: just press the small reset button on the Arduino board.
using System.IO.Ports;// libraries that for the serial communication
using System.IO;
using System;*/

public class functioningcolorandmovement : MonoBehaviour
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

    // Index für die ausgewählte Säule
    private int selectedColumnIndex = 0;

   
/*SerialPort sp; // declared serial port object*/

    // Start is called before the first frame update
    void Start()
    {
       /* sp = new SerialPort("COM3", 19200); //initialized and opened the port in the enter the COM port number you got from the               
        sp.ReadTimeout = 10;             //Arduino IDE. Make sure the baud rate matches the one from the Arduino program
        sp.Open();                    //If you have problems with the serial communication, you can try increasing the baud rate*/

        // Setze den Start-Index auf den Index der ursprünglichen Position des Balls
        xSnapIndex = System.Array.IndexOf(xSnapPoints, transform.position.x);
        ySnapIndex = System.Array.IndexOf(ySnapPoints, transform.position.y);

        // Falls die ursprüngliche Position nicht in den definierten Snapping-Punkten liegt, setze den Index auf 0
        if (xSnapIndex == -1) xSnapIndex = 0;
        if (ySnapIndex == -1) ySnapIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
       /* if (sp.IsOpen)
        {
            try //try-catch statement prevents game from stopping in case the serial readfunction does not work( because no data is being sent by the Arduino)
            {
                int value = sp.ReadByte(); //read the bytes sent from the Arduino and store them locally in the integer 
                float positionUnity = (10 - ((float)value / 10)); // map the input into our Unity coordinate system and assign it to the float variable positionUnity
                transform.position = new Vector3(transform.position.x, positionUnity, transform.position.z);// assign the mapped position to the ball, update the game object’s y-position with new value at every frame
            }
            catch (System.Exception e)
            {
            }
        }*/
    
        ColorWall();
      
        MoveBall();
    }


    void MoveBall()
    {// Bewegung auf der Y-Achse
        float verticalInput = Input.GetKey(KeyCode.UpArrow) ? 1f : Input.GetKey(KeyCode.DownArrow) ? -1f : 0f;

        // Wenn die linke Pfeiltaste gedrückt wurde und Y-Wert zwischen 9 und 13 liegt
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            // Zum vorherigen Snapping-Punkt auf der X-Achse springen lassen
            JumpToPreviousXSnapPoint();
        }
        // Wenn die rechte Pfeiltaste gedrückt wurde und Y-Wert zwischen 9 und 13 liegt
        else if (Input.GetKeyDown(KeyCode.RightArrow))
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

    void ColorWall()
    {   // Überprüfe, ob Pfeiltaste nach links gedrückt wurde
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            // Ausgewählte Säule nach links verschieben (falls nicht bereits am Anfang)
            if (selectedColumnIndex > 0)
            {
                selectedColumnIndex--;
            }
        }

        // Überprüfe, ob Pfeiltaste nach rechts gedrückt wurde
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            // Ausgewählte Säule nach rechts verschieben (falls nicht bereits am Ende)
            if (selectedColumnIndex < xSnapPoints.Length - 1)
            {
                selectedColumnIndex++;
            }
        }
        // Färbe die Seitenwände der ausgewählten Säule
        ColorAdjacentColumnWalls(selectedColumnIndex);
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

    void ColorAdjacentColumnWalls(int columnIndex)
    {
        for (int i = 1; i <= 7; i++)
        {
            GameObject sideWall = GameObject.Find("Sidewall " + i);
            if (sideWall != null)
            {
                if (i == columnIndex + 1 || i == columnIndex + 2)
                {
                    // Farbe für benachbarte Säulen
                    sideWall.GetComponent<Renderer>().material.color = Color.blue;
                }
                else
                {

                    // Farbe für nicht ausgewählte Säulen
                    sideWall.GetComponent<Renderer>().material.color = Color.white;
                }
            }
        }
    }

}
