using System.Collections;
using System.Collections.Generic;
using UnityEngine;



    public enum LandscapeTypes
    {
        ore,
        sheep,
        wood,
        hay,
        gold,
        clay
    }

    public class LandscapeCard : MonoBehaviour
    {

        public LandscapeTypes type;
        public int diceValue = 0;
        public int resource = 0;
        public int rotationPosition = 0;
        private bool hasChanged = false;
        public float rotationSpeed = 40;
        private void Start()
        {

        }
        private void Update()
        {
            if (hasChanged)
            {
                updateRotation();
            }
        }
        public void addResource(int i)
        {
            if (resource == 3)
            {
                Debug.LogWarning("addResource:reached max");
            }
            else
            {
                if (i <= 0)
                {
                    Debug.LogWarning("addResource: i <= 0");
                }
                else
                {
                    resource = resource + i;
                    if (resource > 3)
                    {
                        resource = 3;
                    }
                    hasChanged = true;
                }

            }
        }

        public void removeResource(int i)
        {
            if (i <= 3 && i > 0 && i <= resource)
            {
                resource = resource - i;
                hasChanged = true;
            }
            else
            {
                Debug.LogWarning("removeResource: i is not valid");
            }
        }

        public void updateRotation()
        {
            int rotationChange = resource - rotationPosition;
            if (rotationChange != 0)
            {
                if (this.transform.rotation != Quaternion.Euler(0, 90 * rotationChange, 0))
                {
                    this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, Quaternion.Euler(0, 90 * rotationChange, 0), rotationSpeed * Time.deltaTime);
                    Debug.Log("turn");
                }
                else
                {
                    rotationPosition = resource;
                    hasChanged = false;
                }


            }

        }


    }
