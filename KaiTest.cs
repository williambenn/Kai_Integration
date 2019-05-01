using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kai.Module;

public class KaiTest : MonoBehaviour
{
    private Kai.Module.Kai k1;
    private Kai.Module.Kai k2;
    public Camera main;
    private float[] angles = new float[3];

    public int speed = 5;

    //public GameObject cube;
    
    // Start is called before the first frame update
    void Start()
    {
        KaiSDK.Initialise("4C4C4544-0031-3910-805A-CAC04F533332", "qwerty");
        KaiSDK.Connect();


        KaiSDK.GetConnectedKais();

        //cube.GetComponent<Transform>();

        while ((k1 = KaiSDK.GetKaiByID(0)) == null);

        Debug.Log("Connected...");

        Capabilities();

        Debug.Log(k1.Capabilities);

        k1.GyroscopeData += gyro;

        k1.Gesture += gestr;

        k1.PYRData += pyr;

        k1.QuaternionData += quat;

        k1.MagnetometerData += mag;

        k1.AccelerometerData += accel;

        k1.FingerPositionalData += fpd;
    }
    private void Update()
    {
        UnityEngine.Quaternion rot = UnityEngine.Quaternion.identity;
        angles[0] += angles[0] * 5 * Time.deltaTime; //yaw
        angles[1] += angles[1] * 5 * Time.deltaTime; //pitch
        angles[2] += angles[2] * 5 * Time.deltaTime; //roll
        
        rot.eulerAngles = new UnityEngine.Vector3(-angles[0], 0, -angles[2]);

        //main.transform.rotation *= rot;
        //main.transform.eulerAngles = new UnityEngine.Vector3(-angles[1], -angles[0], -angles[2]);

        main.transform.Rotate(UnityEngine.Vector3.right * angles[1] * Time.deltaTime);
        main.transform.Rotate(UnityEngine.Vector3.up * -angles[0] * Time.deltaTime);
        main.transform.Rotate(UnityEngine.Vector3.forward * -angles[2] * Time.deltaTime);

        //main.transform.forward *= 5;
        main.GetComponent<Rigidbody>().velocity = main.transform.forward * speed;
    }

   

    void fpd(object sender, FingerPositionalEventArgs f)
    {
        Debug.Log("Data fpd: " + f.IndexFinger);
    }

    void mag(object sender, MagnetometerEventArgs m)
    {
        Debug.Log("Data Q: " + m.Magnetometer.x);
    }

    void accel(object sender, AccelerometerEventArgs a)
    {
        Debug.Log("Data A: " + a.Accelerometer.x);
    }

    void quat(object sender, QuaternionEventArgs q)
    {
        Debug.Log("Data Q: " + q.Quaternion.x);
    }

    void pyr(object sender, PYREventArgs p)
    {
        angles[0] = p.Pitch;
        angles[1] = p.Yaw;
        angles[2] = p.Roll;
    }

    void gyro(object sender, GyroscopeEventArgs g)
    {
        Debug.Log("Data G: " + g.Gyroscope.x);
    }

    void gestr(object sender, GestureEventArgs g)
    {
        if (g.Gesture.ToString().ToLower().Contains("grabbegin"))
        {
            Debug.Log("Grabbed!");
        }
        if (g.Gesture.ToString().ToLower().Contains("grabend"))
        {
            Debug.Log("Let Go!");
        }
        if (g.Gesture.ToString().ToLower().Contains("pinch2begin"))
        {
            Debug.Log("Pinch On2!");
        }
        if (g.Gesture.ToString().ToLower().Contains("pinch2end"))
        {
            Debug.Log("Pinch Off2!");
        }
        if (g.Gesture.ToString().ToLower().Contains("pinch3begin"))
        {
            Debug.Log("Pinch On3!");
        }
        if (g.Gesture.ToString().ToLower().Contains("pinch3end"))
        {
            Debug.Log("Pinch Off3!");
        }
        if (g.Gesture.ToString().ToLower().Contains("dialbegin"))
        {
            Debug.Log("Dial it!");
        }
        if (g.Gesture.ToString().ToLower().Contains("dialend"))
        {
            Debug.Log("Wrong Number!");
        }
        if (g.Gesture.ToString().ToLower().Contains("swipeleft"))
        {
            Debug.Log("Left!");
            
        }
        if (g.Gesture.ToString().ToLower().Contains("swiperight"))
        {
            Debug.Log("Right!");
        }
        if (g.Gesture.ToString().ToLower().Contains("swipeup"))
        {
            Debug.Log("Up!");
            Increase();
        }
        if (g.Gesture.ToString().ToLower().Contains("swipedown"))
        {
            Debug.Log("Down!");
            Decrease();
        }
    }

    void Increase()
    {
        speed += 5;
    }

    void Decrease()
    {
        speed -= 5;
    }

    void Capabilities()
    {

        k1.UnsetCapabilities(KaiCapabilities.GestureData);
        //k1.UnsetCapabilities(KaiCapabilities.GyroscopeData);
        //k1.UnsetCapabilities(KaiCapabilities.PYRData);
        //k1.UnsetCapabilities(KaiCapabilities.QuaternionData);
        //k1.UnsetCapabilities(KaiCapabilities.AccelerometerData);
        //k1.UnsetCapabilities(KaiCapabilities.MagnetometerData);
        //k1.UnsetCapabilities(KaiCapabilities.FingerPositionalData);

        k1.SetCapabilities(KaiCapabilities.GestureData);
        //k1.SetCapabilities(KaiCapabilities.GyroscopeData);
        k1.SetCapabilities(KaiCapabilities.PYRData);
        //k1.SetCapabilities(KaiCapabilities.QuaternionData);
        //k1.SetCapabilities(KaiCapabilities.AccelerometerData);
        //k1.SetCapabilities(KaiCapabilities.MagnetometerData);
        //k1.SetCapabilities(KaiCapabilities.FingerPositionalData);
    }
}
