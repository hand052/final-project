using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class creatureScript : MonoBehaviour
{
    public float distance;
    public Vector3 SpawnPoint;

    public GameObject R_Arm;
    
    public HingeJoint R_Arm_Joint;

    public GameObject L_Arm;
    
    public HingeJoint L_Arm_Joint;

    public GameObject R_Leg;
    
    public HingeJoint R_Leg_Joint;

    public GameObject L_Leg;
    
    public HingeJoint L_Leg_Joint;


    //Right Arm Values
    public Vector3 RA_Scale = new Vector3(1f,1f,1f);
    public float RA_f1;
    public float RA_t1;
    public float RA_f2;
    public float RA_t2;
    //Left Arm Values
    public Vector3 LA_Scale = new Vector3(1f, 1f, 1f);
    public float LA_f1;
    public float LA_t1;
    public float LA_f2;
    public float LA_t2;

    //Right Leg Values
    public Vector3 RL_Scale = new Vector3(1f, 1f, 1f);
    public float RL_f1;
    public float RL_t1;
    public float RL_f2;
    public float RL_t2;
    //Left Leg Values
    public Vector3 LL_Scale = new Vector3(1f, 1f, 1f);
    public float LL_f1;
    public float LL_t1;
    public float LL_f2;
    public float LL_t2;

    void Start()
    {
        SpawnPoint = transform.position;

        //Get Hinge Joints
        R_Arm_Joint = R_Arm.GetComponent<HingeJoint>();
        L_Arm_Joint = L_Arm.GetComponent<HingeJoint>();
        R_Leg_Joint = R_Leg.GetComponent<HingeJoint>();
        L_Leg_Joint = L_Leg.GetComponent<HingeJoint>();
        StartCoroutine("DelayedStart");
    }

    IEnumerator DelayedStart()
    {
        //Start moving Joints 1 second late so values can be set during instantiation (this is inefficient but a temporary bug fix)
        yield return new WaitForSeconds(1f);
        StartCoroutine(Move(R_Arm_Joint, RA_f1, RA_t1, RA_f2, RA_t2));
        StartCoroutine(Move(L_Arm_Joint, LA_f1, LA_t1, LA_f2, LA_t2));
        StartCoroutine(Move(R_Leg_Joint, RL_f1, RL_t1, RL_f2, RL_t2));
        StartCoroutine(Move(L_Leg_Joint, LL_f1, LL_t1, LL_f2, LL_t2));

        
    }

    
    IEnumerator Move(HingeJoint joint, float force1, float time1, float force2, float time2)
    {
        JointMotor motor = joint.motor;
        motor.force = 1000;
        motor.targetVelocity = force1;
        joint.motor = motor;
        yield return new WaitForSeconds(time1);
        motor.force = 1000;
        motor.targetVelocity = -force2;
        joint.motor = motor;
        yield return new WaitForSeconds(time2);
       
        StartCoroutine(Move(joint,force1,time1,force2,time2));
    }
    
    void Update()
    {
        //Distance forward
       // distance = transform.position.z -(Mathf.Abs(transform.position.x) /3);
        distance = Vector3.Distance(SpawnPoint, transform.position);
        R_Arm.transform.localScale = RA_Scale;
        L_Arm.transform.localScale = LA_Scale;
        R_Leg.transform.localScale = RL_Scale;
        L_Leg.transform.localScale = LL_Scale;
    }
}
