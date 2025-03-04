﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sing : MonoBehaviour
{
    protected static Factory instance;

    public static Factory Instance{
        get{
            if(instance == null){
                instance = (Factory)FindObjectOfType(typeof(Factory));
                // Debug.Log("asdaszzzz");
            }
        return instance;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public class Factory : MonoBehaviour
{
    public List<GameObject> UFO_on = new List<GameObject>();
    public List<GameObject> UFO_off = new List<GameObject>();

    public Adapter manager1;
    public CCActionManager cca;
    public CCActionManager2 cca2;

    public int mode;


    public void genUFO(int round)    //借鉴了网上的博客
    {
        if(cca == null){
            cca = GetComponent<CCActionManager>() as CCActionManager;
        }
        if(cca2==null){
            cca2 = GetComponent<CCActionManager2>() as CCActionManager2;
            if(cca2 == null)
                Debug.Log("kkkkkkkkk");
        }
        if(mode == 0){
             manager1 = cca;
        }
        else if(mode == 1){
            manager1 = cca2;
        }
        // Debug.Log("gen!");
        GameObject ufo;
        if(UFO_off.Count == 0)
        {
            float num = Random.Range(0f,9f);
            if(num > 6)
                ufo = Instantiate(Resources.Load("Prefabs/ufo1"), Vector3.zero, Quaternion.identity) as GameObject;
            else if(num > 3)
                ufo = Instantiate(Resources.Load("Prefabs/ufo2"), Vector3.zero, Quaternion.identity) as GameObject;
            else
                ufo = Instantiate(Resources.Load("Prefabs/ufo3"), Vector3.zero, Quaternion.identity) as GameObject;
        }
        else
        {
            ufo = UFO_off[0];
            UFO_off.RemoveAt(0);
        }
        float x = Random.Range(-8f,8f);
        float y = Random.Range(-5f,5f);
        float z = Random.Range(-2f,2f);
        ufo.transform.position = new Vector3(x, y ,0);
        ufo.transform.Rotate(new Vector3(x < 0 ? -x*60 : x*60, y < 0 ? -y*60:y*60, z<0?-z*20:z*20));

        if(mode == 0){
            Vector3 v = new Vector3(ufo.transform.localRotation.x,ufo.transform.localRotation.y,ufo.transform.localRotation.z);
            ufo.transform.Translate(v*Time.deltaTime*round*2);

            manager1.fly(ufo,round*2);
        }
        else if(mode==1){
            Vector3 v = new Vector3(ufo.transform.localRotation.x,ufo.transform.localRotation.y,ufo.transform.localRotation.z);
            ufo.transform.Translate(v*Time.deltaTime*round*2);
            if(manager1 == null)
                Debug.Log("ssssss");
            manager1.fly(ufo,round*2);
        }
        UFO_on.Add(ufo);
    }

    public void recycleUFO(GameObject obj)
    {
        for(int i=0; i < UFO_on.Count ;i++){
            if(UFO_on.ToArray()[i] == obj){
                UFO_on.RemoveAt(i);
            }
        }
        obj.transform.position = new Vector3(100,100,100);
        UFO_off.Add(obj);
    }

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
