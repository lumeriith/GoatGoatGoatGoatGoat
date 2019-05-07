using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level03 : MonoBehaviour {
    public Arrow arrow;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Goat.instance.currentWeight > 1f)
        {
            GameManager.instance.Message("왼쪽 아래에 표시된 자신의 최대 몸무게를 넘게 먹을 수 없습니다.");
            arrow.Indicate(8f);
            this.enabled = false;
        }
	}
}
