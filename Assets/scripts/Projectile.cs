﻿using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {
	public float speed = 5; // Speed at which the projectile moves
	public Rigidbody2D rb; // RB for collisions/physics

	public int teamId; // team id to know who to hit
	public AttackInfo atkInfo; // attack info to pass along to whoever is hit

	private bool expired = false; // used so that only 1 hit occurs

	// launch the projectile
	void OnEnable ()
	{
		rb.AddForce (transform.up * speed * 10);
		expired = false;
	}
		
	void OnTriggerEnter2D(Collider2D col)
	{
		// If we havent hit a valid target yet
		if (!expired) {
			// if object is not an enemy
			if (col.gameObject.CompareTag ("Enemy")) {
				col.gameObject.BroadcastMessage ("TakeDamage", atkInfo);
			} else {
				switch (col.gameObject.tag) {
				case "Player":
				case "Projectile":
				case "UnCollidable":
					return;
					break;
				}
			}
			expired = true;
			SendMessage ("Destroy", 0.2f);
		}
	}
}