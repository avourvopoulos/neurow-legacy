#pragma strict

function Awake()//OPTIMIZED
{	
	animation["" + animation.clip.name].normalizedTime = Random.Range(0.0, 1.0);
}