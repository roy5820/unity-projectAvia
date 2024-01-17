using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    public float maxSpeed;
    public float maxAccel;
    public float orientation;
    public float rotation;
    public Vector2 velocity;
    protected Steering steering;
    private Rigidbody2D aRigidBody;

    private void Start()
    {
        velocity = Vector2.zero;
        steering = new Steering();

        aRigidBody = GetComponent<Rigidbody2D>();//강체 컴포넌트의 레퍼런스를 획득
    }

    public void SetSteering(Steering steering)
    {
        this.steering = steering;
    }

    public virtual void FixedUpdate()
    {
        //강체 컴포넌트가 없으면 강제 리턴
        if (aRigidBody == null)
            return;
        Vector2 displacement = velocity * Time.deltaTime;

        aRigidBody.AddForce(displacement, ForceMode2D.Impulse);
    }

    public virtual void Update()
    {
        //강체 컴포넌트가 없으면 강제 리턴
        if (aRigidBody == null)
            return;

        Vector2 displacement = velocity * Time.deltaTime;

        transform.Translate(displacement, Space.World);
    }

    public virtual void LateUpdate()
    {
        //강체 컴포넌트가 없으면 강제 리턴
        if (aRigidBody == null)
            return;

        velocity += steering.linear * Time.deltaTime;
        if(velocity.magnitude > maxSpeed)
        {
            velocity.Normalize();
            velocity = velocity * maxSpeed;
        }
        if(steering.linear.sqrMagnitude == 0.0f)
        {
            velocity = Vector2.zero;
        }

        steering = new Steering();
    }

    //방향값을 벡터로변환하는 함수 구현
    public Vector2 OriToVec(float orientation)
    {
        Vector2 vector = Vector2.zero;
        vector.x = Mathf.Cos(orientation * Mathf.Deg2Rad) * 1.0f;
        vector.y = Mathf.Sin(orientation * Mathf.Deg2Rad) * 1.0f;
        return vector.normalized;
    }
}
