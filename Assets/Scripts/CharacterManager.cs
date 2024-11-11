using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;


public class CharacterManager : MonoBehaviour
{
    Animator anim;
    public GameObject character1;
    public GameObject character2;
    public GameObject combinedCharacter;

    public Collider character1Collider;
    public Collider character2Collider;

    public CameraManager cameraManager;

    private bool isCombined = false;

    public AudioSource audioSource; 
    public AudioClip combineSound;   
    public AudioClip separateSound; 

    private void Start()
    {
        anim = GetComponent<Animator>();
        combinedCharacter.SetActive(false);

        cameraManager.SetCameras(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
                ToggleCombineCharacters();
        }
    }

    bool CanCombineCharacters()
    {
        float distance = Vector3.Distance(character1.transform.position, character2.transform.position);
        return character1Collider.bounds.Intersects(character2Collider.bounds);
    }

    void ToggleCombineCharacters()
    {
        if (!isCombined)
            CombineCharacters();
        else
            SeparateCharacters();
    }

    void CombineCharacters()
    {
        if (CanCombineCharacters())
        {
            Vector3 combinedPosition = (character1.transform.position + character2.transform.position) / 2;

            character1.SetActive(false);
            character2.SetActive(false);

            combinedCharacter.SetActive(true);
            combinedCharacter.transform.position = combinedPosition;

            cameraManager.SetCameras(true);

            audioSource.PlayOneShot(combineSound);

            isCombined = true;

            Debug.Log("Characters combined");
        }
    }

    void SeparateCharacters()
    {
        combinedCharacter.SetActive(false);

        character1.SetActive(true);
        character2.SetActive(true);

        Vector3 offset = new Vector3(1, 0, 0);
        character1.transform.position = combinedCharacter.transform.position + offset;
        character2.transform.position = combinedCharacter.transform.position - offset;

        cameraManager.SetCameras(false);

        audioSource.PlayOneShot(separateSound);

        isCombined = false;

        Debug.Log("Characters separated");
    }
}
