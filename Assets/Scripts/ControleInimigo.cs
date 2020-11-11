using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControleInimigo : MonoBehaviour
{
    public AudioSource som1;
    private Rigidbody2D rig;
    private float mov = 1F;
    private Animator animator;

    public GameObject vitimas;
    public Text txtKills;
    private int kill = 0;

    public Text txtVidas;
    public static int life;

    void Start()
    {
        life = 10;

        som1 = GetComponents<AudioSource>()[0];
        rig = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        txtKills.text = "Kill: "+kill;
        txtVidas.text = "Life: "+life;
    }

    void Update()
    {
        if (mov > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else if (mov < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        rig.velocity = new Vector2(mov, rig.velocity.y);
    }
    
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (col.gameObject.transform.position.y > gameObject.transform.position.y + 1)
            {
                animator.SetBool("Morto", true);
                mov = 0;
                som1.Play();

                kill++;
                txtKills.text = "Kill: " + kill;
            }
            else
            {
                if(life == 1){
                    Destroy(col.gameObject);
                    SceneManager.LoadScene("GameOver");
                }
                else{
                    life--;
                    txtVidas.text = "Life: "+life;
                }
            }
        }
        else
        {
            mov = mov * -1;
            
        }
    }
}

