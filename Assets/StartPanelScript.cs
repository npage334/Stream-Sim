using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class StartPanelScript : MonoBehaviour
{
    public dataclass data;
    public TMPro.TMP_Text endtitle;
    public TMPro.TMP_Text enddesc;
    public Image endimage;
    // Start is called before the first frame update
    void Start()
    {
        //populate startpanel
        transform.Find("title").GetComponent<TMPro.TMP_Text>().text = data.GenSetting.starttitle;
        transform.Find("desc").GetComponent<TMPro.TMP_Text>().text = data.GenSetting.startdesc;
        transform.Find("Image").GetComponent<Image>().sprite = data.GenSetting.startpic;
        //populate endpanel while we are here
        endtitle.text = data.GenSetting.endtitle;
        enddesc.text = data.GenSetting.enddesc;
        endimage.sprite = data.GenSetting.endpic;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
