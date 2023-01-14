using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BeastSheet : MonoBehaviour
{
    BeastSelectorPlayer player;
    public int beastSlot;
    Items itemList;

    [Header("UI")]
    public TextMeshProUGUI nameText;
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

    [Header("Resources")]
    public Sprite defaultItem;
    public Sprite defaultHability;
    // Start is called before the first frame update

    // Update is called once per frame
    private void Awake()
    {
        player = FindObjectOfType<BeastSelectorPlayer>();
        itemList = FindObjectOfType<Items>();
    }

    private void Start()
    {
        UpdateSheet();
    }
    void UpdateSheet()
    {
        nameText.text = player.team[beastSlot].name;
        image.sprite = player.team[beastSlot].image;
        float hp = player.team[beastSlot].unit.mHp;
        float sinergiaElemental = player.team[beastSlot].unit.sinergiaElemental;
        float fuerza = player.team[beastSlot].unit.fuerza;
        float control = player.team[beastSlot].unit.control;
        float resistenciaFisica = player.team[beastSlot].unit.resistenciaFisica;
        float resistenciaMagica = player.team[beastSlot].unit.resistenciaMagica;
        float movementPoints = player.team[beastSlot].unit.maxMovementPoints;

        int index = 0;
        foreach (Item item in itemList.items)
        {
            if (item.equipado)
            {
                switch (index)
                {
                    case 0:
                        hp += item.hp * item.cantidad;
                        break;
                    case 1:
                        sinergiaElemental += item.hp * item.cantidad;
                        break;
                    case 2:
                        fuerza += item.fuerza * item.cantidad;
                        break;
                    case 3:
                        control += item.control * item.cantidad;
                        break;
                    case 4:
                        resistenciaFisica += item.rFisica * item.cantidad;
                        break;
                    case 5:
                        resistenciaMagica += item.rMagica * item.cantidad;
                        break;
                    case 6:
                        movementPoints += item.movimiento * item.cantidad;
                        break;
                }
            }
        }

        statsText.text =    hp + "\n" +
                            sinergiaElemental + "\n" +
                            fuerza + "\n" +
                            control + "\n" +
                            resistenciaFisica + "\n" +
                            resistenciaMagica + "\n" +
                            movementPoints;

        ChangeImage(item1Image, player.team[beastSlot].item1Selected);
        ChangeImage(item2Image, player.team[beastSlot].item2Selected);

        ChangeHabilitiesInfo();
        
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
        hab1Image.sprite = player.team[beastSlot].unit.GetHabIcon(player.team[beastSlot].hab1Selected - 1);
        hab1Name.text = player.team[beastSlot].unit.GetHabName(player.team[beastSlot].hab1Selected - 1);
        hab1Description.text = player.team[beastSlot].unit.GetHabDescription(player.team[beastSlot].hab1Selected - 1);

        hab2Image.sprite = player.team[beastSlot].unit.GetHabIcon(player.team[beastSlot].hab2Selected - 1);
        hab2Name.text = player.team[beastSlot].unit.GetHabName(player.team[beastSlot].hab2Selected - 1);
        hab2Description.text = player.team[beastSlot].unit.GetHabDescription(player.team[beastSlot].hab2Selected - 1);

        hab3Image.sprite = player.team[beastSlot].unit.GetHabIcon(player.team[beastSlot].hab3Selected - 1);
        hab3Name.text = player.team[beastSlot].unit.GetHabName(player.team[beastSlot].hab3Selected - 1);
        hab3Description.text = player.team[beastSlot].unit.GetHabDescription(player.team[beastSlot].hab3Selected - 1);

        hab4Image.sprite = player.team[beastSlot].unit.GetHabIcon(player.team[beastSlot].hab4Selected - 1);
        hab4Name.text = player.team[beastSlot].unit.GetHabName(player.team[beastSlot].hab4Selected - 1);
        hab4Description.text = player.team[beastSlot].unit.GetHabDescription(player.team[beastSlot].hab4Selected - 1);
    }
}

