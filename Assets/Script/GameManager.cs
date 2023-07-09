using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public BallMovement bm;
    public int clickInt;
    // Start is called before the first frame update
    void Start()
    {
        bm = GameObject.FindGameObjectWithTag("Player").GetComponent<BallMovement>();
        
    }

    // Update is called once per frame
    void Update()
    {
        clickInt = bm.click;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
