#include <Servo.h>
#define LED_PIN 8
Servo servo;
char buffer[5];
int inputchar;
void setup(){
    Serial.begin(9600);
    servo.attach(3);
    pinMode(LED_PIN, OUTPUT);
}

void loop(){
  int i = 0;
    while(1) {
      if(Serial.available()) {
        buffer[i] = Serial.read();

        switch(buffer[i]){
          case 'o':
          // 読み込みデータが　o の場合
            digitalWrite(LED_PIN, HIGH);
            break;
          case 'p':  
          // 読み込みデータが　p の場合
            digitalWrite(LED_PIN, LOW);
            break;
        }
        //サーボ制御
        if(buffer[i] == '\0') {
          servo.write(atoi(buffer));
          break;
          }
          i++;
        }
      }
}
