﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_1 : Enemy {
    [Header("Set in Inspector: Enemy_1")]
    public float waveFrequency = 2;
    public float waveWidth = 4;
    public float waveRotY = 45;
    private float xO;
    private float birthTime;

	// Use this for initialization
	void Start () {
        xO = pos.x;
        birthTime = Time.time;
	}
    public override void Move()
    {
        Vector3 tempPos = pos;

        float age = Time.time - birthTime;
        float theta = Mathf.PI * 2 * age / waveFrequency;
        float sin = Mathf.Sin(theta);
        tempPos.x = xO + waveWidth * sin;
        pos = tempPos;

        Vector3 rot = new Vector3(0, sin * waveRotY); 
        this.transform.rotation = Quaternion.Euler(rot);
        base.Move();
        //print(bndCheck.isOnScreen);
    }
    // Update is called once per frame
    
}