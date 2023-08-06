using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BeastEditor : MonoBehaviour
{
    public BeastSelectorPlayer player;
    public BeastSheet sheet;
    public int beastSlot;
    public GameObject itemSelector;
    public int itemSlot;
    public Color32 selectedColor;
    public Color32 notSelectedColor;
    Items itemList;


    public Image image;
    public TextMeshProUGUI statsText;
    public Image item1Image;
    public Image item2Image;
    public Image hab1Image;
    public TextMeshProUGUI hab1Name;
    public TextMeshProUGUI hab1Description;
    public Image hab2Image;
    public TextMeshProUGUI hab2Name;
    public TextMeshProUGUI hab2Description;
    public Image hab3Image;
    public TextMeshProUGUI hab3Name;
    public TextMeshProUGUI hab3Description;
    public Image hab4Image;
    public TextMeshProUGUI hab4Name;
    public TextMeshProUGUI hab4Description;
    public Image hab5Image;
    public TextMeshProUGUI hab5Name;
    public TextMeshProUGUI hab5Description;
    public Image hab6Image;
    public TextMeshProUGUI hab6Name;
    public TextMeshProUGUI hab6Description;
    public Image hab7Image;
    public TextMeshProUGUI hab7Name;
    public TextMeshProUGUI hab7Description;
    public Image hab8Image;
    public TextMeshProUGUI hab8Name;
    public TextMeshProUGUI hab8Description;

    private void Awake()
    {
        itemList = FindObjectOfType<Items>();
    }
    // Start is called before the first frame update
    public void UpdateEditor()
    {
        image.sprite = player.team[beastSlot].icon;
        float hp =  player.team[beastSlot].unit.mHp +   
                    itemList.items[player.team[beastSlot].item1Selected].hp + 
                    itemList.items[player.team[beastSlot].item2Selected].hp;

        float sinergiaElemental =   player.team[beastSlot].unit.sinergiaElemental +
                                    itemList.items[player.team[beastSlot].item1Selected].sinergia +
                                    itemList.items[player.team[beastSlot].item2Selected].sinergia;

        float fuerza =  player.team[beastSlot].unit.fuerza +
                        itemList.items[player.team[beastSlot].item1Selected].fuerza +
                        itemList.items[player.team[beastSlot].item2Selected].fuerza;

        float control = player.team[beastSlot].unit.control +
                        itemList.items[player.team[beastSlot].item1Selected].control +
                        itemList.items[player.team[beastSlot].item2Selected].control;

        float resistenciaFisica =   player.team[beastSlot].unit.resistenciaFisica +
                                    itemList.items[player.team[beastSlot].item1Selected].rFisica +
                                    itemList.items[player.team[beastSlot].item2Selected].rFisica;

        float resistenciaMagica =   player.team[beastSlot].unit.resistenciaMagica +
                                    itemList.items[player.team[beastSlot].item1Selected].rMagica +
                                    itemList.items[player.team[beastSlot].item2Selected].rMagica;

        float movementPoints =  player.team[beastSlot].unit.maxMovementPoints +
                                itemList.items[player.team[beastSlot].item1Selected].movimiento +
                                itemList.items[player.team[beastSlot].item2Selected].movimiento;




        statsText.text =    "<color=green>" + (itemList.items[player.team[beastSlot].item1Selected].hp + itemList.items[player.team[beastSlot].item2Selected].hp) + " " + "</color>"+ hp + "\n" +
                            "<color=green>" + (itemList.items[player.team[beastSlot].item1Selected].sinergia + itemList.items[player.team[beastSlot].item2Selected].sinergia) + " " + "</color>" + sinergiaElemental + "\n" +
                            "<color=green>" + (itemList.items[player.team[beastSlot].item1Selected].fuerza + itemList.items[player.team[beastSlot].item2Selected].fuerza) + " " + "</color>" + fuerza + "\n" +
                            "<color=green>" + (itemList.items[player.team[beastSlot].item1Selected].control + itemList.items[player.team[beastSlot].item2Selected].control) + " " + "</color>" + control + "\n" +
                            "<color=green>" + (itemList.items[player.team[beastSlot].item1Selected].rFisica + itemList.items[player.team[beastSlot].item2Selected].rFisica) + " " + "</color>" + resistenciaFisica + "\n" +
                            "<color=green>" + (itemList.items[player.team[beastSlot].item1Selected].rMagica + itemList.items[player.team[beastSlot].item2Selected].rMagica) + " " + "</color>" + resistenciaMagica + "\n" +
                            "<color=green>" + (itemList.items[player.team[beastSlot].item1Selected].movimiento + itemList.items[player.team[beastSlot].item2Selected].movimiento) + " " + "</color>" + movementPoints;

        ChangeImage(item1Image, player.team[beastSlot].item1Selected);
        ChangeImage(item2Image, player.team[beastSlot].item2Selected);

        ChangeHabilitiesInfo();

        UpdateHabImages();
    }

    void ChangeImage(Image image, int index)
    {
        if (index < itemList.items.Count)
        {
            image.sprite = itemList.items[index].icon;
        }
    }

    void ChangeHabilitiesInfo()
    {
        float sinergiaElemental = player.team[beastSlot].unit.sinergiaElemental +
                                   itemList.items[player.team[beastSlot].item1Selected].sinergia +
                                   itemList.items[player.team[beastSlot].item2Selected].sinergia;

        float fuerza = player.team[beastSlot].unit.fuerza +
                        itemList.items[player.team[beastSlot].item1Selected].fuerza +
                        itemList.items[player.team[beastSlot].item2Selected].fuerza;

        float control = player.team[beastSlot].unit.control +
                        itemList.items[player.team[beastSlot].item1Selected].control +
                        itemList.items[player.team[beastSlot].item2Selected].control;

        hab1Image.sprite = player.team[beastSlot].unit.GetHabIcon(1);
        hab1Name.text = player.team[beastSlot].unit.GetHabName(1);
        hab1Description.text = player.team[beastSlot].unit.GetHabDescription(1, sinergiaElemental, fuerza, control);

        hab2Image.sprite = player.team[beastSlot].unit.GetHabIcon(2);
        hab2Name.text = player.team[beastSlot].unit.GetHabName(2);
        hab2Description.text = player.team[beastSlot].unit.GetHabDescription(2, sinergiaElemental, fuerza, control);

        hab3Image.sprite = player.team[beastSlot].unit.GetHabIcon(3);
        hab3Name.text = player.team[beastSlot].unit.GetHabName(3);
        hab3Description.text = player.team[beastSlot].unit.GetHabDescription(3, sinergiaElemental, fuerza, control);

        hab4Image.sprite = player.team[beastSlot].unit.GetHabIcon(4);
        hab4Name.text = player.team[beastSlot].unit.GetHabName(4);
        hab4Description.text = player.team[beastSlot].unit.GetHabDescription(4, sinergiaElemental, fuerza, control);

        hab5Image.sprite = player.team[beastSlot].unit.GetHabIcon(5);
        hab5Name.text = player.team[beastSlot].unit.GetHabName(5);
        hab5Description.text = player.team[beastSlot].unit.GetHabDescription(5, sinergiaElemental, fuerza, control);

        hab6Image.sprite = player.team[beastSlot].unit.GetHabIcon(6);
        hab6Name.text = player.team[beastSlot].unit.GetHabName(6);
        hab6Description.text = player.team[beastSlot].unit.GetHabDescription(6, sinergiaElemental, fuerza, control);

        hab7Image.sprite = player.team[beastSlot].unit.GetHabIcon(7);
        hab7Name.text = player.team[beastSlot].unit.GetHabName(7);
        hab7Description.text = player.team[beastSlot].unit.GetHabDescription(7, sinergiaElemental, fuerza, control);

        hab8Image.sprite = player.team[beastSlot].unit.GetHabIcon(8);
        hab8Name.text = player.team[beastSlot].unit.GetHabName(8);
        hab8Description.text = player.team[beastSlot].unit.GetHabDescription(8, sinergiaElemental, fuerza, control);


    }

    private void UpdateHabImages()
    {
        int index = 1;
        while(index <= 8)
        {
            UnselectHabImage(index);
            index++;
        }
        index = 1;
        if(player.team[beastSlot].hab1Selected != 0)
        {
            SelectHabImage(player.team[beastSlot].hab1Selected);
        }
        if(player.team[beastSlot].hab2Selected != 0)
        {
            SelectHabImage(player.team[beastSlot].hab2Selected);
        }
        if(player.team[beastSlot].hab3Selected != 0)
        {
            SelectHabImage(player.team[beastSlot].hab3Selected);
        }
        if(player.team[beastSlot].hab4Selected != 0)
        {
            SelectHabImage(player.team[beastSlot].hab4Selected);
        }
    }

    public void ChangeHability(int hability)
    {
        if (player.team[beastSlot].hab1Selected != hability && player.team[beastSlot].hab2Selected != hability && player.team[beastSlot].hab3Selected != hability && player.team[beastSlot].hab4Selected != hability)
        {
            if (player.team[beastSlot].hab1Selected == 0)
            {
                if (player.team[beastSlot].hab1Selected != hability)
                {
                    player.team[beastSlot].hab1Selected = hability;
                    SelectHabImage(hability);
                }
            }
            else if (player.team[beastSlot].hab2Selected == 0)
            {
                if (player.team[beastSlot].hab2Selected != hability)
                {
                    player.team[beastSlot].hab2Selected = hability;
                    SelectHabImage(hability);
                }
            }
            else if (player.team[beastSlot].hab3Selected == 0)
            {
                if (player.team[beastSlot].hab3Selected != hability)
                {
                    player.team[beastSlot].hab3Selected = hability;
                    SelectHabImage(hability);
                }
            }
            else if (player.team[beastSlot].hab4Selected == 0)
            {
                if (player.team[beastSlot].hab4Selected != hability)
                {
                    player.team[beastSlot].hab4Selected = hability;
                    SelectHabImage(hability);
                }
            }
        }
        else
        {
            if (player.team[beastSlot].hab1Selected == hability)
            {
                player.team[beastSlot].hab1Selected = 0;
                UnselectHabImage(hability);
            }
            else if (player.team[beastSlot].hab2Selected == hability)
            {
                player.team[beastSlot].hab2Selected = 0;
                UnselectHabImage(hability);
            }
            else if(player.team[beastSlot].hab3Selected == hability)
            {
                player.team[beastSlot].hab3Selected = 0;
                UnselectHabImage(hability);
            }
            else if(player.team[beastSlot].hab4Selected == hability)
            {
                player.team[beastSlot].hab4Selected = 0;
                UnselectHabImage(hability);
            }
        }
        UpdateEditor();
    }

    private void SelectHabImage(int hability)
    {
        switch (hability)
        {
            case 1:
                hab1Image.color = selectedColor;
                break;
            case 2:
                hab2Image.color = selectedColor;
                break;
            case 3:
                hab3Image.color = selectedColor;
                break;
            case 4:
                hab4Image.color = selectedColor;
                break;
            case 5:
                hab5Image.color = selectedColor;
                break;
            case 6:
                hab6Image.color = selectedColor;
                break;
            case 7:
                hab7Image.color = selectedColor;
                break;
            case 8:
                hab8Image.color = selectedColor;
                break;
        }
    }

    private void UnselectHabImage(int hability)
    {
        switch (hability)
        {
            case 1:
                hab1Image.color = notSelectedColor;
                break;
            case 2:
                hab2Image.color = notSelectedColor;
                break;
            case 3:
                hab3Image.color = notSelectedColor;
                break;
            case 4:
                hab4Image.color = notSelectedColor;
                break;
            case 5:
                hab5Image.color = notSelectedColor;
                break;
            case 6:
                hab6Image.color = notSelectedColor;
                break;
            case 7:
                hab7Image.color = notSelectedColor;
                break;
            case 8:
                hab8Image.color = notSelectedColor;
                break;
        }
    }

    public void EditItem(int item)
    {
        itemSlot = item;
        itemSelector.SetActive(true);
    }

    public void ChangeItem(int itemToChange)
    {

        if (itemSlot == 1)
        {
            player.team[beastSlot].item1Selected = itemToChange;
        }
        else
        {
            player.team[beastSlot].item2Selected = itemToChange;
        }

        itemList.items[itemToChange].equipado = true;
        itemList.items[itemToChange].cantidad++;

        itemSelector.SetActive(false);
        UpdateEditor();
    }

    public void CloseBeastEditor()
    {
        sheet.UpdateSheet();
        gameObject.SetActive(false);
    }
}
