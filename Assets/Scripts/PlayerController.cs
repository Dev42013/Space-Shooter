using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour {

    ScoreBoard scoreBoard;

    [Header("General")]
    [Tooltip("In ms^-1")][SerializeField] float xControlSpeed= 6f;
    [Tooltip("In ms^-1")] [SerializeField] float yControlSpeed = 6f;

    [SerializeField] GameObject[] guns;

    [Tooltip("In m")] [SerializeField] float xRange = 5f;
    [Tooltip("In m")] [SerializeField] float yMin = 3f;
    [Tooltip("In m")] [SerializeField] float yMax = 3f;

    [Header("Control-throw based")]
    [SerializeField] float controlRollFactor = -20f;
    [SerializeField] float controlPitchFactor = -30f;

    [Header("Position-throw based")]
    [SerializeField] float positionPitchFactor = -5f;
    [SerializeField] float positionYawFactor = 4.5f;

    [SerializeField] int timeBonusInterval = 10;  // value to update time bonus every nth frame

    float xThrow, yThrow;
    bool isControlEnabled = true;
    int timeUpdateBonusCount = 0;

    void Start()
    {
        scoreBoard = FindObjectOfType<ScoreBoard>();

    }

    // Update is called once per frame
    void Update ()
    {
        if (isControlEnabled)
        {
            ProcessTranslation();
            ProcessRotation();
            UpdateTimeBonus();
            ProcessFiring();
        }

    }

    private void UpdateTimeBonus()
    {
        timeUpdateBonusCount++;

        if (timeUpdateBonusCount % timeBonusInterval == 0)   // add time bonus every nth frame
        {
            scoreBoard.AddTimeBonus();
        }
    }

    void OnPlayerDeath()  // called by string method
    {
        isControlEnabled = false;
    }

    private void ProcessRotation()
    {
        float pitchDuetoPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControlThrow = yThrow * controlPitchFactor;
        float pitch = pitchDuetoPosition + pitchDueToControlThrow;

        float yaw = transform.localPosition.x * positionYawFactor;

        float roll = xThrow * controlRollFactor;


        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    private void ProcessTranslation()
    {
        xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        yThrow = CrossPlatformInputManager.GetAxis("Vertical");

        float xOffset = xThrow * xControlSpeed * Time.deltaTime;
        float yOffset = yThrow * yControlSpeed * Time.deltaTime;

        float rawPos = transform.localPosition.x + xOffset;
        float rawYPos = transform.localPosition.y + yOffset;

        float clampedXPos = Mathf.Clamp(rawPos, -xRange, xRange);
        float clampedYPos = Mathf.Clamp(rawYPos, -yMin, yMax);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }



    private void ProcessFiring()
    {
        if (CrossPlatformInputManager.GetButton("Fire"))
        {
            SetGunsActive(true);
        }
        else
        {
            SetGunsActive(false);
        }
    }

    private void SetGunsActive(bool isActive)
    {
        foreach(GameObject gun in guns)  // warning - may effect death
        {
            var emissionModule = gun.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = isActive;
        }
    }

}
