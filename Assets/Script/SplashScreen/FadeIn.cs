using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FadeIn : MonoBehaviour
{
   public CanvasGroup[] objectsToFade; 
      public float fadeDuration = 1.0f;
  
      private void Start()
      {
         
          foreach (var obj in objectsToFade)
          {
              obj.alpha = 0;
          }
  
          
          FadeInAll();
      }
  
      private void FadeInAll()
      {
          foreach (var obj in objectsToFade)
          {
              obj.DOFade(1, fadeDuration); 
          }
      }
}
