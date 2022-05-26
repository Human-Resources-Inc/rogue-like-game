using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreCounter : MonoBehaviour
{
  public Text score;

  public void Start(){
      score = GetComponent<Text> ();
  }
  public void Update() {
      score.text = "SCORE: " +  GameManager.instance.enemiesKilled;
  }
}
