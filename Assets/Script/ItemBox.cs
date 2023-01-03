using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemBox : MonoBehaviour
{
    public Text title;
    public Text description;
    public Image img;

    public scr_CharacterController playerController;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void init(string itemName, string itemDescr, Sprite itemImg)
    {
        title.text = itemName;
        description.text = itemDescr;
        img.sprite = itemImg;
    }

    public void Show(string itemName, string itemDescr, Sprite itemImg)
    {
        init(itemName, itemDescr, itemImg);

        gameObject.SetActive(true);

        playerController.disableInput();
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void Hide()
    {
        gameObject.SetActive(false);

        playerController.enableInput();
        Cursor.lockState = CursorLockMode.Locked;
    }
}
