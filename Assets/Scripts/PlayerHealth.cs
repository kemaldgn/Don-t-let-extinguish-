using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    
    private int health = 0;
    [SerializeField] private int maxHealth  = 100;

    public HealthBar healthBar;

    

    void Start()
    {
        health = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateHealth(int mod){
        health += mod;

        if(health > maxHealth){
            health = maxHealth; // if we gain health in the game, it will increase the max health
        }else if(health <= 0){
            health = 0;
            Debug.Log("Player dead");
            ScoreController.finalScoreVal = ScoreController.scoreNum;
            PlayerDied();
            ScoreController.scoreNum = 0;
            
        }
        healthBar.setHealth(health);
    }


private void PlayerDied(){
    LevelManager.instance.GameOver();
    gameObject.SetActive(false);
}
}