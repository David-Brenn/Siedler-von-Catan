using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public abstract class Card : ScriptableObject
    {
        public new string name;
        public string description;
        public GameObject prefab;

        public Sprite sprite;

        public abstract void playCard();
            

    }

