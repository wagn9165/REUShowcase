using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class OutOfBounds : NetworkBehaviour
{
    public Vector3 origin = new Vector3(0, 100, 0);
    public Transform spawnPlatform;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"{other.transform.root.name} just fell out of the world");

        if (other.transform.root.CompareTag("Player"))
        {
            if (Physics.Raycast(origin, Vector3.down, out RaycastHit hit, 100f))
            {
                
                //Debug.Log($"Setting pos to {hit.point}, + {other.transform.root.localScale.y}");
                other.transform.root.position = hit.point + new Vector3(0, other.transform.localScale.y, 0);
                other.transform.root.gameObject.GetComponent<Rigidbody>().position = hit.point + new Vector3(0, other.transform.localScale.y, 0);
            }
        }
        else if(other.transform.root.CompareTag("Critter"))
        {
            
            CommandDie(other.transform.root.gameObject);
            other.transform.root.gameObject.GetComponent<Animal>().Die();
        }
        else
        {
            Debug.Log($"{other.transform.root.name} Untagged entity sent to origin");
            if (Physics.Raycast(origin, Vector3.down, out RaycastHit hit, 100f))
            {
                other.transform.root.gameObject.GetComponent<Animal>().rigidbodyRef.transform.position = hit.point + new Vector3(0, other.transform.root.localScale.y, 0);
                
                other.transform.root.gameObject.GetComponent<Animal>().rigidbodyRef.position =- hit.point + new Vector3(0, other.transform.root.localScale.y, 0);
            }
        }
    }

    [Command]
    private void CommandDie(GameObject animal)
    {
        animal.GetComponent<Animal>().Die();
    }

    [Command]
    private void MovePlayerToOriginCommand(GameObject player)
    {
        if (Physics.Raycast(origin, Vector3.down, out RaycastHit hit, 100f))
        {
            player.transform.position = hit.point + new Vector3(0, player.transform.localScale.y, 0);
        }
    }
}
