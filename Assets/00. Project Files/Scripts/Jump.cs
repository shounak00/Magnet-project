using UnityEngine ;
using System.Collections;
using System.Collections.Generic;

using UnityEngine.UI;
using GG.Infrastructure.Utils.Swipe ;

public class Jump : MonoBehaviour 
{
   [SerializeField] private SwipeListener swipeListener ;
   
   private void OnEnable () {
      swipeListener.OnSwipe.AddListener (OnSwipe) ;
   }

   private void OnSwipe (string swipe) {
      switch (swipe) {
         case "Up":
            PlayerController.Instance.Jump();
            break ;

      }
   }
   
   private void OnDisable () {
      swipeListener.OnSwipe.RemoveListener (OnSwipe) ;
   }
}
