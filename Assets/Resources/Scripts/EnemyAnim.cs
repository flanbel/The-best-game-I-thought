using UnityEngine;
using System.Collections;
//エネミーのアニメーション
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]

public class EnemyAnim : MonoBehaviour {
    //アニメーションクリップの名前を入れる
    [SerializeField]
    private string Idle,Move, Attack, Hit, Die;
    private int idle, move, attack, hit, die;
    private Animator anim;
    private NavMeshAgent agent;
    GameObject player;

    // Use this for initialization
    void Start () {
        idle = Animator.StringToHash(Idle);
        move = Animator.StringToHash(Move);
        anim = GetComponent<Animator>();
        
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {
        //if (Input.GetKey(KeyCode.L))
        {
            agent.destination = player.transform.position;
        }
        //移動しているなら
        if (agent.hasPath)
        {
            anim.SetBool(anim.parameters[0].name, true);
        }else
        {
            anim.SetBool(anim.parameters[0].name, false);
        }
        
        //攻撃範囲内
        if(agent.stoppingDistance >= agent.remainingDistance &&
            anim.GetCurrentAnimatorStateInfo(0).shortNameHash !=  Animator.StringToHash(anim.runtimeAnimatorController.animationClips[3].name))
        {
            anim.SetTrigger(anim.parameters[1].name);
        }

    }
}
