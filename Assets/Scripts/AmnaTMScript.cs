
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class AmnaTMScript : MonoBehaviour {

    public Button button;
    public Text input_field, txt;
    public bool isPalindrome;
    public string[] cubearr;
    private GameObject cube;
    private const int ScaleValue = 10;
    public Material cubemat;
    public Camera camera;
    GameObject[] cubes, childs;
    Ray ray;
    RaycastHit hit;
    Vector3 position;
    char[] charArr;
    public GameObject optxt;
    public int currentindex = 1;
    public string state = "q0", status = "";
    public char move = 'R';
    public GameObject myPrefab;
    void Start()
    {
        button.onClick.AddListener(onbtnClick);
        //GameObject.Find("Audiofile").GetComponent<AudioSource>().Play();
        ray = new Ray(transform.position, transform.forward);
    }
    void Update()
    {
   
        if (Input.GetKeyDown(KeyCode.Space) && status == "")
        {
            print("space clicked after rej");
            txt.GetComponent<Text>().text = " ";
            optxt.GetComponent<TextMesh>().text = "";
            Debug.Log("current" + currentindex);
            optxt.GetComponent<TextMesh>().text = state;
            txt.GetComponent<Text>().text = state;
            Physics.Raycast(ray, out hit);
            position = camera.transform.position;
            if (move == 'R') { position.x = position.x + 6f; currentindex++; }
            if (move == 'L') { position.x = position.x - 6f; currentindex--; }
            camera.transform.position = position;
            runTuringMachine();
        }
        
        else if (status == "accepted")
        {
            print("acc");
            txt.GetComponent<Text>().text = "✔";
            optxt.GetComponent<TextMesh>().text = "✔";
        }
        else if (status == "rejected")
        {
            print("rej");
            txt.GetComponent<Text>().text = "✘";
            optxt.GetComponent<TextMesh>().text = "✘";
            destroyObject(charArr);
        }
    }
    private void runTuringMachine()
    {
        for (int i = 0; i < (charArr.Length); i++)
        {
            Destroy(cubes[i]);
        }
        print("runTuringMachine");
        if (state == "q0")
        {
            if (charArr[currentindex] == '1') { charArr[currentindex] = 'x'; move = 'R'; state = "q1"; }
            else if (charArr[currentindex] == '#') { charArr[currentindex] = '#'; state = "q7"; move = 'R'; }
            else if (charArr[currentindex] == '0') { charArr[currentindex] = 'x'; state = "q4"; move = 'R'; }
            else { status = "rejected"; }
        }
        else if (state == "q1")
        {
            if (charArr[currentindex] == '0' ) { charArr[currentindex] = '0'; move = 'R';  state = "q1"; }
            else if (charArr[currentindex] == '1') { charArr[currentindex] = '1'; state = "q1"; move = 'R'; }
            else if (charArr[currentindex] == '#') { charArr[currentindex] = '#'; state = "q2"; move = 'R'; }
            else { status = "rejected"; }
        }
        else if (state == "q2")
        {
            if (charArr[currentindex] == 'y') { charArr[currentindex] = 'y'; state = "q2"; move = 'R'; }
            else if (charArr[currentindex] == '1' ){ charArr[currentindex] = 'y'; move = 'L'; state = "q3"; }

            else { status = "rejected"; }
        }
        else if (state == "q3")
        {
            if (charArr[currentindex] == '1' || charArr[currentindex] == '0' || charArr[currentindex] == '#' || charArr[currentindex] == 'y') { move = 'L'; state = "q3"; }
            else if (charArr[currentindex] == 'x') { charArr[currentindex] = 'x'; move = 'R'; state = "q0"; }
            else { status = "rejected"; }
        }
        else if (state == "q4")
        {
            if (charArr[currentindex] == '0') { charArr[currentindex] = '0'; state = "q4"; move = 'R'; }
            else if (charArr[currentindex] == '1') { charArr[currentindex] = '1'; move = 'R'; state = "q4"; }
            else if (charArr[currentindex] == '#') { charArr[currentindex] = '#'; state = "q5"; move = 'R'; }
            else { status = "rejected"; }
        }
        else if (state == "q5")
        {
            if (charArr[currentindex] == 'y') { charArr[currentindex] = 'y'; state = "q5"; move = 'R'; }
            else if (charArr[currentindex] == '0') { charArr[currentindex] = 'y'; move = 'L'; state = "q6"; }
            else { status = "rejected"; }
        }
        else if (state == "q6")
        {
            if (charArr[currentindex] == '1' || charArr[currentindex] == '0' || charArr[currentindex] == '#' || charArr[currentindex] == 'y') { move = 'L'; state = "q6"; }
            else if (charArr[currentindex] == 'x') { charArr[currentindex] = 'x'; move = 'R'; state = "q0"; }
            else { status = "rejected"; }
        }
        else if (state == "q7")
        {
            if (charArr[currentindex] == 'y') { charArr[currentindex] = 'y'; move = 'R'; state = "q7"; }
            else if (charArr[currentindex] == 'Δ') { charArr[currentindex] = 'Δ'; move = 'R'; state = "q8"; }
            else { status = "rejected"; }
        }

        else if (state == "q8")
        {
            status = "accepted";
        }
        showCubes(charArr);
      
        print(state);
    }

    public void onbtnClick()
    {
        //button.gameObject.SetActive(false);
       // input_field.gameObject.SetActive(false);
        string str = "ΔΔ";
        str += input_field.text.ToString();
        str += "ΔΔ";
        charArr = str.ToCharArray();
        string inputpattern = @"^([01#]+)$";
        Match match = Regex.Match(input_field.text, inputpattern);
        if (match.Success)
        {
            showCubes(charArr);
            
        }
        else
        {
            print("Invalid input");
// showCubes(charArr);

        }
        GameObject.Find("Button7").transform.position = new Vector3( 300, 800f, -901f);
        GameObject.Find("InputField").transform.position = new Vector3(300, 800f, -901f);

    }
    public void showCubes(char[] charArr)
    {
        int len = charArr.Length;
        cubes = new GameObject[len];
        childs = new GameObject[len];
        TextMesh tm = GetComponent<TextMesh>();
        //camera.transform.position= new Vector3(2.19f, -3, -3.78f);
        for (int i = 0; i < (len); i++)
        {
            generateCubes(cubes, i, charArr[i].ToString());
        }
        //generateCubes(cubes, charArr, len+1, "#");
    }
    public void generateCubes(GameObject[] cubes, int i, string txt)
    {
        cubes[i] = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cubes[i].transform.position = new Vector3(((i * 6) - 6f) + 300, 8f, -11f);
        cubes[i].transform.localScale = new Vector3(2, 2, 1.5f);
        cubes[i].GetComponent<MeshRenderer>().material = cubemat;
    
        GameObject child = new GameObject("childobject");
        child.transform.SetParent(cubes[i].transform);
        child.transform.position = new Vector3(((i * 6) - 6f) + 300, 8f, -11f);
        child.AddComponent<TextMesh>();
        child.GetComponent<TextMesh>().text = txt;
        child.GetComponent<TextMesh>().characterSize = 4;
        child.GetComponent<TextMesh>().fontSize = 4 * ScaleValue;
        child.GetComponent<TextMesh>().color = Color.white;
        child.GetComponent<TextMesh>().offsetZ = -8;
        child.GetComponent<TextMesh>().anchor = TextAnchor.MiddleCenter;
        child.GetComponent<TextMesh>().transform.localScale = child.GetComponent<TextMesh>().transform.localScale / ScaleValue;
        //GameObject.Find(Button).active = false;
        //button.IsActive = false;
    //  
    }
    public void destroyObject(char[] charArr)
    {
        for (int i = 0; i < (charArr.Length); i++)
        {
            Destroy(cubes[i]);

            Instantiate(myPrefab, new Vector3(((i * 6) - 6f) + 300, 8f, -11f), Quaternion.identity);
            // myPrefab.transform.localScale= new Vector3(3f, 3f, 1f);
            Destroy(gameObject);
        }
    }
}
