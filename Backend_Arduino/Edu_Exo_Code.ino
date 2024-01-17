#include <Servo.h>

Servo myservo;
int minServoPos; // Minimum servo position
int maxServoPos; // Maximum servo position
int currentServoPos;
int wantedServoPos = 0; 
char unityCommand;
int servoAnalogInPin = A3; 
int emgAnalogInPin = A1; 
int posIs; 
int range;

void setup() {
    Serial.begin(19200);
    myservo.detach();

    // Manual calibration routine
    //calibrateServo();
}

void loop() {
    if (Serial.available() > 0) {
        unityCommand = Serial.read();
        handleUnityCommand(unityCommand); 
    }

    
    readServoPosition();
    
    delay(500);

    sendEMGSignal(); 
    
    delay(500);
    

}    

void calibrateServo() {
    int firstPosition, secondPosition;

    Serial.println("Move servo to one end position and send 'M'");
    while(Serial.available() == 0 || Serial.read() != 'M'); // Wait for 'M' command
    firstPosition = readServoPosition(); // Read first position

    Serial.println("Move servo to the other end position and send 'M'");
    while(Serial.available() == 0 || Serial.read() != 'M'); // Wait for 'M' command
    secondPosition = readServoPosition(); // Read second position

    // Assign min and max positions
    if (firstPosition < secondPosition) {
        minServoPos = firstPosition;
        maxServoPos = secondPosition;
    } else {
        minServoPos = secondPosition;
        maxServoPos = firstPosition;
    }

    range = maxServoPos - minServoPos;
    Serial.print("Range of the eduExo: ");
    Serial.println(range); 
}

void handleUnityCommand(char command) {
    switch (command) {
        case 'R': //Move servo to right
            moveServo(1); // Move 1 degree
            break;
        case 'L': //Move servo to left
            moveServo(-1); // Move -1 degree
            break;
        case 'E': //Read the current servo position
            Serial.write(readServoPosition());
            break; 
        case 'X': //Read EMG
            sendEMGSignal(); 
            break;
    }
}

void moveServo(int step) {
    wantedServoPos += step;

    myservo.attach(3);
    myservo.write(wantedServoPos);

    delay(500);
  
}

int readServoPosition() {
    for (int i = 0; i < 50; i++) {
    posIs += analogRead(servoAnalogInPin);
    }

    posIs = posIs/50; 

    Serial.print("Motor-Encoder: ");
    Serial.println(posIs);

    return posIs; 
}

void sendEMGSignal() {
    int emgSignal = analogRead(emgAnalogInPin);
    Serial.print("EMG-Value: "); 
    Serial.println(emgSignal); 
}
