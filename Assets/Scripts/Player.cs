using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public int score;
    public Text scoreText;
    private Animation thisAnimation;
    private bool gameOver = false;
    void Start()
    {
        thisAnimation = GetComponent<Animation>();
        thisAnimation["Flap_Legacy"].speed = 3;
        score = 0;
    }
    void Update()
    {
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -5, 5), 0);
        if (Input.GetKeyDown(KeyCode.Space) && !gameOver) 
        {
            thisAnimation.Play();
            if (transform.position.y < 4)
                GetComponent<Rigidbody>().AddForce(new Vector3(0, 300, 0));
        }
        if (transform.position.y < -4.5)
            SceneManager.LoadScene("GameOver");
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Obstacle")
            gameOver = true;
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "ClearObstacle")
        {
            if (!gameOver)
            {
                score++;
                scoreText.text = "SCORE : " + score;
            }
            else
                scoreText.text = "GAME OVER!";
        }
    }
}
