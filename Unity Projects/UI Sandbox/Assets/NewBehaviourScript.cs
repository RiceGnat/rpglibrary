﻿using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		EnumerableView view = GetComponent<EnumerableView>();

		view.Bind(new int[] { 1, 2, 3 });
		view.Draw();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}