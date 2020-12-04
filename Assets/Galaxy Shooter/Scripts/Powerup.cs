using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;
    [SerializeField]
    private int powerupID; // 0 = Triple Shot 1 = Speed Boost 2 = Shields
    [SerializeField]
    private AudioClip _clip;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if(transform.position.y < -7){
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other){
       
        if(other.tag =="Player"){
            // Access the Player
            Player player = other.GetComponent<Player>();

            AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position, 1f);

            if(player != null){

                if(powerupID == 0){
                    player.TripleShotPowerupOn();
                }else if(powerupID == 1){
                    player.SpeedBoostPowerupOn();
                }else if(powerupID == 2){
                    player.EnableShields();
                }
               
            }

            // Destroy the Power up object
            Destroy(this.gameObject);

        }
        
    }
}
