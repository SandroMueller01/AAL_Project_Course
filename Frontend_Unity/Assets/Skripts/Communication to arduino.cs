// implement serial read functionality in functioning ball behavior script to connect to arduino and Edu-Exo
//enable the serial communication: In the menu bar, select: ’File’ → ’Build Settings’ → ’Player Settings ...’.
//This opens the inspector in the main window scroll down to ’Configuration’ andselect ’.NET Standard 2.0’ for API compatibility level.
//ake sure that you closed the serial connection between the Aduino IDE and the Arduino microcontroller. Otherwise, you may encounter
// problems with the serial communication between Unity and the Arduino. Closing the serial monitor in the Arduino IDE should do that.
// If the game starts before you can control the bar with the exoskeleton, add a delay at the beginning ofthe game.
// If you restart the game and have a strange behavior, it can help to also restart the Arduino: just press the small reset button on the Arduino board.
/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;// libraries that for the serial communication
using System.IO;
using System;

public class Communicationtoarduino : MonoBehaviour
{
SerialPort sp; // declared serial port object

void Start()
    {
        sp = new SerialPort("COM5", 9600); //initialized and opened the port in the enter the COM port number you got from the               
        sp.ReadTimeout = 10;             //Arduino IDE. Make sure the baud rate matches the one from the Arduino program
        sp.Open();                    //If you have problems with the serial communication, you can try increasing the baud rate
    }

void Update()
    {
        if (sp.IsOpen)
        {
            try //try-catch statement prevents game from stopping in case the serial readfunction does not work( because no data is being sent by the Arduino)
            {
                int value = sp.ReadByte(); //read the bytes sent from the Arduino and store them locally in the integer 
                float positionUnity = (10 - ((float)value / 10)); // map the input into our Unity coordinate system and assign it to the float variable positionUnity
                transform.position = new Vector3(transform.position.x, positionUnity, transform.position.z);// assign the mapped position to the ball, update the game object’s y-position with new value at every frame
             }
catch(System.Exception e) {
                }
            }
         }
}
*/