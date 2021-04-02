using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Grab : MonoBehaviour
{
	public Text nav;
	public float force = 50; // сила броска
	public float sensitivity = 25; // регулировка скорости при перемещении объекта
	
	public float distance = 10; // максимальная дистанция, с которой можно схватить объект
	public float maxMass = 10; // максимальная масса объекта, который можно поднять
	public float stopDistance = 3; // допустимое расстояние от точки назначения и текущего положения объекта, при перемещении (чтобы нельзя было перетащить предмет сквозь графику)

	private Rigidbody body;
	private float mass, curForce;
	private Transform clone, local;
	private static bool _get;
	private string tagRedBean = "RedBean", tagYellowLife = "YellowLife", tagGreenGrass = "GreenGrass", tagComplex = "Complex", tagRed="red",tagBlue="blue",tagGlue="glue", tagTrash = "trash";
	public static bool isDrag
	{
		get { return _get; }
	}

	void Update()
	{
		if (Input.GetMouseButtonDown(1))
		{
			body = GetRigidbody();
		}
		else if (Input.GetMouseButtonUp(1) && body)
		{
			Clear();
		}
		else if (Input.GetMouseButtonDown(0) && body)
		{
			Rigidbody tmpBody = body;
			Clear();
			tmpBody.velocity = Camera.main.transform.TransformDirection(Vector3.forward) * curForce;
		}

		
	}

	private void GetName()
    {
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
		if (Physics.Raycast(ray, out hit, distance))
		{
			if (hit.rigidbody && !hit.rigidbody.isKinematic && hit.rigidbody.mass <= maxMass)
			{
				if (hit.transform.tag == tagRedBean)
				{
					nav.text = "RedBean";
				}
				else if (hit.transform.tag == tagYellowLife)
				{
					nav.text = "YellowLife";
				}
				else if (hit.transform.tag == tagGreenGrass)
				{
					nav.text = "GreenGrass";
				}
				else if (hit.transform.tag == tagComplex)
				{
					nav.text = "Complex";
				}
				else if (hit.transform.tag == tagRed)
				{
					nav.text = "Red";
				}
				else if (hit.transform.tag == tagBlue)
				{
					nav.text = "Blue";
				}
				else if (hit.transform.tag == tagGlue)
				{
					nav.text = "Glue";
				}
				else if (hit.transform.tag == tagTrash)
				{
					nav.text = "Trash";
				}

			}
			else nav.text = "";
		}
	}
	

	Rigidbody GetRigidbody()
	{
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
		if (Physics.Raycast(ray, out hit, distance))
		{
			if (hit.rigidbody && !hit.rigidbody.isKinematic && hit.rigidbody.mass <= maxMass)
			{

				mass = hit.rigidbody.mass;
				if (mass < 2) mass = 2; // сила броска, зависит от массы, поэтому проверяем, чтобы минимальная масса не была равна или меньше единицы
				curForce = force / mass;
				hit.rigidbody.useGravity = false;
				hit.rigidbody.freezeRotation = true;
				clone.position = hit.point; // устанавливаем точку, где "подхватили" объект
				
				
				return hit.rigidbody;
			}
		}
		return null;
	}

	void SetLocal() // копируем позицию и вращение, оригинала
	{
		if (_get) return;
		local.rotation = body.rotation;
		local.position = body.position;
		_get = true;
	}

	float RoundTo(float f, int to) // округлить до
	{
		return ((int)(f * to)) / (float)to;
	}

	void FixedUpdate()
	{
		GetName();
		if (!body) return;

		Vector3 lookAt = Camera.main.transform.position;
		lookAt.y = clone.position.y;
		clone.LookAt(lookAt);
		SetLocal();
		body.velocity = (local.position - body.position) * sensitivity;
		body.rotation = local.rotation;

		float dist = Vector3.Distance(body.position, local.position);
		dist = RoundTo(dist, 100); // округляем до сотых, для исключения погрешностей
		if (dist > stopDistance) // сброс, при попытке протащить предмет сквозь стену или если резко повернуть камеру
		{
			body.velocity = Vector3.zero; // обязательный сброс скорости, иначе объект может улететь в стратосферу
			Clear();
		}
	}

	void CheckVelocity() // проверка скорости, когда мы отпускаем объект во время движения камеры, чтобы его скорость не могла превышать силы броска
	{
		Vector3 velocity = body.velocity.normalized * curForce;
		if (body.velocity.sqrMagnitude > velocity.sqrMagnitude)
		{
			body.velocity = velocity;
		}
	}

	void Clear()
	{
		
		_get = false;
		clone.localPosition = Vector3.zero;
		local.localPosition = Vector3.zero;
		if (!body) return;
		CheckVelocity();
		body.useGravity = true;
		body.freezeRotation = false;
		body = null;
	}

	void Start()
	{
		if (!clone) // создание вспомогательных точек
		{
			local = new GameObject().transform;
			clone = new GameObject().transform;
			local.parent = clone;
			clone.parent = Camera.main.transform;
		}

		

		Clear();
	}
}
