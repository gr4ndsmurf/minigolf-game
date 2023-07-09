using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public BallMovement bm;
    public int clickInt;
    public Rigidbody ballRB;

    public AudioSource nextlevelSound;

    public TMP_Text clickText;
    public TMP_Text levelText;
    // Start is called before the first frame update
    void Start()
    {
        bm = GameObject.FindGameObjectWithTag("Player").GetComponent<BallMovement>();
        levelText.text = SceneManager.GetActiveScene().buildIndex.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        clickInt = bm.click;
        clickText.text = ((int)clickInt).ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            nextlevelSound.Play();
            StartCoroutine(loadNextScene());
            ballRB = bm.gameObject.GetComponent<Rigidbody>();
            ballRB.constraints = RigidbodyConstraints.FreezePosition;
        }
    }

    IEnumerator loadNextScene()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
