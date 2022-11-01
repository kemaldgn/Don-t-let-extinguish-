using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Enemy : MonoBehaviour
{

    private Transform player;
    private Rigidbody2D rb;
    public int health;
    public int maxHealth;

    private Vector2 movement;

    public float moveSpeed = 2.5f;
    [SerializeField]private int attackDamage = 10;
    [SerializeField] private int attackSpeed = 1;
    private float canAttack;

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = 30;
        health = maxHealth;
        player = GameObject.Find("Player").transform;
        //transform.position = new Vector3((float)13,(float)6,-1);
        rb = this.GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = -player.position + transform.position;
        float angle = Mathf.Atan2(direction.y,direction.x) * Mathf.Rad2Deg;
        rb.rotation = angle ;
        direction.Normalize();
        movement = direction;
        
    }

    void FixedUpdate(){
        moveCharacter(movement);
    }

    void moveCharacter(Vector2 direction){
        rb.MovePosition((Vector2)transform.position+(direction * moveSpeed * Time.deltaTime));
    }

    public void OnCollisionStay2D(Collision2D other){
        
        if(other.gameObject.tag == "Player"){
            
            if(attackSpeed <= canAttack){
            other.gameObject.GetComponent<PlayerHealth>().UpdateHealth(-attackDamage);
            canAttack = 0f;
        }   else{
            canAttack += Time.deltaTime;
        }
        }
    }

    public void UpdateHealth(int mod){
        health += mod;

        if(health > maxHealth){
            health = maxHealth; // if enemy gains health in the game, it will increase the max health
        }else if(health <= 0){ // an enemy died
            health = 0;
            Destroy(gameObject);
            Debug.Log("Enemy dead");
            ScoreController.scoreNum++;
        }
    }

  
}
