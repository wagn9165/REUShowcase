using UnityEngine;
using Mirror;

public class NetManager : NetworkBehaviour
{

    Animator character;

    public Transform netTransform;

    private void Start()
    {
        character = GetComponent<Animator>();
    }

    private void Update()
    {

        if (!isLocalPlayer) return;

        if (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0))
        {
            character.SetTrigger("SwingNet");
            //Debug.Log(gameObject.ToString());
        }

        netTransform.localScale = Vector3.one * character.GetFloat("NetScale");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isClient) return;

        if (!other.CompareTag("Critter")) return;

        if (character.GetFloat("NetScale") < 0.1f) return;

        Debug.Log($"{other.transform.root.gameObject.name.Split()[0]}");

        CommandDie(gameObject, other.transform.root.gameObject);

        if(isClientOnly)
        {
            other.transform.root.gameObject.GetComponent<Animal>().Catcher = gameObject;
            other.transform.root.gameObject.GetComponent<Animal>().Die();
        }
        
    }

    [Command]
    void CommandDie(GameObject player, GameObject animalGO)
    {
        animalGO.GetComponent<Animal>().Catcher = player;
        animalGO.GetComponent<Animal>().Die();
    }
}