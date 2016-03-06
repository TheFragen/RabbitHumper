using UnityEngine;
using System.Collections;

public class PlaySound : StateMachineBehaviour {
    public AudioClip clip;
    public AudioClip stopClip;
    public bool loop;
    AudioSource source;

	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if(source == null)
        {
            source = animator.gameObject.transform.parent.GetComponent<AudioSource>();
        }

        source.loop = loop;
        source.clip = clip;
        source.Play();     
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	//override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
//        if (source.loop) source.loop = false;

        if(stopClip != null){
            source.clip = stopClip;
            source.PlayOneShot(stopClip);
        } else
        {
            source.Stop();
        }

    }

	// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}
}
