using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuestManager : MonoBehaviour
{
    public Transform sPoint;
    public GameObject guestPrefab;
    public bool guest;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "guest" && other.gameObject.GetComponent<AIGuest>().readyDel)
        {
            StartCoroutine("wait");
            guest = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (!guest)
        {
            StartCoroutine("wait");
            guest = true;
            Instantiate(guestPrefab, sPoint.position, Quaternion.identity);
        }
    }
    IEnumerator wait()
    {
        yield return new WaitForSeconds(10);
    }
}
