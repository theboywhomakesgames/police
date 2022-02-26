using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DB.Utils{
    public class ParticlePool : MonoBehaviour
    {
        public GameObject GetInstance(){
            if(pool.Count > 0){
                GameObject go = pool[0];
                pool.RemoveAt(0);
                return go;
            }
            else{
                GameObject go = Instantiate(prefab);
                go.transform.parent = transform;
                go.SetActive(false);
                return go;
            }
        }

        public void ReturnInstance(GameObject go){
            go.SetActive(false);
            pool.Add(go);
        }

        [SerializeField] private GameObject prefab;
        [SerializeField] private int poolSize;

        [SerializeField] private List<GameObject> pool;

        private void Awake(){
            for(int i = 0; i < poolSize; i++){
                GameObject go = Instantiate(prefab);
                go.transform.parent = transform;
                pool.Add(go);
                go.SetActive(false);
            }
        }
    }
}
