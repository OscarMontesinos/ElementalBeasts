using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BeastSheet : MonoBehaviourPunCallbacks
{
    int playerManager=2;
    BeastSelectorPlayer player;
    public int beastSlot;
    Items itemList;
    public BeastEditor editor;
    public BeastEditor editorGO;

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
        if (photonView.IsMine)
        {
            transform.parent = null;
        }
        itemList = FindObjectOfType<Items>();
        editor = FindObjectOfType<BeastEditor>();
    }

    private void Start()
    {
        player = editor.player;

        editor.transform.SetSiblingIndex(0);
        UpdateSheet();
    }
    public void UpdateSheet()
    {
        nameText.text = player.team[beastSlot].name;
        image.sprite = player.team[beastSlot].image;

        float hp = player.team[beastSlot].unit.mHp +
                    itemList.items[player.team[beastSlot].item1Selected].hp +
                    itemList.items[player.team[beastSlot].item2Selected].hp;

        float sinergiaElemental = player.team[beastSlot].unit.sinergiaElemental +
                                    itemList.items[player.team[beastSlot].item1Selected].sinergia +
                                    itemList.items[player.team[beastSlot].item2Selected].sinergia;

        float fuerza = player.team[beastSlot].unit.fuerza +
                        itemList.items[player.team[beastSlot].item1Selected].fuerza +
                        itemList.items[player.team[beastSlot].item2Selected].fuerza;

        float control = player.team[beastSlot].unit.control +
                        itemList.items[player.team[beastSlot].item1Selected].control +
                        itemList.items[player.team[beastSlot].item2Selected].control;

        float resistenciaFisica = player.team[beastSlot].unit.resistenciaFisica +
                                    itemList.items[player.team[beastSlot].item1Selected].rFisica +
                                    itemList.items[player.team[beastSlot].item2Selected].rFisica;

        float resistenciaMagica = player.team[beastSlot].unit.resistenciaMagica +
                                    itemList.items[player.team[beastSlot].item1Selected].rMagica +
                                    itemList.items[player.team[beastSlot].item2Selected].rMagica;

        float movementPoints = player.team[beastSlot].unit.maxMovementPoints +
                                itemList.items[player.team[beastSlot].item1Selected].movimiento +
                                itemList.items[player.team[beastSlot].item2Selected].movimiento;

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
        float sinergiaElemental = player.team[beastSlot].unit.sinergiaElemental +
                                    itemList.items[player.team[beastSlot].item1Selected].sinergia +
                                    itemList.items[player.team[beastSlot].item2Selected].sinergia;

        float fuerza = player.team[beastSlot].unit.fuerza +
                        itemList.items[player.team[beastSlot].item1Selected].fuerza +
                        itemList.items[player.team[beastSlot].item2Selected].fuerza;

        float control = player.team[beastSlot].unit.control +
                        itemList.items[player.team[beastSlot].item1Selected].control +
                        itemList.items[player.team[beastSlot].item2Selected].control;


        hab1Image.sprite = player.team[beastSlot].unit.GetHabIcon(player.team[beastSlot].hab1Selected);
        hab1Name.text = player.team[beastSlot].unit.GetHabName(player.team[beastSlot].hab1Selected);
        hab1Description.text = player.team[beastSlot].unit.GetHabDescription(player.team[beastSlot].hab1Selected, sinergiaElemental, fuerza, control);

        hab2Image.sprite = player.team[beastSlot].unit.GetHabIcon(player.team[beastSlot].hab2Selected);
        hab2Name.text = player.team[beastSlot].unit.GetHabName(player.team[beastSlot].hab2Selected);
        hab2Description.text = player.team[beastSlot].unit.GetHabDescription(player.team[beastSlot].hab2Selected, sinergiaElemental, fuerza, control);

        hab3Image.sprite = player.team[beastSlot].unit.GetHabIcon(player.team[beastSlot].hab3Selected);
        hab3Name.text = player.team[beastSlot].unit.GetHabName(player.team[beastSlot].hab3Selected);
        hab3Description.text = player.team[beastSlot].unit.GetHabDescription(player.team[beastSlot].hab3Selected, sinergiaElemental, fuerza, control);

        hab4Image.sprite = player.team[beastSlot].unit.GetHabIcon(player.team[beastSlot].hab4Selected);
        hab4Name.text = player.team[beastSlot].unit.GetHabName(player.team[beastSlot].hab4Selected);
        hab4Description.text = player.team[beastSlot].unit.GetHabDescription(player.team[beastSlot].hab4Selected, sinergiaElemental, fuerza, control);
    }

    public void EditBeast()
    {
        playerManager = 1;
        editor.transform.SetSiblingIndex(3);
        editor.sheet = this;
        editor.beastSlot = beastSlot;
        editor.UpdateEditor();
    }
}

