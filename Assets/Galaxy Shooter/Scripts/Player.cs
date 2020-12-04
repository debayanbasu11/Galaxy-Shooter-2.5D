using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public bool canTripleShot = false; 
    public bool isSpeedBoostActive = false;
    public bool shieldsActive = false;
    public int lives = 3;

    [SerializeField]
    private GameObject _explosionPrefab;
    [SerializeField]
    private float _speed = 5.0f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private float _fireRate = 0.25f;
    [SerializeField]
    private GameObject _shieldGameObject;
    [SerializeField]
    private GameObject[] _engines;

    private float _canFire = 0.0f;
    private int hitCount = 0;

    private UIManager _uiManager;
    private GameManager _gameManager;
    private SpawnManager _spawnManager;

    private AudioSource _audioSource;


    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);

        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        
        if(_uiManager != null){
            _uiManager.UpdateLives(lives);
        }

        if(_spawnManager != null){
            _spawnManager.StartSpawnRoutines();
        }

        _audioSource = GetComponent<AudioSource>();

        hitCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();

        if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButton(0)){
           
           Shoot(); 
        } 
    }

    private void Shoot(){

        // If triple shot then shoot three lasers else shoot one

        if(Time.time > _canFire){

            _audioSource.Play();

            if(canTripleShot){
                Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
    
            }else{
                Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.88f, 0), Quaternion.identity);
               
            }
            _canFire = Time.time + _fireRate;
        }

    }

    private void Movement(){

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // if speed boost is active then move at 3x speed
        if(isSpeedBoostActive){
            transform.Translate(Vector3.right * _speed * 3f * horizontalInput * Time.deltaTime );
            transform.Translate(Vector3.up * _speed * 3f * verticalInput * Time.deltaTime );
  
        }else{
            transform.Translate(Vector3.right * _speed * horizontalInput * Time.deltaTime );
            transform.Translate(Vector3.up * _speed * verticalInput * Time.deltaTime );

        }

        if(transform.position.y > 0){
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }else if(transform.position.y < -4.2f){
            transform.position = new Vector3(transform.position.x, -4.2f, transform.position.z);
        }

        if(transform.position.x > 9.3f){
            transform.position = new Vector3(-9.3f, transform.position.y, transform.position.z);
        }else if(transform.position.x < -9.3f){
            transform.position = new Vector3(9.3f, transform.position.y, transform.position.z);
        }

    }

    public void TripleShotPowerupOn(){
        canTripleShot = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    public IEnumerator TripleShotPowerDownRoutine(){
        yield return new WaitForSeconds(5.0f);
        canTripleShot = false;
    }

    
    public void SpeedBoostPowerupOn(){
        isSpeedBoostActive = true;
        StartCoroutine(SpeedBoostPowerDownRoutine());
    }

    public IEnumerator SpeedBoostPowerDownRoutine(){
        yield return new WaitForSeconds(5.0f);
        isSpeedBoostActive = false;
    }

    public void Damage(){

        if(shieldsActive){
            shieldsActive = false;
            _shieldGameObject.SetActive(false);

            return;
        }

        hitCount++;

        if(hitCount == 1){
            _engines[0].SetActive(true);
        }else if(hitCount == 2){
            _engines[1].SetActive(true);
        }

        lives--;

        _uiManager.UpdateLives(lives);

        if(lives < 1){
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            _gameManager.gameOver = true;
            _uiManager.ShowTitleScreen();
            Destroy(this.gameObject);
        }
    }

    public void EnableShields(){
        shieldsActive = true;
        _shieldGameObject.SetActive(true);
    }

}
