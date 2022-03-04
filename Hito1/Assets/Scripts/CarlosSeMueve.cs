using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarlosSeMueve : MonoBehaviour
{
    public Rigidbody2D rb;
    public float h; //Horizontal
    public float speed = 10;
    public float jump = 10;
    bool isJumping = false;
    private int saltosquellevas = 0;
    private int saltosadicionales = 1;
    public BoxCollider2D bc;
    private bool facingRight = true;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
   






    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }


    // Update is called once per frame
    void Update()
    { //Si apretas dcha o izqda añada fuerzas.
        h = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(h * speed, rb.velocity.y);

        Jump();
        BetterJump();
        
        //No es universal, deberia ser que ese Keycode sea a eleccion del jugador
        if (Input.GetKeyDown(KeyCode.A)){
            facingRight = false;
            FlipFace(facingRight);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            facingRight = true;
            FlipFace(facingRight);
        }
        
       
       

    }
    void Jump() {



        if (Input.GetKeyDown(KeyCode.Space))
        {
            
            if (saltosquellevas <= saltosadicionales ) { 
            isJumping = true;
                //rb.velocity = new Vector2( rb.velocity.x, jump);//
                rb.velocity = Vector2.up * jump;
                saltosquellevas++;
                
            }
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            saltosquellevas = 0;
            isJumping = false;
        }
    }
    void BetterJump() { 
            
            if (rb.velocity.y < 0)
            {

               
                rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            }
            if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
            {
                rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
            }
        }
        
    
    void FlipFace(bool facingR)
    {
        float xhorizontal = gameObject.transform.localScale.x;
        if (facingR){

            gameObject.transform.localScale = new Vector3(Mathf.Abs(xhorizontal), gameObject.transform.localScale.y, 1f);


        }
        else if (!facingR)
        {
                gameObject.transform.localScale = new Vector3(-Mathf.Abs(xhorizontal), gameObject.transform.localScale.y, 1f);
            
        }
            

    }


}
