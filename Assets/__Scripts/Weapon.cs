using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum WeaponType
{
    none,       //The default/no weapon
    blaster,    //A simple blaster
    spread,     //Two shots simultaneously
    phaser,     //[NI] Shots that move in waves
    missile,    //[NI] Homing missiles
    laser,      //[NI] Damage over time
    shield      //Raise shieldLevel

}
/// <summary>
/// The WeaponDefinition class allows you to set the properties
/// of a specific weapon in the Inspector. The Main class has
/// an array of WeaponDefinitions that make this possible
/// </summary>

[System.Serializable]
public class WeaponDefinition{
    public WeaponType type = WeaponType.none;
    public string letter;
    public Color color = Color.white;
    public GameObject projectilePrefab;
    public Color projectileColor = Color.white;
    public float damageOnHit = 0;
    public float continuousDamage = 0;
    public float delayBetweenShots = 0;
    public float velocity = 20;
}

public class Weapon : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
