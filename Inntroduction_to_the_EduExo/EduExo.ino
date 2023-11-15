#include <Servo.h>

int servoAnalogInPin = A3;    //input motor position signal 
int posIs;                    // store sensor data
float posIsDeg;             // new valiable with decimal places (slower computatuion speed)
 
void setup(){                 // set pinmodes, initialize libraries
 Serial.begin(9600);          // send motor position to PC
 delay(1000);
}

 void loop(){
 posIs = analogRead(servoAnalogInPin);           //aquire value and store it as variable posIs
 posIsDeg = (90.0/(540.0-280.0))*(posIs-280.0);   //linear interpolation
 
 Serial.print("Position[degrees]:");
 Serial.println(posIsDeg);                          // display on PC
 delay(100);                                     // time to execute code -> shorten when control system is implemented?
}
