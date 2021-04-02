using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractivePanel : MonoBehaviour
{
	[SerializeField] private string targetTag = "GameController"; // тег интерактивных панелей в игре
	[SerializeField] private float distance = 3; // дистанция взаимодействия
	[SerializeField] private RectTransform playerAim = null; // UI мишень
	[SerializeField] private Transform playerCursor; // курсор (спрайт)
	private bool isMouse;

	void Start()
	{
		Cursor.visible = false; // скрываем системный курсор
		ResetMode();

	}

	void ResetMode()
	{
		isMouse = false;
		Cursor.lockState = CursorLockMode.Locked; // фиксируем системный курсор по центру экрана
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
				Cursor.lockState = CursorLockMode.None; // освобождаем курсор, чтобы взаимодействовать с UI
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

	// управление позицией и вращением курсора
	// так-же, функция нужна чтобы прятать его, если он уходит за границы UI панели
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
