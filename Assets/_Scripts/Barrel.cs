using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    [SerializeField] private float speed = 12f;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private MeshRenderer barrelMr;

    [HideInInspector]
    public Upgrade upgradeReward;

    float _matShineRange;
    float _matShineIntensity;

    bool _playingFlick = false;

    private void Start()
    {
        _matShineRange = barrelMr.material.GetFloat("_ShnRange");
        _matShineIntensity = barrelMr.material.GetFloat("_ShnIntense");
    }

    // Start is called before the first frame update
    void OnEnable()
    {
        rb.velocity = Vector3.zero;

        if(upgradeReward != null)
        {
            Destroy(upgradeReward);
        }
    }

    void Update()
    {
        rb.angularVelocity = Vector3.right * speed;

        if(upgradeReward != null)
        {
            upgradeReward.transform.position = transform.position + Vector3.up * 1.5f;
        }
    }

    public void Flick()
    {
        if (_playingFlick) return;

        _playingFlick = true;
        var seq = DOTween.Sequence();
        seq.Append(barrelMr.material.DOFloat(.8f, "_ShnRange", 0.05f));
        seq.Join(barrelMr.material.DOFloat(.8f, "_ShnIntense", 0.05f));

        seq.AppendInterval(0.05f);

        seq.Append(barrelMr.material.DOFloat(_matShineRange, "_ShnRange", 0.03f));
        seq.Join(barrelMr.material.DOFloat(_matShineIntensity, "_ShnIntense", 0.03f));

        seq.AppendCallback(() => _playingFlick = false);
    }



    public void OnDie()
    {
        if(upgradeReward != null)
            upgradeReward.ProcessUpgrade();
    }
}
