using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

namespace Utils.Extensions
{
    public static class AnimatorExtensions
    {
        public static IEnumerator PlayAsync(this Animator animator, string stateName)
        {
            if (animator == null)
            {
                yield break;
            }
            
            const int animLayer = 0;
            animator.Play(stateName);
            animator.Update(0);
            animator.Update(0);

            //Wait until Animator is done playing
            while (animator.GetCurrentAnimatorStateInfo(animLayer).IsName(stateName) &&
                   animator.GetCurrentAnimatorStateInfo(animLayer).normalizedTime < 1.0f)
            {
                //Wait every frame until animation has finished
                yield return null;
            }
        }
    }
}
