using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    // box management
    public bool hasBox = false;
    public bool isCollidingWithBox = false;
    public GameObject collidingBox = null;

    // Example: Moving the player with physics forces

    private Rigidbody2D _rigidbody2D;

    // Set the movement force in the Inspector 
    public float Force = 100.0f;

    // Use variables to store player input
    private float _xDirection;
    private float _yDirection;


    void Start()
    {
        // Get a reference to the Rigidbody2D component
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Store the player's input into variables each frame
        _xDirection = Input.GetAxis("Horizontal");
        _yDirection = Input.GetAxis("Vertical");

        // 스페이스 키가 눌리면 boxCondition을 호출합니다.
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // 박스를 들고 있는 경우, 또는 박스와 현재 충돌 중인 경우에만 boxCondition 함수를 호출합니다.
            if (hasBox || isCollidingWithBox)
            {
                boxCondition(collidingBox);
            }
        }
    }
    private void FixedUpdate()
    {
        // Apply forces to move the player
        _rigidbody2D.AddForce(new Vector2(_xDirection, _yDirection) * Force);

        // This can provide less slippery movement when using forces
        _rigidbody2D.velocity = Vector2.zero;
    }

    // EXTRA: THIS CAN BE USED TO STOP PLAYER MOVEMENT
    public void StopPlayerMovement()
    {
        // Stops rigidbody from being affected by physics
        _rigidbody2D.isKinematic = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 충돌하는 객체가 "box01" 태그를 가지고 있는지 확인합니다.
        if (collision.gameObject.tag == "box01" && !hasBox)
        {
            isCollidingWithBox = true; // 충돌 상태를 true로 설정합니다.
            collidingBox = collision.gameObject; // 충돌하는 박스를 저장합니다.
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // 충돌이 끝난 객체가 "box01" 태그를 가지고 있는지, 그리고 현재 추적 중인 박스와 동일한지 확인합니다.
        if (collision.gameObject.tag == "box01" && collision.gameObject == collidingBox)
        {
            isCollidingWithBox = false;
            // collidingBox = null;
        }
    }

    private void boxCondition(GameObject box)
    {
        if (hasBox)
        {
            // 이미 박스를 들고 있다면, 박스를 내려놓습니다.
            box.transform.SetParent(null); // 부모-자식 관계를 해제합니다.
            hasBox = false; // 박스를 들고 있지 않다는 상태로 변경합니다.
            print("called");
        }
        else
        {
            // 박스를 들고 있지 않다면, 박스를 잡습니다.
            box.transform.SetParent(this.transform); // 이 객체를 부모로 설정합니다.
            hasBox = true; // 박스를 들고 있다는 상태로 변경합니다.
        }
    }
}