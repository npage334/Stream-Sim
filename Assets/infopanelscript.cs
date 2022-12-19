using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using static dataclass;
public class infopanelscript : MonoBehaviour
{
    public dataclass data;
    public FishClass[] fisharray;
    public int Simimperimiability;
    public GameObject SimPanel;
    public GameObject DescBox;
    public GameObject EndPanel;
    public Image TransitionPanel;
    public Image TransitionImage;
    public Image AerialImage;
    int simstep;
    Transform[] panelarray;

    // Start is called before the first frame update
    void Start()
    {
        //setting the transition panel and image to be clear at start
        TransitionImage.canvasRenderer.SetAlpha(0f);
        
        TransitionPanel.canvasRenderer.SetAlpha(0f);
    }
    // Go forward one step in the simarray, update images and check if fish should be dead at the current imperm level
    public void StepForward()
    {
        if (simstep < data.SimStepArray.Length - 1)
        {
            if (simstep < data.SimStepArray.Length - 1)
            {

                simstep++;
            }
            Transform[] panelarray = new Transform[this.transform.childCount];
            for (int i = 0; i < this.transform.childCount; ++i)
            {
                panelarray[i] = this.transform.GetChild(i);
            }
            SimPanel.GetComponentInChildren<TMPro.TMP_Text>().text = data.SimStepArray[simstep].stepname;
            SimPanel.transform.Find("Sim Step Description").GetComponent<TMPro.TMP_Text>().text = data.SimStepArray[simstep].stepdesc;
            DescBox.transform.Find("info").GetComponent<TMPro.TMP_Text>().text = data.SimStepArray[simstep].infobox;
            SimPanel.transform.Find("Image").GetComponent<Image>().sprite = data.SimStepArray[simstep].propertypic;
            AerialImage.GetComponent<Image>().sprite = data.SimStepArray[simstep].aerialpic;
            for (int i = 0; i < panelarray.Length; i++)
            {
                if (data.SimStepArray[simstep].imperm > fisharray[i].impermthreshold)
                {
                    panelarray[i].Find("vignette").GetComponent<Image>().color = Color.white;
                }
            }
            TransitionImage.sprite = data.GenSetting.forwardpic;
            TransitionPanel.canvasRenderer.SetAlpha(0.5f);
            TransitionPanel.CrossFadeAlpha(0, 1.25f, false);
            TransitionImage.canvasRenderer.SetAlpha(1f);
            TransitionImage.CrossFadeAlpha(0, 1.25f, false);

            Debug.Log(simstep);

        } 
        else //if we are at the end of the array, show the endpanel
        {
            EndPanel.SetActive(true);
        }
    }
    //Steps backwards in the simarray, updates images, checks imperm level
    public void StepBackwards()
    {
        if (simstep > 0)
        {
            
            
            simstep--;
            
            Transform[] panelarray = new Transform[this.transform.childCount];
            for (int i = 0; i < this.transform.childCount; ++i)
            {
                panelarray[i] = this.transform.GetChild(i);
            }
            
            SimPanel.GetComponentInChildren<TMPro.TMP_Text>().text = data.SimStepArray[simstep].stepname;
            SimPanel.transform.Find("Sim Step Description").GetComponent<TMPro.TMP_Text>().text = data.SimStepArray[simstep].stepdesc;
            DescBox.transform.Find("info").GetComponent<TMPro.TMP_Text>().text = data.SimStepArray[simstep].infobox;
            SimPanel.transform.Find("Image").GetComponent<Image>().sprite = data.SimStepArray[simstep].propertypic;
            AerialImage.GetComponent<Image>().sprite = data.SimStepArray[simstep].aerialpic;
            for (int i = 0; i < panelarray.Length; i++)
            {
                if (data.SimStepArray[simstep].imperm <= fisharray[i].impermthreshold)
                {
                    panelarray[i].Find("vignette").GetComponent<Image>().color = Color.clear;
                }
            }
            TransitionImage.sprite = data.GenSetting.backwardpic;
            TransitionPanel.canvasRenderer.SetAlpha(0.5f);
            TransitionPanel.CrossFadeAlpha(0, 1.25f, false);
            TransitionImage.canvasRenderer.SetAlpha(1f);
            TransitionImage.CrossFadeAlpha(0, 1.25f, false);

            Debug.Log(simstep);
        }
    }
    // Update is called once per frame


    //place fish data into left image box programmaticly
    //this works well enough that the number boxes in the image box could be dynamicly changed if I had the time to do it.
    //Obsolete with startwrite() because this data never needs to be changed throughout the simulation.
    public void writeinfo(int panelnum, FishClass obj)
    {
        
        for (int i = 0; i < panelarray[panelnum].childCount; ++i)
        {
            Transform element = panelarray[panelnum].transform.GetChild(i);

            if (obj.fishname != null & element.name == "name")
            {
                element.GetComponent<TMPro.TMP_Text>().text = obj.fishname;
            }
            if (obj.fishdesc != null & element.name == "description")
            {
                element.GetComponent<TMPro.TMP_Text>().text = obj.fishdesc;
            }
            if (obj.fishpic != null)
            {
                element.Find("image").GetComponent<Image>().sprite = obj.fishpic;
            }

        }
    }

    //Initalizes infopanel based on your selected fish from the starting page, applies the vignette death image aswell.
    //takes the fish selected on the first page to dataclass to build an array with the selected fish as the first entry.
    //it then populates panels from the fishfirstarray() data.
    public void startwrite(string Selectedfishname)
    {
        
        Transform[] panelarray = new Transform[this.transform.childCount];
        
        fisharray = data.fishfirstarray(Selectedfishname);
        for (int i = 0; i < this.transform.childCount; ++i)
        {
            panelarray[i] = this.transform.GetChild(i);
        }


        if (fisharray[0].fishdesc != null)
        {
            panelarray[0].Find("description").GetComponentInChildren<TMPro.TMP_Text>().text = fisharray[0].fishdesc;
        }

        for (int i = 0; i < panelarray.Length; ++i)
        {
            panelarray[i].Find("vignette").GetComponentInChildren<Image>().sprite = data.GenSetting.deathpic;
            if (fisharray[i].fishname != null)
            {
                panelarray[i].Find("name").GetComponentInChildren<TMPro.TMP_Text>().text = fisharray[i].fishname;
            }
            if (fisharray[i].fishpic != null)
            {
                panelarray[i].Find("image").GetComponentInChildren<Image>().sprite = fisharray[i].fishpic;
            }
        }
        SimPanel.GetComponentInChildren<TMPro.TMP_Text>().text = data.SimStepArray[simstep].stepname;
        SimPanel.transform.Find("Sim Step Description").GetComponent<TMPro.TMP_Text>().text = data.SimStepArray[simstep].stepdesc;
        DescBox.transform.Find("info").GetComponent<TMPro.TMP_Text>().text = data.SimStepArray[simstep].infobox;
        SimPanel.transform.Find("Image").GetComponent<Image>().sprite = data.SimStepArray[simstep].propertypic;
        AerialImage.GetComponent<Image>().sprite = data.SimStepArray[simstep].aerialpic;

    }
    
    public void updatestatus()
    {
        //Update all fish's statuses based on the current simulation state

    }
}
