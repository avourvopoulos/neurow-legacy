//Light_Flicker.js

var time : float = .2;
var min : float = .5;
var max : float = 5;
var useSmooth = false;
var smoothTime : float = 10;

function Start () {
	if(useSmooth == false && light){
	InvokeRepeating("OneLightChange", time, time);
	}
}
function OneLightChange () {
	light.intensity = Random.Range(min,max);

}

function Update () {
	if(useSmooth == true && light){
		light.intensity = Mathf.Lerp(light.intensity,Random.Range(min,max),Time.deltaTime*smoothTime);
	}
	if(light == false){
		print("Please add a light component for light flicker");
	}
}