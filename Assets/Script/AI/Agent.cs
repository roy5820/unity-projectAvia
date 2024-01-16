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
        Vector2 displacement = velocity * Time.deltaTime;
        orientation += rotation * Time.deltaTime;
        if (orientation < 0.0f)
            orientation += 360.0f;
        else if (orientation > 360.0f)
            orientation -= 360.0f;

        //무엇을 하고 싶은지에 따라 포스모드(ForceMode)값을 설정한다
        //여기에서는 보여주는 용도로 VelocityChange를 사용한다.
        aRigidBody.AddForce(displacement, ForceMode2D.Impulse);
        Vector2 orientationVector = OriToVec(orientation);
        aRigidBody.rotation = Quaternion.LookRotation(orientationVector, Vector2.up);
    }

    public virtual void Update()
    {
        //강체 컴포넌트가 없으면 강제 리턴
        if (aRigidBody == null)
            return;

        Vector2 displacement = velocity * Time.deltaTime;
        orientation += rotation * Time.deltaTime;

        //회전 값들의 범위를 0에서 360 사이로
        //제한해야함
        if (orientation < 0.0f)
            orientation += 360.0f;
        else if (orientation > 360.0f)
            orientation -= 360.0f;

        transform.Translate(displacement, Space.World);
        transform.rotation = new Quaternion();
        transform.Rotate(Vector2.up, orientation);
    }

    public virtual void LateUpdate()
    {
        //강체 컴포넌트가 없으면 강제 리턴
        if (aRigidBody == null)
            return;

        velocity += steering.linear * Time.deltaTime;
        rotation += steering.angular * Time.deltaTime;
        if(velocity.magnitude > maxSpeed)
        {
            velocity.Normalize();
            velocity = velocity * maxSpeed;
        }
        if(steering.angular == 0.0f)
        {
            rotation = 0.0f;
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
