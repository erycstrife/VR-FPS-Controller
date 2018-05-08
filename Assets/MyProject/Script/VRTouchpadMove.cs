using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//-------------------------------------------------------------------------------------------------\\
// This code has been developed by Feisty Crab Studios for personal, commercial, and education use.\\
//                                                                                                 \\
// You are free to edit and redistribute this code, subject to the following:                      \\
//                                                                                                 \\
//      1. You will not sell this code or an edited version of it.                                 \\
//      2. You will not remove the copyright messages                                              \\
//      3. You will give credit to Feisty Crab Studios if used commercially                        \\
//      4. Don't be a mean sausage, nobody likes a mean sausage.                                   \\
//                                                                                                 \\
// Contact us @ feistycrabstudios.gmail.com with any questions.                                    \\
//-------------------------------------------------------------------------------------------------\\

public class VRTouchpadMove : MonoBehaviour
{

    [SerializeField]
    private Transform rig;

    private Valve.VR.EVRButtonId touchpad = Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad;

    private SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int)trackedObj.index); } }
    private SteamVR_TrackedObject trackedObj;

    private Vector2 axis = Vector2.zero;


	/// <summary>
	/// Shinn Add
	/// </summary>

	[SerializeField]
	float speed = 1;

	[SerializeField]
	AudioClip[] FootSounds;

	[SerializeField]
	CharacterController m_CharacterController;

	[SerializeField]
	bool FreezeAxisY = false;

	[SerializeField]
	float posy = 1.76f;

	AudioSource AS;
	bool FootSt = false;

    void Start()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
		AS = GetComponent<AudioSource> ();
    }

    void Update()
    {
        if (controller == null)
        {
            Debug.Log("Controller not initialized");
            return;
        }

        var device = SteamVR_Controller.Input((int)trackedObj.index);

		if (controller.GetTouch(touchpad) && m_CharacterController.isGrounded)
        {
            axis = device.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0);

            if (rig != null)
            {
				rig.position += (transform.right * axis.x + transform.forward * axis.y) * Time.deltaTime * speed;

				if(FreezeAxisY)
					rig.position = new Vector3(rig.position.x, posy, rig.position.z);
				else
					rig.position = new Vector3(rig.position.x, rig.position.y, rig.position.z);					


				if (!FootSt) {
					FootSt = true;
					StartCoroutine (PlayWalkSound(.5f));
				}

            }

        }


    }

	IEnumerator PlayWalkSound(float delay){
		yield return new WaitForSeconds (delay);

		int n = Random.Range (0, FootSounds.Length);
		AS.clip = FootSounds [n];
		AS.PlayOneShot (AS.clip);

		FootSounds [n] = FootSounds [0];
		FootSounds [0] = AS.clip;

		FootSt = false;
	}




}