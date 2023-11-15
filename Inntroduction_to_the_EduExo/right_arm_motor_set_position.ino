#include <Servo.h>    //include arduino servo library, allows us to control motor // right arm

Servo myservo;

int servoAnalogInPin = A3;    //input motor position signal right
int posIs;                    // store sensor data
float posIsDegr;             // new valiable with decimal places (slower computatuion speed), right arm


void setup() {
Serial.begin(9600);
delay(100);
myservo.attach(3); 
 Serial.begin(9600);          // send motor position to PC
 delay(1000);                // define to which pin my motor is attached to
}

void loop() {
myservo.write(positionDesired);    //sends desired position to motor


 posIs = analogRead(servoAnalogInPin);           //aquire value and store it as variable posIs
  
 posIsDegr = (90.0/(88.0-280.0))*(posIs-280.0);   //linear interpolation    right
 Serial.print("Position[degrees]:");
 Serial.println(posIsDegr);                          // display on PC
 delay(100);                                     // time to execute code
}
