using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ViveSR.anipal.Eye;

public class EyeTracker : MonoBehaviour
{

    public SRanipal_GazeRaySample gazerayData;
    public ViveSR.anipal.Eye.VerboseData rightEye;
    public ViveSR.anipal.Eye.VerboseData leftEye;
    public ViveSR.anipal.Eye.SingleEyeData rightEyeData;
    public ViveSR.anipal.Eye.SingleEyeData leftEyeData;

    public float currentRightEyePupilSize;
    public float currentLeftEyePupilSize;

    public ViveSR.anipal.Eye.EyeData_v2 eye_data = new ViveSR.anipal.Eye.EyeData_v2();
    public ViveSR.anipal.Eye.EyeData_v2 eyeData = new ViveSR.anipal.Eye.EyeData_v2();

    public float rightEyePupil_diameter;
    public float leftEyePupil_diameter;

    public VerboseData verboseData;

    Vector3 SRCombinedPoint;
    Vector3 gazePoint;
    Ray gazeRay;

    public RaycastHit hit1;


    public GameObject gazeTarget;
    public GameObject environmentGazeTarget;
    // Start is called before the first frame update
    void Start()
    {
        gazerayData = gazerayData.GetComponent<SRanipal_GazeRaySample>();
        rightEyeData = rightEye.right;
        leftEyeData = leftEye.left;


    }

    // Update is called once per frame
    void Update()
    {
        currentRightEyePupilSize = rightEyeData.pupil_diameter_mm;
        currentLeftEyePupilSize = leftEyeData.pupil_diameter_mm;
        SRanipal_Eye.GetVerboseData(out verboseData);
        leftEyePupil_diameter = verboseData.left.pupil_diameter_mm;
        rightEyePupil_diameter = verboseData.right.pupil_diameter_mm;

        Debug.Log("Right Pupil: " + currentRightEyePupilSize);
        Debug.Log("Left Pupil: " + currentLeftEyePupilSize);


        SRCombinedPoint = Camera.main.transform.position - Camera.main.transform.up * 0.05f;
        gazePoint = gazerayData.GazeDirectionCombined * gazerayData.LengthOfRay;
        gazeRay = new Ray(SRCombinedPoint, gazePoint);
        Debug.DrawRay(SRCombinedPoint, gazePoint, Color.yellow);


        if (Physics.Raycast(gazeRay, out hit1, 10000))
        {
            //Debug.Log(hit1.transform.name);
            if (hit1.transform.GetComponent<GazeTarget>() != null) //need to have the target class
            {
                string type = hit1.transform.GetComponent<GazeTarget>().targetType;
                Debug.Log(type);
                //EventManager.instance.FixationOnObject.Invoke(type);
                if (type == "target")
                {
                Debug.Log("Looking at Target");
                }
                    
            }
    }
        
    }
}
