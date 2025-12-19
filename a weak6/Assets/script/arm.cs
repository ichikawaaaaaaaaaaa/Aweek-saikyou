
using UnityEngine;

public class SpinThrowMinimalImproved : MonoBehaviour
{
    public Rigidbody2D arm;
    public Rigidbody2D bottle;

    [Header("回転パラメータ")]
    public float spinTorque = 30f;          // ARMを回し続けるトルク
    public float maxAngularVelDeg = 720f;   // 腕の最大角速度（度/秒）
    public float angularDamping = 0.02f;    // 軽い減衰で暴れ防止

    [Header("リリース調整")]
    public float forwardImpulse = 2f;

    [Header("取り付け位置")]
    public Vector2 attachLocalPos = new Vector2(0.5f, 0f); // ARMの先端座標

    private bool released = false;

    void Start()
    {
        AttachBottle();
        arm.angularDrag = 0.05f; // 安定化


        arm.centerOfMass = new Vector2(0f, 2.0f); // 回転するばしょ
        arm.angularDrag = 0.05f;

        AttachBottle();

    }

    void FixedUpdate()
    {
        // ARMをぐるぐる回す
        float maxRad = maxAngularVelDeg * Mathf.Deg2Rad;
        if (Mathf.Abs(arm.angularVelocity) < maxRad)
        {
            arm.AddTorque(spinTorque, ForceMode2D.Force);
        }

        // 軽いダンピング
        arm.AddTorque(-arm.angularVelocity * angularDamping, ForceMode2D.Force);
    }

    void Update()
    {
        if (!released && Input.GetKeyDown(KeyCode.Space))
        {
            ReleaseBottle();
        }

        // Rキーで再アタッチ（
        if (released && Input.GetKeyDown(KeyCode.R))
        {
            AttachBottle();
        }
    }

    void AttachBottle()
    {
        bottle.transform.SetParent(arm.transform);
        bottle.transform.localPosition = attachLocalPos;
        bottle.bodyType = RigidbodyType2D.Kinematic;
        bottle.velocity = Vector2.zero;
        bottle.angularVelocity = 0f;
        released = false;
    }

    void ReleaseBottle()
    {
        bottle.transform.SetParent(null);
        bottle.bodyType = RigidbodyType2D.Dynamic;

        // ARM の接線速度を継承
        Vector2 v = arm.GetPointVelocity(bottle.worldCenterOfMass);
        bottle.velocity = v;

        // ARMの向きに少しインパルス
        Vector2 left = arm.transform.right;
        bottle.AddForce(left * forwardImpulse, ForceMode2D.Impulse);

        released = true;
    }
}
