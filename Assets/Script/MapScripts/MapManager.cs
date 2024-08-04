using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{
    public Image PlayerImg,mapimg;
    public Transform map;
    public Transform player;
    float realScale,uiScale;
    Vector2 pos;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
        float tempX = player.transform.position.x / (map.transform.localScale.x * 10);
        float tempZ = player.transform.position.z / (map.transform.localScale.z * 10);

        //float uitempx=

        //PlayerImg.rectTransform.localPosition = new Vector2(mapimg.rectTransform.sizeDelta.x* tempX, mapimg.rectTransform.sizeDelta.y* tempZ);


        if (player.transform.position.z >= 20 || player.transform.position.x >= 20 || player.transform.position.z <= 10 || player.transform.position.x <= 10)
        {
            PlayerImg.rectTransform.localPosition = new Vector2(mapimg.rectTransform.localScale.x * tempX, mapimg.rectTransform.localScale.y * tempZ);
            //mapimg.rectTransform.pivot = new Vector2(tempX, tempZ);
            mapimg.rectTransform.pivot = pos;
        }
        else
        {
            PlayerImg.rectTransform.localPosition = Vector2.zero;
            mapimg.rectTransform.pivot = new Vector2(tempX, tempZ);
            pos = mapimg.rectTransform.pivot;
            //PlayerImg.rectTransform.localPosition = Vector2.zero;
            //mapimg.rectTransform.pivot = new Vector2(tempX, tempZ);
            //PlayerImg.rectTransform.localPosition= new Vector2(tempX, tempZ);
        }

    }
}
