using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CinemaMachineManager : MonoBehaviour
{
   [SerializeField] private CinemachineVirtualCamera vcam1;
   [SerializeField] private CinemachineVirtualCamera vcam2;
   [SerializeField] private CinemachineVirtualCamera vcam3;

   private BossEnemy _bossEnemy;
   private void Awake()
   {
      _bossEnemy = FindObjectOfType<BossEnemy>();
   }

   private void Start()
   {
      StartCoroutine(OnStartSwitchCamera());
   }

   private void Update()
   {
      if (!_bossEnemy.isActiveAndEnabled)
      {
         SwitchOnRewardCamera();
      }
   }
   

   private IEnumerator OnStartSwitchCamera()
   {
      SwitchOnPlayerCamera();
      yield return new WaitForSeconds(1);
      SwitchOnEnemyBossCamera();
      yield return new WaitForSeconds(4);
      SwitchOnPlayerCamera();
   }

   private void SwitchOnPlayerCamera()
   {
      vcam1.Priority = 1;
      vcam2.Priority = 0;
      vcam3.Priority = 0;
   }
   private void SwitchOnEnemyBossCamera()
   {
      vcam1.Priority = 0;
      vcam2.Priority = 1;
      vcam3.Priority = 0;
   }
   private void SwitchOnRewardCamera()
   {
      vcam1.Priority = 0;
      vcam2.Priority = 0;
      vcam3.Priority = 1;
   }
}
