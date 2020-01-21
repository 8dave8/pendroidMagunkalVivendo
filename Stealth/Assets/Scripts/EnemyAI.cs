using UnityEngine;
using UnityEngine.AI;
using System.Collections;
    public class EnemyAI : MonoBehaviour {
        public Transform[] points;
        public int destPoint = 0;
        void Start () {
            GotoNextPoint();
        }
    void GotoNextPoint() {
        if (points.Length == 0) return;
        transform.Translate(points[destPoint].position*Time.deltaTime);
    }
    void Update () {
        GotoNextPoint();
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("wrrrrryyyyyyyyyy");
        if(col.gameObject.tag == "Point")
        {
            Debug.Log("POINTT");
            destPoint = (destPoint+1) % points.Length;
        }
    }
}