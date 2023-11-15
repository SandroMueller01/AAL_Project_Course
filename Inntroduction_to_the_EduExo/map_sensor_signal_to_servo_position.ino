#include <Servo.h>

Servo myservo;  //rechts
int servoAnalogInPin = A3;
int posServo1 = 90;
int posServo2 = 180;
int posSensor1;
int posSensor2;
int rep = 3;
int counter = 0;


void setup(){
Serial.begin(9600);
delay(100);
myservo.attach(3);
}

void loop(){
myservo.write(posServo1);
delay(2000);
posSensor1 = analogRead(servoAnalogInPin);
myservo.write(posServo2);
delay(2000);
posSensor2 = analogRead(servoAnalogInPin);
Serial.println(posServo1);
Serial.println(posServo2);
Serial.println(posSensor1);
Serial.println(posSensor2);
counter ++;
if (rep == counter)
{
exit(0);
}
}

/*posIs = analogRead(servoAnalogInPin);
int posIsServo = map(posIs, posSensor1, posSensor2,
posServo1, posServo2);
}*/
