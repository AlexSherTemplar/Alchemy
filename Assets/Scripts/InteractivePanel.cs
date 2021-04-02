using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractivePanel : MonoBehaviour
{
	[SerializeField] private string targetTag = "GameController"; // ��� ������������� ������� � ����
	[SerializeField] private float distance = 3; // ��������� ��������������
	[SerializeField] private RectTransform playerAim = null; // UI ������
	[SerializeField] private Transform playerCursor; // ������ (������)
	private bool isMouse;

	void Start()
	{
		Cursor.visible = false; // �������� ��������� ������
		ResetMode();

	}

	void ResetMode()
	{
		isMouse = false;
		Cursor.lockState = CursorLockMode.Locked; // ��������� ��������� ������ �� ������ ������
		playerCursor.gameObject.SetActive(false);
		playerAim.gameObject.SetActive(true);
	}

	void LateUpdate()
	{
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
		if (Physics.Raycast(ray, out hit, distance))
		{
			if (hit.transform.tag == targetTag && Cursor.lockState == CursorLockMode.Locked)
			{
				isMouse = true;
				Cursor.lockState = CursorLockMode.None; // ����������� ������, ����� ����������������� � UI
				playerCursor.gameObject.SetActive(true);
				playerAim.gameObject.SetActive(false);
			}
			else if (hit.transform.tag != targetTag && Cursor.lockState == CursorLockMode.None)
			{
				ResetMode();
			}
		}
		else
		{
			ResetMode();
		}

		if (isMouse) CursorControl();
	}

	// ���������� �������� � ��������� �������
	// ���-��, ������� ����� ����� ������� ���, ���� �� ������ �� ������� UI ������
	void CursorControl()
	{
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(ray, out hit, distance))
		{
			if (hit.transform.tag == targetTag)
			{
				playerCursor.gameObject.SetActive(true);
			}
			else if (hit.transform.tag != targetTag)
			{
				playerCursor.gameObject.SetActive(false);
			}

			playerCursor.position = hit.point;
			playerCursor.rotation = Quaternion.FromToRotation(-Vector3.forward, hit.normal);
		}
		else
		{
			ResetMode();
		}
	}
}
