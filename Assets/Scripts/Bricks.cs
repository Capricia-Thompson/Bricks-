using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bricks : MonoBehaviour
{
    public SpriteRenderer spriteRenderer { get; private set; }
    public int health { get; private set; }
    public Color[] states;

    public Transform burst;

    public int points = 1;

    AudioSource audio;



    private void Awake()
    {
        this.spriteRenderer = GetComponent<SpriteRenderer>();
    }


    // Start is called before the first frame update
    void Start()
    {
        this.health = this.states.Length;
        this.spriteRenderer.material.color = this.states[this.health - 1];
        audio = GetComponent<AudioSource>();
    }

    void OnCollisionEnter2D(Collision2D other)
    {

        if (other.transform.CompareTag("Ball"))
        {
            this.health--;
            FindObjectOfType<Manager>().BrickHit(this);
            if (this.health <= 0)
            {
                Destroy(this.gameObject);
                FindObjectOfType<Manager>().BrickDestroyed();
                Transform brickBurst = Instantiate(burst, other.transform.position, other.transform.rotation);
                Destroy(brickBurst.gameObject, 2f);
            }
            else
            {
                Debug.Log("hit");
                this.spriteRenderer.material.color = this.states[this.health - 1];
            }
        }
    }

    public void Hit()
    {
        audio.Play();
    }
}
