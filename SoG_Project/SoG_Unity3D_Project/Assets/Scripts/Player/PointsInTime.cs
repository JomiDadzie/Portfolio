using UnityEngine;

public class PointsInTime {
	public Vector3 poistion;
	public Quaternion rotation;
	public int playerState;
	public PointsInTime(Vector3 _position, Quaternion _rotation, int _playerState)
	{
		poistion = _position;
		rotation = _rotation;
		playerState = _playerState;
	}
}
