using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{

    public Rigidbody2D body;

    public SpriteRenderer spriteRenderer;

    public List<Sprite> nSprites;
    public List<Sprite> neSprites;
    public List<Sprite> eSprites;
    public List<Sprite> sSprites;

    public float walkSpeed;
    public float frameRate;
    // Start is called before the first frame update
    Vector2 direction;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        direction = new Vector2(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"));

        body.velocity = direction * walkSpeed;
    }
}
