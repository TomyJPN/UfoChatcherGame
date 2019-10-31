// 変数の定義
#define LED_PIN 8
 
void setup(){
  // シリアルポートを9600 bps[ビット/秒]で初期化
  Serial.begin(9600);
  // 開始メッセージ
  Serial.write("START");
  Serial.write("\n");
  pinMode(LED_PIN, OUTPUT);
}
 
void loop(){
  int input;
  // シリアルポートより1文字読み込む
  input = Serial.read();
  if(input != -1 ){
    Serial.print(input);
    switch(input){
      case 'o':  //on の場合
        digitalWrite(LED_PIN, HIGH);
        Serial.print("LED ON\n");
        break;
      case 'f':  //off の場合  
        digitalWrite(LED_PIN, LOW);
        Serial.print("LED OFF\n");
        break;
    }
  }
}
