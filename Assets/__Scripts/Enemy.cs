using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    [Header("Set in Inspector: Enemy")]
    public float speed = 10f;
    public float fireRate = 0.3f;
    public float health = 10;
    public int score = 100;

    public float showDamageDuration = 0.1f; // # seconds to show damage
    public float powerUpDropChance = 1f; // Chance to drop a power-up 

    [Header("Set Dynamically: Enemy")]
    public Color[] originalColors;
    public Material[] materials;
    public bool showingDamage = false;
    public float damageDoneTime;
    public bool notifiedOfDestruction = false;

    protected BoundsCheck bndCheck;

    private void Awake()
    {
        bndCheck = GetComponent<BoundsCheck>();

        materials = Utils.GetAllMaterials(gameObject);
        originalColors = new Color[materials.Length];
        for (int i = 0; i<materials.Length; i++)
        {
            originalColors[i] = materials[i].color;
        }
    }


    public Vector3 pos
    {
        get
        {
            return (this.transform.position);
        }
        set
        {
            this.transform.position = value;
        }
    }
	// Use this for initialization
	
	// Update is called once per frame
	void Update () {
        Move();	

        if(showingDamage && Time.time > damageDoneTime)
        {
            UnShowDamage();
        }

        if(bndCheck != null && bndCheck.offDown)
        {
            
                Destroy(gameObject);
            
        }
	}

    public virtual void Move()
    {
        Vector3 tempPos = pos;
        tempPos.y -= speed * Time.deltaTime;
        pos = tempPos;
    }

    void OnCollisionEnter(Collision coll)
    {
        GameObject otherGo = coll.gameObject;
        switch (otherGo.tag)
        {
            case "ProjectileHero":
                Projectile p = otherGo.GetComponent<Projectile>();
                if (!bndCheck.isOnScreen)
                {
                    Destroy(otherGo);
                    break;
                }
                health -= Main.GetWeaponDefinition(p.type).damageOnHit;
                ShowDamage();
                if (health <= 0)
                {
                    //Tell the Main singleton that this ship was destroyed
                    if (!notifiedOfDestruction)
                    {
                        Main.S.shipDestroyed(this);
                    }
                    notifiedOfDestruction = true;
                    //Destroy this Enemy
                    Destroy(this.gameObject);
                }
                Destroy(otherGo);
                break;
            default:
                print("Enemy hit by non-ProjectileHero: " + otherGo.name);
                break;
                //if(otherGo.tag == "ProjectileHero")
                //{
                //   Destroy(otherGo);
                //  Destroy(gameObject);
                //}
                //else
                //{
                //    print("Enemy hit by non-ProjectileHero: " + otherGo.name);
                //}
        }
    }
    void ShowDamage()
    {
        foreach(Material m in materials)
        {
            m.color = Color.red;
        }
        showingDamage = true;
        damageDoneTime = Time.time + showDamageDuration;
    }
    void UnShowDamage()
    {
        for (int i = 0; i < materials.Length; i++)
        {
            materials[i].color = originalColors[i];
        }
        showingDamage = false;
    }
}
