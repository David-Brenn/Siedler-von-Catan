using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dice : MonoBehaviour
{
    // Start is called before the first frame update
    public string UpperSideTxt;
    public int diceValue;
    public bool isMoving = false;
    public static dice diceInstance { get; private set; }

    public Vector3Int DirectionValues;
    private Vector3Int OpposingDirectionValues;
    readonly List<string> FaceRepresent = new List<string>() { "", "1", "2", "3", "4", "5", "6" };


    private void Awake()
    {
        if (diceInstance != null && diceInstance != this)
        {
            Destroy(this);
        }else
        {
            diceInstance = this;
        }
    }


    void Start()
    {
        OpposingDirectionValues = 7 * Vector3Int.one - DirectionValues;
        
        

    }

    // Update is called once per frame
    void Update()
    {
    }
    public void throwDice()
    {
        StartCoroutine(throwDiceLocal());
    }


    IEnumerator throwDiceLocal(){
        Vector3 forceVector = new Vector3(Random.Range(1, 3), Random.Range(6, 7), Random.Range(1, 3));
        Vector3 torqueVector = new Vector3(Random.Range(1, 10), Random.Range(1, 10), Random.Range(1, 10));
        Vector3 startRotation = new Vector3(Random.Range(-360, 360), Random.Range(-360, 360), Random.Range(-360, 360));
        transform.Rotate(startRotation);
        gameObject.GetComponent<Rigidbody>().AddForce(forceVector,ForceMode.Impulse);
        gameObject.GetComponent<Rigidbody>().AddTorque(torqueVector, ForceMode.Impulse);
        yield return new WaitForSeconds(0.5f);
        isMoving = true;
        while (Vector3.Distance(gameObject.GetComponent<Rigidbody>().velocity,Vector3.zero) != 0)
        {
            yield return new WaitForSeconds(0.5f);
        }
        detectTopValue();
        isMoving = false;
    }
    void detectTopValue()
    {
            if (Vector3.Cross(Vector3.up, transform.right).magnitude < 0.5f) //x axis a.b.sin theta <45
                                                                             //if ((int) Vector3.Cross(Vector3.up, transform.right).magnitude == 0) //Previously
            {
                if (Vector3.Dot(Vector3.up, transform.right) > 0)
                {
                    UpperSideTxt = FaceRepresent[DirectionValues.x];
                diceValue =  DirectionValues.x;
            }
                else
                {
                    UpperSideTxt = FaceRepresent[OpposingDirectionValues.x];
                diceValue = OpposingDirectionValues.x;
                }
            }
            else if (Vector3.Cross(Vector3.up, transform.up).magnitude < 0.5f) //y axis
            {
                if (Vector3.Dot(Vector3.up, transform.up) > 0)
                {
                    UpperSideTxt = FaceRepresent[DirectionValues.y];
                diceValue = DirectionValues.y;
                }
                else
                {
                    UpperSideTxt = FaceRepresent[OpposingDirectionValues.y];
                diceValue = OpposingDirectionValues.y;
                }
            }
            else if (Vector3.Cross(Vector3.up, transform.forward).magnitude < 0.5f) //z axis
            {
                if (Vector3.Dot(Vector3.up, transform.forward) > 0)
                {
                    UpperSideTxt = FaceRepresent[DirectionValues.z];
                diceValue = DirectionValues.z;
                }
                else
                {
                    UpperSideTxt = FaceRepresent[OpposingDirectionValues.z];
                diceValue = OpposingDirectionValues.z;
                }
            }

        
        transform.hasChanged = false;
    }
    
}
