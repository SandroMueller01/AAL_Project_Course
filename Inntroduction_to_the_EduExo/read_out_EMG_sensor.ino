int emgAnalogInPin = A1;
int emgSignal = 0;


void setup() {
Serial.begin(9600);
delay(100);
}

void loop() {
emgSignal = analogRead(emgAnalogInPin);
Serial.print("EMG:");
Serial.println(emgSignal);
delay(100);
}
