using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Limit {
    public float yMin, yMax, dMin, dMax;
}

[System.Serializable]
public class ObservePlayer {
    public float distance, speed, hSpeed, vSpeed;
    public static float ClampAngle(float angle, float min, float max) { // 用上下限 夾值
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }
    public Quaternion generateAngles(float h, float v, Limit l, Quaternion rotation) {
        Vector3 a = rotation.eulerAngles;
        float x = a.y + h * hSpeed * distance * 0.02f;
        float y = a.x - ClampAngle(v * vSpeed * 0.02f, l.yMin, l.yMax);
        return Quaternion.Euler(y, x, 0);
    }
    public void generateDistance(float d, Limit l) {
        distance = Mathf.Clamp(distance - d * 5, l.dMin, l.dMax);
    }
    public Vector3 generateOffset(Quaternion rotation, float d = 0) {
        if(d == 0) d = distance;
        Vector3 negDistance = new Vector3(0.0f, 0.0f, -1.0f * d);
        return rotation * negDistance;
    }
}

public class CameraController : MonoBehaviour {

    public GameObject player;
    public ObservePlayer ob;
    public Limit limit;
    public Text scoreText;
    public Text ballCountText;
    public Text playerInHoleText;
    public Text winText;

    private Quaternion angles;
    private Rigidbody rbody;
    private AudioSource audioS;
    private float distance;
    private bool[] ballIn = new bool[10];
    private int playerInHole;
    private int score;

    void Start () {
        for(int i = 0; i < 10; i++) ballIn[i] = false;
        angles = transform.rotation;
        rbody = player.GetComponent<Rigidbody>();
        audioS = GetComponent<AudioSource>();
        distance = Vector3.Distance(player.transform.position, transform.position);
        score = 0;
        playerInHole = 0;
        SetCountText ();
        winText.text = "";
    }
    
    void LateUpdate () {
        if (Input.GetMouseButton(0)) {
            angles = ob.generateAngles(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), limit, angles);
            ob.generateDistance(Input.GetAxis("Mouse ScrollWheel"), limit);
            transform.rotation = angles;
            transform.position = ob.generateOffset(angles) + player.transform.position;
        } else transform.position = player.transform.position + ob.generateOffset(angles, distance);

        if (Input.GetMouseButton(1)) { // 按滑鼠右鍵 發射
            Vector3 movement = transform.forward;  //the direction of camera(eye) 往前看的方向
            movement.y = 0.0f;      // no vertical movement 不上下移動
            rbody.AddForce(movement * ob.speed, ForceMode.Impulse);
            audioS.Play();
             //力量模式impulse:衝力，speed：初速大小
        }
    }
    public void AddScore (int ballNum) {
        if(ballNum > 0){
            ballIn[ballNum] = true;
            score += 10;
        }else{
            playerInHole += 1;
            score -= 10;
        }
        SetCountText ();
    }
    void SetCountText () {
        int ballCount = 0;
        bool allIn = true;
        scoreText.text = "Score: " + score.ToString ();
        playerInHoleText.text = "母球進洞次數：" + playerInHole.ToString ();
        for(int i = 0; i < 10; i++){
            ballCount += (ballIn[i] ? 1 : 0);
            allIn &= ballIn[i];
        }
        ballCountText.text = "進球數：" + ballCount.ToString();
        if (allIn) {
            winText.text = "You win!";
        }
    }
}
