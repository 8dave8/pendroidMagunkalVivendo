using UnityEngine;
using UnityEngine.AI;
using System.Collections;
    public class EnemyAI : MonoBehaviour {

        public Animator EnemyAnim;
        public bool isTargetingPlayer = false;
        private bool isInBarrel;
        private Vector3 playerPosition;
        public Transform[] points;
        public int destPoint = 0;
        void Start () {
            GotoNextPoint();
            Physics2D.IgnoreLayerCollision(9,10);
        }                         
    void GotoNextPoint() {
        if (points.Length == 0) return;
        float step = 0.15f;
        Vector3 currenttarget;
        if(isTargetingPlayer) currenttarget = playerPosition;
        else currenttarget = points[destPoint].position;
        transform.position = Vector3.MoveTowards(transform.position,currenttarget,step);

        EnemyAnim.SetBool("Walk", true);
        //Debug.Log(transform.position+" "+currenttarget+" "+step);
        //Debug.Log("TargetPLayer: "+isTargetingPlayer+" IsinBarrel"+isInBarrel);
    }
    void Update () {
        GotoNextPoint();
        if(transform.position == points[destPoint].position)
        {
            Debug.Log("POINTT");
            destPoint = (destPoint+1) % points.Length;
        }
    }
    void OnTriggerStay2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            isTargetingPlayer = true;
            playerPosition = col.gameObject.transform.position;
        }
        else isTargetingPlayer = false;
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Player")) isTargetingPlayer = false;
        
    }
}