using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static dataclass;
public class SelectionScript : MonoBehaviour
{
    public dataclass data;
    
    public FishClass[] majorfish = new FishClass[6];
    public Transform[] PanelArray;
    // Start is called before the first frame update
    void Start()
    {

        //Getting all major fish from fish array
        int t = 0;
        for(int i = 0; i < data.fisharray.Length; i++)
        {   
            if (data.fisharray[i].major == true)
            {
                majorfish[t] = data.fisharray[i];
                t++;
            }
        }

        //place fish into selection boxes
        GameObject panel = GameObject.Find("Fish Selection");
        //Transform[] panelarray = new Transform[panel.transform.childCount];
        /*for (int i = 0; i < panel.transform.childCount; ++i)
        {
            PanelArray[i] = panel.transform.GetChild(i);
        }*/
        for (int i = 0; i < PanelArray.Length; i++)
        { 

            for (int j = 0; j < PanelArray[0].childCount; ++j)
            {
                Transform element = PanelArray[i].transform.GetChild(j);

                if (majorfish[i].fishname != null & element.name == "name")
                {
                    element.GetComponent<TMPro.TMP_Text>().text = majorfish[i].fishname;
                }
                if (majorfish[i].fishdesc != null & element.name == "description")
                {
                    element.GetComponent<TMPro.TMP_Text>().text = majorfish[i].fishdesc;
                }
                if (majorfish[i].fishpic != null & element.name == "image")
                {
                    element.GetComponent<Image>().sprite = majorfish[i].fishpic;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    //do a bunch of nonsense to pass data and enable the other page
    public void FishSelection(GameObject selection)
    {

        Debug.Log(selection.GetComponentInChildren<TMPro.TMP_Text>().text);
        GameObject.Find("Main selection Panel").SetActive(false);
        //unity does not allow you to use GameObject.Find to find an inactive object.
        //You need to navigate the tree manually to reach it and enable it, or sift through every gameobject.
        //or manually define it through the property inspector.

        //GetComponentInParent returns only compenents in the parent object
        Transform parent = this.gameObject.GetComponentInParent<Transform>();
        Debug.Log(parent.gameObject.name);
        //GetComponentInChildren returns the objects own components as well as it's children
        //meaning that you need a loop to filter through them every time, or this bad lazy method
        Transform[] child = parent.gameObject.GetComponentsInChildren<Transform>(true);
        Debug.Log(child[1].gameObject.name);
        child[2].gameObject.SetActive(true);
        GameObject infopanel = GameObject.Find("Fish Info Panel");
        Debug.Log(selection.GetComponentInChildren<TMPro.TMP_Text>().text);
        infopanel.GetComponent<infopanelscript>().startwrite(selection.GetComponentInChildren<TMPro.TMP_Text>().text);




    }
}
