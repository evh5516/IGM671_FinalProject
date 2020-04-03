using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    #region Fields
    [SerializeField]
    private float speed; 
    [SerializeField]
    private Vector3 velocity;
    [SerializeField]
    private float timer;

    [SerializeField]
    private bool enemySpell; 

    [SerializeField]
    private int damage;
    #endregion

    #region Properties
    public int Damage
    {
        get { return damage; }
        set { damage = value; }
    }
    public float Speed
    {
        get { return speed; }
        set { speed = value; }
    }
    public Vector3 Velocity
    {
        get { return velocity; }
        set { velocity = value; }
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;

        Physics2D.IgnoreLayerCollision(9, 10);
        Physics2D.IgnoreLayerCollision(0, 9); 
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        //transform.Translate(velocity * Time.deltaTime);
        transform.position += velocity * Time.deltaTime;
        transform.right = velocity.normalized;

        if (timer == 10)
        {
            DestroySpell(); 
        }
    }

    public void DestroySpell()
    {
        //Debug.Log("Destroy"); 
        //Camera.main.GetComponent<CollisionManager>().projectiles.Remove(gameObject);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!enemySpell && collision.gameObject.layer == 8) return;
        if (enemySpell && collision.gameObject.layer == 12) return;

        //Debug.Log(collision.gameObject.tag);

        if (collision.gameObject.tag == "Wall")
        {
            DestroySpell();
            return;
        }
        else if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Vehicle>().Health -= damage;
            try
            {
                collision.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            }
            catch
            {
                collision.gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.red;
            }
            DestroySpell();
            return;
        }
    }
}
