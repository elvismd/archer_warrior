using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;

    [SerializeField] private PlayerMovement movement;
    [SerializeField] private List<Character> characters;
   // [SerializeField] private GameObject[] characterSlots;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        foreach (var character in characters)
        {
            var animator = character.Animator;
            if (animator != null)
            {
                animator.SetFloat("Movement", movement.AnimationBlend * -1f);
            }
        }
    }

    public void OnDie()
    {
        InputManager.Instance.enabled = false;
        foreach (var character in characters)
        {
            var animator = character.Animator;
            if (animator != null)
            {
                animator.SetBool("Dead", true);
            }
        }

        var seq = DOTween.Sequence();
        seq.SetUpdate(true);

        seq.Append(DOVirtual.Float(1.0f, 0.1f, 0.3f, (float v) => Time.timeScale = v));
        seq.AppendInterval(2f);
        seq.AppendCallback(() =>
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        });
    }

    public void AddCharacter(Character character)
    {
        const float heightOffset = -1.092f;
        const float horizontalOffset = 0.5f;

        var characterObj = character.gameObject;

        if(characters.Count >= 3)
        {
            var charToRemove = characters.FirstOrDefault(it => it.Series != character.Series);
            if(charToRemove == null)
            {
                Destroy(character.gameObject);
                return;
            }
            else
            {
                characters.Remove(charToRemove);
                Destroy(charToRemove.gameObject);
            }
        }

        var bowManager = character.BowManager;
        bowManager.enabled = true;
        characters.Add(character);
        characterObj.transform.SetParent(characters[0].transform.parent);
        characterObj.transform.rotation = characters[0].transform.rotation;

        var amountOfCharacters = characters.Count;
        if (amountOfCharacters <= 1)
        {
            characters[0].transform.localPosition = Vector3.up * heightOffset;
        }
        else if (amountOfCharacters <= 2)
        {
            characters[0].transform.localPosition = Vector3.right * horizontalOffset + Vector3.up * heightOffset;
            characters[1].transform.localPosition = Vector3.right * -horizontalOffset + Vector3.up * heightOffset;
        }
        else
        {
            characters[0].transform.localPosition = Vector3.right * -horizontalOffset + Vector3.up * heightOffset;
            characters[1].transform.localPosition = Vector3.up * heightOffset;
            characters[2].transform.localPosition = Vector3.right * horizontalOffset + Vector3.up * heightOffset;
        }

    }
}
