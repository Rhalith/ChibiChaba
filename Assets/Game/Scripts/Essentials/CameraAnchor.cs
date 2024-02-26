namespace Essentials
{
using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class CameraAnchor : MonoBehaviour
{
	public enum AnchorType
	{
		BottomLeft,
		BottomCenter,
		BottomRight,
		MiddleLeft,
		MiddleCenter,
		MiddleRight,
		TopLeft,
		TopCenter,
		TopRight,
	};
	[SerializeField] private AnchorType anchorType;
	[SerializeField] private Vector3 anchorOffset;
	[SerializeField] private bool offsetIsScale;

	private IEnumerator _updateAnchorRoutine;

	// Use this for initialization
	private void Start()
	{
		_updateAnchorRoutine = UpdateAnchorAsync();
		StartCoroutine(_updateAnchorRoutine);
	}

	/// <summary>
	/// Coroutine to update the anchor only once ViewportHandler.Instance is not null.
	/// </summary>
	private IEnumerator UpdateAnchorAsync()
	{
		uint cameraWaitCycles = 0;
		while (ViewportHandler.Instance == null)
		{
			++cameraWaitCycles;
			yield return new WaitForEndOfFrame();
		}
		if (cameraWaitCycles > 0)
		{
			print(string.Format("CameraAnchor found ViewportHandler instance after waiting {0} frame(s). You might want to check that ViewportHandler has an earlie execution order.", cameraWaitCycles));
		}
		UpdateAnchor();
		_updateAnchorRoutine = null;
	}

	void UpdateAnchor()
	{
		switch (anchorType)
		{
			case AnchorType.BottomLeft:
				SetAnchor(ViewportHandler.Instance.BottomLeft);
				break;
			case AnchorType.BottomCenter:
				SetAnchor(ViewportHandler.Instance.BottomCenter);
				break;
			case AnchorType.BottomRight:
				SetAnchor(ViewportHandler.Instance.BottomRight);
				break;
			case AnchorType.MiddleLeft:
				SetAnchor(ViewportHandler.Instance.MiddleLeft);
				break;
			case AnchorType.MiddleCenter:
				SetAnchor(ViewportHandler.Instance.MiddleCenter);
				break;
			case AnchorType.MiddleRight:
				SetAnchor(ViewportHandler.Instance.MiddleRight);
				break;
			case AnchorType.TopLeft:
				SetAnchor(ViewportHandler.Instance.TopLeft);
				break;
			case AnchorType.TopCenter:
				SetAnchor(ViewportHandler.Instance.TopCenter);
				break;
			case AnchorType.TopRight:
				SetAnchor(ViewportHandler.Instance.TopRight);
				break;
		}
	}

	private void SetAnchor(Vector3 anchor)
	{
		Vector3 newPos;
		if (offsetIsScale)
		{
			newPos = OffsetForScale(anchor);
		}
		else
		{
			newPos = anchor + anchorOffset;
		}
		if (!transform.position.Equals(newPos))
		{
			transform.position = newPos;
		}
	}

	private Vector3 OffsetForScale(Vector3 anchor)
	{
		Vector3 newPos = new Vector3();
		switch (anchorType)
		{
			case AnchorType.BottomLeft:
				newPos = new Vector3(anchor.x + transform.lossyScale.x/2, anchor.y + transform.lossyScale.y/2, anchor.z);
				break;
			case AnchorType.BottomCenter:
				newPos = new Vector3(anchor.x, anchor.y + transform.lossyScale.y/2, anchor.z);
				break;
			case AnchorType.BottomRight:
				newPos = new Vector3(anchor.x - transform.lossyScale.x/2, anchor.y + transform.lossyScale.y/2, anchor.z);
				break;
			case AnchorType.TopLeft:
				newPos = new Vector3(anchor.x + transform.lossyScale.x/2, anchor.y - transform.lossyScale.y/2, anchor.z);
				break;
			case AnchorType.TopCenter:
				newPos = new Vector3(anchor.x, anchor.y - transform.lossyScale.y/2, anchor.z);
				break;
			case AnchorType.TopRight:
				newPos = new Vector3(anchor.x - transform.lossyScale.x/2, anchor.y - transform.lossyScale.y/2, anchor.z);
				break;
			case AnchorType.MiddleLeft:
				newPos = new Vector3(anchor.x + transform.lossyScale.x/2, anchor.y, anchor.z);
				break;
			case AnchorType.MiddleRight:
				newPos = new Vector3(anchor.x - transform.lossyScale.x/2, anchor.y, anchor.z);
				break;
		}
		return newPos;
	}

#if UNITY_EDITOR
	// Update is called once per frame
	private void Update()
	{
		if (_updateAnchorRoutine == null)
		{
			_updateAnchorRoutine = UpdateAnchorAsync();
			StartCoroutine(_updateAnchorRoutine);
		}
	}
#endif
}
}