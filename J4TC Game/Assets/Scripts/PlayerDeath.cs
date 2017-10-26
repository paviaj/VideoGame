﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
        if (gameObject.transform.position.y < -6)
        {
            Die();
        }
	}

    void Die()
    {
        SceneManager.LoadScene("Tutorial");
    }
}
