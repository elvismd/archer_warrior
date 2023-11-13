using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowManager : MonoBehaviour
{
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private Transform firePoint;
    [SerializeField] private ObjectCache arrowCache;
    [SerializeField] private LineRenderer boneChord;
    [SerializeField] private Cronometer shootRate;

    [SerializeField] private Transform[] boneChordRefs;
    
    private GameObject placeHolderPrefab;

    bool processing = false;
    bool releaseChord = false;

    void Start()
    {
        placeHolderPrefab = Instantiate(arrowCache.prefab, firePoint);
        Destroy(placeHolderPrefab.GetComponent<Arrow>());
        Destroy(placeHolderPrefab.GetComponent<Damager>());
    }

    void Update()
    {
        if (InputManager.Instance == null) return;

        if (!processing)
        {
            if (InputManager.Instance.Shoot && shootRate.Ended)
            {
                processing = true;
                shootRate.Tick();

                playerAnimator.SetTrigger("Shoot");
            }
            if (shootRate.Ended)
            {
                releaseChord = false;

                placeHolderPrefab.SetActive(true);
            }
        }


        for (int i = 0; i < boneChordRefs.Length; i++)
        {
            boneChord.SetPosition(i, (i == 1 && releaseChord ? boneChordRefs[i-1].position : boneChordRefs[i].position));
        }
    }

    public void ReleaseArrow()
    {
        releaseChord = true;
        placeHolderPrefab.SetActive(false);

        var rot = Quaternion.LookRotation(transform.up);
        var newArrow = arrowCache.GetObject();// Instantiate(arrowPrefab, placeHolderPrefab.transform.position, rot);
        newArrow.transform.SetPositionAndRotation(placeHolderPrefab.transform.position, rot);
        newArrow.SetActive(true);
    }

    public void EndedShooting()
    {
        processing = false;
    }
}
