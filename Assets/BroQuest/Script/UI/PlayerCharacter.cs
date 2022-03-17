using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class PlayerCharacter : MonoBehaviourPun
{
    public static PlayerCharacter instance;

    public GameObject playerCharacterPanel;

    public TextMeshProUGUI characterName;
    public Image characterImage;
    public TextMeshProUGUI attackDice;
    public TextMeshProUGUI defendDice;
    public TextMeshProUGUI bodyPoints;
    public TextMeshProUGUI mindPoints;

    public int selectedHero = 0;
    public List<Hero> heroes = new List<Hero>();

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        SetupHeroList();
        TogglePanel();
    }

    private void SetupHeroList()
    {
        Hero hero;
        // Adventurer
        hero = new Hero();
        hero.AttackDice = 4;
        hero.DefendDice = 4;
        hero.BodyPoints = 3;
        hero.MindPoints = 2;
        hero.image = "Adventurer";
        hero.name = "Adventurer";
        hero.description = "";
        this.heroes.Add(hero);
        // Alchemist
        hero = new Hero();
        hero.AttackDice = 4;
        hero.DefendDice = 6;
        hero.BodyPoints = 3;
        hero.MindPoints = 2;
        hero.image = "Alchemist";
        hero.name = "Alchemist";
        hero.description = "";
        this.heroes.Add(hero);
        // Assassin
        hero = new Hero();
        hero.AttackDice = 5;
        hero.DefendDice = 3;
        hero.BodyPoints = 3;
        hero.MindPoints = 3;
        hero.image = "Assassin";
        hero.name = "Assassin";
        hero.description = "";
        this.heroes.Add(hero);
        // Barbarian
        hero = new Hero();
        hero.AttackDice = 4;
        hero.DefendDice = 3;
        hero.BodyPoints = 3;
        hero.MindPoints = 5;
        hero.image = "Barbarian";
        hero.name = "Barbarian";
        hero.description = "";
        this.heroes.Add(hero);
        // Battlemage
        hero = new Hero();
        hero.AttackDice = 3;
        hero.DefendDice = 3;
        hero.BodyPoints = 2;
        hero.MindPoints = 4;
        hero.image = "Battlemage";
        hero.name = "Battlemage";
        hero.description = "";
        this.heroes.Add(hero);
        // Cleric
        hero = new Hero();
        hero.AttackDice = 4;
        hero.DefendDice = 6;
        hero.BodyPoints = 3;
        hero.MindPoints = 3;
        hero.image = "Cleric";
        hero.name = "Cleric";
        hero.description = "";
        this.heroes.Add(hero);
        // Dwarf
        hero = new Hero();
        hero.AttackDice = 4;
        hero.DefendDice = 3;
        hero.BodyPoints = 3;
        hero.MindPoints = 2;
        hero.image = "Dwarf";
        hero.name = "Dwarf";
        hero.description = "";
        this.heroes.Add(hero);
        // Elf
        hero = new Hero();
        hero.AttackDice = 3;
        hero.DefendDice = 4;
        hero.BodyPoints = 3;
        hero.MindPoints = 6;
        hero.image = "Elf";
        hero.name = "Elf";
        hero.description = "";
        this.heroes.Add(hero);
        // Gnome
        hero = new Hero();
        hero.AttackDice = 4;
        hero.DefendDice = 4;
        hero.BodyPoints = 3;
        hero.MindPoints = 3;
        hero.image = "Gnome";
        hero.name = "Gnome";
        hero.description = "";
        this.heroes.Add(hero);
        // Goblin Archer
        hero = new Hero();
        hero.AttackDice = 4;
        hero.DefendDice = 4;
        hero.BodyPoints = 3;
        hero.MindPoints = 3;
        hero.image = "GoblinArcher";
        hero.name = "Goblin Archer";
        hero.description = "";
        this.heroes.Add(hero);
        // Goblin Radical
        hero = new Hero();
        hero.AttackDice = 4;
        hero.DefendDice = 4;
        hero.BodyPoints = 3;
        hero.MindPoints = 3;
        hero.image = "GoblinRadical";
        hero.name = "Goblin Radical";
        hero.description = "";
        this.heroes.Add(hero);
        // Goblin Scout
        hero = new Hero();
        hero.AttackDice = 4;
        hero.DefendDice = 4;
        hero.BodyPoints = 3;
        hero.MindPoints = 3;
        hero.image = "GoblinScout";
        hero.name = "Goblin Scout";
        hero.description = "";
        this.heroes.Add(hero);
        // Mentor
        hero = new Hero();
        hero.AttackDice = 4;
        hero.DefendDice = 3;
        hero.BodyPoints = 3;
        hero.MindPoints = 6;
        hero.image = "Mentor";
        hero.name = "Mentor";
        hero.description = "";
        this.heroes.Add(hero);
        // Occultist
        hero = new Hero();
        hero.AttackDice = 4;
        hero.DefendDice = 5;
        hero.BodyPoints = 3;
        hero.MindPoints = 3;
        hero.image = "Occultist";
        hero.name = "Occultist";
        hero.description = "";
        this.heroes.Add(hero);
        // Paladin
        hero = new Hero();
        hero.AttackDice = 3;
        hero.DefendDice = 3;
        hero.BodyPoints = 1;
        hero.MindPoints = 0;
        hero.image = "Paladin";
        hero.name = "Paladin";
        hero.description = "";
        this.heroes.Add(hero);
        // Runecaster
        hero = new Hero();
        hero.AttackDice = 3;
        hero.DefendDice = 4;
        hero.BodyPoints = 3;
        hero.MindPoints = 5;
        hero.image = "Runecaster";
        hero.name = "Runecaster";
        hero.description = "";
        this.heroes.Add(hero);
        // Shield Maiden
        hero = new Hero();
        hero.AttackDice = 3;
        hero.DefendDice = 4;
        hero.BodyPoints = 3;
        hero.MindPoints = 5;
        hero.image = "ShieldMaiden";
        hero.name = "Shield Maiden";
        hero.description = "";
        this.heroes.Add(hero);
        // Sorceress
        hero = new Hero();
        hero.AttackDice = 3;
        hero.DefendDice = 4;
        hero.BodyPoints = 3;
        hero.MindPoints = 5;
        hero.image = "Sorceress";
        hero.name = "Sorceress";
        hero.description = "";
        this.heroes.Add(hero);
        // Warlock
        hero = new Hero();
        hero.AttackDice = 3;
        hero.DefendDice = 4;
        hero.BodyPoints = 3;
        hero.MindPoints = 5;
        hero.image = "Warlock";
        hero.name = "Warlock";
        hero.description = "";
        this.heroes.Add(hero);
        // Wizard
        hero = new Hero();
        hero.AttackDice = 3;
        hero.DefendDice = 4;
        hero.BodyPoints = 3;
        hero.MindPoints = 5;
        hero.image = "Wizard";
        hero.name = "Wizard";
        hero.description = "";
        this.heroes.Add(hero);
        // Wood Elf
        hero = new Hero();
        hero.AttackDice = 3;
        hero.DefendDice = 4;
        hero.BodyPoints = 3;
        hero.MindPoints = 5;
        hero.image = "WoodElf";
        hero.name = "Wood Elf";
        hero.description = "";
        this.heroes.Add(hero);

        ShowHero(0);
    }

    public void ShowHero(int heroID)
    {
        selectedHero = heroID;

        characterName.text = heroes[heroID].name;
        characterImage.sprite = Resources.Load<Sprite>("Heroes/" + heroes[heroID].image);
        attackDice.text = "" + heroes[heroID].AttackDice;
        defendDice.text = "" + heroes[heroID].DefendDice;
        bodyPoints.text = "" + heroes[heroID].BodyPoints;
        mindPoints.text = "" + heroes[heroID].MindPoints;
    }

    public void NextHero()
    {
        int nextMonster = selectedHero + 1;
        if (nextMonster == heroes.Count - 1) nextMonster = 0;
        ShowHero(nextMonster);
    }

    public void PreviousHero()
    {
        int nextMonster = selectedHero - 1;
        if (nextMonster < 0) nextMonster = heroes.Count - 1;
        ShowHero(nextMonster);
    }


    public bool panelIsOpen = true;
    public void TogglePanel()
    {
        if (panelIsOpen)
        {
            panelIsOpen = false;
            playerCharacterPanel.transform.localPosition = new Vector3(0f,878f,0f);
        } else
        {
            panelIsOpen = true;
            playerCharacterPanel.transform.localPosition = new Vector3(0f, 150f, 0f);
        }
        EventSystem.current.SetSelectedGameObject(null);
    }
}