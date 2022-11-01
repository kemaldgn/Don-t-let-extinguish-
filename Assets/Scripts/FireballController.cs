using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballController : MonoBehaviour
{
    public int attackDamage = 10;

    void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.tag == "Enemy"){
            Destroy(gameObject);
            other.gameObject.GetComponent<Enemy>().UpdateHealth(-attackDamage);
        }
    }


}
