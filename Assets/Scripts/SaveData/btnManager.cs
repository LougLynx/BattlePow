using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class btnManager : MonoBehaviour
{
    public string buttonTag = "btnShop";  // Tag để tìm các Button
    private GameObject lastUsedButton;

    private void Start()
    {
        AssignButtonEvents();
    }
    private void Update()
    {

       
    }

    #region Shop
    private void AssignButtonEvents()
    {

        GameObject[] buttonObjects = GameObject.FindGameObjectsWithTag(buttonTag);

        foreach (GameObject obj in buttonObjects)
        {
            Button btn = obj.GetComponent<Button>();

            if (btn != null)
            {
                TextMeshProUGUI buttonText = btn.GetComponentInChildren<TextMeshProUGUI>();
              
              btn.onClick.AddListener(() => goldManager.Instance.canSpend = (buttonText.text == "Buy"));
                

                btn.onClick.AddListener(() => OnShopButtonClick(obj));
            }
            else
            {
                Debug.Log("kh co btn nao duoc tim thay");
            }
        }
    }

    void OnShopButtonClick(GameObject buttonObj)
    {
        TextMeshProUGUI buttonText = buttonObj.GetComponentInChildren<TextMeshProUGUI>();
        Button btn = buttonObj.GetComponent<Button>();
        Image colorBtn = buttonObj.GetComponent<Image>();
        if (buttonText == null || btn == null) return;

        if (buttonText.text == "Buy")
        {
            if (colorBtn != null) colorBtn.color = Color.blue;

            buttonText.text = "Use";
        }


        if (buttonText.text == "Use")
        {




            // Bật lại nút trước đó (nếu có)
            if (lastUsedButton != null && lastUsedButton != buttonObj)
            {
                lastUsedButton.GetComponent<Button>().interactable = true;
                lastUsedButton.GetComponentInChildren<TextMeshProUGUI>().text = "Use";
            }

            buttonText.text = "Used";
            btn.interactable = false;
            lastUsedButton = buttonObj; // Lưu lại nút đã sử dụng
        }

    }
    #endregion

    #region  Upgrade



    #endregion


}
