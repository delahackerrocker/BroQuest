using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class ZargonFeed : MonoBehaviourPun
{
    public static ZargonFeed instance;

    public TextMeshProUGUI characterName;
    public Image characterImage;
    public TextMeshProUGUI description;
    public TextMeshProUGUI movementSquares;
    public TextMeshProUGUI attackDice;
    public TextMeshProUGUI defendDice;
    public TextMeshProUGUI bodyPoints;
    public TextMeshProUGUI mindPoints;

    public GameObject diceContainer;

    [SerializeField] List<GameObject> diceGroup = new List<GameObject>();

    [SerializeField] int selectedMonster = 0;
    [SerializeField] List<Monster> monsters = new List<Monster>();

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        ThreadZargonClear();
        SetupMonsterList();
    }

    private void SetupMonsterList()
    {
        Monster monster;
        // Fimir
        monster = new Monster();
        monster.MovementSquares = 6;
        monster.AttackDice = 3;
        monster.DefendDice = 3;
        monster.BodyPoints = 2;
        monster.MindPoints = 3;
        monster.image = "Fimir";
        monster.name = "Fimir";
        monster.description = "Tail Whip: after attacking, you may do a 2 dice attack against another adjacent figure.";
        this.monsters.Add(monster);
        // Fimir Witch
        monster = new Monster();
        monster.MovementSquares = 7;
        monster.AttackDice = 2;
        monster.DefendDice = 3;
        monster.BodyPoints = 2;
        monster.MindPoints = 5;
        monster.image = "FimirWitch";
        monster.name = "Fimir Witch";
        monster.description = "Tail Whip: after attacking, you may do a 2 dice attack against another adjacent figure. Spellcaster: 3 random Fimir Witch spells.";
        this.monsters.Add(monster);
        // Gargoyle
        monster = new Monster();
        monster.MovementSquares = 6;
        monster.AttackDice = 4;
        monster.DefendDice = 5;
        monster.BodyPoints = 3;
        monster.MindPoints = 4;
        monster.image = "Gargoyle";
        monster.name = "Gargoyle";
        monster.description = "Dual Wield: may perform an extra attack instead of moving";
        this.monsters.Add(monster);
        // Goblin
        monster = new Monster();
        monster.MovementSquares = 10;
        monster.AttackDice = 2;
        monster.DefendDice = 1;
        monster.BodyPoints = 1;
        monster.MindPoints = 1;
        monster.image = "Goblin";
        monster.name = "Goblin";
        monster.description = "Small: may move through any figure; only blocks line of sight of other goblins.";
        this.monsters.Add(monster);
        // Goblin Boss
        monster = new Monster();
        monster.MovementSquares = 10;
        monster.AttackDice = 2;
        monster.DefendDice = 2;
        monster.BodyPoints = 1;
        monster.MindPoints = 2;
        monster.image = "GoblinBoss";
        monster.name = "Goblin Boss";
        monster.description = "Small: may move through any figure; only blocks line of sight of other goblins. Leader: +1 defese die to all other goblins.";
        this.monsters.Add(monster);
        // Goblin Shaman
        monster = new Monster();
        monster.MovementSquares = 10;
        monster.AttackDice = 2;
        monster.DefendDice = 2;
        monster.BodyPoints = 1;
        monster.MindPoints = 3;
        monster.image = "GoblinShaman";
        monster.name = "Goblin Shaman";
        monster.description = "Small: may move through any figure; only blocks line of sight of other goblins. Spellcaster: 2 random Orc Shaman Spells.";
        this.monsters.Add(monster);
        // Goblin Stabber
        monster = new Monster();
        monster.MovementSquares = 12;
        monster.AttackDice = 1;
        monster.DefendDice = 1;
        monster.BodyPoints = 1;
        monster.MindPoints = 1;
        monster.image = "GoblinStabber";
        monster.name = "Goblin Stabber";
        monster.description = "Small: may move through any figure; only blocks line of sight of other goblins. Backstab: +2 attack dice against a figure with another adjacent monster.";
        this.monsters.Add(monster);
        // Lich
        monster = new Monster();
        monster.MovementSquares = 6;
        monster.AttackDice = 4;
        monster.DefendDice = 4;
        monster.BodyPoints = 3;
        monster.MindPoints = 7;
        monster.image = "Lich";
        monster.name = "Lich";
        monster.description = "Spellcaster: 2 random Necromancer spells. Leader: +1 defense die to all undead monsters.";
        this.monsters.Add(monster);
        // Mummy
        monster = new Monster();
        monster.MovementSquares = 4;
        monster.AttackDice = 3;
        monster.DefendDice = 4;
        monster.BodyPoints = 2;
        monster.MindPoints = 0;
        monster.image = "Mummy";
        monster.name = "Mummy";
        monster.description = "Mummy's Curse: all adjacent heroes and mercenaries have -1 attack die.";
        this.monsters.Add(monster);
        // Necromancer
        monster = new Monster();
        monster.MovementSquares = 6;
        monster.AttackDice = 4;
        monster.DefendDice = 4;
        monster.BodyPoints = 2;
        monster.MindPoints = 5;
        monster.image = "Necromancer";
        monster.name = "Necromancer";
        monster.description = "Spellcaster: 3 random Necromancer spells and 'Animate Skeleton' Chaos spell.";
        this.monsters.Add(monster);
        // Ogre Brute
        monster = new Monster();
        monster.MovementSquares = 5;
        monster.AttackDice = 6;
        monster.DefendDice = 5;
        monster.BodyPoints = 4;
        monster.MindPoints = 2;
        monster.image = "OgreBrute";
        monster.name = "Ogre Brute";
        monster.description = "Powerful Blow: after dealing damage to a figure, you may move it 1 square.";
        this.monsters.Add(monster);
        // Orc
        monster = new Monster();
        monster.MovementSquares = 8;
        monster.AttackDice = 3;
        monster.DefendDice = 2;
        monster.BodyPoints = 1;
        monster.MindPoints = 2;
        monster.image = "Orc";
        monster.name = "Orc";
        monster.description = "Green Tide: +1 attack die if the defender is adjacent to another orc.";
        this.monsters.Add(monster);
        // Orc Archer
        monster = new Monster();
        monster.MovementSquares = 8;
        monster.AttackDice = 2;
        monster.DefendDice = 2;
        monster.BodyPoints = 1;
        monster.MindPoints = 2;
        monster.image = "OrcArcher";
        monster.name = "Orc Archer";
        monster.description = "Green Tide: +1 attack die if the defender is adjacent to another orc. Ranged: can only make ranged attacks.";
        this.monsters.Add(monster);
        // Orc Chieftan
        monster = new Monster();
        monster.MovementSquares = 8;
        monster.AttackDice = 3;
        monster.DefendDice = 3;
        monster.BodyPoints = 2;
        monster.MindPoints = 3;
        monster.image = "OrcChieftan";
        monster.name = "Orc Chieftan";
        monster.description = "Green Tide: +1 attack die if the defender is adjacent to another orc. Leader: +1 defense to all other orcs.";
        this.monsters.Add(monster);
        // Orc Pikeman
        monster = new Monster();
        monster.MovementSquares = 7;
        monster.AttackDice = 3;
        monster.DefendDice = 2;
        monster.BodyPoints = 1;
        monster.MindPoints = 2;
        monster.image = "OrcPikeman";
        monster.name = "Orc Pikeman";
        monster.description = "Green Tide: +1 attack die if the defender is adjacent to another orc. Long Weapon: can attack diagonally.";
        this.monsters.Add(monster);
        // Orc Shaman
        monster = new Monster();
        monster.MovementSquares = 8;
        monster.AttackDice = 2;
        monster.DefendDice = 2;
        monster.BodyPoints = 2;
        monster.MindPoints = 4;
        monster.image = "OrcShaman";
        monster.name = "Orc Shaman";
        monster.description = "Green Tide: +1 attack die if the defender is adjacent to another orc. Spellcaster: 3 random Orc Shaman spells.";
        this.monsters.Add(monster);
        // Orc Warlord
        monster = new Monster();
        monster.MovementSquares = 7;
        monster.AttackDice = 4;
        monster.DefendDice = 3;
        monster.BodyPoints = 3;
        monster.MindPoints = 4;
        monster.image = "OrcWarlord";
        monster.name = "Orc Warlord";
        monster.description = "Green Tide: +1 attack die if the defender is adjacent to another orc. Commander: +1 attack die to all other orcs.";
        this.monsters.Add(monster);
        // Shade
        monster = new Monster();
        monster.MovementSquares = 9;
        monster.AttackDice = 3;
        monster.DefendDice = 3;
        monster.BodyPoints = 2;
        monster.MindPoints = 0;
        monster.image = "Shade";
        monster.name = "Shade";
        monster.description = "Spectral: may move through walls, figures, furniture, obstacles.";
        this.monsters.Add(monster);
        // Skeleton
        monster = new Monster();
        monster.MovementSquares = 6;
        monster.AttackDice = 2;
        monster.DefendDice = 2;
        monster.BodyPoints = 1;
        monster.MindPoints = 0;
        monster.image = "Skeleton";
        monster.name = "Skeleton";
        monster.description = "Bare Bones: +2 defense dice against ranged attacks.";
        this.monsters.Add(monster);
        // Skeleton Archer
        monster = new Monster();
        monster.MovementSquares = 6;
        monster.AttackDice = 2;
        monster.DefendDice = 2;
        monster.BodyPoints = 1;
        monster.MindPoints = 0;
        monster.image = "SkeletonArcher";
        monster.name = "Skeleton Archer";
        monster.description = "Bare Bones: +2 defense dice against ranged attacks. Ranged: can only make ranged attacks.";
        this.monsters.Add(monster);
        // Skeleton Spearman
        monster = new Monster();
        monster.MovementSquares = 6;
        monster.AttackDice = 2;
        monster.DefendDice = 3;
        monster.BodyPoints = 1;
        monster.MindPoints = 0;
        monster.image = "SkeletonSpearman";
        monster.name = "Skeleton Spearman";
        monster.description = "Bare Bones: +2 defense dice against ranged attacks. Long Weapon: can attack diagonally.";
        this.monsters.Add(monster);
        // Skeleton Warrior
        monster = new Monster();
        monster.MovementSquares = 5;
        monster.AttackDice = 3;
        monster.DefendDice = 3;
        monster.BodyPoints = 1;
        monster.MindPoints = 0;
        monster.image = "SkeletonWarrior";
        monster.name = "Skeleton Warrior";
        monster.description = "Bare Bones: +2 defense dice against ranged attacks.";
        this.monsters.Add(monster);
        // Tomb Lord
        monster = new Monster();
        monster.MovementSquares = 5;
        monster.AttackDice = 4;
        monster.DefendDice = 5;
        monster.BodyPoints = 2;
        monster.MindPoints = 0;
        monster.image = "TombLord";
        monster.name = "Tomb Lord";
        monster.description = "Mummy's Curse: all adjacent heroes and mercenaries have -1 attack die. Spellcaster: 2 random Necromancer spells.";
        this.monsters.Add(monster);
        // Troll
        monster = new Monster();
        monster.MovementSquares = 6;
        monster.AttackDice = 4;
        monster.DefendDice = 3;
        monster.BodyPoints = 3;
        monster.MindPoints = 1;
        monster.image = "Troll";
        monster.name = "Troll";
        monster.description = "Regeneration: instead of moving or attacking, may heal 1 lost body point.";
        this.monsters.Add(monster);
        // Vampire Lord
        monster = new Monster();
        monster.MovementSquares = 7;
        monster.AttackDice = 4;
        monster.DefendDice = 4;
        monster.BodyPoints = 3;
        monster.MindPoints = 4;
        monster.image = "VampireLord";
        monster.name = "Vampire Lord";
        monster.description = "Spellcaster: 2 random Necromancer spells. Vampirism: heal 1 lost body point when damages a figure.";
        this.monsters.Add(monster);
        // Zargon
        monster = new Monster();
        monster.MovementSquares = 6;
        monster.AttackDice = 4;
        monster.DefendDice = 5;
        monster.BodyPoints = 3;
        monster.MindPoints = 4;
        monster.image = "Zargon";
        monster.name = "Zargon";
        monster.description = "";
        this.monsters.Add(monster);
        // Zombie
        monster = new Monster();
        monster.MovementSquares = 5;
        monster.AttackDice = 2;
        monster.DefendDice = 3;
        monster.BodyPoints = 1;
        monster.MindPoints = 0;
        monster.image = "Zombie";
        monster.name = "Zombie";
        monster.description = "Death Grasp: reduce the movement of adjacent heroes in 1 space.";
        this.monsters.Add(monster);

        ShowMonster(0);
    }

    public void ShowMonster(int monsterID)
    {
        selectedMonster = monsterID;

        characterName.text = monsters[monsterID].name;
        characterImage.sprite = Resources.Load<Sprite>( "Monsters/"+monsters[monsterID].image );
        description.text = monsters[monsterID].description;
        movementSquares.text = ""+monsters[monsterID].MovementSquares;
        attackDice.text = "" + monsters[monsterID].AttackDice;
        defendDice.text = "" + monsters[monsterID].DefendDice;
        bodyPoints.text = "" + monsters[monsterID].BodyPoints;
        mindPoints.text = "" + monsters[monsterID].MindPoints;
    }

    public void NextMonster()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            int nextMonster = selectedMonster + 1;
            if (nextMonster == monsters.Count - 1) selectedMonster = 1;
            ShowMonster(nextMonster);
            photonView.RPC("ThreadShowMonster", RpcTarget.Others, selectedMonster);
        }
    }

    public void PreviousMonster()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            int nextMonster = selectedMonster - 1;
            if (nextMonster < 0) selectedMonster = monsters.Count - 1;
            ShowMonster(nextMonster);
            photonView.RPC("ThreadShowMonster", RpcTarget.Others, selectedMonster);
        }
    }

    public void ZargonClear()
    {
        photonView.RPC("ThreadZargonClear", RpcTarget.All);
    }

    public void OnUpdateFeed(string[,] newRolls)
    {
        if (newRolls.Length > 0)
        {
            //Debug.Log("newRolls.Length:"+newRolls.Length/2);
            int diceCount = newRolls.Length/2;
            for (int i = 0; i < diceCount; i++)
            {
                //Debug.Log("newRolls[i, 0]:" + newRolls[i, 0]);
                //Debug.Log("newRolls[i, 1]:" + newRolls[i, 1]);

                photonView.RPC("ThreadZargonDiceDisplay_"+i, RpcTarget.All, newRolls[i, 0], newRolls[i, 1]);

                //UpdateDiceRollLog(PhotonNetwork.LocalPlayer.NickName, i, newRolls[i, 0], newRolls[i, 1]);
            }
            photonView.RPC("ThreadShowMonster", RpcTarget.Others, selectedMonster);
        }
        EventSystem.current.SetSelectedGameObject(null);
    }

    [PunRPC]
    void ThreadShowMonster(int monsterID)
    {
        ShowMonster(monsterID);
    }

    [PunRPC]
    void ThreadZargonClear()
    {
        for (int i = 0; i < diceGroup.Count; i++)
        {
            diceGroup[i].GetComponent<DiceFinal>().SetNone();
        }
    }

    [PunRPC]
    void ThreadZargonDiceDisplay_0(string rt = null, string rv = null)
    {
        diceGroup[0].gameObject.SetActive(true);
        diceGroup[0].GetComponent<DiceFinal>().SetFinal(rt, rv);
    }
    [PunRPC]
    void ThreadZargonDiceDisplay_1(string rt = null, string rv = null)
    {
        diceGroup[1].gameObject.SetActive(true);
        diceGroup[1].GetComponent<DiceFinal>().SetFinal(rt, rv);
    }
    [PunRPC]
    void ThreadZargonDiceDisplay_2(string rt = null, string rv = null)
    {
        diceGroup[2].gameObject.SetActive(true);
        diceGroup[2].GetComponent<DiceFinal>().SetFinal(rt, rv);
    }
    [PunRPC]
    void ThreadZargonDiceDisplay_3(string rt = null, string rv = null)
    {
        diceGroup[3].gameObject.SetActive(true);
        diceGroup[3].GetComponent<DiceFinal>().SetFinal(rt, rv);
    }
    [PunRPC]
    void ThreadZargonDiceDisplay_4(string rt = null, string rv = null)
    {
        diceGroup[4].gameObject.SetActive(true);
        diceGroup[4].GetComponent<DiceFinal>().SetFinal(rt, rv);
    }
    [PunRPC]
    void ThreadZargonDiceDisplay_5(string rt = null, string rv = null)
    {
        diceGroup[5].gameObject.SetActive(true);
        diceGroup[5].GetComponent<DiceFinal>().SetFinal(rt, rv);
    }
    [PunRPC]
    void ThreadZargonDiceDisplay_6(string rt = null, string rv = null)
    {
        diceGroup[6].gameObject.SetActive(true);
        diceGroup[6].GetComponent<DiceFinal>().SetFinal(rt, rv);
    }
    [PunRPC]
    void ThreadZargonDiceDisplay_7(string rt = null, string rv = null)
    {
        diceGroup[7].gameObject.SetActive(true);
        diceGroup[7].GetComponent<DiceFinal>().SetFinal(rt, rv);
    }

    // copypasta for global roll history tab
    // rollHistory += string.Format("<b>{0}:</b> {1}\n", playerName, newRoll);
}