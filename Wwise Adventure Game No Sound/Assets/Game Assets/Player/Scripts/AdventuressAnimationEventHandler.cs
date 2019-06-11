////////////////////////////////////////////////////////////////////////
//
// Copyright (c) 2018 Audiokinetic Inc. / All Rights Reserved
//
////////////////////////////////////////////////////////////////////////

﻿using UnityEngine;
using System.Collections;

public class AdventuressAnimationEventHandler : MonoBehaviour
{
    public AudioClip leftFootStep1;
    public AudioClip leftFootStep2;
    public AudioClip leftFootStep3;
    public AudioClip leftFootStep4;
    public AudioClip leftFootStep5;
    public AudioClip rightFootStep1;
    public AudioClip rightFootStep2;
    public AudioClip rightFootStep3;
    public AudioClip rightFootStep4;
    public AudioClip rightFootStep5;
    AudioClip leftFootStepFinal;
    AudioClip rightFootStepFinal;

    public AudioClip BAS_imp_axe_dirt;
    public AudioClip BAS_imp_dagger_dirt;
    public AudioClip BAS_imp_sword_dirt;


    [Header("Object Links")]
    [SerializeField]
    private Animator playerAnimator;

    [SerializeField]
    private GameObject runParticles;

    private PlayerFoot foot_L;
    private PlayerFoot foot_R;

    #region private variables
    private bool hasPausedMovement;
    private readonly int canShootMagicHash = Animator.StringToHash("CanShootMagic");
    private readonly int isAttackingHash = Animator.StringToHash("IsAttacking");
    #endregion

    private void Awake()
    {
        GameObject L = GameObject.Find("toe_left");
        GameObject R = GameObject.Find("toe_right");
        if (L != null)
        {
            foot_L = L.GetComponent<PlayerFoot>();
        }
        else {
            print("Left foot missing");
        }
        if (R != null)
        {
            foot_R = R.GetComponent<PlayerFoot>();
        }
        else
        {
            print("Right foot missing");
        }
    }


    void enableWeaponCollider()
    {
        if (PlayerManager.Instance != null && PlayerManager.Instance.equippedWeaponInfo != null)
        {
            PlayerManager.Instance.equippedWeaponInfo.EnableHitbox();
        }
    }

    void disableWeaponCollider()
    {
        if (PlayerManager.Instance != null && PlayerManager.Instance.equippedWeaponInfo != null)
        {
            PlayerManager.Instance.equippedWeaponInfo.DisableHitbox();
        }

    }

    void ScreenShake()
    {
        PlayerManager.Instance.cameraScript.CamShake(new PlayerCamera.CameraShake(0.4f, 0.7f));
    }

    bool onCooldown = false;
    public enum FootSide { left, right };
    public void TakeFootstep(FootSide side)
    {
        if (foot_L != null && foot_R != null) {
            if (!PlayerManager.Instance.inAir && !onCooldown)
            {
                Vector3 particlePosition;

                if (side == FootSide.left )
                {
                    //if (foot_L.FootstepSound.Validate())
                    { 
                        // HINT: Play left footstep sound
                        
                        switch(Random.Range(1, 6)){

                            case 1:
                                leftFootStepFinal = leftFootStep1;
                                break;

                            case 2:
                                leftFootStepFinal = leftFootStep2;
                                break;

                            case 3:
                                leftFootStepFinal = leftFootStep3;
                                break;

                            case 4:
                                leftFootStepFinal = leftFootStep4;
                                break;

                            case 5:
                                leftFootStepFinal = leftFootStep5;
                                break;

                        }


                        particlePosition = foot_L.transform.position;
                        FootstepParticles(particlePosition);
                        AudioSource audioSource = GetComponent<AudioSource>();
                        audioSource.PlayOneShot(leftFootStepFinal, 0.7F);
                    }
                }
                else
                {
                    //if (foot_R.FootstepSound.Validate())
                    {
                        // HINT: Play right footstep sound

                        switch (Random.Range(1, 6))
                        {

                            case 1:
                                rightFootStepFinal = rightFootStep1;
                                break;

                            case 2:
                                rightFootStepFinal = rightFootStep2;
                                break;

                            case 3:
                                rightFootStepFinal = rightFootStep3;
                                break;

                            case 4:
                                rightFootStepFinal = rightFootStep4;
                                break;

                            case 5:
                                rightFootStepFinal = rightFootStep5;
                                break;

                        }

                        particlePosition = foot_R.transform.position;
                        FootstepParticles(particlePosition);
                        AudioSource audioSource = GetComponent<AudioSource>();
                        audioSource.PlayOneShot(rightFootStepFinal, 0.7F);
                    }
                }
            }
        }
    }

    void FootstepParticles(Vector3 particlePosition) {
        GameObject p = Instantiate(runParticles, particlePosition + Vector3.up * 0.1f, Quaternion.identity) as GameObject;
        p.transform.parent = SceneStructure.Instance.TemporaryInstantiations.transform;
        Destroy(p, 5f);
        StartCoroutine(FootstepCooldown());
    }

    IEnumerator FootstepCooldown()
    {
        onCooldown = true;
        yield return new WaitForSecondsRealtime(0.2f);
        onCooldown = false;
    }

    void ReadyToShootMagic()
    {
        PlayerManager.Instance.playerAnimator.SetBool(canShootMagicHash, true);
    }

    public enum AttackState { NotAttacking, Attacking };
    void SetIsAttacking(AttackState state)
    {
        if (state == AttackState.NotAttacking)
        {
            playerAnimator.SetBool(isAttackingHash, false);
        }
        else
        {
            playerAnimator.SetBool(isAttackingHash, true);
        }
    }

    public void Weapon_SwingEvent()
    {
        // PLAY SOUND
        Weapon W = PlayerManager.Instance.equippedWeaponInfo;
        // HINT: PlayerManager.Instance.weaponSlot contains the selected weapon;
        AudioSource audioSource = GetComponent<AudioSource>();
        switch (PlayerManager.Instance.weaponSlot.GetComponentInChildren<Weapon>().weaponType)
        {

            case WeaponTypes.Dagger:
                print("dagger");
                audioSource.PlayOneShot(BAS_imp_dagger_dirt, 0.7F);
                break;
            case WeaponTypes.Sword:
                print("sword");
                audioSource.PlayOneShot(BAS_imp_sword_dirt, 0.7F);
                break;
            case WeaponTypes.Axe:
                print("axe");
                audioSource.PlayOneShot(BAS_imp_axe_dirt, 0.7F);
                break;
            /*case "Pick Axe":
                print("Grog SMASH!");
                break;
            case "Hammer":
                print("Ulg, glib, Pblblblblb");
                break;
            case "Evil Spit Plant":
                print("Ulg, glib, Pblblblblb");
                break;
            case "Evil Crawler":
                print("Ulg, glib, Pblblblblb");
                break;
            case "Evil Head":
                print("Ulg, glib, Pblblblblb");
                break;*/
            default:
                print("Incorrect intelligence level.");
                break;
        }

       
        // HINT: This is a good place to play the weapon swing sounds
    }

    public void PauseMovement()
    {
        if (!hasPausedMovement)
        {
            hasPausedMovement = true;
            PlayerManager.Instance.motor.SlowMovement();
        }
    }

    public void ResumeMovement()
    {
        if (hasPausedMovement)
        {
            hasPausedMovement = false;
            PlayerManager.Instance.motor.UnslowMovement();
        }
    }

    public void FreezeMotor()
    {
        StartCoroutine(PickupEvent());
    }

    private IEnumerator PickupEvent()
    {
        PlayerManager.Instance.PauseMovement(gameObject);
        yield return new WaitForSeconds(2f);
        PlayerManager.Instance.ResumeMovement(gameObject);
    }

    public void PickUpItem()
    {
        PlayerManager.Instance.PickUpEvent();
        // HINT: This is a good place to play the Get item sound and stinger
    }

    public void WeaponSound()
    {
        Weapon EquippedWeapon = PlayerManager.Instance.equippedWeaponInfo;
        // HINT: This is a good place to play equipped weapon impact sound
    }
}
