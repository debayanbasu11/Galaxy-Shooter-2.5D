using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{

    private float _speed = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);

        // Destroy the laser if position of y is >= 6 i.e. out of screen
        if(transform.position.y >= 6){

            if(transform.parent != null){
                Destroy(transform.parent.gameObject);
            }
            Destroy(this.gameObject);
        }
    }
}
