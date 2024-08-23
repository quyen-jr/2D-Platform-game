using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    Rigidbody2D MyRigidbody2D;
    float speedMovement = 1f;
    // Start is called before the first frame update
    void Start()
    {
        MyRigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        MyRigidbody2D.velocity = new Vector2(speedMovement, 0f);
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        speedMovement = -speedMovement;
        FLip();
    }
    void FLip()
    {
        transform.localScale = new Vector2(-(Mathf.Sign(MyRigidbody2D.velocity.x)), 1f);
    }
}
