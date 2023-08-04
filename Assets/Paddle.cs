using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{

    public float speed;
    public float rightEdge;
    public float leftEdge;
    public Rigidbody2D rb {get; private set;}

    private void Awake()
    {
        this.rb = GetComponent<Rigidbody2D>();

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");

        transform.Translate(Vector2.right * horizontal * Time.deltaTime * speed);

        if(transform.position.x < leftEdge){
            transform.position = new Vector2(leftEdge,transform.position.y);
        }

        transform.Translate(Vector2.right * horizontal * Time.deltaTime * speed);
        if(transform.position.x > rightEdge){
            transform.position = new Vector2(rightEdge,transform.position.y);
        }
    }
}
