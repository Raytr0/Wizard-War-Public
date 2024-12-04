using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigSpell: MonoBehaviour
{
    public Transform spellSpawnPoint1;
    public Transform spellSpawnPoint2;
    public Transform spellSpawnPoint3;
    public GameObject spellPrefab;
    public float spellSpeed = 8;
    public float lastCast = -10f;
    public float spellCD = 1f;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && Time.time - lastCast >= spellCD)
        {
            lastCast = Time.time;
            var bullet = Instantiate(spellPrefab, spellSpawnPoint1.position, spellSpawnPoint1.rotation);
            bullet.GetComponent<Rigidbody>().velocity = spellSpawnPoint1.forward * spellSpeed;
            var bullet2 = Instantiate(spellPrefab, spellSpawnPoint2.position, spellSpawnPoint2.rotation);
            bullet2.GetComponent<Rigidbody>().velocity = spellSpawnPoint2.forward * spellSpeed;
            var bullet3 = Instantiate(spellPrefab, spellSpawnPoint3.position, spellSpawnPoint3.rotation);
            bullet3.GetComponent<Rigidbody>().velocity = spellSpawnPoint3.forward * spellSpeed;
            foreach (Transform t in bullet.transform)
            {
                t.gameObject.tag = "";
            }
            foreach (Transform t in bullet2.transform)
            {
                t.gameObject.tag = "";
            }
            foreach (Transform t in bullet3.transform)
            {
                t.gameObject.tag = "";
            }
        }
    }
}
