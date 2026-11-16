using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour {
	private static readonly int IsBoostingParameter = Animator.StringToHash("IsBoosting");
	private static Animator boostAnimator;

	public static Transform Target { get; set; } // ENCAPSULATION
	public static float Distance { get; set; } // ENCAPSULATION
	public static float Height { get; set; } // ENCAPSULATION

	[SerializeField] private float _damping = 6f;
	[SerializeField] private float _rotationDamping = 10f;
	[SerializeField] private bool _smoothRotation = true;
	[SerializeField] private bool _followBehind = true;

	private bool canFollow = false;

	public static void ChangeCamPrefs(Transform vehicle, float dist, float height) { // ABSTRACTION
		Target = vehicle;
		Distance = dist;
		Height = height;
	}

	public static void SetBoosting(bool isBoosting) {
		if (boostAnimator != null)
			boostAnimator.SetBool(IsBoostingParameter, isBoosting);
	}

	private void Awake() {
		boostAnimator = GetComponent<Animator>();
		StartCoroutine(WaitForTransition());
	}

	private void OnDestroy() {
		if (boostAnimator != null && boostAnimator.gameObject == gameObject)
			boostAnimator = null;
	}

	private void FixedUpdate() {
		if (canFollow) {
			Vector3 wantedPosition;
			if (_followBehind)
				wantedPosition = Target.TransformPoint(0, Height, -Distance);
			else
				wantedPosition = Target.TransformPoint(0, Height, Distance);

			transform.position = Vector3.Lerp(transform.position, wantedPosition, Time.deltaTime * _damping);

			if (_smoothRotation) {
				Quaternion wantedRotation = Quaternion.LookRotation(Target.position - transform.position, Target.up);
				transform.rotation = Quaternion.Slerp(transform.rotation, wantedRotation, Time.deltaTime * _rotationDamping);
			} else transform.LookAt(Target, Target.up);
		}
	}

	private IEnumerator WaitForTransition() { // ABSTRACTION
		canFollow = true;

		float defaultDamp = _damping;
		_damping /= 8f;

		yield return new WaitForSeconds(7f);
		_damping = defaultDamp;
	}
}
