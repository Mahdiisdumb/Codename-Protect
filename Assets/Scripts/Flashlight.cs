using UnityEngine;
using UnityEngine.InputSystem;
public class Flashlight:MonoBehaviour{
public Light flashlight;
public float duration=0.1f,distanceFromCamera=2f;
float timer,lastTapTime;
float doubleTapThreshold=0.3f;
Camera cam;
Player p;
void Awake(){cam=Camera.main;p=FindObjectOfType<Player>();}
void Update(){
if(p&&p.playerHP<=0){flashlight.enabled=false;return;}
Vector3 t=transform.position;
if(Mouse.current!=null){
var m=Mouse.current.position.ReadValue();
t=cam.ScreenToWorldPoint(new Vector3(m.x,m.y,distanceFromCamera));
if(Mouse.current.leftButton.wasPressedThisFrame)Flash();
}
if(Touchscreen.current!=null&&Touchscreen.current.primaryTouch.press.isPressed){
var tc=Touchscreen.current.primaryTouch;
var tp=tc.position.ReadValue();
t=cam.ScreenToWorldPoint(new Vector3(tp.x,tp.y,distanceFromCamera));
if(tc.press.wasPressedThisFrame){
if(Time.time-lastTapTime<doubleTapThreshold)Flash();
lastTapTime=Time.time;
}}
transform.position=t;
if(timer>0&&(timer-=Time.deltaTime)<=0)flashlight.enabled=false;
}
void Flash(){flashlight.enabled=true;timer=duration;}
}