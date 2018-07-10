using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplaySystem: MonoBehaviour {
    private const int bufferFrames = 150;
    private MyKeyFrame[] keyFrames = new MyKeyFrame[bufferFrames];
    private Rigidbody rigidbody;
    private GameManager gameManager;
    private int lastFrameCount = 0;
    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        rigidbody = GetComponent<Rigidbody>();   
    }

    private void Update()
    {
        if (gameManager.recording)
        {
            Record();
        }
        else
        {
            PlayBack();
        }
    }

    void PlayBack()
    {
        rigidbody.isKinematic = true;
        int frame = Time.frameCount % bufferFrames;
        if (lastFrameCount < bufferFrames - 1)
        {
            frame = frame % lastFrameCount;
        }
        print("Reading fream: " + frame);
        transform.position = keyFrames[frame].pos;
        transform.rotation = keyFrames[frame].rot;
    }
    private void Record()
    {
        rigidbody.isKinematic = false;
        int frame = Time.frameCount % bufferFrames;
        if(frame < bufferFrames - 1 && lastFrameCount != bufferFrames -1)
        {
            lastFrameCount = frame;
        }
        float time = Time.time;
        keyFrames[frame] = new MyKeyFrame(transform.position, transform.rotation, time);
    }
}
/// <summary>
/// A structure for storing rotation, position, and time
/// </summary>
public struct MyKeyFrame{
    public Vector3 pos;
    public Quaternion rot;
    public float frameTime;

    public MyKeyFrame(Vector3 myPos, Quaternion myRot, float aTime)
    {
        pos = myPos;
        rot = myRot;
        frameTime = aTime;
    }
}
