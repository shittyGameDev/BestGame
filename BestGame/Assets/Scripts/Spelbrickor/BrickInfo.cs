using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BrickInfo : MonoBehaviour
{
    [SerializeField] private Transform brickInfo;
    [SerializeField] private Image brickImage;
    private TextMeshProUGUI brickName;
    [SerializeField] private TextMeshProUGUI brickDescription;
    [SerializeField] private TextMeshProUGUI brickPrice;
    [SerializeField] private TextMeshProUGUI brickHealth;
    [SerializeField] private TextMeshProUGUI brickDamage;


    [SerializeField] private BrickData brickData;
    // Start is called before the first frame update
    void Awake()
    {
        brickInfo = transform.GetChild(0);
        brickImage = brickInfo.Find("BrickImage").GetComponent<Image>();
        brickName = brickInfo.Find("BrickName").GetComponent<TextMeshProUGUI>();
        brickDescription = brickInfo.Find("BrickDesc").GetComponent<TextMeshProUGUI>();
        brickPrice = brickInfo.Find("BrickCost").GetComponent<TextMeshProUGUI>();
        brickHealth = brickInfo.Find("BrickHealth").GetComponent<TextMeshProUGUI>();
        brickDamage = brickInfo.Find("BrickDmg").GetComponent<TextMeshProUGUI>();
        
        if(brickData != null)
        {
            brickImage.sprite = brickData.brickImage;
            brickName.text = brickData.brickName;
            brickDescription.text = brickData.brickDescription;
            brickPrice.text = brickData.brickPrice.ToString();
            brickHealth.text = brickData.brickHealth.ToString();
            brickDamage.text = brickData.brickDamage.ToString();
        }
        else
        {
            Debug.Log("BrickData is null");
        }

    }

    // Update is called once per frame
    void Update()
    {

    }


}
