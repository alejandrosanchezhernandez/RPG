using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class MovimientoPlayer : MonoBehaviour
{
    Animator animaciones;
    Rigidbody2D rb2d;
    Vector2 mov;
    CircleCollider2D attackcollider;
    public GameObject prefabSlash;
    Aura aura;

    public float speed = 0.2f;
    bool movePrevent;
    
    




    void Start()
    {
        
        
        animaciones = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        attackcollider = transform.GetChild(0).GetComponent<CircleCollider2D>();
        attackcollider.enabled = false;
        aura = transform.GetChild(1).GetComponent<Aura>();
        
        
        
    }

    
    void Update()
    {

        Movements();
        Animations();
        SwordAttack();
        PreventMove();
        SlashAttack();
    }
    private void FixedUpdate()
    {
        rb2d.MovePosition(rb2d.position + mov * speed * Time.deltaTime);
    }
    void Movements()
    {
        mov = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

    }
    void Animations()
    {
        if (mov != Vector2.zero)
        {
            animaciones.SetFloat("movX", mov.x);
            animaciones.SetFloat("movY", mov.y);
            animaciones.SetBool("Walking", true);
        }
        else
        {
            animaciones.SetBool("Walking", false);
        }

    }
    void SwordAttack()
    {
        AnimatorStateInfo stateinfo = animaciones.GetCurrentAnimatorStateInfo(0);
        bool attacking = stateinfo.IsName("PlayerAtack");

        if (Input.GetKeyDown("space") && !attacking)
        {
            animaciones.SetTrigger("Attacking");

        }
        if (mov != Vector2.zero) attackcollider.offset = new Vector2(mov.x / 20, mov.y / 15);

        if (attacking)
        {
            float playbacktime = stateinfo.normalizedTime;

            if (playbacktime > 0.33 && playbacktime < 0.66) attackcollider.enabled = true;

            else attackcollider.enabled = false;
        }

    }
    void SlashAttack()
    {
        AnimatorStateInfo stateinfo = animaciones.GetCurrentAnimatorStateInfo(0);
        bool loading = stateinfo.IsName("PlayerSlash");
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            animaciones.SetTrigger("Loading");
            aura.AuraStart();
        }
        else if (Input.GetKeyUp(KeyCode.LeftAlt) && Aura.cargado)
        {
            animaciones.SetTrigger("Attacking");
            float angle = Mathf.Atan2(animaciones.GetFloat("movY"), animaciones.GetFloat("movX") * Mathf.Rad2Deg);
            GameObject slashObj = Instantiate(prefabSlash, transform.position, Quaternion.AngleAxis(angle, Vector3.forward));
            if (animaciones.GetFloat("movX") <= -1f)
            {
                slashObj.transform.Rotate(new Vector3(0, 0, 180 * animaciones.GetFloat("movX")));
            }
            if (Mathf.Abs(animaciones.GetFloat("movY")) >= 1f)
            {
                slashObj.transform.Rotate(new Vector3(0, 0, 90 * animaciones.GetFloat("movY")));
            }
            if (animaciones.GetFloat("movX") <= -1f && (animaciones.GetFloat("movY") <= -1f))
            {
                slashObj.transform.Rotate(new Vector3(0, 0, -136 * animaciones.GetFloat("movX")));
            }
            if (animaciones.GetFloat("movX") >= 1f && (animaciones.GetFloat("movY") >= 1f))
            {
                slashObj.transform.Rotate(new Vector3(0, 0, -45 * animaciones.GetFloat("movY")));
            }
            if (animaciones.GetFloat("movX") >= 1f && (animaciones.GetFloat("movY") <= -1f))
            {
                slashObj.transform.Rotate(new Vector3(0, 0, 45 * animaciones.GetFloat("movX")));
            }
            if (animaciones.GetFloat("movX") <= -1f && (animaciones.GetFloat("movY") >= 1f))
            {
                slashObj.transform.Rotate(new Vector3(0, 0, -135 * animaciones.GetFloat("movY")));
            }
            Slash slash = slashObj.GetComponent<Slash>();
            slash.mov.x = animaciones.GetFloat("movX");
            slash.mov.y = animaciones.GetFloat("movY");
            aura.AuraStop();
        }
        if (loading)
        {
            movePrevent = true;
        }
        else
        {
            movePrevent = false;
        }
    }
    void PreventMove()
    {
        if (movePrevent)
        {
            mov = Vector2.zero;
        }
    }
   
}
